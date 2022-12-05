using System;
using System.IO;
using System.Windows;

namespace Cobalt.Files {
    class FileManager {
        public static string projectPathRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string projectPath;

        public static void Boot(string version, bool isDebug) {

            if(isDebug) {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You're about to enter DEBUG mode. This is only meant for developers, it could crash your program. Do you wish to continue? \nY/N" );
                
                ConsoleKey key;
                ConsoleKeyInfo keyInfo = Console.ReadKey(false);
                key = keyInfo.Key;
        
                if(key == ConsoleKey.Y) {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Main.Program.textColor = ConsoleColor.Green;
                    Main.Program.consoleColor = ConsoleColor.Black;

                    Console.Title = "Cobalt v" + version + " DEBUG";
                    
                    projectPath = projectPathRoot + @"\project-cobalt v" + version;
                    Main.Program.rootDir = projectPath;
                    Console.WriteLine(projectPath);    
                }
                else if(key == ConsoleKey.N){
                    Main.Program.ConsoleDeploy();
                }
                else {
                    Main.Program.ConsoleDeploy();
                }
            }
            else {
                Console.Title = "Cobalt v" + version;
                projectPath = projectPathRoot.Replace(@"\bin", "").Replace(@"\cobalt_Run", "");
                Console.WriteLine(projectPath);    
                Main.Program.rootDir = projectPath;
            }
        }
    }
}