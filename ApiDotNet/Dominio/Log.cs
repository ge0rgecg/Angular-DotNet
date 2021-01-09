using System.Net;
using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Log
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public DateTime DataLog { get; set; }
        public string Texto { get; set; }
        public int CodigoRetorno { get; set; }
        public int? Retorno { get; set; }

        public List<string> Validacao()
        {
            List<string> retorno = new List<string>();
            if(Id < 0)
            {
                retorno.Add("Campo Id é inválido");
            }

            if (string.IsNullOrEmpty(Chave))
            {
                retorno.Add("Campo Chave é obrigátorio");
            }

            if(DataLog == DateTime.MinValue)
            {
                retorno.Add("Campo DataLog é obrigátorio");
            }

            if(!Enum.IsDefined(typeof(HttpStatusCode), CodigoRetorno))
            {
                retorno.Add("Campo CodigoRetorno é inválido");
            }

            if (string.IsNullOrWhiteSpace(Texto))
            {
                retorno.Add("Campo Texto é obrigátorio");
            }

            return retorno;
        }
    }
}
