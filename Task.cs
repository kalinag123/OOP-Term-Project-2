/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

namespace CPSC_3200_P3
{
    public class Task
    {
        /// <summary>
        /// Class Invariants:
        /// Task name can only be set when the instance is created.
        /// Task description can be changed any time. 
        /// Base time that it takes to complete a task can only be set when the instance is created.
        /// Task skills and associated difficulties are publicly readable, but can only be set when the instance is created.
        /// Client can specify 0 or more skills, but skills and their difficulty level must be in (Skill, double) 
        /// format- otherwise the program cannot be compiled. 
        /// Task cannot have duplicate skills, but skills are case sensitive. 
        /// </summary>
        private readonly string _name;
        private string _description;
        public string Name { get { return _name; } }
        public string Description {
            get => _description;
            set => _description = value;
        }

        private readonly double _baseTime;
        public double BaseTime { get { return _baseTime; } }

        private bool _done = false;
        public bool Done {
            get => _done;
            set { if(_done) return; _done = value; }
        }


        // creates a dictionary with skill name as the key and skill difficulty as the value. 
        private readonly Dictionary<Skill, double> _skillsRequired = new Dictionary<Skill, double>();
        public Dictionary<Skill, double> SkillsRequired { get { return _skillsRequired; } }    


        public Task(string name, string description, double time, params Tuple<Skill,double>[] skills)
        {
            _name = name;
            Description = description;
            _baseTime = time;

            foreach(Tuple<Skill, double> item in skills)
            {
                _skillsRequired.Add(item.Item1, item.Item2);
            }
        }
    }
}
