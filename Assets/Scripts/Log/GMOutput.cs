using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Base
{
    public class GMOutput : ILogOutput
    {
        public static GMOutput Instance { get { return instance; } }
        private static GMOutput instance = new GMOutput();

        public List<string> Errors { get; private set; }
        public List<string> Logs   { get; private set; }

        private int count;
        private Queue<string> cache;
        public string cacheString  { get; private set; }

        private const int NormalCountBeforeError = 10;
        private const int NormalCountAfterError = 10;

        private GMOutput()
        {
            this.Errors = new List<string>(20);
            this.Logs = new List<string>(20);

            this.cacheString = string.Empty;
            this.cache = new Queue<string>(NormalCountBeforeError);
            this.count = 0;

            var n = NormalCountBeforeError + NormalCountAfterError;
            for (var i = 0; i <= n; i++)
                this.cache.Enqueue(String.Empty);
        }

        public void Close()
        {
        }

        public void Output(LogLevel level, string category, object context, string message)
        {
            if (this.Errors.Count > 20)
                return;

            var prefix = null as string;
            if (Thread.CurrentThread.IsThreadPoolThread ||
                Thread.CurrentThread.IsBackground ||
                Thread.CurrentThread.ManagedThreadId != 1)
                prefix = String.Format("[{0:HH:mm:ss}/----] ", DateTime.Now);
            else
                prefix = String.Format("[{0:HH:mm:ss}/{1,4}] ", DateTime.Now, Time.frameCount);

            message = prefix + message;

            this.cache.Dequeue();
            this.cache.Enqueue(message);

            if (level > LogLevel.Error)
            {
                if (count-- > 0)
                {
                    this.cacheString += message + Environment.NewLine;
                    if (count <= 0)
                    {
                        this.Logs.Add(this.cacheString);
                        this.cacheString = String.Empty;
                    }
                }

                return;
            }

            if (count < -NormalCountBeforeError)
            {
                this.cacheString = "...";
                count = -NormalCountBeforeError;
            }

            foreach (var prev in cache)
            {
                if ((this.cache.Count + count--) > 0)
                    continue;

                this.cacheString += prev + Environment.NewLine;
            }

            count = NormalCountAfterError;

            this.Errors.Add(message);
        }

        // End of class
    }
}
