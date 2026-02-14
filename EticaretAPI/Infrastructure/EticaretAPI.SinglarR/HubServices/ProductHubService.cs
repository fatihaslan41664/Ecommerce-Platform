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
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubcontext;

        public ProductHubService(IHubContext<ProductHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubcontext.Clients.All.SendAsync(ReciveFunctionNamesService.ProductAddedMessage,message);    
        }
    }
}
