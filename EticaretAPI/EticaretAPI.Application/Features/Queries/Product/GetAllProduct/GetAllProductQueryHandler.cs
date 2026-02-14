using EticaretAPI.Application.Repositories.ProductRep;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EticaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _repository;
        readonly ILogger<GetAllProductQueryHandler> _logger;
        readonly IConfiguration _config; // ✅ EKLEYIN

        public GetAllProductQueryHandler(
            IProductReadRepository repository,
            ILogger<GetAllProductQueryHandler> logger,
            IConfiguration config) // ✅ INJECT EDİN
        {
            _repository = repository;
            _logger = logger;
            _config = config; // ✅ ATAYIN
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll(false);
            var totalCount = await query.CountAsync(cancellationToken);

            // ✅ Önce veritabanından çek
            var products = await query
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .Include(p => p.ProductImages)
                .ToListAsync(cancellationToken);

            // ✅ Sonra memory'de map et
            var result = products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Series,
                p.Category,
                p.Price,
                p.Stock,
                p.Height,
                p.Color,
                p.Description,
                ProductImages = p.ProductImages?.Select(img => new
                {
                    img.Id,
                    img.FileName,
                    img.showCase,
                    img.Storage,
                    Path = $"{_config["BaseAWSUrl"]}/{img.Path}"
                }).ToList()
            }).ToList();

            _logger.LogInformation("urunler gosterildi ...");

            return new()
            {
                Products = result,
                Totalcount = totalCount
            };
        }
    }
}
