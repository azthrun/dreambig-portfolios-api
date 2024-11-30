using DreamBig.Portfolios.Web.Domain.Entities;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IExperienceRepository
{
    Task<IEnumerable<Experience>> GetExperiencesAsync(string profileId, CancellationToken cancellationToken = default);
}