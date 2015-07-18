using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ConsoleApplication1
{
    /// <summary>
    /// Helper for Log issues
    /// </summary>
    public class Log
    {
        /// <summary>
        /// NLog Logger
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Write a Line in log
        /// </summary>
        /// <param name="line"></param>
        private static void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        /// <summary>
        /// Writes a line inside log
        /// </summary>
        /// <param name="line"></param>
        /// <param name="type"></param>
        public static void WriteLine(string line, LogType type)
        {
            switch (type)
            {
                case LogType.Console:
                    WriteLine(line);
                    _logger.Info(line);

                    break;
                case LogType.Debug:
                    WriteLine(line);
                    _logger.Debug(line);
                    break;
                case LogType.Trace:
                    WriteLine(line);
                    _logger.Trace(line);
                    break;
            }
        }
    }

    /// <summary>
    /// List of types of Log
    /// </summary>
    public enum LogType
    {
        Debug,
        Console,
        Trace
    }
}
