using ProfileEntity = DreamBig.Portfolios.Web.Domain.Entities.Profile;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IProfileRepository
{
    Task<ProfileEntity?> GetProfileAsync(CancellationToken cancellationToken = default);
}