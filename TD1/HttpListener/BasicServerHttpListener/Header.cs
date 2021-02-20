using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasicServerHTTPlistener
{
    class Header
    {
        NameValueCollection receivedHeaders;
        public Header(NameValueCollection headers) {
            receivedHeaders = new NameValueCollection();
            foreach (string key in headers)
            {
                if (key.Contains('-'))
                {
                    string[] splittedKey = key.Split('-');
                    string a = splittedKey[0];
                    string b = char.ToUpper(splittedKey[1][0]) + splittedKey[1].Substring(1);
                    receivedHeaders.Add(a + b, headers.Get(key));
                }
                else
                {
                    receivedHeaders.Add(key, headers.Get(key));
                }
            }
        }

        /*
         * Prints all the header available in the HttpRequestHeader enum
         * and specify if the header has been received or not.
         */
        public void printAllHeaders()
        {
            Console.WriteLine("\n\n****** Headers ******");
            foreach (HttpRequestHeader headerEnum in Enum.GetValues(typeof(HttpRequestHeader)))
            {
                Console.Write(headerEnum.ToString() + " : ");
                string tempHeader = receivedHeaders.Get(headerEnum.ToString());
                if ( tempHeader != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("received");
                    Console.ResetColor();
                    Console.WriteLine(", value -> " + tempHeader);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("not received.");
                    Console.ResetColor();
                }
            }
        }

        /* 
         * Print the wanted header and its value
         */
        public void printSingleHeader(HttpRequestHeader header)
        {
            Console.WriteLine("\n\n****** Simple Header: "+header.ToString()+" ******\n"+receivedHeaders.Get(header.ToString()));
        }
    }
}
