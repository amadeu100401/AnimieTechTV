using AnimieTechTv.Application.Commad.Animie.Create;
using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Domain.DTOs;
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

    public static GetAnimieCommand GetValidGetAnimieCommand(bool isWithFilter = false, PaginationDTO? pagination = null)
    {
        if (pagination == null)
        {
            pagination = new PaginationDTO
            {
                PageNumber = 1,
                PageSize = 10
            };
        }

        var command = new GetAnimieCommand(pagination);

        if (isWithFilter)
        {
            command.Director = _faker.Name.FullName();
            command.Name = _faker.Lorem.Word();
        }

        return command;
    }
}
