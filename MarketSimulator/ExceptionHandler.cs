using System;
using System.IO;

namespace MarketSimulator
{
	/// <summary>
	/// Summary description for ExceptionHandler.
	/// </summary>
	public static class ExceptionHandler
	{
		public static void Handle(Exception ex)
		{
			Logger.Write("Log", null, ex);
		}

        public static void Handle(string msg)
		{
			Logger.Write("Log", msg, null);
		}

        public static void Handle(string msg, Exception ex)
		{
			Logger.Write("Log", msg, ex);
		}

        public static void Handle(string logname, string msg, Exception ex)
		{
			Logger.Write("Log", msg, ex);
		}

		public static void Log(string logname, string msg)
		{
			Logger.Write(logname, msg, null);
		}
		
	}
}
