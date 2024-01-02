using System.Collections.Generic;

namespace FiltexNet.Constants
{
    public class Logic
    {
        public static readonly Logic LogicUnknown = new("");
        public static readonly Logic LogicAnd = new("and");
        public static readonly Logic LogicOr = new("or");

        private Logic(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public static Logic ParseLogic(string name)
        {
            return name.ToLower() switch
            {
                "and" => LogicAnd,
                "or" => LogicOr,
                _ => LogicUnknown
            };
        }

        public TokenType ToTokenType()
        {
            var map = new Dictionary<Logic, TokenType>
            {
                { LogicAnd, TokenType.TokenTypeAnd },
                { LogicOr, TokenType.TokenTypeOr }
            };

            if (map.TryGetValue(this, out var tokenType))
            {
                return tokenType;
            }

            return TokenType.TokenTypeNone;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}