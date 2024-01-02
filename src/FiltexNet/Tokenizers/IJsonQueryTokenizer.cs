namespace FiltexNet.Tokenizers
{
    public interface IJsonQueryTokenizer
    {
        object[] Tokenize(string query);
    }
}