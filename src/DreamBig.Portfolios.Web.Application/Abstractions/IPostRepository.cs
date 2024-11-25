using DreamBig.Portfolios.Web.Domain.Entities;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync(string profileId);
}