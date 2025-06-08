using System.Threading.Tasks;
using UFF.Domain.Commands;

namespace UFF.Domain.Services
{
    public interface IFavoriteService
    {
        public Task<CommandResult> LikeStore(int storeId, int userId);
        public Task<CommandResult> LikeProfessional(int professionalId, int userId);
        public Task<CommandResult> DislikeStore(int storeId, int userId);
        public Task<CommandResult> DislikeProfessional(int professionalId, int userId);       
    }
}