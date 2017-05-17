using System;
using System.IO;
using System.Text;

namespace MarketSimulator
{
	/// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class Logger
	{
		private static string LogFilePath;
        private static Object WriteLock = new Object();

		public Logger()
		{
		}
		
		public static void Init(string filepath)
		{
			LogFilePath = filepath;
            LogFilePath = LogFilePath.Trim();
            if (LogFilePath[LogFilePath.Length - 1] != '\\')
            {
                LogFilePath += "\\";
            }
		}

		public static void Write(string logname, string msg)
		{
			Write(logname, msg, null);
		}

		public static void Write(Exception ex)
		{
			Write("Log", null, ex);
		}

		public static void Write(string msg)
		{
			Write("Log", msg, null);
		}

		public static void Write(string msg, Exception ex)
		{
			Write("Log", msg, ex);
		}

		public static void WriteLine(string logname, string msg)
		{
			try
			{
                lock (WriteLock)
                {
                    DateTime timestamp = System.DateTime.Now;
                    string filename = LogFilePath + logname + " " + timestamp.Month + "-" + timestamp.Day + "-" + timestamp.Year + ".txt";
                    StreamWriter sw = new StreamWriter(filename, true);
                    sw.WriteLine(msg);
                    sw.Close();
                }
			}
			catch
			{
			}
		}

        public static void WriteLine(StringBuilder buffer, string msg)
        {
            try
            {
                lock (WriteLock)
                {
                    buffer.Append(msg);
                    buffer.Append("\n");
                }
            }
            catch
            {
            }
        }

		public static void Write(string logname, string msg, Exception ex)
		{
			try
			{
                lock (WriteLock)
                {
                    DateTime timestamp = System.DateTime.Now;
                    string error = "";
                    if (ex != null)
                    {
                        error = ex.ToString();
                    }
                    string filename = LogFilePath + logname + " " + timestamp.Month + "-" + timestamp.Day + "-" + timestamp.Year + ".txt";
                    StreamWriter sw = new StreamWriter(filename, true);
                    sw.WriteLine("_________________________________________________________________________________");
                    sw.WriteLine(timestamp.ToString());
                    if (msg != null)
                    {
                        sw.WriteLine(msg);
                    }
                    sw.WriteLine(error);
                    sw.Close();
                }
			}
			catch
			{
			}
		}	
	}
}
