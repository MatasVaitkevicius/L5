using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U1_12
{

    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            List<Hero> heroes = p.ReadHeroData();
            p.SaveReportToFile(heroes);
            ReportTable(heroes, "L1ReportTable.csv");

            var BestHero = FindBestHero(heroes);
            Console.WriteLine("Herojus su geriausiomis charakteristikomis: {0} {1} {2}, jega: {3}, vikrumas: {4}, intelektas: {5}",
                BestHero.Name, BestHero.Race, BestHero.Role, BestHero.Strength, BestHero.Agility, BestHero.Intelligence);

            GetCommonRole(heroes);
            

            var Elves = GetElves(heroes);
            SavingToFile(Elves, "Elfai.csv");

            p.Tankai(heroes, "Tankai.csv");
        }

        /// <summary>
        /// Finds the best hero from a list
        /// </summary>
        /// <param name="heroes"> List of heroes </param>
        /// <returns></returns>
        static Hero FindBestHero(List<Hero> heroes)
        {
            return heroes.OrderByDescending(x => (x.Strength + x.Agility + x.Intelligence)).First().DeepClone();
        }

        /// <summary>
        /// Finds the most common role from a list of heroes
        /// </summary>
        /// <param name="heroes"> List of heroes </param>
        static void GetCommonRole(List<Hero> heroes)
        {
            string role;
            string name;
            int heroNumber;
            bool hasCommonRole;
            var SortedList = heroes.GroupBy(x => x.Role).OrderByDescending(group => group.Count());
            role = SortedList.First().First().Role;

            heroNumber = SortedList.First().Count();
            int tempHeroNumber = heroNumber;
            hasCommonRole = SortedList.Where(group => group.Count() == tempHeroNumber).Count() == 1;

            if (hasCommonRole == false)
                Console.WriteLine("Dažniausios klasės nėra");
            else
                {
                Console.WriteLine("Dažniausia klasė:{0}", role);
                    for (int i = 0; i < heroNumber; i++)
                    {
                        name = SortedList.First().First().Name;
                        Console.WriteLine("{0}", name);
                    }
                }
        }

        /// <summary>
        /// Finds elves from a list of heroes
        /// </summary>
        /// <param name="heroes"></param>
        /// <returns></returns>
        static List<Hero> GetElves(List<Hero> heroes)
        {
            return heroes.Where(x => x.Race == " elfas").Select(x => x.DeepClone()).ToList();
        }

        /// <summary>
        /// Finds tank heroes from a list
        /// </summary>
        /// <param name="heroes"> The list of heroes </param>
        /// <param name="FileName"> The name of the file </param>
        void Tankai(List<Hero> heroes, string FileName)
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                writer.WriteLine("Tankai: ");
                foreach (Hero hero in heroes)
                {
                    if (hero.HitPoints >= 100 && hero.Defence >= 30)
                        writer.WriteLine("{0} {1} {2} {3}", hero.Name, hero.Role, hero.Race, hero.Power);
                }
            }
        }
        
        /// <summary>
        /// Reads hero data from a list
        /// </summary>
        /// <returns></returns>
        List<Hero> ReadHeroData()
        {
            List<Hero> heroes = new List<Hero>();

            string[] lines = File.ReadAllLines(@"L1Data2.csv");
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                string name = values[0];
                string race = values[1];
                string role = values[2];
                int hitPoints = int.Parse(values[3]);
                int mana = int.Parse(values[4]);
                int damage = int.Parse(values[5]);
                int defence = int.Parse(values[6]);
                int strength = int.Parse(values[7]);
                int agility = int.Parse(values[8]);
                int intelligence = int.Parse(values[9]);
                string power = values[10];
                Hero hero = new Hero(name, race, role, hitPoints, mana, damage, defence, strength, agility, intelligence, power);
                heroes.Add(hero);
            }
            return heroes;
        }

        /// <summary>
        /// Saves all hero data to a file
        /// </summary>
        /// <param name="heroes"> The list of heroes </param>
        void SaveReportToFile(List<Hero> heroes)
        {
            string[] lines = new string[heroes.Count];
            for (int i = 0; i < heroes.Count; i++)
            {
                lines[i] = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};",
                    heroes[i].Name, heroes[i].Race,
                    heroes[i].Role, heroes[i].HitPoints, heroes[i].Mana,
                    heroes[i].Damage, heroes[i].Defence, heroes[i].Strength, heroes[i].Agility,
                    heroes[i].Intelligence, heroes[i].Power);
            }
            File.WriteAllLines(@"L1SavedData.csv", lines);
        }

        /// <summary>
        /// Creates a report table
        /// </summary>
        /// <param name="heroes"> The list of heroes </param>
        /// <param name="FileName"> The name of the file </param>
        static void ReportTable(List<Hero> heroes, string FileName)
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                writer.WriteLine("Herojų sarašas");
                writer.WriteLine(new String('-', 215));

                writer.WriteLine("| {0, -15} | {1, -15} | {2, -15} | {3, -15} | {4, -15} | {5, -15} | {6, -15} | {7, -15} | {8, -15} | {9, -15} | {10, -15} |",
                    "Vardas", "Rasė", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai", "Ginybos taškai", "Jėga", "Vikrumas", "Intelektas", "Ypatinga galia");

                writer.WriteLine(new String('-', 215));

                foreach (var hero in heroes)
                {
                    writer.WriteLine("| {0, -16} | {1, -20} | {2, -20} | {3, -25} | {4, -20} | {5, -28} | {6, -28} | {7, -19} | {8, -20} | {9, -25} | {10, -15} |",
                        hero.Name, hero.Race, hero.Role, hero.HitPoints, hero.Mana, hero.Damage, hero.Defence, hero.Strength, hero.Agility, hero.Intelligence, hero.Power);
                    writer.WriteLine(new String('-', 215));
                }
            }
        }

        /// <summary>
        /// Saves all hero data to a file with a specified name
        /// </summary>
        /// <param name="heroes"> The list of heroes </param>
        /// <param name="FileName"> The name of the file </param>
        static void SavingToFile(List<Hero> heroes, string FileName)
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                if (heroes.Count == 0)
                    writer.WriteLine("Herojų sarašas tuščias");
                else
                {
                    foreach (var hero in heroes)
                    {
                        writer.WriteLine("{0} ; {1} ; {2} ; {3} ; {4} ; {5} ; {6} ; {7} ; {8} ; {9} ; {10}",
                            hero.Name, hero.Race, hero.Role, hero.HitPoints, hero.Mana,
                            hero.Damage, hero.Defence, hero.Strength, hero.Agility,
                            hero.Intelligence, hero.Power);
                    }
                }
            }
        }

    }
}
