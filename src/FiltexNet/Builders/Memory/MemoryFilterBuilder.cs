using FiltexNet.Builders.Memory.Types;
using FiltexNet.Exceptions;
using FiltexNet.Expressions;

namespace FiltexNet.Builders.Memory
{
    public class MemoryFilterBuilder
    {

        public MemoryExpression Build(IExpression expression)
        {
            throw BuildException.NewCouldNotBeBuiltError();
        }
    }
}