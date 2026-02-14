using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileRequest : IRequest<RemoveProductImageFileResponse>
    {
        public string id { get; set; }
        public string? imageId { get; set; }
    }
}
