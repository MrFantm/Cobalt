using System;
using System.IO;


namespace Cobalt.Economy 
{
    public class Generic 
    {
        public static string rootDir = Main.Program.rootDir;

        public static string ecoDir = rootDir + @"\cobalt_Run\data\economy\economy_info.eco";
        public static string[] eco = new string[500];
        public static string[] botAttributes = new string[500];

        public static void Init(string state) 
        {
            int num = 0;
            if(File.Exists(ecoDir)) 
            {
                foreach(string s in File.ReadAllLines(ecoDir)) 
                {
                    eco[num] = s;
                    num++;
                }
            }
            else 
            {
                Console.WriteLine("Economy file not found!");
                Console.Write("Starting Amount: ");
                int startAmount = Int32.Parse(Console.ReadLine().Replace("Starting Amount: ", ""));
                Console.Write("Tax Rate: ");
                int taxRate = Int32.Parse(Console.ReadLine().Replace("Tax Rate: ", ""));
                using(StreamWriter sw = new StreamWriter(ecoDir)) 
                {
                    sw.WriteLine(startAmount.ToString());
                    sw.WriteLine(taxRate.ToString());
                }
                Console.WriteLine("Economy File Created!");
                Main.Program.ConsoleDeploy();
            }

            if(state == "none") 
            {
                Main.Program.ConsoleDeploy();
            }
            else if(state == "pool") 
            {
                MoneyPool.GetCurrentAmount();
            }
            else if(state == "collect") 
            {
                Taxes.GetAmounts();
            }
        }

        static object syncRoot;
        public static void Add(string value, int amount) 
        {
            if(value == "eco") 
            {
                if(eco == null) 
                {
                    int num = 0;
                    foreach(string s in File.ReadAllLines(ecoDir)) 
                    {
                        eco[num] = s;
                        num++;
                    }
                }
                else 
                {
                    int newTotal = Int32.Parse(eco[0]) + amount;

                }
            }
            else 
            {
                string path = rootDir + @"\cobalt_Run\data\bots\" + value + "_attributes.cbinfo";
                syncRoot = path;
                if(!File.Exists(path)) 
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Error- Cannot find a bot with the name: '" + value + "'. Check for spelling mistakes! Use !help for help with commands");
                    Main.Program.textColor = ConsoleColor.DarkBlue;
                    Main.Program.consoleColor = ConsoleColor.Gray;

                    Main.Program.ConsoleDeploy();
                }
                else 
                {

                    int num = 0;
                    foreach(string s in File.ReadAllLines(path)) 
                    {
                        botAttributes[num] = s;
                        num++;
                    }

                    File.Delete(path);
                    lock(syncRoot) {
                        using(StreamWriter sw = File.CreateText(path)) {
                            sw.WriteLine(botAttributes[0]); // Name          
                            sw.WriteLine(botAttributes[1]); //Eye Color        
                            sw.WriteLine(botAttributes[2]); //Hair Color      
                            sw.WriteLine(botAttributes[3]); //Height          
                            sw.WriteLine(botAttributes[4]); //Gender           
                            sw.WriteLine(botAttributes[5]); //Age              
                            sw.WriteLine(botAttributes[6]); //Birthday         
                            sw.WriteLine(botAttributes[7]); //Marital Status 
                            sw.WriteLine(botAttributes[8]); //Occupation     

                            int botBalance = Int32.Parse(botAttributes[9]) + amount;
                            sw.WriteLine(botBalance.ToString()); //Bank Balance 
                            sw.WriteLine(botAttributes[10]);//Salary         
                            sw.WriteLine(botAttributes[11]);//Address       
                            sw.Dispose();

                        }
                    } 

                    Console.Write("$" + amount + " has successfully been added to " + value + "'s balance");
                    Console.WriteLine();
                    Main.Program.ConsoleDeploy();
                }
            }
        }
    }

    class MoneyPool 
    {
        public static int poolAmount;
        public static string ecoDir = Generic.ecoDir;
        public static string[] eco = Generic.eco;

        public static void GetCurrentAmount() 
        {
            poolAmount = Int32.Parse(eco[0]);

            Console.WriteLine("Current Funds in the economy: $" + poolAmount);
            Main.Program.ConsoleDeploy();
        }
    }

    class Taxes 
    {
        public static string rootDir = Main.Program.rootDir;
        public static string ecoDir = Generic.ecoDir;
        public static string[] eco = Generic.eco;
        public static int[] totals = new int[99999];
        

        static object syncRoot;
        public static void Collect(bool isManual) 
        {
            int num = 0;
                    
            for(int i = 0; i < botAmount; i++) {

                string path = rootDir + @"\cobalt_Run\data\bots\" + bots[num] + "_attributes.cbinfo";
                syncRoot = path;
                string[] lines = File.ReadAllLines(path);

                botAttributes[0] = lines[0];    //Name
                botAttributes[1] = lines[1];    //
                botAttributes[2] = lines[2];
                botAttributes[3] = lines[3];
                botAttributes[4] = lines[4];
                botAttributes[5] = lines[5];
                botAttributes[6] = lines[6];
                botAttributes[7] = lines[7];
                botAttributes[8] = lines[8];
                int balance = Int16.Parse(lines[9]);

                if(balance > 0) 
                {

                    //Tax Logic

                    //Just SUM complicated math, nothin' to see here
                    int rate = Int32.Parse(eco[1]);
                    float newRate = (float)rate / 100;
                    newRate = newRate * balance;
                    float totalL = (float)balance - newRate;
                    totalL = (float)MathF.Round(totalL);
                    balance = (int)totalL;

                    botAttributes[9] = balance.ToString();
                    botAttributes[10] = lines[10];
                    botAttributes[11] = lines[11];

                    lock(syncRoot) {
                        using(StreamWriter sw = File.CreateText(path)) {
                            sw.WriteLine(botAttributes[0]);
                            sw.WriteLine(botAttributes[1]);
                            sw.WriteLine(botAttributes[2]);
                            sw.WriteLine(botAttributes[3]);
                            sw.WriteLine(botAttributes[4]);
                            sw.WriteLine(botAttributes[5]);
                            sw.WriteLine(botAttributes[6]);
                            sw.WriteLine(botAttributes[7]);
                            sw.WriteLine(botAttributes[8]);
                            sw.WriteLine(botAttributes[9]);
                            sw.WriteLine(botAttributes[10]);
                            sw.WriteLine(botAttributes[11]);
                            sw.Dispose();
                        }
                        
                    }
                    
                    num++;
                }
                else 
                {
                    num++;
                }

            } 

            if(isManual) 
            {
                Console.WriteLine("Taxes collected successfully!");
                Main.Program.ConsoleDeploy();
            }
            else 
            {
                Console.WriteLine("It's Tax Day!");
            }
        }

        public static string[] bots = new string[9000];
        public static string[] botAttributes = new string[50];
        public static int botAmount;
        public static void GetAmounts() 
        {
            int num = 0;
            botAmount = 0;
            foreach (var filePath in Directory.EnumerateFiles(rootDir + @"\cobalt_Run\data\bots")) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    bots[num] = line;
                    botAmount++;
                    num++;
                    reader.Dispose();
                }
            }
            Console.WriteLine("Loaded " + botAmount + " bots!");
            Collect(true);
        }

        public static void SetTaxRate(int value) 
        {

        }
    }
}