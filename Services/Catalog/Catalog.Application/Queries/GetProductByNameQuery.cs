using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductByNameQuery : IRequest<IList<ProductViewModel>>
{
    public string Name { get; set; }

    public GetProductByNameQuery(string name)
    {
        Name = name;
    }
}