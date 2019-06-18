using System;

namespace DebitoCredito.Infra
{
    public sealed class VariaveisGlobais
    {
        private VariaveisGlobais()
        {
        }

        private static string _conn;
        private static Guid _idTransacao;

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

        public static Guid Transacao
        {
            get
            {
                return _idTransacao;
            }

            set
            {
                _idTransacao = value;
            }
        }
    }
}
