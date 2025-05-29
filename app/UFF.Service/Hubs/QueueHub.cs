using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Repository;

namespace UFF.Service.Hubs
{
    public class QueueHub : Hub
    {
        private readonly IUserRepository _userRepository;

        public QueueHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task JoinGroup(string companyId)
        {
            var groupName = $"company-{companyId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Conexão {Context.ConnectionId} entrou no grupo {groupName}");
        }      

        public async Task JoinGroups(List<string> companyIds)
        {
            foreach (var companyId in companyIds)
            {
                var groupName = $"company-{companyId}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine($"Conexão {Context.ConnectionId} entrou no grupo {groupName}");
            }
        }

        public async Task LeaveGroup(string companyId)
        {
            var groupName = $"company-{companyId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Conexão {Context.ConnectionId} saiu do grupo {groupName}");
        }

        public async Task AlertNewPersonInQueue(string companyId, object data)
        {
            var groupName = $"company-{companyId}";
            Console.WriteLine($"Enviando atualização para o grupo {groupName}");

            await Clients.Group(groupName).SendAsync("UpdateQueue", new
            {
                CompanyId = companyId,
                QueueData = data
            });
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Nova conexão: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Conexão {Context.ConnectionId} desconectada");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
