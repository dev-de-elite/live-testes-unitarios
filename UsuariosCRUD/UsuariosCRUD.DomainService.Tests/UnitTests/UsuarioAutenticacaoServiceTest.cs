using Moq;
using FluentAssertions;
using UsuariosCRUD.DomainService.Models;
using UsuariosCRUD.DomainService.Repositories;
using UsuariosCRUD.DomainService.Services;
using UsuariosCRUD.DomainService.Exceptions;

namespace UsuariosCRUD.DomainService.Tests.UnitTests
{
    [TestClass]
    public class UsuarioAutenticacaoServiceTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, DisplayName = "Caso teste id 1")]
        [DataRow(5)]
        [DataRow(99)]
        [DataRow(-859425)]
        public async Task ObterTokenValidoAsync_DeveRetornar_TokenValido(long codigoUsuario)
        {
            // Arrange
            var tokenRepositoryMock = new Mock<IUsuarioTokenRepository>(MockBehavior.Strict);
            tokenRepositoryMock.Setup(m => m.ExisteTokenAtivoPorUsuarioAsync(It.IsAny<long>()))
                .ReturnsAsync(true);
            tokenRepositoryMock.Setup(m => m.ObterValidoPorUsuarioAsync(It.IsAny<long>()))
                .ReturnsAsync(new TokenUsuario(
                    CodigoUsuario: codigoUsuario,
                    Token: "token",
                    DataExpiracao: DateTime.Today));

            var service = new UsuarioAutenticacaoService(
                usuarioRepository: null!,
                autenticacaoService: null!,
                usuarioTokenRepository: tokenRepositoryMock.Object);

            //Act            
            var tokenUsuario = await service.ObterTokenValidoAsync(codigoUsuario);

            //Assert
            tokenUsuario
                .Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    CodigoUsuario = codigoUsuario,
                    Token = "token",
                    DataExpiracao = DateTime.Today,
                });
            tokenRepositoryMock.Verify(m => m.ExisteTokenAtivoPorUsuarioAsync(It.IsAny<long>()), Times.Once);
            tokenRepositoryMock.Verify(m => m.ObterValidoPorUsuarioAsync(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public async Task ObterTokenValidoAsync_DeveRetornarErro_TokenNaoEncontrado()
        {
            //Arrange
            var tokenRepositoryMock = new Mock<IUsuarioTokenRepository>(MockBehavior.Strict);
            tokenRepositoryMock.Setup(m => m.ExisteTokenAtivoPorUsuarioAsync(It.IsAny<long>()))
                .ReturnsAsync(false);

            var service = new UsuarioAutenticacaoService(
                usuarioRepository: null!,
                autenticacaoService: null!,
                usuarioTokenRepository: tokenRepositoryMock.Object);

            //Act + Assert
            await Assert.ThrowsExceptionAsync<DomainServiceException>(
                async () => await service.ObterTokenValidoAsync(0L));
            tokenRepositoryMock
                .Verify(m =>
                    m.ExisteTokenAtivoPorUsuarioAsync(It.IsAny<long>()),
                    Times.Once);
        }
    }
}