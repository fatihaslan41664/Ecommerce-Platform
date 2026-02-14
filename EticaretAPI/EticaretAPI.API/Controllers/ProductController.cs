using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Consts;
using EticaretAPI.Application.CustomAttributes;
using EticaretAPI.Application.Features.Commands.Product.CreateProduct;
using EticaretAPI.Application.Features.Commands.Product.DeleteProduct;
using EticaretAPI.Application.Features.Commands.Product.UpdateProduct;
using EticaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowCaseImage;
using EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using EticaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using EticaretAPI.Application.Features.Queries.Product.GetAllProduct;
using EticaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using EticaretAPI.Application.Features.Queries.ProductImageFile.GetProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        readonly IMediator mediator;
        public ProductController(

            IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Reading,
            Definition = "Get All Products")]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Writing,
            Definition = "Create a Products")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Reading,
            Definition = "Get By Id Product")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Updating,
            Definition = "Update a product")]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Deleting,
            Definition = "Delete a Product")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest request)
        {
            try
            {
                DeleteProductCommandResponse response = await mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Updating,
            Definition = "Update Product File")]
        public async Task<IActionResult> Upload(string id)
        {
            var request = new UploadProductImageFileCommandRequest
            {
                Id = id,
                Files = Request.Form.Files
            };

            UploadProductImageFileCommandResponse response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]/{id}")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Reading,
            Definition = "Get all Product Image for one product")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageFileQueryRequest request)
        {
            List<GetProductImageFileQueryResponse> response = await mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Deleting,
            Definition = "Delete Product Image")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageFileRequest request, [FromQuery] string imageId)
        {
            request.imageId = imageId;
            RemoveProductImageFileResponse response = await mediator.Send(request);
            return Ok();
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinitionAttribute(Menu = AuthorizeDefinitionConstants.Product, ActionType = Application.Enums.ActionType.Updating,
            Definition = "Update Show Image")]
        public async Task<IActionResult> ChangeShowCase([FromQuery]ChangeShowCaseCommandImageRequest request)
        {
            ChangeShowCaseCommandImageResponse response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
