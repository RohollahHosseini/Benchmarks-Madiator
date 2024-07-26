namespace Benchmarks.Mediator
{
    public record MediatRRequests(string message):MediatR.IRequest<string>
    {
    }


    public class MediatRRequestsHanderl : MediatR.IRequestHandler<MediatRRequests, string>
    {
        public Task<string> Handle(MediatRRequests request, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>($"MediatR Request Handler To Request Message {request.message}");
        }
    }
}
