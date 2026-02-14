using EticaretAPI.Application.Repositories.ProductRep;
using MediatR;
using T = EticaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileHandler : IRequestHandler<RemoveProductImageFileRequest, RemoveProductImageFileResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        public RemoveProductImageFileHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task<RemoveProductImageFileResponse> Handle(RemoveProductImageFileRequest request, CancellationToken cancellationToken)
        {
            T.Product? product = await _productReadRepository.Table.Include(p => p.ProductImages)
    .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.id));
            T.ProductImageFile? productImageFile = product?.ProductImages.FirstOrDefault(p => p.Id == Guid.Parse(request.imageId));
            if (productImageFile != null) 
                product?.ProductImages.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
