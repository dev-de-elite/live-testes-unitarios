using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsuariosCRUD.API.Dtos.Usuarios;
using UsuariosCRUD.DomainModel.Models;
using UsuariosCRUD.DomainService.Services;

namespace UsuariosCRUD.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ILogger<UsuariosController> _logger;
    private readonly IMapper _mapper;
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(
        ILogger<UsuariosController> logger,
        IMapper mapper,
        IUsuarioService usuarioService)
    {
        _logger = logger;
        _mapper = mapper;
        _usuarioService = usuarioService;
    }

    [HttpGet("{codigo}")]
    public async Task<IActionResult> Get(long codigo)
    {
        try
        {
            var model = await _usuarioService.ObterAsync(codigo);
            var dto = _mapper.Map<UsuarioOutputDto>(model);
            return Ok(dto);
        }
        catch (Exception exception)
        {
            var mensagem = $"Erro ao consultar o usuário ({codigo}).";
            _logger.LogError(exception, mensagem);
            return BadRequest(mensagem + exception.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioInputDto dto)
    {
        try
        {
            var model = _mapper.Map<Usuario>(dto);
            var id = await _usuarioService.CriarAsync(model);
            return Ok(id);
        }
        catch (Exception exception)
        {
            var mensagem = "Erro ao criar o usuário.";
            _logger.LogError(exception, mensagem);
            return BadRequest(mensagem + exception.Message);
        }
    }

    [HttpPut("{codigo}")]
    public async Task<IActionResult> Put(long codigo, [FromBody] UsuarioInputDto dto)
    {
        try
        {
            var model = _mapper.Map<Usuario>(dto);
            model.Codigo = codigo;
            await _usuarioService.AlterarAsync(model);
            return NoContent();
        }
        catch (Exception exception)
        {
            var mensagem = $"Erro ao atualizar o usuário ({codigo}).";
            _logger.LogError(exception, mensagem);
            return BadRequest(mensagem + exception.Message);
        }
    }

    [HttpDelete("{codigo}")]
    public async Task<IActionResult> Delete(long codigo)
    {
        try
        {
            await _usuarioService.RemoverAsync(codigo);
            return NoContent();
        }
        catch (Exception exception)
        {
            var mensagem = $"Erro ao remover o usuário ({codigo}).";
            _logger.LogError(exception, mensagem);
            return BadRequest(mensagem + exception.Message);
        }
    }

    [HttpPatch("{codigo}/senha")]
    public async Task<IActionResult> AtualizarSenha(long codigo, [FromBody] string senha)
    {
        try
        {
            await _usuarioService.AlterarSenhaAsync(codigo, senha);
            return NoContent();
        }
        catch (Exception exception)
        {
            var mensagem = $"Erro ao atualizar a senha do usuário ({codigo}).";
            _logger.LogError(exception, mensagem);
            return BadRequest(mensagem + exception.Message);
        }
    }
}
