using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U1_12
{
    /// <summary>
    /// A class for storing Hero data
    /// </summary>
    class Hero
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Role { get; set; }
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Defence { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public string Power { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Hero()
        {
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="name"> Name of the hero</param>
        /// <param name="race"> Race of the hero</param>
        /// <param name="role"> Role of the hero</param>
        /// <param name="hitPoints"> Hit Points of the hero</param>
        /// <param name="mana"> Mana of the hero</param>
        /// <param name="damage"> Damage of the hero</param>
        /// <param name="defence"> Defence of the hero</param>
        /// <param name="strength"> Strength of the hero</param>
        /// <param name="agility"> Agility of the hero</param>
        /// <param name="intelligence"> Intelligence of the hero</param>
        /// <param name="power"> Special power of the hero</param>
        public Hero(string name, string race, string role, int hitPoints, int mana,
            int damage, int defence, int strength, int agility, int intelligence, string power)
        {
            Name = name;
            Race = race;
            Role = role;
            HitPoints = hitPoints;
            Mana = mana;
            Damage = damage;
            Defence = defence;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Power = power;
        }

        /// <summary>
        /// Creates a deep copy of the current Hero instance
        /// </summary>
        /// <returns></returns>
        public Hero DeepClone()
        {
            return new Hero(Name, Race, Role, HitPoints, Mana, Damage, Defence, Strength, Agility, Intelligence, Power);
        }
    }
}
