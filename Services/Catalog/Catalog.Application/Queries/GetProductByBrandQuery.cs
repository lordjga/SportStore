using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductByBrandQuery :  IRequest<IList<ProductViewModel>>
{
    public string Brandname { get; set; }

    public GetProductByBrandQuery(string brandname)
    {
        Brandname = brandname;
    }
}