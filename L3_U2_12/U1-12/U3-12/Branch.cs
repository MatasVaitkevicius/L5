namespace U3_12
{
    class Branch
    {
        public const int MaxNumberOfPlayers = 100;
        public string Race { get; set; }
        public string Town { get; set; }
        public HeroContainer Heroes { get; set; }
        public NPCContainer NPCs { get; set; }

        public Branch()
        {
            Heroes = new HeroContainer();
            NPCs = new NPCContainer();
        }

        public Branch(string race, string town)
        {
            Race = race;
            Town = town;
            Heroes = new HeroContainer();
            NPCs = new NPCContainer();
        }

        public void AddHero(Hero hero)
        {
            Heroes.AddHero(hero);
        }

        public void AddNPC(NPC nPC)
        {
            NPCs.AddNPC(nPC);
        }
    }
}
