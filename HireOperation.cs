/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{ 
    /*
     * Class Invariants:
     * Implements IOrgChange interface. 
     * Name of Employee cannot be changed or publicly accessed after class is instantiated.
     */
    internal class HireOperation : IOrgChange
    {
        private readonly string _employeeName;

        public HireOperation(string employeeName)
        {
            _employeeName = employeeName;
        }

        /*
         * preconditions: Uses AllSkills helper static class to randomly assign skills to Employee- skills in this class 
         * should be in a valid state.
         * postconditions: Calls the hireEmployee() Company method, creating a new instance of Employee with given name. 
         * Assumes that Employee has number of skills between 0 and number of skills defined by AllSkills.
         * Assumes that Employee has a proficiency between 0 and 10 in each of the skills.
         * Assumes that Employee has worked for 0 hours when they are hired. 
         */
        void IOrgChange.Apply(Company company)
        {
            Random rnd = new Random();
            int number_of_skills = rnd.Next(AllSkills.Items.Length);


            var skills = new List<Tuple<Skill, double>>();

            for (int i = 0; i < number_of_skills; i++)
            {
                int skill_index = rnd.Next(AllSkills.Items.Length);
                double skill_level = rnd.Next(10);


                var current = new Tuple<Skill, double>(AllSkills.Items[skill_index], skill_level);
               

                if (!skills.Any(s => s.Item1.Name == current.Item1.Name))
                {
                    skills.Add(current);
                }
            }

            company.hireEmployee(_employeeName,0,skills.ToArray());
        }
        public override string ToString()
        {
            return "Hire " + _employeeName;
        }
    }
}
