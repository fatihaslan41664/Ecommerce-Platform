using EticaretAPI.Application.Repositories.ProductRep;
using EticaretAPI.Domain.Entities;
using MediatR;
using ProductEntity = EticaretAPI.Domain.Entities.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Abstraction.Hubs;

namespace EticaretAPI.Application.Features.Commands.Product.CreateProduct
{   

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly IProductHubService _productHubService;
        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            this.productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await productWriteRepository.AddAsync(new ProductEntity
            {
                Name = request.Name,
                Series = request.Series,
                Category = request.Category,
                Price = request.Price,
                Stock = request.Stock,
                Height = request.Height,
                Color = request.Color,
                Description = request.Description,

            });
            await productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde urun eklendi");
            return new(); 
        }
    }
}
