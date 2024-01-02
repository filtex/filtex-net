using FiltexNet.Constants;
using FiltexNet.Expressions;

namespace FiltexNet.Models
{
    public class OperatorExpression : IExpression
    {
        public FieldType Type { get; set; }
        public string Field { get; set; }
        public Operator Operator { get; set; }
        public object Value { get; set; }
    }
}