using log4net;
using System;
using System.IO;

namespace LT_Support
{
    public class LoggingSample
    {
        //------------------------------------------------------------
        // Khai báo đối tượng log4net
        //------------------------------------------------------------
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoggingSample()
        {
            log.Debug("Create ctor of class");
        }

        //------------------------------------------------------------
        // Đọc nội dung log từ file log hiện tại
        //------------------------------------------------------------
        private void ReadLog(object sender, EventArgs e)
        {
            string log = "";
            string currentLogFile = AppDomain.CurrentDomain.BaseDirectory + "log\\AppLog_";
            using (var fileStream = new FileStream(currentLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                log = textReader.ReadToEnd();
            }
        }
    }
}
