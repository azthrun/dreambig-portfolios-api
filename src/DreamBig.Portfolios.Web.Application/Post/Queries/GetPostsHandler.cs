using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Application.Post.Dtos;
using Mediator;

public sealed class GetPostsQuery : IRequest<PostDto[]?>
{
    public string? ProfileId { get; set; }
}

public sealed class GetPostsHandler(
    IPostRepository postRepository
) : IRequestHandler<GetPostsQuery, PostDto[]?>
{
    private readonly IPostRepository _postRepository = postRepository;

    public async ValueTask<PostDto[]?> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ProfileId))
        {
            throw new ArgumentNullException(nameof(GetPostsQuery.ProfileId));
        }
        var posts = await _postRepository.GetPostsAsync(request.ProfileId, cancellationToken);
        return posts?.Select(p => new PostDto(p.Id.ToString(), p.Title, p.Link, p.UpdateTime.ToString("yyyy-MM-dd")))?.ToArray();
    }
}
