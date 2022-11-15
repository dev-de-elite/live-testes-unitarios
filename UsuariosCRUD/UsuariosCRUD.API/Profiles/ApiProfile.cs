using AutoMapper;
using UsuariosCRUD.API.Dtos.Usuarios;
using UsuariosCRUD.DomainModel.Models;

namespace UsuariosCRUD.API.Profiles;

public class ApiProfile : Profile
{
	public ApiProfile()
	{
        CreateMap<UsuarioInputDto, Usuario>()
            .ForMember(u => u.Nome, opt => opt.MapFrom(dto => $"{dto.PrimeiroNome} {dto.UltimoNome}"));

        CreateMap<Usuario, UsuarioOutputDto>()
            .ForMember(e => e.PrimeiroNome, opt => opt.MapFrom(u => u.Nome.PrimeiroNome))
            .ForMember(e => e.UltimoNome, opt => opt.MapFrom(u => u.Nome.UltimoNome));
    }
}
