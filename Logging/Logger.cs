using System;
using System.Runtime.CompilerServices;

namespace SyncWhole.Logging
{
	public static class Logger
	{
		private static ILogWriter _writer;

		/// <summary>
		/// Initialize logger by supplying a writer implementation.
		/// </summary>
		/// <param name="writer"></param>
		public static void Initialize(ILogWriter writer)
		{
			if (_writer != null)
			{
				throw new InvalidOperationException("Double initialization attempt");
			}
			_writer = writer;
		}

		/// <summary>
		/// Log an error message.
		/// Should be used to indicate that the application cannot finish the requested operation and user intervention is required.
		/// </summary>
		/// <param name="message"></param>
		public static void Error(string message)
		{
			LogMessage(LogLevel.Error, message);
		}

		/// <summary>
		/// Log an exception.
		/// Should be used in situations not considered by the developers and not handled properly.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="ex"></param>
		public static void Exception(string message, Exception ex)
		{
			LogMessage(ex, message);
		}

		/// <summary>
		/// Log a warning message.
		/// Should be used to attract user's attention when the requested operation can be finished anyway by ignoring the problem.
		/// </summary>
		/// <param name="message"></param>
		public static void Warning(string message)
		{
			LogMessage(LogLevel.Warning, message);
		}

		/// <summary>
		/// Log an info message.
		/// Should be used to display progress of the requested operation to the user.
		/// </summary>
		/// <param name="message"></param>
		public static void Info(string message)
		{
			LogMessage(LogLevel.Info, message);
		}

		/// <summary>
		/// Log a verbose message.
		/// Should be used to provide detailed information about every action performed by the application.
		/// </summary>
		/// <param name="message"></param>
		public static void Verbose(string message)
		{
			LogMessage(LogLevel.Verbose, message);
		}

		/// <summary>
		/// Log a debug message.
		/// Should be used to supply developers with additional information in case of a problem.
		/// </summary>
		/// <seealso cref="Scope"/>
		/// <param name="message"></param>
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

		/// <summary>
		/// Begin a debug scope.
		/// Should be used to indicate boundaries of distinct operations.
		/// To end a debug scope, call <c>.Dispose()</c> on the result.
		/// </summary>
		/// <seealso cref="Debug"/>
		/// <param name="scopeName"></param>
		/// <returns>A disposable debug scope object, or <c>null</c> if debug logging is not enabled.</returns>
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