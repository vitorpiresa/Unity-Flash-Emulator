using System;
using Lab5.Swf.Interfaces;

public static class Log
{
	public static ILogger Logger { get; set; }
	public static void Error(object message) => Logger?.LogError(message);
	public static void Assert(object message) => Logger?.LogAssert(message);
	public static void Warning(object message) => Logger?.LogAssert(message);
	public static void Info(object message) => Logger.Log(message);
	public static void Exception(Exception exception) => Logger.LogException(exception);
}