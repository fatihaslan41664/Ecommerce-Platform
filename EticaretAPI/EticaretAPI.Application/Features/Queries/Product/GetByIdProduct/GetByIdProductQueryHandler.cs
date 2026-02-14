using EticaretAPI.Application.Repositories.ProductRep;
using MediatR;
using T = EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            T.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name= product.Name,
                Series = product.Series,
                Category = product.Category,
                Price = product.Price,
                Stock = product.Stock,
                Height = product.Height,
                Color = product.Color,
                Description = product.Description,
            };
        }
    }
}
