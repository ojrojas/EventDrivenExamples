using MediatR;

namespace EventDrivenDesign.Rest2.Queries.Commands
{
    public record CreateUserCommand(Guid Id, string Name) : IRequest<bool> {}
}