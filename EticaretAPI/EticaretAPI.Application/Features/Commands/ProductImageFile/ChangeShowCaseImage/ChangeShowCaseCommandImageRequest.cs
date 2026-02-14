using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowCaseImage
{
    public class ChangeShowCaseCommandImageRequest:IRequest<ChangeShowCaseCommandImageResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
