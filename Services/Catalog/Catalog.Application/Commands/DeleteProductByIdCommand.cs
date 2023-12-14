using MediatR;

namespace Catalog.Application.Queries;

public class DeleteProductByIdCommand : IRequest<bool>
{
    public string Id { get; set; }

    public DeleteProductByIdCommand(string id)
    {
        Id = id;
    }
}