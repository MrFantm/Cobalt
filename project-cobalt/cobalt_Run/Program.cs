using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Cobalt;

namespace Cobalt.Main {
    class Program {
        
        //Variables



        //strings
        public static string command;
        public static string name;
        public static string botNameL;
        public static string eyeColorL;
        public static string hairColorL;
        public static string heightL;
        public static string genderL;
        public static string causeOfDeathL;
        public static string birthdayL;
        public static string maritalStatisL;
        public static string familyName;
        public static string occupationL;

        //settings
        public static string color1;
        public static string color2;
        public static int dayInterval;


        //ints
        public static int ageL;
        public static int bankBalanceL;
        public static int salery;

        //directories
        public static string textFile = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots\";
        public static string dir = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots";

        public static string helpDir = @"C:\Program Files\project-cobalt\cobalt_Run\data\other\help.cobalt";
        public static string logDir = @"C:\Program Files\project-cobalt\cobalt_Run\data\other\";
        public static string fileName;
        public static string path;



        //bools
        public static bool continueBot = false;
        public static bool hasLoaded = false;
        public static bool isManual = false;
        public static bool isChild = false;

        static void Main(string[] args) {
            Cobalt.Settings.MainSettings.LoadSettings();

            color1 = Cobalt.Settings.MainSettings.color1L;
            color2 = Cobalt.Settings.MainSettings.color2L;
            dayInterval = Cobalt.Settings.MainSettings.dayIntervalL;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;
            causeOfDeathL = " ";

            Console.WriteLine("Welcome To Cobalt");
            Console.WriteLine("Write !help for a list of commands");
            LoadAiHierarchyOnStart();
            LoadElligableGenders();
        }

        public static void CheckCommand() {

            //Checks commands... Yes it's messy... it works so I dont care.... kinda...

            if(command == "!create") {  //Creates a new AI with random Attributes
                isManual = true;
                CreateNew();
            }
            else if(command == "!test") { //For test purposes
            //    isManual = true;
            //    LoadElligableGenders();
                TestColor();
            }
            else if(command == "!help") { //Lists all the Commands and their actions
                CommandHelp();
            }
            else if(command == "!age up") { //Increase every AI's Age by 1
                Cobalt.Bots.BotsRun.GetAllBots();
            }
            else if(command == "!review") { //Review a Deceased AI's life file
                ReadLifeData();
            }
            else if(command == "!view") {   //Veiw a Specified AI using an int from a hierarchy
                Console.WriteLine("What active AI would you like to view? Use the number from the hierarchy; which you can view with '!list'");
                ViewAI();
            }
            else if(command == "!list") {   //Lists all the AI still living on your Machine; Creates a Hierarchy
                ListActiveBots();
            }
            else if(command == "!list deceased") {  //Lists all the Deceased AI
                ListDeceased();
            }
            else if(command == "!settings") {
                Settings();
            }
            else if(command == "!clear") {  //Clears the console  NOT SUGGESTED!!!!!
                
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Are you sure you want to CLEAR the console? Clearing the console is only suggested as a means of reset! \n Y/N" );
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                
                ConsoleKey key;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
        
                if(key == ConsoleKey.Y) {
                    Console.Clear();
                }
                else if(key == ConsoleKey.N){
                    Console.WriteLine("Canceled Clear!");
                }

                ConsoleDeploy();
            }
            else if(command == "!play") {   //Plays The simulation
                Event();
            }
            else if(command == "!kill") {   //Kills a Specified AI
                KillBot();
            }   
            else if(command == "!exit" || command == "!quit") {  //To exit the program
                
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You're about to close the program! Are you sure you want to continue? \n Y/N" );
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                
                ConsoleKey key;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                key = keyInfo.Key;

                if(key == ConsoleKey.Y) {
                    Environment.Exit(0); 
                }
                else if(key == ConsoleKey.N){
                    ConsoleDeploy();
                }
                else {
                    
                }
            }
            else { //If no valid command is sent
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(">Cobalt: " + "Error - The Command: " +  "\"" + command + "\"" + " was not found! Please check for spelling errors, or the Command does not exist.");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                
                ConsoleDeploy();
            }
        }

