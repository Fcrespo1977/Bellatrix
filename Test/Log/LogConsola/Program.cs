using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            bool logToFile=false;
            bool logToConsole = true;
            bool logToDatabase = false;
            bool logMessage = false;
            bool logWarning = true;
            bool logError = false;
            string message= "prueba";
            bool message1 =false;
            bool warning=true;
            bool error=false;


            JobLogger log = new JobLogger(logToFile, logToConsole, logToDatabase, logMessage, logWarning, logError);
            JobLogger.LogMessage(message, message1, warning, error);
            Console.ReadKey();
        }
    }
}
