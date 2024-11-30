namespace DreamBig.Portfolios.Web.Application.Profile.Dtos;

public sealed record ProfileDto(
    Guid ProfileId,
    string OwnerName,
    string? Description,
    string? AboutMeContents,
    ContactDto[] Contacts,
    ExperienceDto[] Experiences
);
