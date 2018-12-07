using System.Collections.Generic;
namespace U1_12
{
    class HeroContainer
    {
        private Hero[] Heroes;
        public int Count { get; private set; }

        /// <summary>
        /// konstruktorius
        /// </summary>
        /// <param name="size"></param>
        public HeroContainer(int size)
        {
            Heroes = new Hero[size];
            Count = 0;
        }

        /// <summary>
        /// prideda heroju
        /// </summary>
        /// <param name="hero"></param>
        public void AddHero(Hero hero)
        {
            Heroes[Count++] = hero;
        }

        /// <summary>
        /// prideda heroju pagal indexa
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="index"></param>
        public void AddHero(Hero hero, int index)
        {
            Heroes[index] = hero;
        }

        /// <summary>
        ///  grazina indexo heroju
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Hero GetHero(int index)
        {
            return Heroes[index];
        }

        /// <summary>
        /// suranda geriausio herojaus indexa
        /// </summary>
        /// <returns></returns>
        public int GetBestIndex()
        {
            int index = 0;
            Hero Best = Heroes[0];
            for (int i = 1; i < Count; i++)
            {
                if (Best <= Heroes[i])
                {
                    Best = Heroes[i];
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// suranda identiskus herojus
        /// </summary>
        /// <returns></returns>
        public List<string> FindCopy()
        {
            List<string> temp = new List<string>();
            List<int> tempIndex = new List<int>();

            for (int i = 0; i < Count; i++)
                for (int j = 1; j < Count; j++)
                    if (Heroes[i] == Heroes[j])
                        if (!tempIndex.Contains(i))
                        {
                            tempIndex.Add(i);
                            temp.Add(Heroes[i].Name);
                        }
            return temp;
        }

    }
}
