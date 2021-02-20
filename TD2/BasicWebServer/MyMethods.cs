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
        /*
         * This method returns a Web page with your personnalized text and color
         */
        public string textColoring(string color, string text)
        {
            Console.WriteLine(text + ", color: " + color);
            string page = "<html>"+
                "<style>"+
                "legend{background-color:black; color:white; padding:0.5em; font-size:20px; font-family:sans-serif; font-weight:bold;}"+
                "fieldset{border: 1px solid black;}"+
                "div{color:"+color+"; font-family:sans-serif; font-size:20px;}"+
                "</style>"+
                "<body>"+
                "<fieldset>"+
                "<legend> Your colored text will be displayed below</legend>"+
                "<div>"+text+"</div>"+
                "</fieldset>"+
                "</body>"+
                "</html>";
            return page;
        }

        public string multMethod(string param1, string param2)
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
