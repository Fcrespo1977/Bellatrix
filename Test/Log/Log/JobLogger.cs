using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Log
{    
    public class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;
        private bool _initialized;
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }
        public static void LogMessage(string message, bool message1, bool warning, bool error)
        {
            message.Trim();
            
            //if (message == null || message.Length == 0)
            if(string.IsNullOrEmpty(message))
            {
                return;
            }
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new ApplicationException("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!message1 && !warning && !error))
            {
                throw new ApplicationException("Error or Warning or Message must be specified");
            }
            if (_logToDatabase)
            { 
                System.Data.SqlClient.SqlConnection connection = new
                System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                connection.Open();
                int? tipoError = (int?)null;
                if (message1 && _logMessage)
                {
                    tipoError = 1;
                }
                if (error && _logError)
                {
                    tipoError = 2;
                }
                if (warning && _logWarning)
                {
                    tipoError = 3;
                }
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " + tipoError.ToString() + ")");
                command.ExecuteNonQuery();
                connection.Close();
            }
            if (_logToFile)
            {
                string contenido = string.Empty;
                string archivo = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                if (!System.IO.File.Exists(archivo))
                {
                    System.IO.File.Create(archivo).Close();

                }
                else
                {
                    contenido = System.IO.File.ReadAllText(archivo);
                }

                if (error && _logError)
                {
                    contenido += DateTime.Now.ToShortDateString() + message;
                }
                if (warning && _logWarning)
                {
                    contenido += DateTime.Now.ToShortDateString() + message;
                }
                if (message1 && _logMessage)
                {
                    contenido += DateTime.Now.ToShortDateString() + message;
                }
                System.IO.File.WriteAllText(archivo, contenido); // System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
            }
            if(_logToConsole)
            { 
                if (error && _logError)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (warning && _logWarning)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (message1 && _logMessage)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
                //Console.ReadKey();
            }
        }
    }
}
