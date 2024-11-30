using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Application.Profile.Dtos;
using Mediator;

namespace DreamBig.Portfolios.Web.Application.Profile.Queries;

public sealed class GetProfileInfoQuery : IRequest<ProfileDto?>
{
}

internal sealed class GetProfileInfoHandler(
    IContactRepository contactRepository,
    IExperienceRepository experienceRepository,
    IProfileRepository profileRepository
) : IRequestHandler<GetProfileInfoQuery, ProfileDto?>
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private readonly IExperienceRepository _experienceRepository = experienceRepository;
    private readonly IProfileRepository _profileRepository = profileRepository;

    public async ValueTask<ProfileDto?> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetProfileAsync(cancellationToken);
        if (profile is null)
        {
            return null;
        }

        var profileId = profile.Id.ToString();
        var contactsLookup = _contactRepository.GetContactsAsync(profileId, cancellationToken);
        var contactTypesLookup = _contactRepository.GetContactTypesAsync();
        var experienceLookup = _experienceRepository.GetExperiencesAsync(profileId, cancellationToken);
        await Task.WhenAll(contactsLookup, contactTypesLookup, experienceLookup);
        var contacts = await contactsLookup;
        var contactTypes = await contactTypesLookup;
        var experiences = await experienceLookup;

        return new ProfileDto(
            profile.Id,
            profile.Name,
            profile.Description,
            profile.AboutMe,
            [.. contacts.Select(c => new ContactDto(contactTypes.First(ct => ct.Id == c.ContactTypeId).Description, c.Link))],
            [.. experiences.OrderByDescending(e => e.StartDate).Select((e, idx) => new ExperienceDto(idx, e.Company, e.Description, e.StartDate.ToString("yyyy-MMMM"), e.EndDate?.ToString("yyyy-MMMM") ?? "Present"))]
        );
    }
}