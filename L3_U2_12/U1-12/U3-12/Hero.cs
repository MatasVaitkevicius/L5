namespace U3_12
{
    class Hero : Player
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public string Power { get; set; }

        public Hero(string name, string role, int hitPoints, int mana, int damage, int defence, int strength, int agility, int intelligence, string power)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Power = power;
        }

        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Strength,-10};{Agility,-10};{Intelligence,-10};{Power,10}";
        }
    }
}