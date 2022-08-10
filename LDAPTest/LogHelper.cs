using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAPTest
{
    public static class LogHelper
    {

        private static string _pathLog;

        static LogHelper()
        {
            _pathLog = string.Format("{0}/logs",AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(_pathLog))
                Directory.CreateDirectory(_pathLog);
        }

        public static void Write(string msg) {

            var filePath = string.Format("{0}/{1}.log", _pathLog, DateTime.Now.ToString("dd-MM-yyyy"));
            try
            {
                using (var writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(msg);
                
                };
            }
            catch (Exception)
            {
                using (var writer = new StreamWriter(string.Format("{0}/{1}.log", _pathLog, DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")), true))
                {
                    writer.WriteLine(msg);
                };
            }
            
        }
    }
}