        public static void ConsoleDeploy() {
            isManual = false;
            Console.Write(">Cobalt: ");
            CommandReady();
        }

        public static void CommandReady() {
            command = Console.ReadLine();
            CheckCommand();
        }

        //Timeline and Bot Actions

        private static Timer timer;
        public static void Event() {
            timer = new Timer(TimerCallback, null, 10000, 10000);

            Console.ReadKey();
            timer.Dispose();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Simulation Terminated!");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;

            ConsoleDeploy();
        }

        public static string currentBot;
        public static int botAmmount = 0;
        public static string lastBot;

        private static void TimerCallback(Object o) 
        {
            Random random = new Random();
            LoadAiHierarchy();
            int length = botAmmount;
            int num = random.Next(0,length);
            int num2 = random.Next(1,100);
            currentBot = activeBots[num];
            path = textFile + currentBot + "_attributes.cbinfo";
            LoadDataFromHierarchy();
            LoadElligableGenders();

            Cobalt.Bots.BotsRun.botsName = currentBot;
            Cobalt.Bots.BotsRun.Run();

        }

        //Settings
        public static void Settings() {

        }

        //Bot Stuff
        public static void Die() {
            string botToDestroy = Cobalt.Bots.BotsRun.dname;
            path = textFile + botToDestroy + "_attributes.cbinfo";
            syncRoot = path;

        lock(syncRoot) {
            File.Delete(path);
        }    


            Random random = new Random();
            int num = random.Next(0,6);
            string causeOfDeath = "";

            string cause = Cobalt.Bots.BotsRun.botsCauseOfDeath;
            causeOfDeath = cause;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(botToDestroy + " sadly perished from " + causeOfDeath + "! You can review " + botToDestroy + "'s life with !review.");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;

            path = textFile + @"deceased\" + botToDestroy + "_life.cblife";
            DateTime now = DateTime.Now;
            string dateOfDeath = now.ToString();

            if(!File.Exists(path)) {
                using(StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("Full Name: " + botNameL);
                    sw.WriteLine("Eye Color: " + eyeColorL);
                    sw.WriteLine("Hair Color: " + hairColorL);
                    sw.WriteLine("Height: " + heightL + " feet");
                    sw.WriteLine("Gender: " + genderL);
                    sw.WriteLine("Age of death: " + ageL);
                    sw.WriteLine("Birthday: " + birthdayL);
                    sw.WriteLine("Cause of death: " + causeOfDeath);
                    sw.WriteLine("Time of Death: " + dateOfDeath);
                    sw.WriteLine("Marital Status: " + maritalStatisL);
                    sw.WriteLine("Occupation: " + occupationL);
                    sw.WriteLine("Bank Balance: " + bankBalanceL);
                } 
                path = textFile;
                continueBot = false;
                
            }
            path = textFile;
        }

