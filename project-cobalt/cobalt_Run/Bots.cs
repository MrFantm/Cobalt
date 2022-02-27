using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Cobalt;

namespace Cobalt.Bots {
    public class BotsRun {

        
        //Log
        public static string[] log = new string[99999999];

        public static string botsName;
        public static string maleBotName;
        public static string secondBot;
        public static string botsGender;
        public static int botsAge;
        public static string botsCauseOfDeath;
        public static string botsOccupation;
        public static string botsOccupationL;
        public static int botsSalery;
        public static int saleryS;
        public static int botsBalance;
        public static string botsMaritalStatus;
        public static string dirStatic = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots\";        
        public static string dir = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots\";
        public static string dir2 = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots";
        public static string marFName;

        public static int day = 1;
        public static int year = 1;

        public static int logNum = 0;

        public static string dname;

        private static string[] botInfo = new string[25];
        public static void Run() {
            dir = dirStatic;

            string dir2 = dir + botsName + "_attributes.cbinfo";
            dir = dir2;
            dname = botsName;

            GetBotData();
        }

        public static void EPIKJOB() {
            string epikJOb = Cobalt.Jobs.Careers.jobs[1];
            Console.WriteLine(epikJOb);
            Cobalt.Main.Program.ConsoleDeploy();
        }

        public static void Actions() {
            
               
            Random random = new Random();
            int num = random.Next(0,26);
            int[] ammounts = {
                Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath.Length, 
                Cobalt.Events.EventStrings.botevents.Length, 
                Cobalt.Events.EventStrings.randomevents.Length, 
                Cobalt.Jobs.PartTime.jobs.Length, 
                Cobalt.Jobs.Careers.jobs.Length
            };


            int codNum = random.Next(0,ammounts[0]);
            int eventNum1 = random.Next(0,ammounts[1]);
            int eventNum2 = random.Next(0,ammounts[2]);
            int eventNum3 = random.Next(0,ammounts[3]);
            int eventNum4 = random.Next(0,ammounts[4]);

            string botEvent = Cobalt.Events.EventStrings.botevents[eventNum1];
            string randomEvent = Cobalt.Events.EventStrings.randomevents[eventNum2];

            //PartTime Jobs
            string partTimeJob = Cobalt.Jobs.PartTime.jobs[eventNum3];
            int ptSalery = Cobalt.Jobs.PartTime.pay[eventNum3];
            
            //Career Jobs
            string careerJob = Cobalt.Jobs.Careers.jobs[eventNum4];
            int crSalery = Cobalt.Jobs.Careers.pay[eventNum4];



            //Dates
            int sum = day / 31;
            Console.WriteLine("Year: " + year + " | " + "Day: " + day++);
                
            if(day == 366) {
                year++;
                day = 1;
                Cobalt.Main.Program.AgeUp();
            }


            //Marriage & Dating
            int[] genderAmmounts = {Cobalt.Main.Program.femaleBots.Length, Cobalt.Main.Program.maleBots.Length};
            string[] females = Cobalt.Main.Program.femaleBots;
            string[] males = Cobalt.Main.Program.maleBots;

            if(num >= 1 && botsAge >= 18) { //Marriage
                if(botsMaritalStatus == "single") {
                    Cobalt.Main.Program.TryForMarriage();
                }
            }


            if(num > 20 && botsMaritalStatus != "single") { //Reproduce
                using(StreamReader s = new StreamReader(dir)) {
                    string[] fname = s.ReadLine().Split(' ');
                    Cobalt.Main.Program.familyName = fname[1];
                    Cobalt.Main.Program.isChild = true;
                    Cobalt.Main.Program.CreateNew();
                }
            }
            
                
            //Events
            Console.WriteLine(botsName + botEvent);
            if(num < 15) {
                Console.WriteLine(randomEvent);
            }


            //Education
            if(botsAge >= 18 && botsAge < 22 && num == 8) {
                Console.WriteLine(botsName + " is now going to college");
            }


            //Jobs
            if(botsAge >= 16 && num > 10) {
                if(botsOccupationL != "unemployed") {
                    int month = 1;
                    if(sum == month) {
                        botsBalance = botsBalance + botsSalery / 12;
                        month++;
                        UpdateBotBalance();
                    }
                }
                else {
                    botsOccupation = partTimeJob;
                    saleryS = ptSalery;
                    Console.WriteLine(botsName + " is now working as a " + botsOccupation);
                    WriteJobInfo();
                }
            }
            if(botsAge >= 21 && botsAge < 25 && num > 15) {
                if(botsOccupationL != "unemployed") {
                    int month = 1;
                    if(sum == month) {
                        botsBalance = botsBalance + botsSalery / 12;
                        month++;
                        UpdateBotBalance();
                    }
                }
                else {
                    botsOccupation = careerJob;
                    saleryS = crSalery;
                    Console.WriteLine(botsName + " is now working as a " + botsOccupation);
                    WriteJobInfo();
                }
            }


            //Death
            if(num == 4) {
                botsCauseOfDeath = Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath[codNum];
                Cobalt.Main.Program.lastBot = dname;
                Cobalt.Main.Program.Die();
            }
            if(botsAge > 60) {
                if(num > 8) {
                    botsCauseOfDeath = Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath[codNum];
                    Cobalt.Main.Program.lastBot = dname;
                    Cobalt.Main.Program.Die();
                }                
            }
            if(botsAge > 70) {
                if(num > 6) {
                    botsCauseOfDeath = Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath[codNum];
                    Cobalt.Main.Program.lastBot = dname;
                    Cobalt.Main.Program.Die();
                }
            }
            if(botsAge > 80) {
                if(num > 5) {
                    botsCauseOfDeath = Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath[codNum];
                    Cobalt.Main.Program.lastBot = dname;
                    Cobalt.Main.Program.Die();
                }
            }
        }

