using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IList<ProductViewModel>>
    {
    }
}
