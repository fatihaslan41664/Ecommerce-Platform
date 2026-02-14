using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Repositories.OrderRep;
using EticaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;
        readonly IOrderReadRepository _orderReadRepository;
        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService, IOrderReadRepository orderReadRepository)
        {
            _orderService = orderService;
            _mailService = mailService;
            _orderReadRepository = orderReadRepository;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _orderService.CompleteOrder(request.Id);

            if (result) {
                var order = await _orderReadRepository.GetOrderWithUserDetailsAsync(request.Id);
                await _mailService.SendMailCompletedOrderMailAsync(order.Basket.User.Email, order.OrderCode, order.CreatedDate, order.Basket.User.NameSurname);
            }
            return new CompleteOrderCommandResponse(); 
        }
    }
}
//Task SendMailCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName, string userSurname);