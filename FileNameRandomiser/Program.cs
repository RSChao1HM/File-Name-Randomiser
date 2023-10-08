using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FileNameRandomiser
{
    internal class Program
    {
        public readonly static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static FileStream[] files;
        static int repeatings;
        static string[] texts = new string[repeatings + 1];
        //TODO: Save each file in a files memory space
        //Read settings from .txt file *Donenzo*
        static void Main(string[] args)
        {
            FileStream settings = File.Open(path + "\\config.txt", FileMode.OpenOrCreate);
            settings.Close();


            try
            {

                string[] configs = File.ReadAllLines(path + "/config.txt");
                repeatings = int.Parse(configs[0]);
                texts = new string[repeatings + 1];
                Random random = new Random();
                
                RandomStrings randomStrings = new RandomStrings();
                texts = randomStrings.GetRandomStrings(repeatings);
                string LPath = configs[1];


                RenameFilesFr(LPath);

            }
            catch(Exception ex) {
                Debug.Log(ex);
            }
        }
        ///<Summary>
        ///rename this bitch
        ///</Summary>
        public static void RenameFilesFr(string path)
        {
            string folderPath = path;
            string[] filePaths = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            files = new FileStream[filePaths.Length];
            string oldFilePath;
            string newFilePath;
            for (int i = 0; i < filePaths.Length; i++)
            {
                try
                {
                    files[i] = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read);
                    
                    oldFilePath = filePaths[i];
                    newFilePath = path + "/renamed/" + texts[i] + Path.GetExtension(filePaths[i]);

                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }

                    File.Copy(oldFilePath, newFilePath);
                }
                catch (Exception e) { Debug.Log(e); if (e.Message != "Index was outside the bounds of the array.") Console.WriteLine(e.Message);  }
            }
            //Console.WriteLine("Press enter to exit the program");
            //Console.ReadLine();

        }
    }
    ///<Summary>
    ///log shit into files cuz why not lol
    ///</Summary>
    public class Debug
    {
        public static void Log(Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            int lineNumber = st.GetFrame(st.FrameCount - 1).GetFileLineNumber();
            File.AppendAllText(Program.path + "/error.log", "\n" + ex.Message + " line: " + lineNumber);
        }
        public static void LogMessage(string message)
        {
            File.AppendAllText(Program.path + "/error.log", message + "\n");
        }
    }
}
