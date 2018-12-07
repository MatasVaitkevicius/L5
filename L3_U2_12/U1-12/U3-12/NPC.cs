namespace U3_12
{
    class NPC : Player
    {
        public string Guild { get; set; }

        public NPC(string name, string role, int hitPoints, int mana, int damage, int defence, string guild)
            : base(name, role, hitPoints, mana, damage, defence)
        {
            Guild = guild;
        }

        public override string ToString()
        {
            return $"{Name,-5};{Role,-10};{HitPoints,10};{Mana,10};{Damage,10};{Defence,10};{Guild,-10}";
        }
    }
}
