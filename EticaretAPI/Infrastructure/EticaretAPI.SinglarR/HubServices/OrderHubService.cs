using EticaretAPI.Application.Abstraction.Hubs;
using EticaretAPI.SinglarR.Hubs;
using EticaretAPI.SinglarR.ReciveFunctionNames;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.SinglarR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubcontext;

        public OrderHubService(IHubContext<OrderHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
            await _hubcontext.Clients.All.SendAsync(ReciveFunctionNamesService.OrderAddedMessage, message);
        }
    }
}
