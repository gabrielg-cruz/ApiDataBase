using Xunit;
using Microsoft.AspNetCore.Mvc;
using ApiBase.Controllers;
using ApiBase.Models;
using ApiBase.Services.Interfaces;
using Moq;

namespace ApiBase.Tests.Controllers
{
    public class PessoasControllerTests
    {
        private readonly PessoasController _controller;
        private readonly Mock<IPessoasService> _serviceMock;

        public PessoasControllerTests()
        {
            _serviceMock = new Mock<IPessoasService>();
            _controller = new PessoasController(_serviceMock.Object);
        }

        [Fact]
        public void ObterPorId_RetornaNaoEncontradoQuandoIDnull()
        {
            // Arrange
            int id = 1;
            _serviceMock.Setup(x => x.ObterPorId(id)).Returns((Pessoas)null);

            // Act
            var result = _controller.ObterPorId(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1, "John Doe")]
        [InlineData(2, "Jane Doe")]
        [InlineData(15, "Doe John")]
        public void ObterPorId_RetornaOkResultado_QuandoPessoaNaoNull(int id, string nome)
        {
            // Arrange
            _serviceMock.Setup(x => x.ObterPorId(id)).Returns(pessoa);

            // Act
            var result = _controller.ObterPorId(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void ObterPorNome_RetornaBadRequestQuandoNomeVazio()
        {
            // Arrange
            string nome = "";

            // Act
            var result = _controller.ObterPorNome(nome);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void ObterPorNome_RetornaBadRequestQuandoNomeNull()
        {
            // Arrange
            string nome = null;

            // Act
            var result = _controller.ObterPorNome(nome);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Theory]
        [InlineData("John Doe")]
        [InlineData("Jane Doe")]
        public void ObterPorNome_RetornaOkResultado_QuandoPessoaNaoNull(string nome)
        {
            // Arrange
            var pessoa = new Pessoa { Nome = nome, Idade = 30, Email = "john.doe@example.com" };
            _serviceMock.Setup(x => x.ObterPorNome(nome)).Returns(pessoa);

            // Act
            var result = _controller.ObterPorNome(nome);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPessoa = Assert.IsType<Pessoa>(okResult.Value);
            Assert.Equal(nome, returnedPessoa.Nome);
            Assert.Equal(30, returnedPessoa.Idade);
            Assert.Equal("john.doe@example.com", returnedPessoa.Email);
        }
    }
}