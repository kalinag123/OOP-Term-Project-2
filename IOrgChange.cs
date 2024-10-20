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
    public interface IOrgChange
    {
        /*
         * Class Invariants:
         * All classes that inherit from this interface must implement an Apply() method that takes a Company as a parameter.
         */
        public void Apply(Company company);
    }
}
