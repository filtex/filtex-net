using System;
using System.Collections.Generic;

namespace FiltexNet.Builders.Memory.Types
{
    public class MemoryExpression
    {
        public Func<IDictionary<string, object>, bool> Fn { get; set; }
    }
}