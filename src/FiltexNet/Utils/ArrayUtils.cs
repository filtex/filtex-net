using System.Linq;

namespace FiltexNet.Utils
{
    public class ArrayUtils
    {
        public static bool IsInAny<T>(T value, params T[][] sources)
        {
            return sources.Any(source => source.Any(item => Equals(item, value)));
        }
    }
}