using System;

namespace U3_12
{
    abstract class Player
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Defence { get; set; }

        public Player(string name, string role, int hitPoints, int mana, int damage, int defence)
        {
            Name = name;
            Role = role;
            HitPoints = hitPoints;
            Mana = mana;
            Damage = damage;
            Defence = defence;
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

        public static bool operator >=(Player lhs, Player rhs)
        {
            int ip = String.Compare(lhs.Role, rhs.Role, StringComparison.CurrentCulture);
            int ip2 = String.Compare(lhs.Name, rhs.Name, StringComparison.CurrentCulture);
            return ip > 0 || ip == 0 && ip2 > 0;
        }
        public static bool operator <=(Player lhs, Player rhs)
        {
            int ip = String.Compare(lhs.Role, rhs.Role, StringComparison.CurrentCulture);
            int ip2 = String.Compare(lhs.Name, rhs.Name, StringComparison.CurrentCulture);
            return ip < 0 || ip == 0 && ip2 < 0;
        }
    }
}
