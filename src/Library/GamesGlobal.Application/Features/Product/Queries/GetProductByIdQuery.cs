// In the folder: GamesGlobal.Application.Features.Products.Queries

using GamesGlobal.Application.Features.Product.Responses;
using GamesGlobal.Common.Wrappers;
using MediatR;

namespace GamesGlobal.Application.Features.Product.Queries;

public class GetProductByIdQuery : IRequest<IResult<ProductResponse>>
{
    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }

    public Guid ProductId { get; set; }
}