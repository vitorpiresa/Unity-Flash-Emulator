using System;
using UnityEngine;
using ILogger = Lab5.Swf.Interfaces.ILogger;
using Object = UnityEngine.Object;

namespace Lab5.Flash
{
	public class UnityLoggerAdapter : ILogger
	{
		ILogHandler m_Handler;
		Object m_Context;

		public UnityLoggerAdapter(Object context)
		{
			m_Handler = Debug.unityLogger;
			m_Context = context;
		}

		public void Log(object message) => m_Handler.LogFormat(LogType.Log, m_Context, message.ToString());

		public void LogAssert(object message) => m_Handler.LogFormat(LogType.Assert, m_Context, message.ToString());

		public void LogError(object message) => m_Handler.LogFormat(LogType.Error, m_Context, message.ToString());

		public void LogException(Exception exception) => m_Handler.LogException(exception, m_Context);

		public void LogWarning(object message) => m_Handler.LogFormat(LogType.Warning, m_Context, message.ToString());
	}
}