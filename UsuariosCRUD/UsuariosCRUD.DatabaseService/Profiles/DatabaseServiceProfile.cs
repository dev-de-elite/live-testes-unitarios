using AutoMapper;
using UsuariosCRUD.DatabaseService.Entities;
using UsuariosCRUD.DomainModel.Models;
using UsuariosCRUD.DomainService.Models;

namespace UsuariosCRUD.DatabaseService.Profiles;

internal class DatabaseServiceProfile : Profile
{
    public DatabaseServiceProfile()
    {
        CreateMap<Usuario, UsuarioEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(u => u.Codigo))
            .ForMember(e => e.PrimeiroNome, opt => opt.MapFrom(u => u.Nome.PrimeiroNome))
            .ForMember(e => e.UltimoNome, opt => opt.MapFrom(u => u.Nome.UltimoNome))
            .ForMember(e => e.Tokens, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(u => u.Codigo, opt => opt.MapFrom(e => e.Id))
            .ForMember(u => u.Nome, opt => opt.MapFrom(e => $"{e.PrimeiroNome} {e.UltimoNome}"));

        CreateMap<TokenUsuario, UsuarioTokenEntity>()
            .ForMember(e => e.Usuario, opt => opt.Ignore())
            .ForMember(e => e.UsuarioId, opt => opt.MapFrom(t => t.CodigoUsuario))
            .ReverseMap()
            .ForCtorParam(nameof(TokenUsuario.CodigoUsuario), opt => opt.MapFrom(e => e.UsuarioId));
            //.ForMember(t => t.CodigoUsuario, opt => opt.MapFrom(e => e.UsuarioId));
    }
}
