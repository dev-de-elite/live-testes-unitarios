using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UsuariosCRUD.DatabaseService.DataContext;
using UsuariosCRUD.DatabaseService.Entities;
using UsuariosCRUD.DatabaseService.Profiles;
using UsuariosCRUD.DatabaseService.Repositories;
using UsuariosCRUD.DomainService.Exceptions;
using UsuariosCRUD.DomainService.Services;

namespace UsuariosCRUD.DomainService.Tests.IntegrationTests
{
    [TestClass]
    public class UsuarioAutenticacaoServiceTest
    {
        private ApplicationDataContext _context = default!;
        private UsuarioTokenRepository _tokenRepository = default!;

        [TestInitialize]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDataContext>()
                                    //.UseInMemoryDatabase(Guid.NewGuid().ToString())
                                    .UseInMemoryDatabase("bancoTeste")
                                    .Options;
            _context = new ApplicationDataContext(dbOptions);
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new DatabaseServiceProfile()));
            _tokenRepository = new UsuarioTokenRepository(
                context: _context,
                mapper: new Mapper(mapperConfig));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task ObterTokenValidoAsync_DeveRetornar_TokenValido()
        {
            //Arrange
            var usuarioEntity = await _context.Usuarios.AddAsync(new UsuarioEntity
            {
                PrimeiroNome = "Thiago",
                UltimoNome = "Bechara",
                Email = "teste@teste.com",
                NomeUsuario = "thiago.bechara",
                Senha = "12345678"
            });
            var idUsuario = usuarioEntity.Entity.Id;
            var token = Guid.NewGuid().ToString();
            var dataExpiracao = DateTime.UtcNow.AddDays(1);
            await _context.UsuariosToken.AddAsync(new UsuarioTokenEntity
            {
                Token = token,
                DataExpiracao = dataExpiracao,
                UsuarioId = idUsuario
            });
            await _context.SaveChangesAsync();

            var service = new UsuarioAutenticacaoService(
                usuarioRepository: null!,
                autenticacaoService: null!,
                usuarioTokenRepository: _tokenRepository);

            //Act            
            var tokenUsuario = await service.ObterTokenValidoAsync(idUsuario);

            //Assert
            tokenUsuario
                .Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    CodigoUsuario = idUsuario,
                    Token = token,
                    DataExpiracao = dataExpiracao,
                });
        }

        [TestMethod]
        public async Task ObterTokenValidoAsync_DeveRetornarErro_TokenNaoEncontrado()
        {
            //Arrange
            var service = new UsuarioAutenticacaoService(
                usuarioRepository: null!,
                autenticacaoService: null!,
                usuarioTokenRepository: _tokenRepository);

            //Act + Assert
            await Assert.ThrowsExceptionAsync<DomainServiceException>(
                async () => await service.ObterTokenValidoAsync(0L));
        }
    }
}
