using System.Collections.Generic;

namespace Dominio
{
    public class Retorno<T>
    {
        public bool Ok { get; set; }

        public List<string> Mensagens { get; set; }

        public T Objeto { get; set; }
    }
}
