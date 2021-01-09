using ApiDotNet.Controllers;
using AutoFixture.Xunit2;
using Dominio;
using Moq;
using Servico.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.test.Controllers
{
    public class LogControllerTest
    {
        private readonly Mock<ILogServico> _mockServico = new Mock<ILogServico>();


        [Fact]
        public void Constructor_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => new LogController(null)
            );
        }

        [Theory]
        [AutoData]
        public async Task Post_Log(int numeroComanda)
        {
            _mockServico.Setup(mock => mock.AdicionarLog(It.IsAny<Log>()))
                .Returns(Task.FromResult(new Retorno<SemConteudo>()));

            var controller = new LogController(_mockServico.Object);

            var response = "";// await controller.Post(new Log());

            Assert.NotNull(response);
            Assert.IsType<Retorno<SemConteudo>>(response);

            _mockServico.Verify(
                mock => mock.AdicionarLog(It.IsAny<Log>()),
                Times.Once);
        }
    }
}
