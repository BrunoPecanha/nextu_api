using AutoMapper;
using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public QueueService(IQueueRepository repository, IMapper mapper, IStoreRepository storeRepository)
        {
            _queueRepository = repository;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        public async Task<CommandResult> CreateAsync(QueueCreateCommand command)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(command.StoreId);

                if (store == null)
                    return new CommandResult(false, "Store Not Found");

                var queue = new Queue(command.Description, store);

                if (!queue.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                await _queueRepository.AddAsync(queue);
                await _queueRepository.SaveChangesAsync();

                return new CommandResult(true, queue);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    

        public async Task<CommandResult> GetAllByStoreIdAsync(int idStore)
        {
            var queue = await _queueRepository.GetAllByStoreIdAsync(idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetByDateAsync(int idStore, DateTime date)
        {
            var queue = await _queueRepository.GetByDateAsync(date, idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }    

        public async Task<CommandResult> UpdateAsync(QueueEditCommand command)
        {
            throw new NotImplementedException();
        }

        //public async Task<CommandResult> GetAllAsync()
        //{
        //    var stores = await _storeRepository.GetAllAsync();

        //    if (stores is null || !stores.Any())
        //        return new CommandResult(false, stores);

        //    return new CommandResult(true, _mapper.Map<List<StoreDto>>(stores));
        //}

        //public async Task<CommandResult> GetByIdAsync(int id)
        //{
        //    var store = await _storeRepository.GetByIdAsync(id);

        //    if (store is null)
        //        return new CommandResult(false, store);            

        //    return new CommandResult(true, _mapper.Map<StoreDto>(store));
        //}

        //public async Task<CommandResult> GetByCategoryIdAsync(int id)
        //{
        //    var store = await _storeRepository.GetByCategoryId(id);

        //    if (store is null)
        //        return new CommandResult(false, store);

        //    return new CommandResult(true, _mapper.Map<StoreDto[]>(store));
        //}

        //public async Task<CommandResult> CreateAsync(StoreCreateCommand command)
        //{
        //    try
        //    {             
        //        var store = new Store(command);

        //        if (!store.IsValid())
        //            return new CommandResult(false, Resources.MissingInfo);

        //        var owner = await _userRepository.GetByIdAsync(command.OwnerId);

        //        if (owner is null)
        //            return new CommandResult(false, Resources.OwnerNotFound);

        //        store.SetOwner(owner);

        //        await _storeRepository.AddAsync(store);
        //        await _storeRepository.SaveChangesAsync();

        //        return new CommandResult(true, store);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CommandResult(false, ex.Message);
        //    }
        //}

        //public async Task<CommandResult> UpdateAsync(StoreEditCommand command)
        //{
        //    try
        //    {
        //        var store = await _storeRepository.GetByIdAsync(command.Id);

        //        if (store is null)
        //            return new CommandResult(false, Resources.NotFound);

        //        store.UpdateAllUserInfo(command);

        //        _storeRepository.Update(store);
        //        await _storeRepository.SaveChangesAsync();

        //        return new CommandResult(true, _mapper.Map<UserDto>(store));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CommandResult(false, ex.Message);
        //    }
        //}

        //public async Task<CommandResult> DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var store = await _storeRepository.GetByIdAsync(id);
        //        if (store is not null)
        //        {
        //            store.Disable();
        //            _storeRepository.Update(store);
        //            await _storeRepository.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CommandResult(false, ex.Message);
        //    }

        //    return new CommandResult(true, null);
        //}
    }
}