        public static string childName;
        public static void CreateNew() {

            Random random = new Random();
            Bot bot01 = new Bot();
            string botFirstName;
            string botLastName;

            string[] pronounMale = {"He", "His"};
            string[] pronounFemale = {"She", "Her"};
            string[] pronoun = new string[2];

            int num = random.Next(0,2);   
            int heightNum = 1;

           
           
           
            if(num == 0) { 
                Console.WriteLine("Your AI is a Male");
                bot01.gender = "male";
            }
            else {
                Console.WriteLine("Your AI is a Female");
                bot01.gender = "female";
            }


           //Hair and eye color

           int eyeColorNum = random.Next(0,5);
           int hairColorNum = random.Next(0,12);

                //5 entries
            string[] eyeColor = {"blue", "green", "brown", "hazel", "grey", "amber"}; 

                //12 entries
            string[] hairColor = {"black", "dark brown", "medium brown", "light brown", "chesnut brown", "auburn", "red", "orange red", "copper", "titan", "strawberry blonde", "light blonde", "golden blonde"};

            bot01.eyeColor = eyeColor[eyeColorNum];
            bot01.hairColor = hairColor[hairColorNum];

            //Names
 
                int fnameNum = random.Next(0,49);
                int lnameNum = random.Next(0,64);


                //First Names

                    //Male       //49 entries
                    string[] maleName = {"James","William","Elijah","Oliver","Noah","Benjamin","Lucas","Henry","Alexander","Mason","Michael","Ethan","Daniel","Jacob","Logan","Jackson","Levi","Sebastian","Mateo","Jack","Owen","Theodore","Aiden","Samuel","Joseph","John","David","Wyatt","Matthew","Luke","Asher","Carter","Julian","Grayson","Leo","Jayden","Gabriel","Isaac","Lincoln","Anthony","Hudson","Dylan","Odinsky","Thomas","Charles","Christopher","Jaxon","Maverick","Josiah"};
                    
                    //Female     //49 entries
                    string[] femaleName = {"Harper","Camila","Gianna","Abigail","Luna","Ella","Elizabeth","Sofia","Emily","Avery","Mila","Scarlett","Eleanor","Madison","Layla","Penelope","Aria","Chloe","Grace","Ellie","Nora","Hazel","Zoey","Riley","Victoria","Lily","Aurora","Violet","Nova","Hannah","Emilia","Zoe","Stella","Everly","Isla","Leah","Lillian","Addison","Willow","Lucy","Paisley", "Rebecca", "Kindle", "Lisa", "Georgia", "Kelly", "Jamie", "Antiqua", "Wendy", "Marsha", "Lorrie", "Jillian", "Amanda"};
                
                //Last Name     //64 entries
                string[] lastName = {"Gay","Downs","Miller","Crane","Dudley","Duarte","Greene","Esparza","Ramirez","Kramer","Vance","Bowen","Avery","Avila","Hull","Solis","Jarvis","Clayton","Scott","Harvey","Kirby","Townsend","Shepard","Vaughn","Butler","Roth","Malone","Koch","Reid","Rollins","Mays","Payne","Wood","Bernard","Chen","Stephens","Waters","Barker","Miles","Galvan","Boyer","Bennett","Orr","Delacruz","Wall","Gamble","Buchanan","Mason","Ferrell","Barron","Reed","Cooper","Dickson","Morrison","Stevenson","Eaton","Summers","Adams","Hendrix","Walton","Pierce","Roth","Reeves","Boyd","Jenkins"};

            if(bot01.gender == "male") {
                pronoun[0] = pronounMale[0];
                pronoun[1] = pronounMale[1];
                bot01.firstName = maleName[fnameNum];
            }
            else {
                pronoun[0] = pronounFemale[0];
                pronoun[1] = pronounFemale[1];
                bot01.firstName = femaleName[fnameNum];
            }

            if(isChild != true) {
                bot01.lastName = lastName[lnameNum];
            }
            else {
                bot01.lastName = familyName;
            }
           
            botFirstName = bot01.firstName;
            botLastName = bot01.lastName;

            bot01.age = 1;
            DateTime now = DateTime.Now;
            bot01.birthday = now.ToString();
            bot01.height = heightNum.ToString();
            name = botFirstName + " " + botLastName;
            path = textFile + name + "_attributes.cbinfo";
            fileName = path;

            //assigning bot values for data transfer
            botNameL = name;
            eyeColorL = bot01.eyeColor;
            hairColorL = bot01.hairColor;
            ageL = bot01.age;
            heightL = bot01.height;
            genderL = bot01.gender;
            birthdayL = bot01.birthday;
            maritalStatisL = "single";
            occupationL = "unemployed";
            bankBalanceL = 0;
            salery = 0;

            if(isChild == true) {
                Console.WriteLine("The " + familyName + "s had a baby! "+ pronoun[1] + " name is " + name + "!");
                childName = name;
            }
            Console.WriteLine("Your New AI's Name is : " + name);
            Console.WriteLine(name + ", Has " + bot01.eyeColor + " eyes, and " + bot01.hairColor + " hair! " + pronoun[0] + " is " + bot01.height + " feet tall, and " + bot01.age + " years old!");


            
            if(isChild == true) {
                Cobalt.Bots.BotsRun.UpdateMarriage(); 
            }
            WriteData();
            LoadAiHierarchyOnStart();
            
            if(isManual == true) {
                ConsoleDeploy();
            } 

        }

