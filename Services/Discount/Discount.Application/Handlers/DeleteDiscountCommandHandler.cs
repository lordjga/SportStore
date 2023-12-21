using Discount.Application.Commands;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await _discountRepository.DeleteDiscount(request.ProductName);

        if (!result)
        {
            throw new RpcException(new Status(StatusCode.Unknown,
                $"Discount with the product name = {request.ProductName} wasn't deleted"));
        }

        return result;
    }
}