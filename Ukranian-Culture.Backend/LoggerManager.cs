using Contracts;
using NLog;
using ILogger = NLog.ILogger;

namespace Ukranian_Culture.Backend;

public class LoggerManager : ILoggerManager
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public LoggerManager()
    {
    }

    public void LogInfo(string message)
    {
        Logger.Info(message);
    }

    public void LogWarn(string message)
    {
        Logger.Warn(message);
    }

    public void LogDebug(string message)
    {
        Logger.Debug(message);
    }

    public void LogError(string message)
    {
        Logger.Error(message);
    }
}