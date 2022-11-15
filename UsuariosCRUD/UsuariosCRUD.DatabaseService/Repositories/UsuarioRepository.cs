using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UsuariosCRUD.DatabaseService.DataContext;
using UsuariosCRUD.DatabaseService.Entities;
using UsuariosCRUD.DomainModel.Models;
using UsuariosCRUD.DomainService.Repositories;

namespace UsuariosCRUD.DatabaseService.Repositories;

internal class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDataContext _context;
    private readonly IMapper _mapper;

    public UsuarioRepository(ApplicationDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AlterarAsync(Usuario usuario)
    {
        var entity = _mapper.Map<UsuarioEntity>(usuario);

        _context.Usuarios.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<long> CriarAsync(Usuario usuario)
    {
        var entity = _mapper.Map<UsuarioEntity>(usuario);

        await _context.Usuarios.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Usuario?> ObterAsync(long codigo)
    {
        var entity = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == codigo);
        if (entity is null)
            return default;
        _context.Entry(entity).State = EntityState.Detached;
        var model = _mapper.Map<Usuario?>(entity);
        return model;
    }

    public async Task RemoverAsync(Usuario usuario)
    {
        // TODO BECHARA: Refazer esse processo quando lançar o EF7
        var entity = _mapper.Map<UsuarioEntity>(usuario);
        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
