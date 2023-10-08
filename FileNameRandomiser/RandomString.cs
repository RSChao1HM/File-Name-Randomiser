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
    public class RandomStrings
    {
        //lmao


        public string[] GetRandomStrings(int strNumber)
        {
            string[] myStrings = File.ReadAllLines(Program.path + "\\nameList.dat");
            List<string> randomStrings = new List<string>();
            Random random = new Random();
            
            while (randomStrings.Count < strNumber)
            {
                int index = random.Next(myStrings.Length);
                string randomString = myStrings[index];

                if (!randomStrings.Contains(randomString))
                {
                    randomStrings.Add(randomString);
                    Console.WriteLine(randomString);
                }
            }

            string[] result = randomStrings.ToArray();
            return result;
        }
        
    }
}
