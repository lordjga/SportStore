using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductByIdQuery: IRequest<ProductViewModel>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}