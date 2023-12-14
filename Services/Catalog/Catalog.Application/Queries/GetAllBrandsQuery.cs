using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<ProductBrandViewModel>>
    {
    }
}
