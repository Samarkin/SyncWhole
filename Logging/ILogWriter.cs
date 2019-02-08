using System;

namespace SyncWhole.Logging
{
	public interface ILogWriter
	{
		LogLevel MaxLevel { get; }

		void WriteMessage(LogLevel level, string message);
		void WriteMessage(Exception exception, string message);
	}
}
