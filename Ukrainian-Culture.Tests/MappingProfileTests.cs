namespace Ukrainian_Culture.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper
        = new Mapper(new MapperConfiguration(opt => opt.AddProfile(new MappingProfile())));

    [Fact]

}