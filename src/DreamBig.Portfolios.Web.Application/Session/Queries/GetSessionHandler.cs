using DreamBig.Portfolios.Web.Application.Abstractions;
using DreamBig.Portfolios.Web.Application.Session.Dtos;
using Mediator;

namespace DreamBig.Portfolios.Web.Application.Session.Queries;

public sealed class GetSessionQuery : IRequest<SessionResponseDto?>
{
    public string? UserAgent { get; set; }
}

internal sealed class GetSessionHandler(
    ISessionRepository sessionRepository
) : IRequestHandler<GetSessionQuery, SessionResponseDto?>
{
    private readonly ISessionRepository _sessionRepository = sessionRepository;

    public async ValueTask<SessionResponseDto?> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var newSessionId = await _sessionRepository.CreateSessionAsync(request.UserAgent, cancellationToken);
        return new(newSessionId);
    }
}