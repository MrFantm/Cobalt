using System;
using System.IO;


namespace Cobalt.Settings {
    class MainSettings {
        public static string rootDir = Files.FileManager.projectPath;

        //Directory
        public static string dir = rootDir + @"\cobalt_Run\data\other\settings.cobalt";

        //Load
        public static string color1L;
        public static string color2L;
        public static int dayIntervalL;

        //Save
        public static string color1S;
        public static string color2S;
        public static int dayIntervalS;

        public static void LoadSettings() {
            if(File.Exists(dir)) {
                string[] lines = File.ReadAllLines(dir);
                color1L = lines[0];
                color2L = lines[1];
                dayIntervalL = Int16.Parse(lines[2]);;
            }
        }

        public static void SaveSettings() {

        }

    }
}