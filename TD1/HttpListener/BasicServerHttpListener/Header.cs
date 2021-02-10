using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasicServerHTTPlistener
{
    class Header
    {
        System.Collections.Specialized.NameValueCollection receivedHeaders;
        public Header(System.Collections.Specialized.NameValueCollection headers) {
            receivedHeaders = headers;
        }

        public void printAllHeaders()
        {
            Console.WriteLine("\n\n****** All Received Headers ******\n"+receivedHeaders.ToString());
        }

        public void printSingleHeader(System.Net.HttpRequestHeader header)
        {
            Console.WriteLine("\n\n****** Simple Header: "+header.ToString()+" ******\n"+receivedHeaders.Get(header.ToString()));
        }
    }
}
