using System;
using System.IO;
using System.Diagnostics;


namespace cobalt_Terminal
{
    public class Program
    {
        //D:\Projects\Cobalt\builds\project-cobalt v0.0.5\cobalt_Run\bin
        public static string projectPatha = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string projectPath = Directory.GetParent(projectPatha).Parent.FullName; // OUTPUT: F:\Projects\Cobalt

        public static void Main(string[] args) {
            Boot();
            //Console.WriteLine(projectPath);
            //Console.ReadLine();
        }

        public static void Boot() {

            Console.WriteLine("Choose A Version to Run  |  " + projectPath);
            
            int num = 0;
            DirectoryInfo di = new DirectoryInfo(projectPath + @"\builds");
            DirectoryInfo[] diArr = di.GetDirectories();

            foreach(var filePath in diArr) {
                Console.WriteLine(num + ". " + filePath.Name);
                num++;
            }

            if(int.TryParse(Console.ReadLine(), out int value)) {
                projectPath = projectPath + @"\builds\" + diArr[value].Name;
                RunCobalt();
            }
            else {
                Boot();
            }
        }

        public static void RunCobalt()
        {
            Console.Clear();
            try {
                Process.Start(projectPath + @"\cobalt_Run\bin\Debug\net5.0\cobalt_Run.exe");
            }
            catch(Exception exception) {
                Console.WriteLine("An error occurred! '" + exception.Message + "'.");
            }
            Console.WriteLine(projectPath + @"\cobalt_Run\bin\Debug\net5.0\cobalt_Run.exe");
            Console.ReadKey();
        }
    }
}
