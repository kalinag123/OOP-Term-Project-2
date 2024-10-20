/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{
    public class Company
    {
        /// <summary>
        /// Class invariants:
        /// Name can only be set when instance is created. 
        /// Company is responsible for its employee and team instances. 
        /// </summary>

        private readonly string _name;
        public string Name { get { return _name; } }

        private List<Employee> _companyEmployees = new();
        public List<Employee> CompanyEmployees { get { return _companyEmployees; } }

        private List<Team> _companyTeams = new();
        public List<Team> CompanyTeams { get { return _companyTeams; } }

        // constructor 
        public Company(string name)
        {
            _name = name;
        }
        
        /// <summary>
        /// Preconditions: Employee's skills must be given in (Skill,double) format- otherwise, program will not compile.
        /// Postconditions: Employee will be added to the company's employee list. 
        /// </summary>
        public Employee hireEmployee(string name, double hours, params Tuple<Skill, double>[] skills)
        {
            Employee newHire = new Employee(name, hours, skills);

            _companyEmployees.Add(newHire);

            return newHire;

        }

        /// <summary>
        /// Preconditions: None.
        /// Postconditions: Employee will be removed from the company's employee list, as well as from all teams' employee 
        /// lists. 
        /// </summary>
        public bool fireEmployee(string name)
        {
            Employee? emp = GetEmployee(name);

            if (emp != null)
            {
                _companyEmployees.Remove(emp);
                foreach (Team team in _companyTeams)
                {
                    foreach (Employee team_emp in team.TeamEmployees)
                    {
                        if (team_emp.Name == name)
                        {
                            team.TeamEmployees.Remove(emp);
                        }
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Preconditions: None.
        /// Postconditions: Team will be added to the company's teams list. 
        /// </summary>
        public Team createTeam(string name)
        {
            Team newTeam = new(name);
            _companyTeams.Add(newTeam);
            return newTeam;
        }

        /// <summary>
        /// Preconditions: None.
        /// Postconditions: Team will be removed from the company's teams list.  
        /// </summary>
        public bool removeTeam(string name)
        {
            Team? team = getTeam(name);

            if (team != null)
            {
                _companyTeams.Remove(team);
                return true;
            }

            return false;
        }

        public Team? getTeam(string name)
        {
            foreach (Team item in _companyTeams)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        // get employee (from name)
        public Employee? GetEmployee(string name)
        {
            foreach (Employee emp in _companyEmployees)
            {
                if (emp.Name == name)
                {
                    return emp;
                }
            }

            return null;
        }


        /// <summary>
        /// Preconditions: None.
        /// Postconditions: Creates and returns a deep copy of company by iterating through every employee in the company 
        /// and theirskills and creating a new instance of Employee to be added to the copy company's list of employees. 
        /// Also creates deep copies of every team and adds them to the copy team's teams list. 
        /// </summary>
        public Company DeepCopy()
        {
            Company copyCompany = new Company(_name);

            // copy over all employees. 
            foreach (Employee employee in _companyEmployees)
            {
                int num_skills = employee.EmpProficiency.Count();
                Tuple<Skill, double>[] skills_list = new Tuple<Skill, double>[num_skills];

                for (int i = 0; i < num_skills; i++)
                {
                    var item = employee.EmpProficiency.ElementAt(i);
                    var itemKey = item.Key;
                    var itemValue = item.Value;

                    Tuple<Skill, double> current_tuple = new(itemKey, itemValue);
                    skills_list[i] = current_tuple;
                }

                Employee current_employee = new Employee(employee.Name, employee.HoursWorked, skills_list);
                copyCompany._companyEmployees.Add(current_employee);
            }
            // then copy all of the teams. 
            foreach (Team team in _companyTeams)
            {
                Team current_team = team.DeepCopy();
                copyCompany._companyTeams.Add(current_team);
            }

            return copyCompany;
        }
    }
}
