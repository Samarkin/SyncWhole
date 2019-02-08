using System;
using System.Runtime.CompilerServices;

namespace SyncWhole.Logging
{
	public static class Logger
	{
		private static ILogWriter _writer;

		public static void Initialize(ILogWriter writer)
		{
			if (_writer != null)
			{
				throw new InvalidOperationException("Double initialization attempt");
			}
			_writer = writer;
		}

		public static void Error(string message)
		{
			LogMessage(LogLevel.Error, message);
		}

		public static void Error(string message, Exception ex)
		{
			LogMessage(ex, message);
		}

		public static void Warning(string message)
		{
			LogMessage(LogLevel.Warning, message);
		}

		public static void Info(string message)
		{
			LogMessage(LogLevel.Info, message);
		}

		public static void Debug(string message)
		{
			LogMessage(LogLevel.Debug, message);
		}

		private static void LogMessage(LogLevel level, string message)
		{
			if (_writer == null)
			{
				throw new InvalidOperationException("Logger is not initialized");
			}
			_writer?.WriteMessage(level, message);
		}

		private static void LogMessage(Exception exception, string message)
		{
			if (_writer == null)
			{
				throw new InvalidOperationException("Logger is not initialized");
			}
			_writer?.WriteMessage(exception, message);
		}

		public static IDisposable Scope([CallerMemberName] string scopeName = null)
		{
			return _writer.MaxLevel >= LogLevel.Debug ? new DebugScope(scopeName) : null;
		}

		private sealed class DebugScope : IDisposable
		{
			private readonly string _scopeName;

			public DebugScope(string scopeName)
			{
				_scopeName = scopeName ?? throw new ArgumentNullException(nameof(scopeName));
				Debug($"Entering {_scopeName}");
			}

			public void Dispose()
			{
				Debug($"Exiting {_scopeName}");
			}
		}
	}
}