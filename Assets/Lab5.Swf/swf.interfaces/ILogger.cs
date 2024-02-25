using System;

namespace Lab5.Swf.Interfaces
{
	public interface ILogger
	{
		void LogError(object message);
		void LogAssert(object message);
		void LogWarning(object message);
		void Log(object message);
		void LogException(Exception exception);
	}
}