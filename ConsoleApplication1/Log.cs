using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// Helper for Log issues
    /// </summary>
    public class Log
    {

        /// <summary>
        /// Write a Line in log
        /// </summary>
        /// <param name="line"></param>
        private static void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="type"></param>
        public static void WriteLine(string line, LogType type)
        {
            switch (type)
            {
                case LogType.Console:
                    WriteLine(line);
                    break;
                case LogType.Debug:
                    WriteLine(line);
                    //TODO: gravar arquivo usando o NLog
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
        Console
    }
}
