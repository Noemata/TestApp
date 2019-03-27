using System;
using System.Diagnostics;

using Caliburn.Micro;

namespace TestApp.Services
{
    public class CaliburnLogger : ILog
    {    
        #region Fields
        private readonly Type _type;
        #endregion

        #region Constructors
        public CaliburnLogger(Type type)
        {
            _type = type;
        }
        #endregion

        #region Helper Methods
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}",
            DateTime.Now.ToString("o"),
            string.Format(format, args));
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
#if DEBUG
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
#else
            Trace.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
#endif
        }

        public void Info(string format, params object[] args)
        {
#if DEBUG
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
#else
            Trace.WriteLine(CreateLogMessage(format, args), "INFO");
#endif
        }

        public void Warn(string format, params object[] args)
        {
#if DEBUG
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
#else
            Trace.WriteLine(CreateLogMessage(format, args), "WARN");
#endif
        }
        #endregion
    }
}
