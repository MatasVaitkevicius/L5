using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace U3_12
{
    class Program
    {
        public const int TankHealth = 100; //Tanko gyvybės pagal kurias suranda tanka
        public const int TankDefence = 30; // Tanko gynyba pagal kurias suranda tanka

        static void Main(string[] args)
        {
            Console.ReadKey();
            var filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L3Data*.csv");
            var branchContainer = new BranchContainer();
            foreach (var path in filePaths)
            {
                branchContainer.AddBranch(ReadData(path));
            }
            using (var writer = new StreamWriter("ReportTable.txt")) ;
            CreateReportTable(branchContainer, "ReportTable.txt");
            PrintMostPopularRole(branchContainer);
            Console.WriteLine("2.Išspausdina pasikartojančius veikėjų vardus į Klaidos.csv");
            WriteFilteredPlayersData(branchContainer, "Klaidos.csv");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("3.Išspausdina tankus pagal gyvybės ir gynybos taškus į Tankai.csv");
            PrintTanks(branchContainer, "Tankai.csv");
            Console.ReadKey();
        }

        /// <summary>
        /// Nuskaito duomenis
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Duomenis</returns>
        private static Branch ReadData(string file)
        {
            Branch branch;

            using (var reader = new StreamReader(file, Encoding.UTF8))
            {

                var line = reader.ReadLine();
                var race = line;
                var city = reader.ReadLine();
                line = reader.ReadLine();
                branch = new Branch(race, city);

                while (line != null)
                {
                    var values = line.Split(',');
                    char type = line[0];
                    var name = values[1];
                    var role = values[2];
                    var hitPoints = int.Parse(values[3]);
                    var mana = int.Parse(values[4]);
                    var damage = int.Parse(values[5]);
                    var defence = int.Parse(values[6]);

                    switch (type)
                    {
                        case 'H':
                            var strength = int.Parse(values[7]);
                            var agility = int.Parse(values[8]);
                            var intelligence = int.Parse(values[9]);
                            var power = values[10];
                            var hero = new Hero(name, role, hitPoints, mana, damage, defence, strength, agility, intelligence, power);
                            branch.Heroes.AddHero(hero);
                            break;

                        case 'N':
                            var guild = values[7];
                            var nPC = new NPC(name, role, hitPoints, mana, damage, defence, guild);
                            branch.NPCs.AddNPC(nPC);
                            break;
                    }
                    line = reader.ReadLine();
                }
            }
            return branch;
        }

        /// <summary>
        /// Išspausdina žaidėjų lentelę
        /// </summary>
        /// <param name="branchContainer"></param>
        /// <param name="file"></param>
        private static void CreateReportTable(BranchContainer branchContainer, string file)
        {
            using (var writer = new StreamWriter(file,true, Encoding.UTF8))
            {
                writer.WriteLine("Žaidėjų sąrašai");
                writer.WriteLine(new string('-', 181));
                for (int i = 0; i < branchContainer.Count; i++)
                {
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("Naujas žaidėjo sąrašas");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0,-10} | {1,-15} | ", "Rasė", "Miestas");
                    writer.WriteLine($"| {branchContainer.GetBranch(i).Race,-10} | {branchContainer.GetBranch(i).Town,-15} |");

                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("Herojai");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0, -15} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15} | {6,-15} | {7,-15} | {8,-15} | {9,-15} |",
                        "Vardas", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai ", "Gynybos taškai", "Jėga", "Vikrumas", "Intelektas", "Ypatinga galia");
                    writer.WriteLine(new string('-', 181));
                    for (int j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                    {
                        writer.WriteLine($"| {branchContainer.GetBranch(i).Heroes.GetHero(j).Name,-15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Role,-15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).HitPoints,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Mana,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Damage,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Defence,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Strength,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Agility,15} | " +
                                         $"{branchContainer.GetBranch(i).Heroes.GetHero(j).Intelligence,15} | {branchContainer.GetBranch(i).Heroes.GetHero(j).Power,-15} |");
                    }

                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("NPC");
                    writer.WriteLine(new string('-', 181));
                    writer.WriteLine("| {0, -15} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15} | {6,-15} |",
                        "Vardas", "Klasė", "Gyvybės taškai", "Mana", "Žalos taškai ", "Gynybos taškai", "Gildija");
                    writer.WriteLine(new string('-', 181));
                    for (int j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                    {
                        writer.WriteLine($"| {branchContainer.GetBranch(i).NPCs.GetNPC(j).Name,-15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Role,-15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).HitPoints,15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Mana,15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).Damage,15} | {branchContainer.GetBranch(i).NPCs.GetNPC(j).Defence,15} | " +
                                         $"{branchContainer.GetBranch(i).NPCs.GetNPC(j).Guild,-15} |");
                    }
                    writer.WriteLine(new string('-', 181));
                }
            }
        }
        /// <summary>
        /// Suranda populiariausią klasę
        /// </summary>
        /// <param name="branchContainer"></param>
        /// <returns>Populiariausią klasę</returns>
        private static Dictionary<string, int> FindMostPopular(BranchContainer branchContainer)
        {
            var mostPopularRole = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (var j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (mostPopularRole.ContainsKey(branchContainer.GetBranch(i).Heroes.GetHero(j).Role))
                    {
                        mostPopularRole[branchContainer.GetBranch(i).Heroes.GetHero(j).Role]++;
                    }
                    else
                    {
                        mostPopularRole.Add(branchContainer.GetBranch(i).Heroes.GetHero(j).Role, 1);
                    }
                }

                for (var j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (mostPopularRole.ContainsKey(branchContainer.GetBranch(i).NPCs.GetNPC(j).Role))
                    {
                        mostPopularRole[branchContainer.GetBranch(i).NPCs.GetNPC(j).Role]++;
                    }
                    else
                    {
                        mostPopularRole.Add(branchContainer.GetBranch(i).NPCs.GetNPC(j).Role, 1);
                    }
                }
            }
            return mostPopularRole;
        }

        /// <summary>
        /// Išspausdina populiariausią klasę
        /// </summary>
        /// <param name="branchContainer"></param>
        private static void PrintMostPopularRole(BranchContainer branchContainer)
        {
            var mostPopularRole = FindMostPopular(branchContainer);
            var maxValue = mostPopularRole.Values.Max();
            var role = mostPopularRole.FirstOrDefault(f => f.Value == maxValue).Key;

            Console.WriteLine($"1.Daugiausiai šios klasės veikėjų: {role} | Pasikartoja: {maxValue} karto(ų)");
            Console.WriteLine();
        }

        /// <summary>
        /// Suranda vienodus veikėjus
        /// </summary>
        /// <param name="branchContainer"></param>
        /// <returns>Vienodus veikėjus</returns>
        private static Dictionary<string, int> FilterPlayers(BranchContainer branchContainer)
        {
            var samePlayers = new Dictionary<string, int>();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (var j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (samePlayers.ContainsKey(branchContainer.GetBranch(i).Heroes.GetHero(j).Name))
                    {
                        samePlayers[branchContainer.GetBranch(i).Heroes.GetHero(j).Name]++;
                    }
                    else
                    {
                        samePlayers.Add(branchContainer.GetBranch(i).Heroes.GetHero(j).Name, 1);
                    }
                }

                for (var j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (samePlayers.ContainsKey(branchContainer.GetBranch(i).NPCs.GetNPC(j).Name))
                    {
                        samePlayers[branchContainer.GetBranch(i).NPCs.GetNPC(j).Name]++;
                    }
                    else
                    {
                        samePlayers.Add(branchContainer.GetBranch(i).NPCs.GetNPC(j).Name, 1);
                    }
                }
            }
            return samePlayers;
        }

        /// <summary>
        /// Įrašo pasikartojančias rasių vardus į failą
        /// </summary>
        /// <param name="branchContainer"></param>
        /// <param name="file"></param>
        public static void WriteFilteredPlayersData(BranchContainer branchContainer, string file)
        {
            var filteredPlayers = FilterPlayers(branchContainer);
            var filteredPlayersCount = filteredPlayers.Values.Max();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Vardas;");
                foreach (KeyValuePair<string, int> samePlayers in filteredPlayers)
                {
                    if (samePlayers.Value >= 2)
                    {
                        writer.WriteLine($"{samePlayers.Key}");
                    }
                }
            }
        }

        /// <summary>
        /// Suranda herojus kurie atitinka tanko parametrus
        /// </summary>
        /// <param name="branches">masyvas su visu rasiu duomenis</param>
        /// <returns>Gražina tankus</returns>
        public static Branch FindTanks(BranchContainer branchContainer)
        {
            var tanks = new Branch();
            for (int i = 0; i < branchContainer.Count; i++)
            {
                for (int j = 0; j < branchContainer.GetBranch(i).Heroes.Count; j++)
                {
                    if (branchContainer.GetBranch(i).Heroes.GetHero(j).IsTank(TankHealth, TankDefence))
                    {
                        tanks.AddHero(branchContainer.GetBranch(i).Heroes.GetHero(j));
                    }
                }

                for (int j = 0; j < branchContainer.GetBranch(i).NPCs.Count; j++)
                {
                    if (branchContainer.GetBranch(i).NPCs.GetNPC(j).IsTank(TankHealth, TankDefence))
                    {
                        tanks.AddNPC(branchContainer.GetBranch(i).NPCs.GetNPC(j));
                    }
                }
                
            }
            return tanks;
        }

        /// <summary>
        /// Įrašo surikiuotus tankus į failą.
        /// </summary>
        /// <param name="branchContainer"></param>
        /// <param name="file"></param>
        public static void PrintTanks(BranchContainer branchContainer, string file)
        {
            var filteredTanks = FindTanks(branchContainer);
            filteredTanks.Heroes.SortHeroes();
            filteredTanks.NPCs.SortNPCs();
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Herojai");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Jėga;Vikrumas;Intelektas;Ypatinga galia");
                for (int i = 0; i < filteredTanks.Heroes.Count; i++)
                {
                    writer.WriteLine(filteredTanks.Heroes.GetHero(i).ToString());
                }
                writer.WriteLine();
                writer.WriteLine("NPC");
                writer.WriteLine("Vardas;Klasė;Gyvybės taškai;Mana;Žalos taškai;Gynybos taškai;Gildija");
                for (int i = 0; i < filteredTanks.NPCs.Count; i++)
                {
                    writer.WriteLine(filteredTanks.NPCs.GetNPC(i).ToString());
                }
            }
        }
    }
}