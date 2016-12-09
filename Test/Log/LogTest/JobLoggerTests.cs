using Microsoft.VisualStudio.TestTools.UnitTesting;
using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Tests
{
    [TestClass()]
    public class JobLoggerTests
    {
        [TestMethod()]
        public void LogMessageTest()
        {
            bool logToFile = true;
            bool logToConsole = true;
            bool logToDatabase = false;
            bool logMessage = false;
            bool logWarning = true;
            bool logError = false;
            string message = "prueba test";
            bool message1 = false;
            bool warning = true;
            bool error = false;

            JobLogger log = new JobLogger(logToFile, logToConsole, logToDatabase, logMessage, logWarning, logError);
            JobLogger.LogMessage(message, message1, warning, error);
            //Assert.Fail();
        }
    }
}