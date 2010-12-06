using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Membase.Interop
{
	public class BasicLoggerFactory : Enyim.Caching.ILogFactory
	{
		private StreamWriter sw;

		public BasicLoggerFactory(string path)
		{
			this.sw = new StreamWriter(path, true, Encoding.UTF8);
		}

		#region ILogFactory Members

		Enyim.Caching.ILog Enyim.Caching.ILogFactory.GetLogger(string name)
		{
			return new BasicLogger(name, this.sw);
		}

		Enyim.Caching.ILog Enyim.Caching.ILogFactory.GetLogger(Type type)
		{
			return new BasicLogger(type.ToString(), this.sw);
		}

		#endregion
	}

	public class BasicLogger : Enyim.Caching.ILog
	{
		private StreamWriter sw;
		private string name;

		public BasicLogger(string name, StreamWriter sw)
		{
			this.sw = sw;
			this.name = name;
		}

		private void Write(string severity, object message)
		{
			lock (sw)
			{
				sw.WriteLine("[{0:yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'ffff}] {1} {2} {3}", DateTime.Now, severity, this.name, message);
				sw.Flush();
			}
		}

		private void Write(string severity, object message, Exception e)
		{
			lock (sw)
			{
				sw.WriteLine("[{0:yyyy'-'MM'-'dd' 'HH':'mm':'ss'.'ffff}] {1} {2} {3} {4}", DateTime.Now, severity, this.name, message, e);
				sw.Flush();
			}
		}

		#region ILog Members

		bool Enyim.Caching.ILog.IsDebugEnabled
		{
			get { return true; }
		}

		bool Enyim.Caching.ILog.IsInfoEnabled
		{
			get { return true; }
		}

		bool Enyim.Caching.ILog.IsWarnEnabled
		{
			get { return true; }
		}

		bool Enyim.Caching.ILog.IsErrorEnabled
		{
			get { return true; }
		}

		bool Enyim.Caching.ILog.IsFatalEnabled
		{
			get { return true; }
		}

		void Enyim.Caching.ILog.Debug(object message)
		{
			Write("Debug", message);
		}

		void Enyim.Caching.ILog.Debug(object message, Exception exception)
		{
			Write("Debug", message, exception);
		}

		void Enyim.Caching.ILog.DebugFormat(string format, object arg0)
		{
			Write("Debug", String.Format(format, arg0));
		}

		void Enyim.Caching.ILog.DebugFormat(string format, object arg0, object arg1)
		{
			Write("Debug", String.Format(format, arg0, arg1));
		}

		void Enyim.Caching.ILog.DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			Write("Debug", String.Format(format, arg0, arg1, arg2));
		}

		void Enyim.Caching.ILog.DebugFormat(string format, params object[] args)
		{
			Write("Debug", String.Format(format, args));
		}

		void Enyim.Caching.ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			Write("Debug", String.Format(provider, format, args));
		}

		void Enyim.Caching.ILog.Info(object message)
		{
			Write("Info", message);
		}

		void Enyim.Caching.ILog.Info(object message, Exception exception)
		{
			Write("Info", message, exception);
		}

		void Enyim.Caching.ILog.InfoFormat(string format, object arg0)
		{
			Write("Info", String.Format(format, arg0));
		}

		void Enyim.Caching.ILog.InfoFormat(string format, object arg0, object arg1)
		{
			Write("Info", String.Format(format, arg0, arg1));
		}

		void Enyim.Caching.ILog.InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			Write("Info", String.Format(format, arg0, arg1, arg2));
		}

		void Enyim.Caching.ILog.InfoFormat(string format, params object[] args)
		{
			Write("Info", String.Format(format, args));
		}

		void Enyim.Caching.ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			Write("Info", String.Format(provider, format, args));
		}

		void Enyim.Caching.ILog.Warn(object message)
		{
			Write("Warn", message);
		}

		void Enyim.Caching.ILog.Warn(object message, Exception exception)
		{
			Write("Warn", message, exception);
		}

		void Enyim.Caching.ILog.WarnFormat(string format, object arg0)
		{
			Write("Warn", String.Format(format, arg0));
		}

		void Enyim.Caching.ILog.WarnFormat(string format, object arg0, object arg1)
		{
			Write("Warn", String.Format(format, arg0, arg1));
		}

		void Enyim.Caching.ILog.WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			Write("Warn", String.Format(format, arg0, arg1, arg2));
		}

		void Enyim.Caching.ILog.WarnFormat(string format, params object[] args)
		{
			Write("Warn", String.Format(format, args));
		}

		void Enyim.Caching.ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			Write("Warn", String.Format(provider, format, args));
		}

		void Enyim.Caching.ILog.Error(object message)
		{
			Write("Error", message);
		}

		void Enyim.Caching.ILog.Error(object message, Exception exception)
		{
			Write("Error", message, exception);
		}

		void Enyim.Caching.ILog.ErrorFormat(string format, object arg0)
		{
			Write("Error", String.Format(format, arg0));
		}

		void Enyim.Caching.ILog.ErrorFormat(string format, object arg0, object arg1)
		{
			Write("Error", String.Format(format, arg0, arg1));
		}

		void Enyim.Caching.ILog.ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			Write("Error", String.Format(format, arg0, arg1, arg2));
		}

		void Enyim.Caching.ILog.ErrorFormat(string format, params object[] args)
		{
			Write("Error", String.Format(format, args));
		}

		void Enyim.Caching.ILog.ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			Write("Error", String.Format(provider, format, args));
		}

		void Enyim.Caching.ILog.Fatal(object message)
		{
			Write("Fatal", message);
		}

		void Enyim.Caching.ILog.Fatal(object message, Exception exception)
		{
			Write("Fatal", message, exception);
		}

		void Enyim.Caching.ILog.FatalFormat(string format, object arg0)
		{
			Write("Fatal", String.Format(format, arg0));
		}

		void Enyim.Caching.ILog.FatalFormat(string format, object arg0, object arg1)
		{
			Write("Fatal", String.Format(format, arg0, arg1));
		}

		void Enyim.Caching.ILog.FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			Write("Fatal", String.Format(format, arg0, arg1, arg2));
		}

		void Enyim.Caching.ILog.FatalFormat(string format, params object[] args)
		{
			Write("Fatal", String.Format(format, args));
		}

		void Enyim.Caching.ILog.FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			Write("Fatal", String.Format(provider, format, args));
		}

		#endregion
	}
}
