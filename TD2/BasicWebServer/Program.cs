using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Reflection;

namespace BasicServerHTTPlistener
{
    internal class Program
    {

        static string executeMethod(string stringMethod, string param1, string param2)
        {
            Type type = typeof(MyMethods);
            string result;
            MethodInfo method = type.GetMethod(stringMethod);
            MyMethods c = new MyMethods();
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                // !!!!!!!!! Exception pause the execution if started in debug mode
                // Click continue or start in non-debugging mode
                result = (string) method.Invoke(c, new object[] { param1, param2 });
                Console.WriteLine("Executed method " + stringMethod);
                Console.ResetColor();
            }
            catch (NullReferenceException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.WriteLine("No method named \'" + stringMethod + "\' exists.");
                Console.ResetColor();
                result = "<html><body style=\"color:#f00; font-weight:bold; text-align:center; font-family:sans-serif; font-size:50px; padding-top:4em;\">An error ocurred : method "+stringMethod+" not found</body></html>";
            }
            return result;

        }

        static void writeInfos(HttpListenerRequest request)
        {
            // get url 
            Console.WriteLine($"Received request for {request.Url}");

            //get url protocol
            Console.WriteLine(request.Url.Scheme);
            //get user in url
            Console.WriteLine(request.Url.UserInfo);
            //get host in url
            Console.WriteLine(request.Url.Host);
            //get port in url
            Console.WriteLine(request.Url.Port);
            //get path in url 
            Console.WriteLine(request.Url.LocalPath);

            // parse path in url
            foreach (string str in request.Url.Segments)
            {
                Console.WriteLine(str);
            }
        }
        private static void Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
 
            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // Uncomment this line to write request Url infos in console
                //writeInfos(request);

                //get params un url. After ? and between &

                Console.WriteLine("Url query: "+request.Url.Query);

                //parse params in url
                string param1 = HttpUtility.ParseQueryString(request.Url.Query).Get("param1");
                string param2 = HttpUtility.ParseQueryString(request.Url.Query).Get("param2");
                Console.WriteLine("param1 = " + param1);
                Console.WriteLine("param2 = " + param2);
                Console.WriteLine("param3 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param3"));
                Console.WriteLine("param4 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param4"));

                //
                Console.WriteLine(documentContents);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // *-*-*-*-*-*-*-* Urls example *-*-*-*-*-*-*-*
                // localhost:8080/anything/textColoring?param1=red&param2=Ce texte sera vert!
                // localhost:8080/anything/multMethod?param1=95&param2=6


                string responseString = "<HTML><BODY>You can request an url like localhost:8080/anything/textColoring?param1=red&param2='My text'</BODY></HTML>";
                string method = request.Url.Segments[request.Url.Segments.Length - 1];
                if (method != "favicon.ico")
                {
                    responseString = executeMethod(method,param1,param2 );
                }

                // Construct a response.
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            // Httplistener neither stop ... But Ctrl-C do that ...
            // listener.Stop();
        }
    }
}