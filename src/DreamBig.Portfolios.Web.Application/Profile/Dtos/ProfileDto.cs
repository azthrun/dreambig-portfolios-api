namespace DreamBig.Portfolios.Web.Application.Profile.Dtos;

public sealed record ProfileDto(
    string OwnerName,
    string? Description,
    ContactDto[] Contacts
);