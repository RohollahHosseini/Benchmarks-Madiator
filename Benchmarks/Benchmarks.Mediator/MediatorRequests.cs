using Mediator;

namespace Benchmarks.Mediator
{
    public record MediatorRequests (string message):IRequest<string>
    {
    }

    public class MediatorRequestsHandler : IRequestHandler<MediatorRequests, string>
    {
        public ValueTask<string> Handle(MediatorRequests request, CancellationToken cancellationToken)
        {
            return ValueTask.FromResult<string>($"MediatR Request Handler To Request Message {request.message}");
        }
    }
}
