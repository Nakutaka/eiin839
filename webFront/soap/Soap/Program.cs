using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soap
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calculator.CalculatorSoap c = new Calculator.CalculatorSoapClient();
            //Console.WriteLine(c.Multiply(5, 4));

            MathsLibrary.IMathsOperations m = new MathsLibrary.MathsOperationsClient();
            Console.WriteLine("Addition 10+10: "+ m.Add(10, 10));
            Console.WriteLine("Division 30/6 : "+ m.Divide(30.0, 5.0));
            Console.WriteLine("Multiply 5*5  : "+ m.Multiply(5, 5));
            Console.WriteLine("Substract 7-2 : "+ m.Substract(7, 2));
            Console.ReadLine();
        }
    }
}
