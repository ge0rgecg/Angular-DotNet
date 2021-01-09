using ApiDotNet.Controllers;
using AutoFixture.Xunit2;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Servico.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ApiDotNet.test.Controllers
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

        [Fact]
        public void Get_Log()
        {
            _mockServico.Setup(mock => mock.ObterLogs())
                .Returns(Task.FromResult(new Retorno<IEnumerable<Log>>()));

            var controller = new LogController(_mockServico.Object);

            var response = controller.Get();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            _mockServico.Verify(
                mock => mock.ObterLogs(),
                Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void Get_Log_With_Id
        (
            int id
        )
        {
            _mockServico.Setup(mock => mock.ObterLog(It.IsAny<int>()))
                .Returns(Task.FromResult(new Retorno<Log>()));

            var controller = new LogController(_mockServico.Object);

            var response = controller.Get(id);

            if (id == 1)
            {
                var objectResult = Assert.IsType<OkObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status200OK);
            }
            else
            {
                var objectResult = Assert.IsType<BadRequestObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status400BadRequest);
            }


            _mockServico.Verify(
                mock => mock.ObterLog(It.IsAny<int>()),
                Times.Exactly(id));
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void Post_Log(bool hasValue, Log log)
        {
            _mockServico.Setup(mock => mock.AdicionarLog(It.IsAny<Log>()))
                .Returns(Task.FromResult(new Retorno<SemConteudo>()));

            var controller = new LogController(_mockServico.Object);

            log.CodigoRetorno = 400;

            var response = controller.Post(hasValue ? log : new Log());

            if (hasValue)
            {
                var objectResult = Assert.IsType<OkObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status200OK);
                _mockServico.Verify(
                    mock => mock.AdicionarLog(It.IsAny<Log>()),
                    Times.Once);
            }
            else
            {
                var objectResult = Assert.IsType<BadRequestObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status400BadRequest);
                _mockServico.Verify(
                    mock => mock.AdicionarLog(It.IsAny<Log>()),
                    Times.Never);
            }
        }

        [Theory]
        [InlineAutoData(true, 1)]
        [InlineAutoData(true, 0)]
        [InlineAutoData(false, 1)]
        public void Put_Log(bool hasValue, int id, Log log)
        {
            _mockServico.Setup(mock => mock.AtualizarLog(It.IsAny<int>(), It.IsAny<Log>()))
                .Returns(Task.FromResult(new Retorno<SemConteudo>()));

            var controller = new LogController(_mockServico.Object);

            log.CodigoRetorno = 400;

            var response = controller.Put(id, hasValue ? log : new Log());

            if (hasValue && id == 1)
            {
                var objectResult = Assert.IsType<OkObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status200OK);
                _mockServico.Verify(
                    mock => mock.AtualizarLog(It.IsAny<int>(), It.IsAny<Log>()),
                    Times.Once);
            }
            else
            {
                var objectResult = Assert.IsType<BadRequestObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status400BadRequest);
                _mockServico.Verify(
                    mock => mock.AtualizarLog(It.IsAny<int>(), It.IsAny<Log>()),
                    Times.Never);
            }
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void Post_File_Log(bool hasCorrectType)
        {
            var fileMock = new Mock<IFormFile>();
            var content = "216.239.46.60 - - [04/Jan/2003:14:56:50 +0200] \"GET / blablablabla\" 300 300";
            var fileName = "file.txt";
            var contentType = hasCorrectType ? "text/plain" : "";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(s => s.OpenReadStream()).Returns(ms);
            fileMock.Setup(s => s.FileName).Returns(fileName);
            fileMock.Setup(s => s.ContentType).Returns(contentType);

            var file = fileMock.Object;

            _mockServico.Setup(mock => mock.AdicionarListaLogs(It.IsAny<List<Log>>()))
                .Returns(Task.FromResult(new Retorno<SemConteudo>()));

            var controller = new LogController(_mockServico.Object);

            var response = controller.Post(new List<IFormFile> { file });

            if (hasCorrectType)
            {
                var objectResult = Assert.IsType<OkObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status200OK);
                _mockServico.Verify(
                    mock => mock.AdicionarListaLogs(It.IsAny<List<Log>>()),
                    Times.Once);
            }
            else
            {
                var objectResult = Assert.IsType<BadRequestObjectResult>(response);
                Assert.Equal(objectResult.StatusCode, StatusCodes.Status400BadRequest);
                _mockServico.Verify(
                    mock => mock.AdicionarListaLogs(It.IsAny<List<Log>>()),
                    Times.Never);
            }
        }
    }
}
