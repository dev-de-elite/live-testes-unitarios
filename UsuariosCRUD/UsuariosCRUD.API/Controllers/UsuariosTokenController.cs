using Microsoft.AspNetCore.Mvc;
using UsuariosCRUD.API.Dtos.UsuariosToken;
using UsuariosCRUD.DomainService.Services;

namespace UsuariosCRUD.API.Controllers
{
    [Route("api/usuarios/token")]
    [ApiController]
    public class UsuariosTokenController : ControllerBase
    {
        private readonly ILogger<UsuariosTokenController> _logger;
        private readonly IUsuarioAutenticacaoService _usuarioAutenticacaoService;

        public UsuariosTokenController(ILogger<UsuariosTokenController> logger, IUsuarioAutenticacaoService usuarioAutenticacaoService)
        {
            _logger = logger;
            _usuarioAutenticacaoService = usuarioAutenticacaoService;
        }

        /// <summary>
        /// Cria um novo token para o usuário. Se o usuário possuir um token válido, retorna esse token.
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns>Um token válido</returns>
        /// <response code="200">Retorna o token válido do usuário</response>
        /// <response code="400">Ocorreu algum erro ao tentar gerar o token</response>
        [HttpPost("{codigoUsuario}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Post(long codigoUsuario)
        {
            try
            {
                var token = await _usuarioAutenticacaoService.GerarTokenAsync(codigoUsuario);
                var dto = new UsuarioTokenDto(token.Token, token.DataExpiracao);
                return Ok(dto);
            }
            catch (Exception exception)
            {
                var mensagem = $"Erro ao gerar token de usuário ({codigoUsuario}).";
                _logger.LogError(exception, mensagem);
                return BadRequest(mensagem + exception.Message);
            }
        }


        /// <summary>
        /// Retorna um token válido do usuário
        /// </summary>
        /// <param name="codigoUsuario"></param>
        /// <returns>Um token válido</returns>
        /// <response code="200">Retorna o token válido do usuário</response>
        /// <response code="400">Se não for encontrato nenhum token válido</response>
        [HttpGet("{codigoUsuario}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Get(long codigoUsuario)
        {
            try
            {
                var token = await _usuarioAutenticacaoService.ObterTokenValidoAsync(codigoUsuario);
                var dto = new UsuarioTokenDto(token.Token, token.DataExpiracao);
                return Ok(dto);
            }
            catch (Exception exception)
            {
                var mensagem = $"Erro ao consultar token de usuário ({codigoUsuario}).";
                _logger.LogError(exception, mensagem);
                return BadRequest(mensagem + exception.Message);
            }
        }
    }
}
