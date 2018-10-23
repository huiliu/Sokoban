using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
    public interface ILogIntegration
    {
        void Write(LogLevel level, string category, object context, string message);
        void SetOutputList(List<ILogOutput> outputList);
    }
}
