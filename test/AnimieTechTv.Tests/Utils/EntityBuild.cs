using AnimieTechTv.Application.Commad.Animie;
using Bogus;

namespace AnimieTechTv.Tests.Utils;

public static class EntityBuild
{
    private static readonly Faker _faker = new Faker();

    public static CreateAnimieCommand GetValidCreateAnimieCommand()
    {
        return new CreateAnimieCommand
        {
            Name = _faker.Lorem.Word(),
            Director = _faker.Name.FullName(),
            Resume = _faker.Lorem.Paragraph()
        };
    }
}
