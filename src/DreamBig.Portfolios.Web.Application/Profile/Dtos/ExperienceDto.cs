namespace DreamBig.Portfolios.Web.Application.Profile.Dtos;

public sealed record ExperienceDto(
    int Order,
    string Company,
    string Description,
    string StartDate,
    string EndDate
);