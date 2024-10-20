/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using CPSC_3200_P3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{
    internal class CreateTeamOperation : IOrgChange
    {
        /*
         * Class Invariants:
         * Implements IOrgChange interface.
         * Team Name is not publicly accessible and can only be set when class instance is created. 
         */
        private readonly string _teamName;

        public CreateTeamOperation(string Name) { _teamName = Name; }

        /*
         * preconditions: None.
         * postconditions: Calls company's createTeam method with _teamName passed as a parameter. 
         */
        void IOrgChange.Apply(Company company)
        {
            company.createTeam(_teamName);
        }

        public override string ToString()
        {
            return "Create team " + _teamName;
        }
    }
}
