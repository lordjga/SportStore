using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.ViewModels;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Query;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<ProductTypeViewModel>>
{
    private readonly ITypeRepository _typesRepository;

    public GetAllTypesHandler(ITypeRepository typesRepository)
    {
        _typesRepository = typesRepository;
    }
    public async Task<IList<ProductTypeViewModel>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _typesRepository.GetAll();
        var typesResponseList = ProductMapper.Mapper.Map<IList<ProductTypeViewModel>>(typesList);
        return typesResponseList;
    }
}