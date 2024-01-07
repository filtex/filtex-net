using FiltexNet.Builders.Memory.Types;

namespace FiltexNet.Builders.Memory.Logics
{
    public static class AndLogic
    {
        public static MemoryExpression Build(MemoryExpression[] expressions)
        {
            return new MemoryExpression
            {
                Fn = data =>
                {
                    foreach (var exp in expressions)
                    {
                        if (!exp.Fn(data))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            };
        }
    }
}