        public static void GetBotData() {
            if(File.Exists(dir)) {
                string[] lines = File.ReadAllLines(dir);
                botsGender = lines[4];
                botsAge = Int16.Parse(lines[5]);
                botsOccupationL = lines[8];
                botsMaritalStatus = lines[7];
                botsBalance = Int32.Parse(lines[9]);
                botsSalery = Int32.Parse(lines[10]);
            }
            Actions();
        }

        public static string[] marriageAttributes = new string[100];
        public static void UpdateMarriage() {

            int num = 0;
            dir = dirStatic + botsName + "_attributes.cbinfo";
            using(StreamReader s = new StreamReader(dir)) {
                string[] fname = s.ReadLine().Split(' ');
                dir = dirStatic + @"marriages\" + fname[1] + "_marriage.cbmar";
                Console.WriteLine("FAMILY NAME: " + fname[1]);
            }

            string[] lines = File.ReadAllLines(dir);
            foreach(string line in lines) {
                marriageAttributes[num] = lines[num];
                num++;
            }
            using(StreamWriter sw = File.AppendText(dir)) {
                sw.WriteLine(Cobalt.Main.Program.childName);
            }
        }
         
        public static void WriteJobInfo() {
            int num = 0;
            string[] lines = File.ReadAllLines(dir);
            foreach(string line in lines) {
                botInfo[num] = lines[num];
                num++;
            }
            botInfo[8] = botsOccupation;
            botInfo[10] = saleryS.ToString();
            syncRoot = dir;
            lock(syncRoot) {
                using(StreamWriter sw = File.CreateText(dir)) {
                    sw.WriteLine(botInfo[0]); // Name
                    sw.WriteLine(botInfo[1]); //Eye Color
                    sw.WriteLine(botInfo[2]); //Hair Color
                    sw.WriteLine(botInfo[3]); //Height
                    sw.WriteLine(botInfo[4]); //Gender
                    sw.WriteLine(botInfo[5]); //Age
                    sw.WriteLine(botInfo[6]); //Birthday
                    sw.WriteLine(botInfo[7]); //Marital Status
                    sw.WriteLine(botInfo[8]); //Occupation
                    sw.WriteLine(botInfo[9]); //Bank Balance
                    sw.WriteLine(botInfo[10]); //Salery
                    sw.Dispose();
                }
            }    
        }

        public static void UpdateBotBalance() {
            int num = 0;
            string[] lines = File.ReadAllLines(dir);
            foreach(string line in lines) {
                botInfo[num] = lines[num];
                num++;
            }
            syncRoot = dir;
            botInfo[9] = botsBalance.ToString();
            
            lock(syncRoot) {
                using(StreamWriter sw = File.CreateText(dir)) {
                    sw.WriteLine(botInfo[0]);
                    sw.WriteLine(botInfo[1]);
                    sw.WriteLine(botInfo[2]);
                    sw.WriteLine(botInfo[3]);
                    sw.WriteLine(botInfo[4]);
                    sw.WriteLine(botInfo[5]);
                    sw.WriteLine(botInfo[6]);
                    sw.WriteLine(botInfo[7]);
                    sw.WriteLine(botInfo[8]);
                    sw.WriteLine(botInfo[9]);
                    sw.WriteLine(botInfo[10]);
                    sw.Dispose();
                }
            }    
        }

        public static string[] allBots = new string[500];
        private static int botAmmount;
        public static string path;
        public static void GetAllBots() {
        
            int num = 0;
            botAmmount = 0;
            foreach (var filePath in Directory.EnumerateFiles(dir2)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    allBots[num] = line;
                    botAmmount++;
                    num++;
                    reader.Dispose();
                }
            }
            Console.WriteLine("Loaded " + botAmmount + " bots!");
            SetBotsAge();
        }

        static object syncRoot;

        public static string[] botAttributes = new string[25];
        public static void SetBotsAge() {
            int num = 0;
                    
            for(int i = 0; i < botAmmount; i++) {
                string path = dirStatic + allBots[num] + "_attributes.cbinfo";
                syncRoot = path;
                string[] lines = File.ReadAllLines(path);
                botAttributes[0] = lines[0];
                botAttributes[1] = lines[1];
                botAttributes[2] = lines[2];
                botAttributes[3] = lines[3];
                botAttributes[4] = lines[4];
                int age = Int16.Parse(lines[5]) + 1;
                botAttributes[5] = age.ToString();
                botAttributes[6] = lines[6];
                botAttributes[7] = lines[7];
                botAttributes[8] = lines[8];
                botAttributes[9] = lines[9];
                botAttributes[10] = lines[10];

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
                        sw.Dispose();
                    }
                    
                }
                
                num++;
            } 
        }
    }
}