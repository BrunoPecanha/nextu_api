using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Service.Hubs
{
    //  [Authorize]
    public class QueueHub : Hub
    {
        private readonly IUserRepository _userRepository;

        public QueueHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Conexão {Context.ConnectionId} entrou no grupo {groupName}");
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst("sub")?.Value;

            //if (userId != null)
            //{
            //    var user = await _userRepository.GetByIdAsync(int.Parse(userId));
              //  var companyId = user.Id;

                await Groups.AddToGroupAsync(Context.ConnectionId, $"company-{2}");

                await base.OnConnectedAsync();
          //  }
        }

        public async Task AlertNewPersonInQueue(string companyId, object data)
        {
            await Clients.Group($"company-{2}").SendAsync("UpdateQueue", data);
        }
    }
}