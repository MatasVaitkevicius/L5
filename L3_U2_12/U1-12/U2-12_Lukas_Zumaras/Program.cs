using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace U1_12
{
    class Program
    {
        public const int TankHealth = 100;
        public const int TankDefence = 30;

        static void Main(string[] args)
        {
            Program p = new Program();

            // Surandami visi duomenu failai atitinkantis nurodymus
            string[] filePaths =
                Directory.GetFiles(Directory.GetCurrentDirectory(),
                "L2*.csv");

            // Pagal surastus duomenis yra surandama kiek yra skirtingu rasiu
            int NumberOfBranches = filePaths.Length;
            Branch[] branches = new Branch[NumberOfBranches];

            // Duomenu nuskaitimo ciklas
            for (int i = 0; i < filePaths.Length; i++)
                p.ReadHeroData(filePaths[i], branches, i);

            if (NumberOfBranches == 0)
                Console.WriteLine("Nerasta tinkamu failu");
            else
            {
                // 1 punktas - randami ir atspausdinami geriausi veikejai
                p.PrintBestHeroes(branches);
                // 2 punktas - randami ir atspausdinami isivelusios klaidos
                if (p.PrintCopy(p.FindCopy(branches), "Klaidos.csv"))
                    Console.WriteLine("Nerasta klaidu, failas nesukurtas");
                // 3 punktas - randami ir atspausinami herojai
                // atitinkantis tanko parametrus
                if (p.FindTanks(branches).Count > 0)
                    p.PrintTanks(p.FindTanks(branches), "Tankai.csv");
                else
                    Console.WriteLine("Nerasta veikeju atitenkanciu tanku" +
                                      " parametru, failas nesukurtas");
            }

            p.SaveReportToFile(branches);
        }

        /// <summary>
        /// Nuskaitomi duomenis is pateikto failo
        /// </summary>
        /// <param name="file">Skaitimo failas</param>
        /// <param name="branches">Masyvas su visu rasiu duomenimis</param>
        /// <param name="k">Pildomas masyvo elementas</param>
        public void ReadHeroData(string file, Branch[] branches, int k)
        {
            string town = null;
            string race = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                string line;

                line = null;
                line = reader.ReadLine();
                if (line != null)
                    race = line;

                line = null;
                line = reader.ReadLine();
                if (line != null)
                    town = line;

                branches[k] = new Branch(town, race);

                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    var name = values[0];
                    var role = values[1];
                    var hitPoints = int.Parse(values[2]);
                    var mana = int.Parse(values[3]);
                    var damage = int.Parse(values[4]);
                    var defence = int.Parse(values[5]);
                    var strenght = int.Parse(values[6]);
                    var agility = int.Parse(values[7]);
                    var intelligence = int.Parse(values[8]);
                    string power = values[9];

                    Hero hero = new Hero(name, race, role, hitPoints, mana,
                                        damage, defence, strenght, agility,
                                        intelligence, power);
                    branches[k].Heroes.AddHero(hero);
                }
            }
        }

        /// <summary>
        /// Metodas saugoti pradinius duomenis lenteleje
        /// </summary>
        /// <param name="branches">visu rasiu duomenis</param>
        void SaveReportToFile(Branch[] branches)
        {
            string file = "Data_L2_" + "ReportedTable.txt";
            int linesSize = 0;
            for (int i = 0; i < branches.Length; i++)
                linesSize += branches[i].Heroes.Count;
            linesSize += 7 * branches.Length;
            string[] lines = new string[linesSize];
            int tableWidth = 211;
            string tableLine = new string('-', tableWidth);
            int k = 0;

            for (int i = 0; i < branches.Length; i++)
            {
                lines[k] = branches[i].Race;
                lines[k + 1] = branches[i].Town;
                lines[k + 2] = tableLine;
                lines[k + 3] = String.Format("| {0, -18} | {1,-18} | {2,-18} |" +
                    " {3,-18} | {4,-18} | {5,-18} | {6,-18} | {7,-18} |" +
                    " {8,-18} | {9,-18} |", "vardas", "klasė", "gyvybės taškai",
                    "mana", "žalos taškai", "gynybos taškai", "jėga",
                    "vikrumas", "intelektas", "ypatinga galia");
                lines[k + 4] = tableLine;
                for (int t = 0; t < branches[i].Heroes.Count; t++)
                    lines[k + t + 5] = branches[i].Heroes.GetHero(t).ToString();
                lines[k + branches[i].Heroes.Count + 5] = tableLine;
                lines[k + branches[i].Heroes.Count + 6] = " ";
                k += branches[i].Heroes.Count;
                k += 7;
            }
            File.WriteAllLines(@file, lines);
        }

        /// <summary>
        /// Metodas spausdinti geriausius rasiu zaidejus
        /// </summary>
        /// <param name="branches">masyvas su visu rasiu duomenimis</param>
        public void PrintBestHeroes(Branch[] branches)
        {
            Console.WriteLine("geriausi visų rasių herojai");
            Console.WriteLine();
            for (int i = 0; i < branches.Length; i++)
            {
                int k = branches[i].Heroes.GetBestIndex();
                Console.WriteLine(branches[i].Heroes.GetHero(k).BestToString());
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Suranda kopijas tarp skirtingu masyvo elementu
        /// </summary>
        /// <param name="branches">masyvas su rasiu duomenimis</param>
        /// <returns>grazina pasikartojimu sarasa</returns>
        public List<string> FindCopy(Branch[] branches)
        {
            List<string> temp = new List<string>();

            for (int i = 0; i < branches.Length; i++)
                for (int j = i + 1; j < branches.Length; j++)
                    for (int t = 0; t < branches[i].Heroes.Count; t++)
                        for (int k = 0; k < branches[i].Heroes.Count; k++)
                            if (branches[i].Heroes.GetHero(t) ==
                                branches[j].Heroes.GetHero(k))
                                temp.Add(branches[i].Heroes.GetHero(t).Name);


            return temp;
        }

        /// <summary>
        /// Atspausdina pateikta string sarasa 
        /// </summary>
        /// <param name="prints">string sarasas</param>
        /// <param name="fileName">Spausdinimo failas</param>
        public bool PrintCopy(List<string> prints, string fileName)
        {
            int k = 0;
            foreach (var print in prints)
                k++;

            if (k == 0)
                return true;

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Veikejų (su klaidomis) vardai :");
                writer.WriteLine();
                foreach (string print in prints)
                    writer.WriteLine(print);
            }
            return false;
        }

        /// <summary>
        /// suranda herojus kurie atitinka tanko parametrus
        /// </summary>
        /// <param name="branches">masyvas su visu rasiu duomenis</param>
        /// <returns></returns>
        public HeroContainer FindTanks(Branch[] branches)
        {
            var temp = new HeroContainer(Branch.MaxNumberOfHeroes);
            for (int i = 0; i < branches.Length; i++)
            {
                for (int j = 0; j < branches[i].Heroes.Count; j++)
                {
                    if (branches[i].Heroes.GetHero(j).IsTank(TankHealth,
                        TankDefence))
                        temp.AddHero(branches[i].Heroes.GetHero(j));
                }
            }
            return temp;
        }

        /// <summary>
        /// atspausdina pateikta tanku HeroContainer
        /// </summary>
        /// <param name="prints"> spausdinimui skirtas masyvas</param>
        /// <param name="fileName">Spausdinimo failas</param>
        public void PrintTanks(HeroContainer prints, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName,
                    false, Encoding.UTF8))
            {
                writer.WriteLine("Veikejai turintys tanko savybes :");
                writer.WriteLine();
                writer.WriteLine("Rasė,Vardas,Klasė,Galia");
                writer.WriteLine();
                for (int i = 0; i < prints.Count; i++)
                    writer.WriteLine(prints.GetHero(i).TankToString());
            }
        }
    }
}
