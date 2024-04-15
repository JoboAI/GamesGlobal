using AutoMapper;
using GamesGlobal.Application.Features.Product.Queries;
using GamesGlobal.Application.Features.Product.Responses;
using GamesGlobal.Common.Wrappers;
using GamesGlobal.Core.Interfaces.Repositories;
using MediatR;

namespace GamesGlobal.Application.Features.Product.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, IResult<ProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IResult<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            return await Result<ProductResponse>.FailAsync($"Product with ID {request.ProductId} not found.");

        var productResponse = _mapper.Map<ProductResponse>(product);
        return await Result<ProductResponse>.SuccessAsync(productResponse);
    }
}