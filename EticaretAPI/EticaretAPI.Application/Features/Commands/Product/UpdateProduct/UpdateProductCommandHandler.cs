using EticaretAPI.Application.Repositories.ProductRep;
using T =EticaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EticaretAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepostirory;
        readonly IProductWriteRepository _productWriteRepostirory;
        readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepostirory, IProductWriteRepository productWriteRepostirory, ILogger<UpdateProductCommandHandler> logger)
        {
            _productReadRepostirory = productReadRepostirory;
            _productWriteRepostirory = productWriteRepostirory;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            T.Product product = await _productReadRepostirory.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Series = request.Series;
            product.Category = request.Category;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Height = request.Height;
            product.Color = request.Color;
            product.Description = request.Description;
            _logger.LogInformation("Product güncellendi");
            await _productWriteRepostirory.SaveAsync();
            return new();
        }
    }
}
