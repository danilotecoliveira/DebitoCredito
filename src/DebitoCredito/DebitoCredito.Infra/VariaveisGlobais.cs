namespace DebitoCredito.Infra
{
    public sealed class VariaveisGlobais
    {
        private VariaveisGlobais()
        {
        }

        private static string _conn;

        public static string Conn
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_conn))
                {
                    _conn = VariaveisSecretas.ObterValor("CONN-DEBITOCREDITO");
                }

                return _conn;
            }

            private set
            {
                _conn = value;
            }
        }
    }
}
