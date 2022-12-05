using System;
using System.Threading;
using System.IO;


namespace Cobalt.Main {
    class Program {
        
        //Variables



        //strings
        public static string name;
        public static string botNameL;
        public static string eyeColorL;
        public static string hairColorL;
        public static string heightL;
        public static string genderL;
        public static string causeOfDeathL;
        public static string birthdayL;
        public static string maritalStatusL;
        public static string familyName;
        public static string occupationL;
        public static string addressL;
        public static string addressS;



        //ints
        public static int ageL;
        public static int bankBalanceL;
        public static int salary;

        //directories

        public static string rootDir;


        public static string mainDir = rootDir + @"\cobalt_Run";                            
        public static string textFile = rootDir + @"\cobalt_Run\data\bots\";                
        public static string dir = rootDir + @"\cobalt_Run\data\bots";                       

        public static string helpDir = rootDir + @"\cobalt_Run\data\other\help.cobalt";     
        public static string logDir = rootDir + @"\cobalt_Run\data\other\logs\";                    
        public static string fileName;
        public static string path;



        //bools
        public static bool continueBot = false;
        public static bool hasLoaded = false;
        public static bool isManual = false;
        public static bool isChild = false;
        public static bool doLogs = true;


        public static ConsoleColor textColor = ConsoleColor.DarkBlue;
        public static ConsoleColor consoleColor = ConsoleColor.Gray;

        public static string version = "0.0.5 Stable";
        static void Main(string[] args) {
            Files.FileManager.Boot(version, false);

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = consoleColor;
            causeOfDeathL = " ";

            Console.WriteLine("Cobalt v" + version + " | Created by: MrFantom, 2022");
            Console.WriteLine("Welcome To Cobalt");
            Console.WriteLine("Write !help for a list of commands");
            LoadAiHierarchyOnStart();
            LoadEligibleGenders();
        }

