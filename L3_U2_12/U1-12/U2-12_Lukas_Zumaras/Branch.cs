namespace U1_12
{
    class Branch
    {
        public const int MaxNumberOfHeroes = 50;
        public string Race { get; set; }
        public string Town { get; set; }
        public HeroContainer Heroes { get; private set; }

        /// <summary>
        /// parametrizuotas konstruktorius
        /// </summary>
        /// <param name="town"></param>
        /// <param name="race"></param>
        public Branch(string town, string race)
        {
            Town = town;
            Race = race;
            Heroes = new HeroContainer(MaxNumberOfHeroes);
        }
    }
}