        public static void AgeUp() {
            Cobalt.Bots.BotsRun.GetAllBots();
        }


        public static void KillBot() {
            

            Console.WriteLine("What is the name of the AI you wish to destroy?");
            string botToDestroy = Console.ReadLine();

            path = textFile + botToDestroy + "_attributes.cbinfo";
            fileName = path;
            LoadData();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You are about to kill: " + botToDestroy + "! Are you sure you want to coninue? \n Y/N");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Gray;
                
            ConsoleKey key;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if(key == ConsoleKey.Y) {
                if(!File.Exists(path)) {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(name + " does not exist! You can create an AI with !create.");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Gray;

                }
                else {
                    File.Delete(path);

                    Random random = new Random();
                    int num = random.Next(0,30);
                    string causeOfDeath = "";

                    string cause = Cobalt.CausesOfDeath.CausesOfDeath.causesOfDeath[num];

                    causeOfDeath = cause;

                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(name + " sadly perished from " + causeOfDeath + "! You can review " + name + "'s life with !review.");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Gray;

                    path = textFile + @"\deceased\" + botToDestroy + "_life.cblife";
                    DateTime now = DateTime.Now;
                    string dateOfDeath = now.ToString();

                    if(!File.Exists(path)) {
                        using(StreamWriter sw = File.CreateText(path)) {
                            sw.WriteLine("Full Name: " + botNameL);
                            sw.WriteLine("Eye Color: " + eyeColorL);
                            sw.WriteLine("Hair Color: " + hairColorL);
                            sw.WriteLine("Height: " + heightL + " feet");
                            sw.WriteLine("Gender: " + genderL);
                            sw.WriteLine("Age of death: " + ageL);
                            sw.WriteLine("Birthday: " + birthdayL);
                            sw.WriteLine("Cause of death: " + causeOfDeath);
                            sw.WriteLine("Time of Death: " + dateOfDeath);
                            sw.WriteLine("Marital Status: " + maritalStatisL);
                            sw.WriteLine("Occupation: " + occupationL);
                            sw.WriteLine("Bank Balance: " + bankBalanceL);
                        } 

                        continueBot = false;
                        ConsoleDeploy();
                    }
                } 
            }
            else if(key == ConsoleKey.N){
                ConsoleDeploy();
            }
            path = textFile;
        }

        //Data

        public static int snum = 0;
        public static string[] activeBots = new string[500];
        public static string botInput;
        public static int bnum = 0;
        
        public static void Test() {

            string input = Console.ReadLine();

            path = textFile + input + "_attributes.cbinfo";

            if(input != "!done") {
                botInput = input;
                Test2();
            }
            else {
                
            }
        }

        public static void Test2() {

            if(!File.Exists(fileName)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("There's no AI with that name; Enter another name or type '!done' to finish.");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                Test();
            }
            else {
                string[] lines = File.ReadAllLines(fileName);
                
                activeBots[bnum] = botInput;
                bnum++;
                Test();
            }

        }

