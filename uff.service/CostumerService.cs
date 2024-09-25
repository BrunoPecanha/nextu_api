using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using uff.Domain;
using uff.Domain.Commands;
using uff.Domain.Dto;
using uff.Domain.Entity;
using uff.service.Properties;

namespace uff.Service
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        private readonly IMapper _mapper;

        public CostumerService(ICostumerRepository repository, IMapper mapper)
        {
            _costumerRepository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var costumers = await _costumerRepository.GetAllAsync();

            if (costumers is null || costumers.Count() == 0)
                return new CommandResult(false, costumers);

            return new CommandResult(true, costumers);
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var costumer = await _costumerRepository.GetByIdAsync(id);

            if (costumer is null )
                return new CommandResult(false, costumer);

            return new CommandResult(true, costumer);
        }

        public async Task<CommandResult> CreateAsync(CostumerCreateCommand command)
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

        public async Task<CommandResult> UpdateAsync(CostumerEditCommand command)
        {
            try
            {
                var costumer = await _costumerRepository.GetByIdAsync(command.Id);

                if (costumer is null)
                    return new CommandResult(false, Resources.NotFound);

                costumer.UpdateAllInfo(command);
                _costumerRepository.Update(costumer);
                await _costumerRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<CostumerDto>(costumer));
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