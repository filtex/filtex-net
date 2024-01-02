using FiltexNet.Models;

namespace FiltexNet.Tokenizers
{
    public interface ITextQueryTokenizer
    {
        Token[] Tokenize(string text);
    }
}