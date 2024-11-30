using PostEntity = DreamBig.Portfolios.Web.Domain.Entities.Post;

namespace DreamBig.Portfolios.Web.Application.Abstractions;

public interface IPostRepository
{
    Task<IEnumerable<PostEntity>> GetPostsAsync(string profileId, CancellationToken cancellationToken = default);
}