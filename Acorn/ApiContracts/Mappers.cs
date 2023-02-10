using AutoMapper;

namespace Acorn.ApiContracts;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Domain.Entities.Post.Post, Post>();
    }
}