        public static void CheckCommand(string command) {

            //Checks commands... Yes it's messy... it works so I don't care.... kinda...

            if(command == "!create" || command == "!cr") {  //Creates a new AI with random Attributes
                isManual = true;
                CreateNew();
            }
            else if(command.Contains("!e init")) {
                if(command.Contains("pool")) 
                {
                    Economy.Generic.Init("pool");
                }
                else if(command.Contains("collect")) 
                {
                    Economy.Generic.Init("collect");
                }
                else 
                {
                    Economy.Generic.Init("none");
                }
            }
            else if(command == "!e taxRate") {
                int value = Int32.Parse(command.Replace("!e taxRate ", ""));
                Economy.Taxes.SetTaxRate(value);
            }
            else if(command == "!e collect") {
                Economy.Taxes.Collect(true);
            }
            else if(command == "!e inf" || command == "!e info") {
                Economy.Generic.Init("pool");
            }
            else if(command.Contains("!e add")) {
                string value = command.Replace("!e add ", "");

                if(value.Contains("eco")) 
                {
                    command.Replace("eco ", "");
                    int amount = Int32.Parse(command);
                    Economy.Generic.Add("eco", amount);
                }
                else 
                {
                    string[] s = value.Split(" ");
                    string name = s[0] + " " + s[1];

                    string amount = value.Replace(name + " ", "");

                    Economy.Generic.Add(name, Int32.Parse(amount));
                }
            }
            else if(command == "!house") {
                Houses.House.GenerateAddress();
            }
            else if(command == "!debug") {
                Files.FileManager.Boot(version, true);
                
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;          

                Console.WriteLine("Cobalt v" + version + " DEBUG | Created by: MrFantom, 2022");
                Console.WriteLine("Welcome To Cobalt");
                Console.WriteLine("Restart Program to Exit Debug Mode!");
                LoadAiHierarchyOnStart();
                LoadEligibleGenders();
            }
            else if(command == "!patch") { //Displays the patch notes
                foreach(string line in File.ReadAllLines(rootDir + @"\PATCH.txt")) 
                {
                    Console.WriteLine(line);
                }
                ConsoleDeploy();
            }
            else if(command == "!help") { //Lists all the Commands and their actions
                CommandHelp();
            }
            else if(command == "!age up" || command == "!a+") { //Increase every AI's Age by 1
                Bots.BotsRun.GetAllBots();
            }
            else if(command == "!review") { //Review a Deceased AI's life file
                //ReadLifeData();
                Console.WriteLine("What deceased AI would you like to view? Use the number from the hierarchy; which you can view with '!list x'");
                ViewDeadAI();
            }
            else if(command == "!view") {   //View a Specified AI using an int from a hierarchy
                Console.WriteLine("What active AI would you like to view? Use the number from the hierarchy; which you can view with '!list'");
                ViewAI();
            }
            else if(command == "!list") {   //Lists all the AI still living on your Machine; Creates a Hierarchy
                ListActiveBots();
            }
            else if(command == "!list deceased" || command == "!list x") {  //Lists all the Deceased AI
                ListDeceased();
            }
            else if(command == "!settings") {
                Settings();
            }
            else if(command == "!clear" || command == "!c") {  //Clears the console  NOT SUGGESTED!!!!!
                
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Are you sure you want to CLEAR the console? Clearing the console is only suggested as a means of reset! \n Y/N" );
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
                
                ConsoleKey key;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
        
                if(key == ConsoleKey.Y) {
                    Console.Clear();
                }
                else if(key == ConsoleKey.N){
                    Console.WriteLine("Canceled Clear!");
                }
                else {
                    ConsoleDeploy();
                }

                ConsoleDeploy();
            }
            else if(command == "!play" || command == "!p") {   //Plays The simulation
                Event();
            }
            else if(command == "!kill" || command == "!k") {   //Kills a Specified AI
                KillBot();
            }   
            else if(command == "!exit" || command == "!quit" || command == "!x") {  //To exit the program
                
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You're about to close the program! Are you sure you want to continue? \n Y/N" );
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
                
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
            else if(command == "!test") 
            {
                Log();
            }
            else { //If no valid command is sent
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(">Cobalt: " + "Error - The Command: " +  "\"" + command + "\"" + " was not found! Please check for spelling errors, or the Command does not exist.");
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
                
                ConsoleDeploy();
            }
        }


        public static string[] consoleLines = new string[999999999];
        public static int auditNum = 0;
        public static void AddToAudit(string message) 
        {
            consoleLines[auditNum] = message;
        }


        public static void Log() 
        {
            if(doLogs) 
            {
                try {
                    DateTime time = DateTime.Now;
                    using(StreamWriter sw = File.CreateText(logDir + time.Ticks + ".txt")) {
                        foreach(string line in consoleLines) 
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
                catch {
                    //This is just in case, hopefully the Error reporter won't run into an error ;)
                }
            }
        }

        public static void ConsoleDeploy() {
            rootDir = Files.FileManager.projectPath;
            mainDir = rootDir + @"\cobalt_Run";                            
            textFile = rootDir + @"\cobalt_Run\data\bots\";                
            dir = rootDir + @"\cobalt_Run\data\bots";                       
            helpDir = rootDir + @"\cobalt_Run\data\other\help.cobalt";     
            logDir = rootDir + @"\cobalt_Run\data\other\logs\";

            isManual = false;
            Console.Write(">Cobalt: ");
            CommandReady();
        }

        public static void CommandReady() {
            CheckCommand(Console.ReadLine());
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
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = consoleColor;

            ConsoleDeploy();
        }

        public static string currentBot;
        public static int botAmount = 0;
        public static string lastBot;

        private static void TimerCallback(Object o) 
        {
            Random random = new Random();
            LoadAiHierarchy();
            int length = botAmount;
            int num = random.Next(0,length);
            int num2 = random.Next(1,100);
            currentBot = activeBots[num];
            path = textFile + currentBot + "_attributes.cbinfo";
            LoadDataFromHierarchy();
            LoadEligibleGenders();

            Bots.BotsRun.botsName = currentBot;
            Bots.BotsRun.Run();

        }

        //Settings
        public static void Settings() {

        }

        //Bot Stuff
        public static void Die() {
            string botToDestroy = Bots.BotsRun.dname;
            path = textFile + botToDestroy + "_attributes.cbinfo";
            syncRoot = path;

        lock(syncRoot) {
            File.Delete(path);
        }    


            Random random = new Random();
            int num = random.Next(0,6);
            string causeOfDeath = "";

            string cause = Bots.BotsRun.botsCauseOfDeath;
            causeOfDeath = cause;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(botToDestroy + " sadly perished from " + causeOfDeath + "! You can review " + botToDestroy + "'s life with !review.");
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = consoleColor;

            path = textFile + @"deceased\" + botToDestroy + "_life.cblife";
            DateTime now = DateTime.Now;
            string dateOfDeath = now.ToString();

            if(!File.Exists(path)) {
                using(StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("Full Name: " + botNameL);
                    sw.WriteLine("Eye Color: " + eyeColorL);
                    sw.WriteLine("Hair Color: " + hairColorL);
                    sw.WriteLine("Height: " + heightL + "ft");
                    sw.WriteLine("Gender: " + genderL);
                    sw.WriteLine("Age of death: " + ageL);
                    sw.WriteLine("Birthday: " + birthdayL);
                    sw.WriteLine("Cause of death: " + causeOfDeath);
                    sw.WriteLine("Time of Death: " + dateOfDeath);
                    sw.WriteLine("Marital Status: " + maritalStatusL);
                    sw.WriteLine("Occupation: " + occupationL);
                    sw.WriteLine("Bank Balance: " + bankBalanceL);
                    sw.WriteLine("Address: " + addressL);
                } 
                path = textFile;
                continueBot = false;
                
            }
            path = textFile;
        }

        public static string childName;
        public static void CreateNew() {
            rootDir = Files.FileManager.projectPath;
            textFile = rootDir + @"\cobalt_Run\data\bots\";

            Random random = new Random();
            Bots.Bot bot01 = new Bots.Bot();
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
            string[] hairColor = {"black", "dark brown", "medium brown", "light brown", "chestnut brown", "auburn", "red", "orange red", "copper", "titan", "strawberry blonde", "light blonde", "golden blonde"};

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
            maritalStatusL = "single";
            occupationL = "unemployed";
            bankBalanceL = 0;
            salary = 0;

            if(isChild == true) {
                Console.WriteLine("The " + familyName + "s had a baby! "+ pronoun[1] + " name is " + name + "!");
                childName = name;
            }
            Console.WriteLine("Your New AI's Name is : " + name);
            Console.WriteLine(name + ", Has " + bot01.eyeColor + " eyes, and " + bot01.hairColor + " hair! " + pronoun[0] + " is " + bot01.height + " feet tall, and " + bot01.age + " years old!");


            
            if(isChild == true) {
                Bots.BotsRun.UpdateMarriage(); 
            }
            else {
                Houses.House.GenerateAddress();
            }
            WriteData();
            LoadAiHierarchyOnStart();
            
            if(isManual == true) {
                ConsoleDeploy();
            } 

        }

        public static void AgeUp() {
            Bots.BotsRun.GetAllBots();
        }


        public static void KillBot() {
            

            Console.WriteLine("What is the name of the AI you wish to destroy?");
            string botToDestroy = Console.ReadLine();

            path = textFile + botToDestroy + "_attributes.cbinfo";
            fileName = path;
            LoadData();

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You are about to kill: " + botToDestroy + "! Are you sure you want to continue? \n Y/N");
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = consoleColor;
                
            ConsoleKey key;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if(key == ConsoleKey.Y) {
                if(!File.Exists(path)) {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(name + " does not exist! You can create an AI with !create.");
                    Console.ForegroundColor = textColor;
                    Console.BackgroundColor = consoleColor;

                }
                else {
                    File.Delete(path);

                    Random random = new Random();
                    int num = random.Next(0,30);
                    string causeOfDeath = "";

                    string cause = CausesOfDeath.CausesOfDeath.accidents[num];

                    causeOfDeath = cause;

                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(name + " sadly perished from " + causeOfDeath + "! You can review " + name + "'s life with !review.");
                    Console.ForegroundColor = textColor;
                    Console.BackgroundColor = consoleColor;

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
                            sw.WriteLine("Marital Status: " + maritalStatusL);
                            sw.WriteLine("Occupation: " + occupationL);
                            sw.WriteLine("Bank Balance: " + bankBalanceL);
                            sw.WriteLine("Address: " + addressL);
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
        public static string[] deadBots = new string[500];
        public static int bnum = 0;

        public static void ListDeceased() {

            string dirS = dir;
            dir = dir + @"\deceased";
            botAmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(dir)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line.Replace("Full Name: ", "");
                    deadBots[num] = name;
                    Console.WriteLine(num + ". " + name);
                    botAmount++;
                    num++;
                }
            }
            ConsoleDeploy();
        }

        public static int femaleBotsNum = 0;
        public static string[] femaleBots = new string[500];

        public static int maleBotsNum = 0;
        public static string[] maleBots = new string[500];

        
        public static void LoadEligibleGenders() {
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
            botAmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(textFile)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    Console.WriteLine(num + ". " + name);
                    botAmount++;
                    num++;
                }
            }
            ConsoleDeploy();
        }

        public static void LoadAiHierarchy() {
            botAmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;

            foreach (var filePath in Directory.EnumerateFiles(dir)) {
                using (var reader = new StreamReader(filePath)) {
                    var line = reader.ReadLine();
                    name = line;
                    activeBots[num] = name;
                    botAmount++;
                    num++;
                }
            }
        }

        public static void LoadAiHierarchyOnStart() {
            rootDir = Files.FileManager.projectPath;
            textFile = rootDir + @"\cobalt_Run\data\bots\";

            botAmount = 0;
            string name = " ";
            int num = 0;
            hasLoaded = true;
            try {
                foreach (var filePath in Directory.EnumerateFiles(textFile)) {
                    using (var reader = new StreamReader(filePath)) {
                        var line = reader.ReadLine();
                        name = line;
                        activeBots[num] = name;
                        botAmount++;
                        num++;
                    }
                }
            }
            catch {
                ConsoleDeploy();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            int snum = num - 1;
            Console.WriteLine("AI Hierarchy Successfully Loaded " + num  + " AI data profiles!");
            Console.ForegroundColor = textColor;
            ConsoleDeploy();
        }

        public static int aiNum3;
        public static void ViewDeadAI() {
            string aiNumS = Console.ReadLine();
            int aiNum = Int16.Parse(aiNumS);
            aiNum3 = aiNum;

            Console.WriteLine("You are about to view " + deadBots[aiNum] + ". Would you like to continue? \n Y/N");
            path = textFile + @"deceased\" + deadBots[aiNum] + "_life.cblife";
            
            ConsoleKey key;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if(key == ConsoleKey.Y) {
                if(File.Exists(path)) {
                    PrintDeadBotData();
                }
                else {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("No AI with the given number is currently living on this machine; create one by using the command '!create'");
                    Console.ForegroundColor = textColor;
                    Console.BackgroundColor = consoleColor;
                    ConsoleDeploy();
                }
            }
            else if(key == ConsoleKey.N){
                Console.WriteLine("Canceled");
            }
            path = textFile;
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
                    Console.ForegroundColor = textColor;
                    Console.BackgroundColor = consoleColor;
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
                    sw.WriteLine(maritalStatusL);
                    sw.WriteLine(occupationL);
                    sw.WriteLine(bankBalanceL);
                    sw.WriteLine(salary);
                    sw.WriteLine(addressL);
                } 
            }
            path = textFile;
            ConsoleDeploy();
        }


        public static void PrintDeadBotData() {
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine(lines[0]);
            Console.WriteLine(lines[1]);
            Console.WriteLine(lines[2]);
            Console.WriteLine(lines[3]);
            Console.WriteLine(lines[4]);
            Console.WriteLine(lines[5]);
            Console.WriteLine(lines[6]);
            Console.WriteLine(lines[7]);
            Console.WriteLine(lines[8]);
            Console.WriteLine(lines[9]);
            Console.WriteLine(lines[10]);
            Console.WriteLine(lines[11]);

            ConsoleDeploy();
        }


        public static void PrintBotData() {
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("Name: " + lines[0]);
            Console.WriteLine("Eye Color: " + lines[1]);
            Console.WriteLine("Hair Color: " + lines[2]);
            Console.WriteLine("Height: " + lines[3] + "ft");
            Console.WriteLine("Gender: " + lines[4]);
            Console.WriteLine("Age: " + lines[5]);
            Console.WriteLine("Birthday: " + lines[6]);
            
            //Checks if the bot is married and displays their spouse's name
            if(lines[7] != "single") {
                Console.WriteLine("Marital Status: Married to " + lines[7]);
            }
            else {
                Console.WriteLine("Marital Status: " + lines[7]);
            }

            Console.WriteLine("Occupation: " + lines[8]);
            Console.WriteLine("Bank Balance: " + lines[9]);
            Console.WriteLine("Current Salary: " + lines[10]);
            Console.WriteLine("Address: " + lines[11]);

            ConsoleDeploy();
        }





        //Marriages

        public static string femaleNameS;
        public static string maleNameS;

        public static int lengthM;
        public static int lengthF;

        public static void TryForMarriage() {

            LoadEligibleGenders();

            
            Random random = new Random();
            int num = random.Next(0,2);
            int num2 = random.Next(0,lengthM);
            int num3 = random.Next(0,lengthF);
            string answer;
            string nameS = currentBot; //Bots.BotsRun.botsName
            
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
            maritalStatusL = lines[7];
            occupationL = lines [8];
            bankBalanceL = Int32.Parse(lines[9]);
            salary = Int32.Parse(lines[10]);
            addressL = lines[11];

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
                    sw.WriteLine(salary);
                    sw.WriteLine(addressL);
                }
                GetLastNameFemale();
            }
        }

