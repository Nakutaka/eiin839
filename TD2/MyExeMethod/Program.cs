using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                string str = "<html>"+
                    "<style>"+
                    "body{color:white; background-color:black; text-align:center;}"+
                    ".title{font-size:30px; font-family:sans-serif;}"+
                    ".result{padding:2em; font-size:30px; font-family:sans-serif;}"+
                    "</style>"+
                    "<body>"+
                    "<div class=\"title\">The Multiplication page</div>"+
                    "<div class=\"result\">"+args[0]+" x "+args[1]+" = "+double.Parse(args[0])*double.Parse(args[1])+"</div>"+
                    "</body>"+
                    "</html>";
                Console.WriteLine(str);
            }
        }
    }
}
