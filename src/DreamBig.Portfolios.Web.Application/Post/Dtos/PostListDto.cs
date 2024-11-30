namespace DreamBig.Portfolios.Web.Application.Post.Dtos;

public sealed record PostDto(
    string Id,
    string Title,
    string Link,
    string Date
);