        public static void ListDeceased() {

            string dirS = dir;
            dir = dir + @"\deceased";
            botAmmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(dir)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    Console.WriteLine(num + ". " + name);
                    botAmmount++;
                    num++;
                }
            }
            ConsoleDeploy();
        }

        public static int femaleBotsNum = 0;
        public static string[] femaleBots = new string[500];

        public static int maleBotsNum = 0;
        public static string[] maleBots = new string[500];

       
        public static void LoadElligableGenders() {
            int num = 0;
            femaleBotsNum = 0;
            maleBotsNum = 0;

            foreach (var filePath in Directory.EnumerateFiles(dir)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    path = textFile + name + "_attributes.cbinfo";
                    
                    string[] lines = File.ReadAllLines(path);
                    if(lines[4] == "male" && Int16.Parse(lines[5]) >= 16) {
                        maleBots[maleBotsNum] = name;
                        maleBotsNum++;
                        num++;
                    }
                    else if(lines[4] == "female" && Int16.Parse(lines[5]) >= 16) {
                        femaleBots[femaleBotsNum] = name;
                        femaleBotsNum++;
                        num++;
                    }
                }
            }

            lengthM = maleBotsNum;
            lengthF = femaleBotsNum;
        }

        public static void ListActiveBots() {
            botAmmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(textFile)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    Console.WriteLine(num + ". " + name);
                    botAmmount++;
                    num++;
                }
            }
            ConsoleDeploy();
        }

        public static void LoadAiHierarchy() {
            botAmmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(dir)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    botAmmount++;
                    num++;
                }
            }
        }

        public static void LoadAiHierarchyOnStart() {
            botAmmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(textFile)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    botAmmount++;
                    num++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            int snum = num - 1;
            Console.WriteLine("AI Hierarchy Successfully Loaded " + num  + " AI data profiles!");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            ConsoleDeploy();
        }

        public static int aiNum2;
        public static void ViewAI() {
            string aiNumS = Console.ReadLine();
            int aiNum = Int16.Parse(aiNumS);
            aiNum2 = aiNum;

            Console.WriteLine("You are about to view " + activeBots[aiNum] + ". Would you like to continue? \n Y/N");
            path = textFile + activeBots[aiNum] + "_attributes.cbinfo";
            
            ConsoleKey key;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if(key == ConsoleKey.Y) {
                if(File.Exists(path)) {
                    PrintBotData();
                }
                else {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("No AI with the given number is currently living on this machine; create one by using the command '!create'");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    ConsoleDeploy();
                }
            }
            else if(key == ConsoleKey.N){
                Console.WriteLine("Canceled");
            }
            path = textFile;
            ConsoleDeploy();
        }

        public static void WriteData() {

            if(!File.Exists(fileName)) {
                using(StreamWriter sw = File.CreateText(fileName)) {
                    sw.WriteLine(botNameL);
                    sw.WriteLine(eyeColorL);
                    sw.WriteLine(hairColorL);
                    sw.WriteLine(heightL);
                    sw.WriteLine(genderL);
                    sw.WriteLine(ageL);
                    sw.WriteLine(birthdayL);
                    sw.WriteLine(maritalStatisL);
                    sw.WriteLine(occupationL);
                    sw.WriteLine(bankBalanceL);
                    sw.WriteLine(salery);
                } 
            }
            path = textFile;
            ConsoleDeploy();
        }


        public static void PrintBotData() {
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("AI's Name: " + lines[0]);
            Console.WriteLine("Eye Color: " + lines[1]);
            Console.WriteLine("Hair Color: " + lines[2]);
            Console.WriteLine("Height: " + lines[3] + "ft");
            Console.WriteLine("Gender: " + lines[4]);
            Console.WriteLine("Age: " + lines[5]);
            Console.WriteLine("Birthday: " + lines[6]);

            if(lines[7] != "single") {
                Console.WriteLine("Marital Status: Married to " + lines[7]);
            }
            else {
                Console.WriteLine("Marital Status: " + lines[7]);
            }
            Console.WriteLine("Occupation: " + lines[8]);
            Console.WriteLine("Bank Balance: " + lines[9]);
            Console.WriteLine("Current Salery: " + lines[10]);

            ConsoleDeploy();
        }





        //Marriages

        public static string femaleNameS;
        public static string maleNameS;

        public static int lengthM;
        public static int lengthF;

        public static void TryForMarriage() {

            LoadElligableGenders();

            
            Random random = new Random();
            int num = random.Next(0,2);
            int num2 = random.Next(0,lengthM);
            int num3 = random.Next(0,lengthF);
            string answer;
            string nameS = currentBot; //Cobalt.Bots.BotsRun.botsName
            
            if(Bots.BotsRun.botsGender == "male") {

                maleNameS = nameS;
                femaleNameS = femaleBots[num3];

                if(num == 0) {
                    answer = "yes";
                }
                else {
                    answer = "no";
                }
                
                Console.WriteLine(nameS + " asked " + femaleBots[num3] + " to marry him. She said " + answer + "!");

                if(answer == "yes") {
                    Console.WriteLine(nameS + " and " + femaleBots[num3] + " are now married!");
                    GetLastNameMale();
                }
                else {
                    Console.WriteLine(femaleBots[num3] + " rejected " + nameS + "'s proposal");
                    Console.WriteLine("They both remain single");
                }
            }
            else if(Bots.BotsRun.botsGender == "female") {
            
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.White;
                {
                    
                }

                femaleNameS = nameS;
                maleNameS = maleBots[num2];
                if(num == 0) {
                    answer = "yes";
                }
                else {
                    answer = "no";
                }
                
                Console.WriteLine(maleBots[num2] + " asked " + nameS + " to marry him. She said " + answer + "!");

                if(answer == "yes") {
                    Console.WriteLine(nameS + " and " + maleBots[num2] + " are now married!");
                    GetLastNameMale();
                }
                else {
                    Console.WriteLine(nameS + " rejected " + maleBots[num2] + "'s proposal");
                    Console.WriteLine("They both remain single");
                }
            }
        }


        public static void GetLastNameMale() { //Gets the last name of the male
            
            path = textFile + maleNameS + "_attributes.cbinfo";

            using(StreamReader s = new StreamReader(path)) {
                string[] fname = s.ReadLine().Split(' ');
                familyName = fname[1];
            }
            LoadMaleData();
        }

        public static void TestColor() {


            Console.WriteLine("What color do ya want?");
            string color = Console.ReadLine();
            

           try {
                Console.BackgroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
                Console.ForegroundColor = ConsoleColor.White;

            
                Console.WriteLine("Poop");

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;

                ConsoleDeploy();
           }
           catch(Exception) {
               Console.WriteLine("That color don't exist pal...");
               ConsoleDeploy();
           }
        }

        public static void LoadMaleData() {
            string[] lines = File.ReadAllLines(path);

            name = lines[0];
            botNameL = lines[0];
            eyeColorL = lines[1];
            hairColorL = lines[2];
            heightL = lines[3];
            genderL = lines[4];
            ageL = Int16.Parse(lines[5]);
            birthdayL = lines[6];
            maritalStatisL = lines[7];
            occupationL = lines [8];
            bankBalanceL = Int32.Parse(lines[9]);
            salery = Int32.Parse(lines[10]);

            SetLastNameMale();
        }


        static object syncRoot = "";
        public static void SetLastNameMale() {
            path = textFile + maleNameS + "_attributes.cbinfo";
            syncRoot = path;
            
            lock(syncRoot) {
                using(StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine(maleNameS);
                    sw.WriteLine(eyeColorL);
                    sw.WriteLine(hairColorL);
                    sw.WriteLine(heightL);
                    sw.WriteLine(genderL);
                    sw.WriteLine(ageL);
                    sw.WriteLine(birthdayL);
                    sw.WriteLine(femaleNameS);
                    sw.WriteLine(occupationL);
                    sw.WriteLine(bankBalanceL);
                    sw.WriteLine(salery);
                }
                GetLastNameFemale();
            }
        }

        public static void GetLastNameFemale() { //Sets the last name of the female to the male's
            path = textFile + femaleNameS + "_attributes.cbinfo";

            using(StreamReader s = new StreamReader(path)) {
                string[] fname = s.ReadLine().Split(' ');
                femaleNameS = fname[0] + " " +familyName;
            }
            LoadFemaleData();
        }

        public static void LoadFemaleData() {
            string[] lines = File.ReadAllLines(path);
            
            name = lines[0];
            botNameL = lines[0];
            eyeColorL = lines[1];
            hairColorL = lines[2];
            heightL = lines[3];
            genderL = lines[4];
            ageL = Int16.Parse(lines[5]);
            birthdayL = lines[6];
            maritalStatisL = lines[7];
            occupationL = lines [8];
            bankBalanceL = Int32.Parse(lines[9]);
            salery = Int32.Parse(lines[10]);
            File.Delete(path);

            path = textFile + femaleNameS + "_attributes.cbinfo";
            SetFemaleData();
        }

        public static void SetFemaleData() {
            using(StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(femaleNameS);
                sw.WriteLine(eyeColorL);
                sw.WriteLine(hairColorL);
                sw.WriteLine(heightL);
                sw.WriteLine(genderL);
                sw.WriteLine(ageL);
                sw.WriteLine(birthdayL);
                sw.WriteLine(maleNameS);
                sw.WriteLine(occupationL);
                sw.WriteLine(bankBalanceL);
                sw.WriteLine(salery);
            }

            CreateMarriage();
        }
        public static string marriageDirStatic = @"C:\Program Files\project-cobalt\cobalt_Run\data\bots\marriages\";
        public static void CreateMarriage() {
            string newDir = marriageDirStatic + maleNameS + " & " + femaleNameS;
            path = newDir + @"\" + familyName + "_marriage.cbmar";

            if(!File.Exists(path)) {
                syncRoot = path;
                lock(syncRoot) {
                    Directory.CreateDirectory(newDir);
                    using(StreamWriter sw = File.CreateText(path)) {
                        sw.WriteLine(familyName);
                        sw.WriteLine(maleNameS);
                        sw.WriteLine(femaleNameS);
                    }
                }  
            }
            path = textFile;
        }

        public static void EvenNum() {
            //Space
            //Space
            //Space
            //Space
            //Space
            //Space
            //Space
            //Space
            //Space
        }

        public static void ReadLifeData() { 

            Console.WriteLine("What is the name of the Deceased AI you wish to view?");
            string life = Console.ReadLine();

            path = textFile + @"deceased\" + life + "_life.cblife";

            if(!File.Exists(path)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(">Cobalt: " + "Error - We can't seem to find a deceased AI with that name; Create one by using the command !create.");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                ConsoleDeploy();
            }
            else {
                //string[] lines = File.ReadAllLines(path);
                foreach(string lines in File.ReadAllLines(path)){
                    Console.WriteLine(lines);
                }
                ConsoleDeploy();
            }
        }

        public static void LoadDataFromHierarchy() {
            if(File.Exists(path)) {
                string[] lines = File.ReadAllLines(path);

                name = lines[0];
                botNameL = lines[0];
                eyeColorL = lines[1];
                hairColorL = lines[2];
                heightL = lines[3];
                genderL = lines[4];
                ageL = Int16.Parse(lines[5]);
                birthdayL = lines[6];
                maritalStatisL = lines[7];
                occupationL = lines[8];
                bankBalanceL = Int32.Parse(lines[9]);
                salery = Int32.Parse(lines[10]);
            }
            path = textFile;
        }

        public static void LoadData() {
            if(!File.Exists(fileName)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">Cobalt: " + "Error - No AI with that name is currently living on this machine; create one by using the command !create.");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                ConsoleDeploy();
            }
            else {
                string[] lines = File.ReadAllLines(fileName);

                name = lines[0];
                botNameL = lines[0];
                eyeColorL = lines[1];
                hairColorL = lines[2];
                heightL = lines[3];
                genderL = lines[4];
                ageL = Int16.Parse(lines[5]);
                birthdayL = lines[6];
                maritalStatisL = lines[7];
                occupationL = lines [8];
                bankBalanceL = Int32.Parse(lines[9]);
                salery = Int32.Parse(lines[10]);
            }
        }

        //Help
        public static void CommandHelp() {

            if(!File.Exists(helpDir)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">Cobalt: " + "Error - Could not locate: ' " + helpDir + " '. Compiler Issue 0967: Check installation! File returned null");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.Gray;
                ConsoleDeploy();
            }
            else {
                foreach(string lines in File.ReadAllLines(helpDir)){
                    Console.WriteLine(lines);
                }
                ConsoleDeploy();
            }
        }
    }

    class Bot {
        
        //Bot Info
        public string firstName;
        public string lastName;
        public string eyeColor;
        public string hairColor;
        public string height;
        public string gender;
        public string birthday;
        public int age;
        
    }
}




