using Microsoft.EntityFrameworkCore;
using supply.service.Properties;
using Supply.Domain;
using Supply.Domain.Commands;
using Supply.Domain.Entity;
using Supply.Repository.Context;
using System;
using System.Threading.Tasks;

namespace Supply.Service
{
    public class CostumerService : ServiceBase<Costumer>, ICostumerService
    {
        private readonly ISupplyContext _context;
        private readonly ICostumerRepository _costumerRepository;

        public CostumerService(ICostumerRepository repository, ISupplyContext context)
            : base(repository)
        {
            _context = context;
            _costumerRepository = repository;
        }

        public async Task<CommandResult> CreateAsync(CostumerCommand command)
        {
            try
            {
                var costumer = new Costumer(command);

                if (!costumer.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                await _context.Costumer.AddAsync(costumer);
                _context.SaveChanges();

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
                _context.Costumer.Update(costumer);
                _context.SaveChanges();

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
                var costumer = await _context.Costumer.FirstOrDefaultAsync(x => x.Id == id);
                if (costumer is not null)
                {
                    _context.Costumer.Remove(costumer);
                    _context.SaveChanges(); 
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