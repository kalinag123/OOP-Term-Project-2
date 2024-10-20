/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using CPSC_3200_P3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{
    /*
     * Class Invariants:
     * Implements IOrgChange interface.
     * Team Name and Employee Name are not publicly accessible and can only be set when class instance is created. 
     */
    internal class TransferOperation : IOrgChange 
    {
        private readonly string _employeeName;
        private readonly string? _teamName;

        public TransferOperation(string employeeName, string? teamName)
        {
            _employeeName = employeeName;
            _teamName = teamName;
        }

        /*
         * preconditions: None.
         * postconditions: 
         * If employee exists in the company, they are removed from ALL teams they are members in. 
         * If team exists in the company, employee will be added to that team. 
         * If neither condition is met, nothing happens. 
         */
        void IOrgChange.Apply(Company company)
        {
            Employee? emp = company.GetEmployee(_employeeName);

            if (emp != null)
            {
                foreach (var item in company.CompanyTeams)
                {
                    if (item.TeamEmployees.Contains(emp))
                    {
                        item.TeamEmployees.Remove(emp);
                    }
                }

                Team? team = company.getTeam(_teamName);

                if (team != null)
                {
                    team.addEmployee(emp);
                }

            }
        }

        public override string ToString()
        {
            if (_teamName != null)
            {
                return "Transfer " + _employeeName + " to " + _teamName;
            }
            return "Remove " + _employeeName;

        }
    }
}
