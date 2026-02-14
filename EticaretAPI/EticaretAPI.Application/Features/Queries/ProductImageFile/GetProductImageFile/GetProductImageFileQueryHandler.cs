using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Repositories.ProductRep;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile
{
    public class GetProductImageFileQueryHandler : IRequestHandler<GetProductImageFileQueryRequest, List<GetProductImageFileQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _config;
        readonly IStorage _storage;
        public GetProductImageFileQueryHandler(IProductReadRepository productReadRepository, IConfiguration config, IStorage storage)
        {
            _productReadRepository = productReadRepository;
            _config = config;
            _storage = storage;
        }

        public async Task<List<GetProductImageFileQueryResponse>> Handle(GetProductImageFileQueryRequest request, CancellationToken cancellationToken)
        {
            T.Product? product = await _productReadRepository.Table.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            if (product == null || product.ProductImages == null)
            {
                return new List<GetProductImageFileQueryResponse>(); // Boş liste dön
            }

            return product.ProductImages.Select(p => new GetProductImageFileQueryResponse()
            {
                Path = $"{_config["BaseAWSUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();
        }
    }
}
