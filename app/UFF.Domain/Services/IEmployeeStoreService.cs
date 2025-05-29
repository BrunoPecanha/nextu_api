using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Employee;
using UFF.Domain.Entity;

namespace UFF.Domain.Services
{
    public interface IEmployeeStoreService
    {
        public Task<CommandResult> SendInviteToEmployee(EmployeeStoreSendInviteCommand command);
        public Task<CommandResult> RespondInvite(EmployeeStoreAswerInviteCommand command);
        public Task<CommandResult> GetPendingAndAcceptedInvitesByUser(int id);
        public Task<CommandResult> GetPendingAndAcceptedInvitesByStore(int id);
    }
}