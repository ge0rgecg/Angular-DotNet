using AutoFixture.Xunit2;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Repositorio.Interface;
using Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiDotNet.test.Servicos
{
    public class LogServicoTest
    {
        private readonly Mock<ILogRepositorio> _mockRepositorio = new Mock<ILogRepositorio>();

        [Fact]
        public void Constructor_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => new LogServico(null)
            );
        }

        [Theory]
        [AutoData]
        public void Obter_Log(int id)
        {
            _mockRepositorio.Setup(mock => mock.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(new Log()));

            var servico = new LogServico(_mockRepositorio.Object);

            var response = servico.ObterLog(id);

            Assert.NotNull(response);
            Assert.IsType<Task<Retorno<Log>>>(response);

            _mockRepositorio.Verify(
                mock => mock.GetById(It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public void Obter_Logs()
        {
            _mockRepositorio.Setup(mock => mock.GetAll())
                .Returns(new List<Log>());

            var servico = new LogServico(_mockRepositorio.Object);

            var response = servico.ObterLogs();

            Assert.NotNull(response);
            Assert.IsType<Task<Retorno<IEnumerable<Log>>>>(response);

            _mockRepositorio.Verify(
                mock => mock.GetAll(),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public void Adicionar_Log(Log log)
        {
            _mockRepositorio.Setup(mock => mock.Create(It.IsAny<Log>()));

            var servico = new LogServico(_mockRepositorio.Object);

            var response = servico.AdicionarLog(log);

            Assert.NotNull(response);
            Assert.IsType<Task<Retorno<SemConteudo>>>(response);

            _mockRepositorio.Verify(
                mock => mock.Create(It.IsAny<Log>()),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public void Atualizar_Log(int id, Log log)
        {
            _mockRepositorio.Setup(mock => mock.Update(It.IsAny<int>(), It.IsAny<Log>()));

            var servico = new LogServico(_mockRepositorio.Object);

            var response = servico.AtualizarLog(id, log);

            Assert.NotNull(response);
            Assert.IsType<Task<Retorno<SemConteudo>>>(response);

            _mockRepositorio.Verify(
                mock => mock.Update(It.IsAny<int>(), It.IsAny<Log>()),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public void Adicionar_Lista_Log(List<Log> logs)
        {
            _mockRepositorio.Setup(mock => mock.CreateList(It.IsAny<List<Log>>()));

            var servico = new LogServico(_mockRepositorio.Object);

            var response = servico.AdicionarListaLogs(logs);

            Assert.NotNull(response);
            Assert.IsType<Task<Retorno<SemConteudo>>>(response);

            _mockRepositorio.Verify(
                mock => mock.CreateList(It.IsAny<List<Log>>()),
                Times.Once);
        }
    }
}
