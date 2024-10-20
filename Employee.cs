/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CPSC_3200_P3
{
    public class Employee
    {
        /// <summary>
        /// Class Invariants:
        /// Name of employee can only be set when employee instance is created.
        /// Dictionary of employee skills and associated proficiencies is publicly gettable but privately settable. 
        /// Client can specify 0 or more skills, but skills and their proficiency level must be in (Skill, double) 
        /// format- otherwise the program cannot be compiled. 
        /// If client tries to do a task that has already been done, no state will be changed.
        /// Employee cannot have duplicate skills, but skills are case sensitive. 
        /// </summary>
        private readonly string _name;
        public string Name { get { return _name; } }

        private double _hoursWorked;
        public double HoursWorked { get { return _hoursWorked; } }

        private Dictionary<Skill, double> _empProficiency  = new Dictionary<Skill, double>();
        public Dictionary<Skill, double> EmpProficiency { get { return _empProficiency; } }
       
        public Employee(string name, double hoursWorked, params Tuple<Skill, double>[] skills)
        {
            _name = name;
            _hoursWorked = hoursWorked;

            Tuple<Skill, double>[] employee_skills = skills;

            foreach (Tuple<Skill, double> item in skills)
            {
                if(!_empProficiency.ContainsKey(item.Item1))
                {
                    _empProficiency.Add(item.Item1, item.Item2);
                }
            }
        }

        /// <summary>
        /// Preconditions: 
        /// Task skills must match Employee skills exactly to count as the same skill- for example "Run" and "Running" are 
        /// two different skills. 
        /// If an employee does not have a skill associated with the task, their proficiency in that task is assumed to be
        /// 0.
        /// 
        /// Postconditions:
        /// Returns a time estimate for how long it will take an employee to complete a task, but does not change any state.
        /// Will add task's skills to the employee's skill dictionary if they don't exist with a skill level of 0.
        /// </summary>
        public double time_estimate(Task task)
        {
            // go through skills required for the task. For each one, call the time estimate function of the sill. 
            // then, average the time cost . 

            if(task.SkillsRequired.Count() == 0)
            {
                return task.BaseTime;
            }

            double total_time = 0;
            int count_skills_required = task.SkillsRequired.Count();

            foreach (KeyValuePair<Skill,double> item in task.SkillsRequired) {
                if(!_empProficiency.ContainsKey(item.Key))
                {
                    _empProficiency.Add(item.Key, 0);
                }
                total_time += item.Key.CalculateCost(task.BaseTime, item.Value, _empProficiency[item.Key]);
            }

            return total_time/count_skills_required;
        }



        /// <summary>
        /// Preconditions: 
        /// Assumes that client has entered employee information correctly- for example, negative employee skill
        /// proficiencies may yield unexpected results.
        /// 
        /// Postconditions:
        /// If task is already done, no state will be changed- employee will not gain any experience or hours worked if
        /// this method is called on a task that is already done.
        /// If task is not done, state of task will change to done, and employee will gain proficiency in the skills they 
        /// needed to complete the task. If the employee did not have any experience in the skill prior to completing the 
        /// task, they will gain at least one level of proficiency. 
        /// An employee's proficiency gained corresponds to the employee's current skill level as well as the difficulty of 
        /// the task- an inexperienced employee earns more experience for a difficult task than an experienced employee. 
        /// </summary>
        public bool do_task(Task task)
        {
            // If the task is not doable (if it is already done) return false. 
            if (task.Done == true)
            {
                return false;
            }

            // Since I wrote it into the setter for done, should be safe to just set it to done in the employee class?
            // But, employee shouldn't gain experience for 'doing' task that was already done, so do_task should still
            // return a bool. 
            task.Done = true;

            // Adds time to employee's hours worked 
            double mins_worked = time_estimate(task);
            _hoursWorked += mins_worked / 60;

            // Assigns skill points to employee based on difficulty of task compared to their proficiency 
            foreach (KeyValuePair<Skill, double> item in task.SkillsRequired)
            {
                if (_empProficiency[item.Key] == 0)
                {
                    // doing any skill for the first time increases an employee's proficiency by 1. 
                    _empProficiency[item.Key] = 1 + 0.33 * item.Value;
                }
                else
                {
                    _empProficiency[item.Key] += 0.33 * (item.Value / _empProficiency[item.Key]);
                }
            }

            return true;
        }
    }
}
