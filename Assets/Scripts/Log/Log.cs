// Create by Yezh on Jun/09/2018

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace  Base
{
    public static class Log
    {
        public static string ToBrieflyString(this LogLevel level)
        {
            switch (level)
            {
            case LogLevel.Exception:
            case LogLevel.Assert:
            case LogLevel.Error:
                return "E";
            case LogLevel.Warning:
                return "W";
            case LogLevel.Message:
                return "M";
            case LogLevel.Info:
                return "I";
            case LogLevel.Print:
                return "P";
            default:
                return null;
            }
        }

        private const string ERROR_CATEGORY = "Error";
        private const string ASSERT_CATEGORY = "Assert";
        private const string EXCEPTION_CATEGORY = "Except";

        private static LogLevel DefaultFilterLevel;

        private static Dictionary<string, LogLevel> CategoryFilterLevels;
        private static Dictionary<int, object>      ThreadContexts;

        private static List<ILogOutput> Outputs;
        private static List<ILogIntegration> Integrations;

        static Log ()
        {
            DefaultFilterLevel = LogLevel.Warning;
            ThreadContexts = new Dictionary<int, object>(10);
            CategoryFilterLevels = new Dictionary<string, LogLevel>(20);
            Outputs = new List<ILogOutput>();
            Integrations = new List<ILogIntegration>();
        }

        public static void Initialize()
        {
            // Trace.Listeners.Clear();
            Trace.Listeners.Add(new SystemDebugListener());
        }

        public static void Stop()
        {
            foreach (var output in Outputs)
                output.Close();
        }

        public static void AddOutput(ILogOutput output)
        {
            Outputs.Add(output);
        }

        public static void AddIntegration(ILogIntegration integration)
        {
            integration.SetOutputList(Outputs);
            Integrations.Add(integration);
        }

        private static void DoOutput(LogLevel level, string category, object context, string message)
        {
            foreach (var output in Outputs)
                output.Output(level, category, context, message);
        }

        public static void Write(LogLevel level, string category, object context, string message)
        {
            if (IsCategoryFilterBy(category, level))
                return;

            if (Integrations.Count == 0)
                DoOutput(level, category, context, message);
            else
            {
                for (var i = 0; i < Integrations.Count; i++)
                    Integrations[i].Write(level, category, context, message);
            }
        }

        public static void Write(LogLevel level, string category, object context, string fmt, params object[] args)
        {
            if (IsCategoryFilterBy(category, level))
                return;

            foreach (var output in Outputs)
                output.Output(level, category, context, String.Format(fmt, args));
        }

        public static void Error(string message)
        {
            Write(LogLevel.Error, ERROR_CATEGORY, GetThreadContext(), message);
        }

        public static void Error(string fmt, params object[] args)
        {
            Write(LogLevel.Error, ERROR_CATEGORY, GetThreadContext(), fmt, args);
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string message = "Assertion failed")
        {
            if (condition)
                return;

            Write(LogLevel.Assert, ASSERT_CATEGORY, GetThreadContext(), message);
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string fmt, params object[] args)
        {
            if (condition)
                return;

            Write(LogLevel.Assert, ASSERT_CATEGORY, GetThreadContext(), fmt, args);
        }

        public static void Exception(Exception e)
        {
            Write(LogLevel.Exception, EXCEPTION_CATEGORY, GetThreadContext(), e.ToString());
        }

        public static void Exception(Exception e, string message)
        {
            Write(LogLevel.Exception, EXCEPTION_CATEGORY, GetThreadContext(),
                message + Environment.NewLine + e.ToString());
        }

        public static void Exception(Exception e, string fmt, params object[] args)
        {
            Write(LogLevel.Exception, EXCEPTION_CATEGORY, GetThreadContext(),
                String.Format(fmt, args) + Environment.NewLine + e.ToString());
        }

        public static void Warning(string category, string message)
        {
            Write(LogLevel.Warning, category, GetThreadContext(), message);
        }

        public static void Warning(string category, string fmt, params object[] args)
        {
            Write(LogLevel.Warning, category, GetThreadContext(), fmt, args);
        }

        public static void Message(string category, string message)
        {
            Write(LogLevel.Message, category, GetThreadContext(), message);
        }

        public static void Message(string category, string fmt, params object[] args)
        {
            Write(LogLevel.Message, category, GetThreadContext(), fmt, args);
        }

        // #6978 ÏÈ×¢ÊÍ
        // [Conditional("DEBUG")]
        public static void Info(string category, string message)
        {
            Write(LogLevel.Info, category, GetThreadContext(), message);
        }

        // #6978 ÏÈ×¢ÊÍ
        // [Conditional("DEBUG")]
        public static void Info(string category, string fmt, params object[] args)
        {
            Write(LogLevel.Info, category, GetThreadContext(), fmt, args);
        }

        [Conditional("DEBUG")]
        public static void Print(string category, string message)
        {
            Write(LogLevel.Print, category, GetThreadContext(), message);
        }

        [Conditional("DEBUG")]
        public static void Print(string category, string fmt, params object[] args)
        {
            Write(LogLevel.Print, category, GetThreadContext(), fmt, args);
        }

        public static void SetCategoryLevel(string category, LogLevel level)
        {
            SetFilterLevel(level, category);
        }

        public static void SetFilterLevel(LogLevel level, string category = null)
        {
            if (String.IsNullOrEmpty(category))
            {
                DefaultFilterLevel = level;
                return;
            }

            if (CategoryFilterLevels.ContainsKey(category))
                CategoryFilterLevels[category] = level;
            else
                CategoryFilterLevels.Add(category, level);
        }

        public static void SetThreadContext(object context)
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            if (!ThreadContexts.ContainsKey(id))
                ThreadContexts.Add(id, context);
            else
                ThreadContexts[id] = context;

            if (context == null)
                ThreadContexts.Remove(id);
        }

        private static bool IsCategoryFilterBy(string category, LogLevel filterLevel)
        {
            var level = default(LogLevel);
            if (CategoryFilterLevels.TryGetValue(category, out level))
                return level < filterLevel;

            return DefaultFilterLevel < filterLevel;
        }

        private static object GetThreadContext()
        {
            var context = null as object;
            ThreadContexts.TryGetValue(Thread.CurrentThread.ManagedThreadId, out context);
            return context;
        }

        private class SystemDebugListener : TraceListener
        {
            public override void WriteLine(string message, string category)
            {
                Log.Write(LogLevel.Info, category, null, message);
            }

            public override void Write(string message, string category)
            {
                Log.Write(LogLevel.Info, category, null, message);
            }

            public override void WriteLine(string message)
            {
                Log.Write(LogLevel.Info, "Unknown", null, message);
            }

            public override void Write(string message)
            {
                Log.Write(LogLevel.Info, "Unknown", null, message);
            }
        }

        // End of class
    }
}
