using System.Linq;

namespace U3_12
{
    class HeroContainer
    {
        private const int MaxHeroes = 100;
        Hero[] Heroes;
        public int Count { get; private set; }

        public HeroContainer()
        {
            Heroes = new Hero[MaxHeroes];
            Count = 0;
        }

        public void AddHero(Hero hero)
        {
            Heroes[Count++] = hero;
        }

        public Hero GetHero(int index)
        {
            return Heroes[index];
        }

        public bool Contains(Hero hero)
        {
            return Heroes.Contains(hero);
        }

        public void SortHeroes()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                var minValueHero = Heroes[i];
                int minValueIndex = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (Heroes[j] <= minValueHero)
                    {
                        minValueHero = Heroes[j];
                        minValueIndex = j;
                    }
                }
                Heroes[minValueIndex] = Heroes[i];
                Heroes[i] = minValueHero;
            }
        }
    }
}
