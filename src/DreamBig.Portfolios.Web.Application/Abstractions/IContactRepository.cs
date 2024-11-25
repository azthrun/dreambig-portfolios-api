using DreamBig.Portfolios.Web.Domain.Entities;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetContactsAsync(string profileId);
    Task<ContactType?> GetContactTypeAsync(string contactTypeId);
}