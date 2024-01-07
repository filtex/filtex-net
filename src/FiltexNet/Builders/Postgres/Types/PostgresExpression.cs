namespace FiltexNet.Builders.Postgres.Types
{
    public class PostgresExpression
    {
        public string Condition { get; set; }
        public object[] Args { get; set; }
    }
}