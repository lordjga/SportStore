using Catalog.Application.ViewModels;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllTypesQuery : IRequest<IList<ProductTypeViewModel>>
{
}