        public static void GetLastNameFemale() { //Sets the last name of the female to the male's
            path = textFile + femaleNameS + "_attributes.cbinfo";
            addressS = addressL;
            using(StreamReader s = new StreamReader(path)) {
                string[] fname = s.ReadLine().Split(' ');
                femaleNameS = fname[0] + " " + familyName;
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
            maritalStatusL = lines[7];
            occupationL = lines [8];
            bankBalanceL = Int32.Parse(lines[9]);
            salary = Int32.Parse(lines[10]);
            addressL = lines[11];
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
                sw.WriteLine(salary);
                sw.WriteLine(addressS);
            }

            CreateMarriage();
        }
        public static string marriageDirStatic = dir + @"\marriages\";
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
                        sw.WriteLine(addressL);
                    }
                }  
            }
            path = textFile;
        }


        public static void ReadLifeData() { 

            Console.WriteLine("What is the name of the Deceased AI you wish to view?");
            string life = Console.ReadLine();

            path = textFile + @"deceased\" + life + "_life.cblife";

            if(!File.Exists(path)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(">Cobalt: " + "Error - We can't seem to find a deceased AI with that name; Create one by using the command !create.");
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
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
                maritalStatusL = lines[7];
                occupationL = lines[8];
                bankBalanceL = Int32.Parse(lines[9]);
                salary = Int32.Parse(lines[10]);
                addressL = lines[11];
            }
            path = textFile;
        }

        public static void LoadData() {
            if(!File.Exists(fileName)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">Cobalt: " + "Error - No AI with that name is currently living on this machine; create one by using the command !create.");
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
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
                maritalStatusL = lines[7];
                occupationL = lines [8];
                bankBalanceL = Int32.Parse(lines[9]);
                salary = Int32.Parse(lines[10]);
                addressL = lines[11];
            }
        }

        //Help
        public static void CommandHelp() {

            if(!File.Exists(helpDir)) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(">Cobalt: " + "Error - Could not locate: ' " + helpDir + " '. Compiler Issue 0967: Check installation! File returned null");
                Console.ForegroundColor = textColor;
                Console.BackgroundColor = consoleColor;
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
}




