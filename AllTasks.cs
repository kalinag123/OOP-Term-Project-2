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
    static class AllTasks
    {
        /*
         * Class invariants:
         * Helper class for P5. Creates a static array of 5 Skills using all 10 Skills created in AllSkills helper class.
         */
        public static Task[] Items =
        {
            new Task("Make a program","Design and write a program",400,new Tuple<Skill,double> (AllSkills.Items[0], 5),
                new Tuple<Skill,double> (AllSkills.Items[1], 5)),
            new Task("Make balance sheet","Create an accurate balance sheet",300,new Tuple<Skill,double> (AllSkills.Items[3], 4),
                new Tuple<Skill,double> (AllSkills.Items[4], 6)),
            new Task("Manage a project","Lead a team during a project",120,new Tuple<Skill,double> (AllSkills.Items[2], 5),
                new Tuple<Skill,double> (AllSkills.Items[5], 10), new Tuple<Skill,double> (AllSkills.Items[7], 7)),
            new Task("Drive a forklift","Safely operate a forklift",50,new Tuple<Skill,double> (AllSkills.Items[7], 5.9),
                new Tuple<Skill,double> (AllSkills.Items[6], 7)),
            new Task("Do miscellaneous things","Do some other stuff",390,new Tuple<Skill,double> (AllSkills.Items[8], 6.7),
                new Tuple<Skill,double> (AllSkills.Items[9], 4.5))
        };
    }
}
