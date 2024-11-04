using AutoMapper;
using Blueshift.Sample.Entities;
using Blueshift.Sample.WebAPI.Models.Books;
using Blueshift.Sample.WebAPI.Models.Members;

namespace Blueshift.Sample.WebAPI.MappingProfiles;

/// <summary>
/// AutoMapper profile for mapping Member business entities to their WebAPI model counterparts.
/// </summary>
public class WebApiModelsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiModelsProfile"/> class.
    /// </summary>
    public WebApiModelsProfile()
    {
        CreateMap<Member, MemberModel>().ReverseMap();
        CreateMap<CreateMemberRequest, Member>();
        CreateMap<UpdateMemberRequest, Member>();

        CreateMap<Book, BookModel>().ReverseMap();
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
    }
}
