using UnityEngine;
using System;
using System.Threading;

namespace Base
{
    public class UnityOutput : ILogOutput
    {
        public UnityOutput()
        {

        }

        public void Close()
        {
        }

        private void DoOutput(LogType type, UnityEngine.Object context, string message)
        {
            switch (type)
            {
                case LogType.Error:
                    Debug.LogError(message, context);
                    break;

                case LogType.Assert:
                    Debug.LogAssertion(message, context);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(message, context);
                    break;

                case LogType.Log:
                    Debug.Log(message, context);
                    break;

                case LogType.Exception:
                default:
                    Debug.LogError(message, context);
                    break;
            }
        }

        public void Output(LogLevel level, string category, object context, string message)
        {
            if (Thread.CurrentThread.IsThreadPoolThread ||
                Thread.CurrentThread.IsBackground ||
                Thread.CurrentThread.ManagedThreadId != 1)
            {
                this.DoOutput(level.ToUnityLogType(), context as UnityEngine.Object,
                    String.Format("[{0:HH:mm:ss.fff}/----] ", DateTime.Now) + message);
                return;
            }

            if (level == LogLevel.Exception)
            {
                // 对于异常，Unity要求使用专门的接口 LogException，并且传递一个 exception 作为参数
                // 考虑到传入一个 exception 对象会增加系统的复杂度，因此这里采用直接将 level 变为
                // error 的解决方案
                level = LogLevel.Error;
            }

            this.DoOutput(level.ToUnityLogType(), context as UnityEngine.Object,
                String.Format("[{0:HH:mm:ss.fff}/{1,4}] ", DateTime.Now, Time.frameCount) + message);
        }
    }

    public static class LogUnityEx
    {
        public static LogType ToUnityLogType(this LogLevel level)
        {
            switch (level)
            {
            case LogLevel.Exception:
                return LogType.Exception;
            case LogLevel.Assert:
                return LogType.Assert;
            case LogLevel.Error:
                return LogType.Error;
            case LogLevel.Warning:
                return LogType.Warning;
            case LogLevel.Message:
            case LogLevel.Info:
            case LogLevel.Print:
            default:
                return LogType.Log;
            }
        }

        public static LogLevel ToLogLevel(this LogType type)
        {
            switch (type)
            {
            case LogType.Exception:
                return LogLevel.Exception;
            case LogType.Assert:
                return LogLevel.Assert;
            case LogType.Error:
                return LogLevel.Error;
            case LogType.Warning:
                return LogLevel.Warning;
            case LogType.Log:
            default:
                return LogLevel.Message;
            }
        }
    }
}
