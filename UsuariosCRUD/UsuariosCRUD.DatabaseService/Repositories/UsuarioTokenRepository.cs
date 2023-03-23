using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UsuariosCRUD.DatabaseService.DataContext;
using UsuariosCRUD.DatabaseService.Entities;
using UsuariosCRUD.DomainService.Models;
using UsuariosCRUD.DomainService.Repositories;

namespace UsuariosCRUD.DatabaseService.Repositories;

public class UsuarioTokenRepository : IUsuarioTokenRepository
{
    private readonly ApplicationDataContext _context;
    private readonly IMapper _mapper;

    public UsuarioTokenRepository(ApplicationDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CriarAsync(TokenUsuario tokenUsuario)
    {
        var entity = _mapper.Map<UsuarioTokenEntity>(tokenUsuario);
        await _context.UsuariosToken.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExisteTokenAtivoPorUsuarioAsync(long codigoUsuario) =>
        _context.UsuariosToken.AnyAsync(t => t.UsuarioId == codigoUsuario && t.DataExpiracao >= DateTime.UtcNow);

    public async Task<TokenUsuario?> ObterValidoPorUsuarioAsync(long codigoUsuario)
    {
        var entity = await _context.UsuariosToken.FirstOrDefaultAsync(t => t.UsuarioId == codigoUsuario && t.DataExpiracao >= DateTime.UtcNow);
        if (entity is null)
            return default;
        var model = _mapper.Map<TokenUsuario>(entity);
        return model;
    }
}
