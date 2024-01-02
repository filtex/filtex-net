using FiltexNet.Constants;
using FiltexNet.Expressions;

namespace FiltexNet.Models
{
    public class LogicExpression : IExpression
    {
        public Logic Logic { get; set; }
        public IExpression[] Expressions { get; set; }
    }
}