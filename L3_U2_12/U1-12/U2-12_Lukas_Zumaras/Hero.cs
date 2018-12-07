using System;

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
        public Hero(string name, string race, string role, int hitPoints,
            int mana, int damage, int defence, int strength, int agility,
            int intelligence, string power)
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
        /// lyginimo operatorius
        /// </summary>
        /// <param name="lhs">kairys herojus</param>
        /// <param name="rhs">desinys herojus</param>
        /// <returns>ar mazesnis</returns>
        public static bool operator <=(Hero lhs, Hero rhs)
        {
            int l = lhs.Agility + lhs.Strength + lhs.Intelligence;
            int r = rhs.Agility + rhs.Strength + rhs.Intelligence;

            return (l <= r);
        }

        /// <summary>
        /// lyginimo operatorius 
        /// </summary>
        /// <param name="lhs">kairys herojus</param>
        /// <param name="rhs">desinys herojus</param>
        /// <returns>ar didesnis</returns>
        public static bool operator >=(Hero lhs, Hero rhs)
        {
            int l = lhs.Agility + lhs.Strength + lhs.Intelligence;
            int r = rhs.Agility + rhs.Strength + rhs.Intelligence;

            return (l >= r);
        }

        /// <summary>
        /// lyginimo metodas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Hero);
        }

        /// <summary>
        /// tikrina ar pateiktas herojus lygus
        /// </summary>
        /// <param name="hero"> herojus</param>
        /// <returns> ar lygus </returns>
        public bool Equals(Hero hero)
        {
            if (Object.ReferenceEquals(hero, null))
            {
                return false;
            }

            if (this.GetType() != hero.GetType())
                return false;

            return (hero.Name == this.Name)
                && (hero.Role == this.Role)
                && (hero.HitPoints == this.HitPoints)
                && (hero.Mana == this.Mana)
                && (hero.Damage == this.Damage)
                && (hero.Defence == this.Defence)
                && (hero.Strength == this.Strength)
                && (hero.Agility == this.Agility)
                && (hero.Intelligence == this.Intelligence)
                && (hero.Power == this.Power);
        }

        /// <summary>
        /// hash kodo metodas 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^
                Role.GetHashCode() ^
                HitPoints.GetHashCode() ^
                Mana.GetHashCode() ^
                Damage.GetHashCode() ^
                Defence.GetHashCode() ^
                Strength.GetHashCode() ^
                Agility.GetHashCode() ^
                Intelligence.GetHashCode() ^
                Power.GetHashCode();
        }

        /// <summary>
        /// lygina herojus
        /// </summary>
        /// <param name="lhs">kairys herojus</param>
        /// <param name="rhs">desinys herojus</param>
        /// <returns>ar lygus</returns>
        public static bool operator ==(Hero lhs, Hero rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// lygina herojus
        /// </summary>
        /// <param name="lhs">kairys herojus</param>
        /// <param name="rhs">desinys herojus</param>
        /// <returns>grazina ar nelygu</returns>
        public static bool operator !=(Hero lhs, Hero rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// spausdinimui paruosia duomenis
        /// </summary>
        /// <returns>parduostas string</returns>
        public override string ToString()
        {
            return string.Format("| {0, -18} | {1, -18} | {2, 18} | {3, 18} |" +
                                 " {4, 18} | {5, 18} | {6, 18} | {7, 18} |" +
                                 " {8, 18} | {9, -18} |", Name, Role,
                                 HitPoints, Mana, Damage, Defence, Strength,
                                 Agility, Intelligence, Power);
        }

        /// <summary>
        /// geriausiu spausdinimui paruosia duomenis
        /// </summary>
        /// <returns>paruostas string</returns>
        public String BestToString()
        {
            return string.Format("Rasė: {0,10} | Vardas: {1,10} |" +
                " Klase: {2,10} | charakteristikos: {3,4} {4,4} {5,4} ",
                Race, Name, Role, Agility, Strength, Intelligence);
        }

        /// <summary>
        /// tanku spausdinimui paruosia duomenis 
        /// </summary>
        /// <returns>paruostas string</returns>
        public String TankToString()
        {
            return string.Format("{0},{1}," +
            "{2},{3}", Race, Name, Role, Power);
        }

        /// <summary>
        /// Tikrina ar atitinka nurodytus parametrus
        /// </summary>
        /// <param name="tankHealth">nurodyti givybes taskai</param>
        /// <param name="tankDefence">nurodyti ginybos taskai</param>
        /// <returns></returns>
        public bool IsTank(int tankHealth, int tankDefence)
        {
            if (HitPoints >= tankHealth && Defence >= tankDefence)
                return true;
            return false;
        }
    }
}