using System;
using System.Threading.Tasks;
using uff.Domain;
using uff.Domain.Commands;
using uff.Domain.Entity;
using uff.service.Properties;

namespace uff.Service
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;

        public CostumerService(ICostumerRepository repository)
        {
            _costumerRepository = repository;
        }

        public async Task<CommandResult> CreateAsync(CostumerCommand command)
        {
            try
            {
                var costumer = new Costumer(command);

                if (!costumer.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                await _costumerRepository.AddAsync(costumer);
                await _costumerRepository.SaveChangesAsync();

                return new CommandResult(true, costumer);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> UpdateAsync(CostumerCommand command)
        {
            try
            {
                var costumer = await _costumerRepository.GetByIdAsync(command.Id ?? 0);

                if (costumer is null)
                    return new CommandResult(false, Resources.NotFound);

                costumer.UpdateAllInfo(command);
                _costumerRepository.Update(costumer);
                await _costumerRepository.SaveChangesAsync();

                return new CommandResult(true, costumer);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> DeleteAsync(int id)
        {
            try
            {
                var costumer = await _costumerRepository.GetByIdAsync(id);
                if (costumer is not null)
                {
                    _costumerRepository.Remove(costumer);
                    await _costumerRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }

            return new CommandResult(true, null);
        }
    }
}