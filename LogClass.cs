using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Projeto_Apollo_16
{
    static class LogClass
    {
        public enum LogType
        {
            INFO,
            WARNING,
            ERROR
        }

        static private bool logIsActive;
        static private LogType logType;
        static private string logFilename;

        /// Initializes the style of the log's output, and overwrites
        /// any existing file.  It defaults to HTML format, and capturing of
        /// all content
        static public void initialize()
        {
            initialize(LogType.ERROR);
        }

        static public void initialize(LogType type)
        {
            // update state
            logIsActive = true;
            logType = type;

            // get application filename
            String appName = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            appName = appName.Remove(appName.LastIndexOf("."));

            // construct filename
            string timeStamp = System.DateTime.Now.ToString("yyyy.MM.dd-hh.mm.ss");
            logFilename = "log.html"; //appName + "-log-" + timeStamp + ".html";

            // reset output file
            System.Console.WriteLine(logFilename);
            try
            {
                StreamWriter textOut = new StreamWriter(new FileStream(logFilename, FileMode.Create, FileAccess.Write));
                textOut.Close();
            }
            catch (System.Exception e)
            {
                string error = e.Message;
            }

            // output header
            logInfo("----------------------------------------");
            logInfo(appName);
            logInfo("----------------------------------------");
        }

        static public void logInfo(string lineBody)
        {
            // is logging enabled?
            if (logIsActive == false)
                return;

            writeln("<span style=\"color: #000000\">", lineBody, "</span><br>");
        }

        static public void logWarning(string lineBody)
        {
            // is logging enabled?
            if (logIsActive == false)
                return;

            writeln("<span style=\"color: #2020A0\">", lineBody, "</span><br>");
        }

        static public void logError(string lineBody)
        {
            // is logging enabled?
            if (logIsActive == false)
                return;

            writeln("<span style=\"color: #A02020\">", lineBody, "</span><br>");
        }

        static private void writeln(string linePrefix, string lineBody, string lineSuffix)
        {
            try
            {
                string timeStamp = System.DateTime.Now.ToString("hh:mm:ss.fff tt : ");
                StreamWriter textOut = new StreamWriter(new FileStream(logFilename, FileMode.Append, FileAccess.Write));
                textOut.WriteLine(timeStamp + linePrefix + lineBody + lineSuffix);
                textOut.Close();
            }
            catch (System.Exception e)
            {
                string error = e.Message;
            }
        }
    }
}
