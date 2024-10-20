/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{
    public class Team
    {
        /// <summary>
        /// Class Invariants:
        /// Name can only be set when an instance is created.
        /// Team is not responsible for employees assigned to it, and does not allocate memory for employees added to it*.
        /// *Exception is in the case of creating a deep copy.
        /// </summary>

        private readonly string _name;
        public string Name { get { return _name; } }

        private List<Employee> _teamEmployees = new();
        public List<Employee> TeamEmployees { get { return _teamEmployees; } }


        
        public Team(string name)
        {
            _name = name;
        }
        // add employee
        public bool addEmployee(Employee employee)
        {
            if(!_teamEmployees.Contains(employee)) { 
                _teamEmployees.Add(employee);
                return true;
            }

            return false;
        }
        // remove employee

        public bool removeEmployee(Employee employee)
        {
            if(_teamEmployees.Contains(employee))
            {
                _teamEmployees.Remove(employee);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Preconditions: None. 
        /// Postconditions: If client tries to get a time estimate from a team without any employees assigned to it, 
        /// program will throw an InvalidOperationException. Client is responsible for catching and handling exception. 
        /// </summary>
        public double timeEstimate(Task task)
        {
            if(_teamEmployees.Count == 0)
            {
                throw new InvalidOperationException("This team has no employees assigned to it.");
            }
            double minTime = _teamEmployees[0].time_estimate(task);
            foreach (Employee emp in _teamEmployees.Skip(1))
            {
                minTime = Math.Min(minTime, emp.time_estimate(task));
            }

            return minTime;
        }

        /// <summary>
        /// Preconditions: None.
        /// Postconditions: Creates and returns a deep copy of team by iterating through every employee in the team and their
        /// skills and creating a new instance of Employee to be added to the copy team's list of employees. 
        /// </summary>
        public Team DeepCopy()
        {
            Team copyTeam = new Team(_name);

            foreach(Employee employee in _teamEmployees) {

                int num_skills = employee.EmpProficiency.Count();
                Tuple<Skill, double>[] skills_list = new Tuple<Skill, double>[num_skills];

                for(int i = 0; i < num_skills; i++)
                {
                    var item = employee.EmpProficiency.ElementAt(i);
                    var itemKey = item.Key;
                    var itemValue = item.Value;

                    Tuple<Skill, double> current_tuple = new(itemKey,itemValue);
                    skills_list[i] = current_tuple;
                }

                Employee current_employee = new Employee(employee.Name,employee.HoursWorked, skills_list);
                copyTeam._teamEmployees.Add(current_employee);
            }

            return copyTeam;    
        }
    }
}
