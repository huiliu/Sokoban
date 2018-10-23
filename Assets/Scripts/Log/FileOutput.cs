using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Base
{
    public class FileOutput : ILogOutput
    {
        private readonly Queue<string>  cache;
        private Thread         writeThread;
        private AutoResetEvent writerEvent;

        public FileOutput(string filepath)
        {
            this.cache = new Queue<string>();

            this.cache.Enqueue(String.Format(
                "============================ [{0:MMM/dd/yyyy HH:mm:ss}] ============================",
                DateTime.Now));

            this.writerEvent = new AutoResetEvent(true);
            this.writeThread = new Thread(() => this.WriteToFile(filepath));
            this.writeThread.Start();
        }

        public void Close()
        {
            this.writeThread.Abort();
            this.writeThread = null;
        }

        public void Output(LogLevel level, string category, object context, string message)
        {
            var session = context as string;
            var fmt = String.IsNullOrEmpty(session) ? "{0:HH:mm:ss.fff} [{1}] {2,-6}: {3}"
                : "{0:HH:mm:ss.fff} [{1}]{2,-6}|{4,-8}: {3}";

            if (level <= LogLevel.Error)
                message += Environment.NewLine + new StackTrace(2, true).ToString();

            lock (this.cache)
                this.cache.Enqueue(string.Format(fmt, DateTime.Now,
                    level.ToBrieflyString(), category, message, session));

            this.writerEvent.Set();
        }

        private void WriteToFile(string filepath)
        {
            var dupCache = new Queue<string>(100);

            var yesterday = default(DateTime);
            if (!File.Exists(filepath))
                yesterday = DateTime.Today;
            else
            {
                yesterday = File.GetLastWriteTime(filepath);
                yesterday -= yesterday.TimeOfDay;
            }

            var writer = new StreamWriter(filepath, true, System.Text.Encoding.UTF8);

            try
            {
                while (true)
                {
                    this.writerEvent.WaitOne(1000);

                    var today = DateTime.Today;
                    if (today != yesterday)
                    {
                        writer.Close();
                        this.SaveLogFile(filepath, yesterday);
                        writer = new StreamWriter(filepath, true, System.Text.Encoding.UTF8);
                        yesterday = today;
                        continue;
                    }

                    lock (this.cache)
                    {
                        while (this.cache.Count > 0)
                            dupCache.Enqueue(this.cache.Dequeue());

                        this.cache.Clear();

                        this.writerEvent.Set();
                        this.writerEvent.WaitOne();
                    }

                    if (dupCache.Count <= 0)
                    {
                        writer.Flush();
                        continue;
                    }
                    else

                    while (dupCache.Count > 0)
                        writer.WriteLine(dupCache.Dequeue());
                    writer.Flush();
                }
            }
            catch (ThreadAbortException)
            {
                writer.Close();
            }
        }

        private void SaveLogFile(string filepath, DateTime time)
        {
            var dir = Path.GetDirectoryName(filepath);
            var extension = Path.GetExtension(filepath);
            var filename = Path.GetFileNameWithoutExtension(filepath);

            var count = 0;
            var newFilepath = default(string);
            do
            {
                var newFilename = String.Format("{0}_{1:yyyyMMdd}_{2}{3}",
                    filename, time, count, extension);
                newFilepath = Path.Combine(dir, newFilename);
                count++;
            }
            while (File.Exists(newFilepath));
            File.Move(filepath, newFilepath);
        }
    }
}
