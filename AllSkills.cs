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
    static class AllSkills
    {
        /*
         * Class invariants:
         * Helper class for P5. Creates a static array of 10 Skills using all subtypes of Skill.
         */
        public static Skill[] Items =
        {
            new Skill("C# Programming", "Write effective programs in C#"),
            new Skill("C++ Programming", "Write effective programs in C++"),
            new StaticSkill("Public Speaking", "Present in front of others"),
            new ExponentialSkill("Excel", "Create satisfactory Excel spreadsheets"),
            new Skill("SAP", "Post journal entries to SAP"),
            new RandomSkill("Leadership", "Lead a team of your peers",new Random(42),new Random(35)),
            new RandomSkill("Heavy Machinery", "Safely operate heavy machinery",new Random(89),new Random(86)),
            new ExponentialSkill("Communication", "Effectively communicate through writing and speech"),
            new StaticSkill("Baking", "Bake something"),
            new Skill("Hiking", "Hike a mountain")
        };
    }
}
