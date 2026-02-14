using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowCaseImage
{
    public class ChangeShowCaseCommandImageHandler : IRequestHandler<ChangeShowCaseCommandImageRequest, ChangeShowCaseCommandImageResponse>
    {
        readonly IProductImageFileWriteRepository _imageFileRepository;

        public ChangeShowCaseCommandImageHandler(IProductImageFileWriteRepository imageFileRepository)
        {
            _imageFileRepository = imageFileRepository;
        }

        public async Task<ChangeShowCaseCommandImageResponse> Handle(ChangeShowCaseCommandImageRequest request, CancellationToken cancellationToken)
        {
            var query = _imageFileRepository.Table.Include(p => p.Product)
                .SelectMany(p => p.Product, (pif, p) => new
                {
                    pif,
                    p
                }
                );
            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.showCase);
            if(data!=null)
                data.pif.showCase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            image.pif.showCase = true;

            await _imageFileRepository.SaveAsync();
            return new ChangeShowCaseCommandImageResponse();
        }
    }
}
