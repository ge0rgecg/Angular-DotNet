using AutoFixture.Xunit2;
using Dominio;
using System;
using Xunit;

namespace ApiDotNet.Dominio.test
{
    public class LogTest
    {
        [Theory]
        [InlineAutoData(-1, "", false, "", 0, 5, "Campo Id é inválido")]
        [InlineAutoData(1, "", false, "", 0, 4, "Campo Chave é obrigátorio")]
        [InlineAutoData(1, "a", false, "", 0, 3, "Campo DataLog é obrigátorio")]
        [InlineAutoData(1, "a", true, "", 0, 2, "Campo Texto é obrigátorio")]
        [InlineAutoData(1, "a", true, "a", 0, 1, "Campo CodigoRetorno é inválido")]
        [InlineAutoData(1, "a", true, "a", 200, 0, "Campo Id é inválido")]
        public void Validacao(
            int id,
            string chave,
            bool hasDatalog,
            string texto,
            int codigoretorno,
            int quantidadeErro,
            string mensagemErro,
            DateTime datalog)
        {
            if (!hasDatalog)
            {
                datalog = DateTime.MinValue;
            }
            var log = new Log
            {
                Id = id,
                Chave = chave,
                DataLog = datalog,
                Texto = texto,
                CodigoRetorno = codigoretorno
            };

            var retorno = log.Validacao();

            Assert.Equal(quantidadeErro, retorno.Count);
            if (quantidadeErro != 0)
            {
                Assert.Contains(retorno, a => a.Equals(mensagemErro));
            }

        }
    }
}
