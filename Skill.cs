/*
 * Author: Kalina Gavrilova
 * Project: P5
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_3200_P3
{
    public class Skill
    {
        /// <summary>
        /// Class Invariants:
        /// Skill name and description are immutable and can only be set when the instance is created.
        /// Skill instances with the same name and description will be considered equal and will return the same
        /// hash code.
        /// 
        /// </summary>
        private readonly string _name;
        public string Name { get { return _name; } }

        private readonly string _description;
        public string Description { get { return _description; } }

        /// <summary>
        /// preconditions: None.
        /// postconditions: Will consider two skill instances equal if their names, descriptions, and types match. 
        /// </summary>
        public override bool Equals(object? obj)
        {
            Skill other = obj as Skill;
            if (other != null)
            {
                return _name == other._name && _description == other._description && GetType() == other.GetType();
            }
            return false;
        }

        public override int GetHashCode()
        {
            string hashID = _name + _description;
            return hashID.GetHashCode();
        }

        public Skill(string name, string description)
        {
            _name = name;
            _description = description;
        }

        /// <summary>
        /// preconditions: None.
        /// postconditions: None.
        /// </summary>      
        public virtual double CalculateCost(double baseCost, double taskDifficulty, double empLevel)
        {
            double timeMultiplier;
            if (empLevel > 0)
            {
                timeMultiplier = taskDifficulty / empLevel;
            }

            else
            {
                timeMultiplier = taskDifficulty + 1;
            }

            return baseCost * timeMultiplier;
            
        }
    }

    /// <summary>
    /// preconditions: Skill must be defined as an ExponentialSkill to have access to this method.
    /// postconditions: None.
    /// </summary>  
    public class ExponentialSkill : Skill
    {
        public ExponentialSkill(string name, string description) : base(name, description) { }

        public override double CalculateCost(double baseCost, double taskDifficulty, double empLevel)
        {
            double difference = empLevel - taskDifficulty;
            double timeMultiplier;

            if (difference > 0)
            {
                timeMultiplier = 1 / Math.Pow(difference, 2.0);
            }

            else { timeMultiplier = Math.Pow(difference, 2.0); }


            return baseCost * timeMultiplier;
        }
    }

    /// <summary>
    /// preconditions: Skill must be defined as a StaticSkill to have access to this method.
    /// postconditions: None.
    /// </summary> 
    public class StaticSkill : Skill
    {
        private readonly Skill _baseSkill; 

        public StaticSkill(string name, string description) : base(name, description) { 
            _baseSkill = new Skill(name, description); 
        }

        public override double CalculateCost(double baseCost, double taskDifficulty, double empLevel)
        {
            double originalEstimate = _baseSkill.CalculateCost(baseCost, taskDifficulty, empLevel);

            return (baseCost + originalEstimate) / 2;
        }

    }


    /// <summary>
    /// preconditions: 
    /// Skill must be defined as a RandomSkill to have access to this method.
    /// Uses dependecy injection (constructor injection) of 2 random number generators.
    /// postconditions: Dependecy exists for the lifetime of a RandomSkill instance and cannot be changed during runtime.
    /// </summary> 
    public class RandomSkill : Skill
    {
        private Random rnd_multiplier;
        private Random rnd_direction;

        public RandomSkill(string name, string desc, Random rnd1, Random rnd2) : base(name, desc) 
        { 
            rnd_multiplier = rnd1; rnd_direction = rnd2; 
        }

        public override double CalculateCost(double baseCost, double taskDifficulty, double empLevel)
        {
            double timeMultiplier = rnd_multiplier.Next(0,Math.Max((int)empLevel,(int)taskDifficulty));


            int faster = rnd_direction.Next(0, 2);

            if(faster == 1)
            {
                return baseCost / timeMultiplier;
            }

            return baseCost * timeMultiplier;
        }

    }

}

