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
        /// Writes a line inside log
        /// </summary>
        /// <param name="line"></param>
        /// <param name="level"></param>
        public static void WriteLine(string line, LogLevel level)
        {
            _logger.Log(level, line);
            
        }
    }

}
