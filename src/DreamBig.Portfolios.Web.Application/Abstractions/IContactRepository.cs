using DreamBig.Portfolios.Web.Domain.Entities;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetContactsAsync(string profileId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ContactType>> GetContactTypesAsync(CancellationToken cancellationToken = default);
}