using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace BasicServerHTTPlistener
{
    class MyMethods
    {   
        public string myMethod(string param1, string param2)
        {
            return "<html><body>"+param1+"<br>"+param2+"</body></html>";
        }

        public string myExeMethod(string param1, string param2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"D:\Docu\Polytech_Nice\2020-2021\S8\SOC\eiin839\TD2\MyExeMethod\bin\Debug\MyExeMethod.exe";
            start.Arguments = param1+" "+param2;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
