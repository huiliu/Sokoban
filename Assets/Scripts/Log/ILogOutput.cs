using System;

namespace Base
{
    public interface ILogOutput
    {
        void Output(LogLevel level, string category, object context, string message);
        void Close();
    }
}
