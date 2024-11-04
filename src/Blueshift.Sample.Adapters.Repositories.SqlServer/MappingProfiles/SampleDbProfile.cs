using AutoMapper;
using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer.MappingProfiles;

/// <summary>
/// AutoMapper profile for mapping Member business entities to their WebAPI model counterparts.
/// </summary>
public class SampleDbProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SampleDbProfile"/> class.
    /// </summary>
    public SampleDbProfile()
    {
        CreateMap<SqlMember, Sample.Entities.Member>().ReverseMap();
        CreateMap<SqlBook, Sample.Entities.Book>().ReverseMap();
        CreateMap<SqlBookLoan, Sample.Entities.BookLoan>().ReverseMap();
    }
}
