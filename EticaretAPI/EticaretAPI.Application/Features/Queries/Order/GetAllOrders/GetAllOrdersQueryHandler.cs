using EticaretAPI.Application.Abstraction.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EticaretAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            // Tüm veriyi çek
            var allOrders = await _orderService.GetAllOrdersAsync();

            // Toplam sayıyı al
            var totalCount = allOrders.Count;

            // Pagination uygula
            var paginatedOrders = allOrders
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.TotalPrice,
                    UserName = o.UserName,
                    CompletedDateTime = o.CompletedDateTime,
                    IsCompleted = o.IsCompleted
                })
                .ToList();

            return new GetAllOrdersQueryResponse
            {
                Orders = paginatedOrders,
                TotalCount = totalCount
            };
        }
    }
}