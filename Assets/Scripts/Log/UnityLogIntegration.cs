using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Base
{
    public class UnityLogIntegration : ILogIntegration
    {
        private List<ILogOutput> outputList;
        public UnityLogIntegration()
        {
            Application.logMessageReceived += this.handleUnityLog;
        }

        private void handleUnityLog(string message, string stackTrace, LogType type)
        {
            var s = null as string;
            switch (type)
            {
                case LogType.Log:
                case LogType.Warning:
                    s = message;
                    break;
                default:
                    s = message + stackTrace;
                    break;
            }

            for (var i = 0; i < this.outputList.Count; i++)
            {
                if (this.outputList[i] is UnityOutput)
                    continue;

                this.outputList[i].Output(type.ToLogLevel(), "UNITY", null, s);
            }
        }

        public void SetOutputList(List<ILogOutput> outputList)
        {
            this.outputList = outputList;
        }

        private void WriteToUnityConsole(LogType type, UnityEngine.Object context, string message)
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

        public void Write(LogLevel level, string category, object context, string message)
        {
            if (Thread.CurrentThread.IsThreadPoolThread ||
                Thread.CurrentThread.IsBackground ||
                Thread.CurrentThread.ManagedThreadId != 1)
            {
                this.WriteToUnityConsole(level.ToUnityLogType(), context as UnityEngine.Object,
                    String.Format("[{0:HH:mm:ss}/----] ", DateTime.Now) + message);
                return;
            }

            if (level == LogLevel.Exception)
            {
                // 对于异常，Unity要求使用专门的接口 LogException，并且传递一个 exception 作为参数
                // 考虑到传入一个 exception 对象会增加系统的复杂度，因此这里采用直接将 level 变为
                // error 的解决方案
                level = LogLevel.Error;
            }

            this.WriteToUnityConsole(level.ToUnityLogType(), context as UnityEngine.Object,
                String.Format("[{0:HH:mm:ss}/{1,4}] ", DateTime.Now, Time.frameCount) + message);
        }
    }
}
