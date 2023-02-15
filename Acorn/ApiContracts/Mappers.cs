using AutoMapper;

namespace Acorn.ApiContracts;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Domain.Entities.Post.Post, Post>()
            .ForPath(a => a.Tags, a => a.MapFrom(p => p.Tags.Select(s => s.Name)));
        CreateMap<Domain.Entities.Tag.Tag, Tag>();
        CreateMap<Domain.Entities.Category.Category, Category>();
    }
}