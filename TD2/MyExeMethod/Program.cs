using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExeMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                string str = "<html><body> Args received<br>" + args[0] + "<br>" + args[1] + "</html></body>";
                Console.WriteLine(str);
            }
        }
    }
}
