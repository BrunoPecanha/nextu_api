using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class StoreRatingService : IStoreRatingService
    {
        private readonly IStoreRatingRepository _storeRatingRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;

        public StoreRatingService(IStoreRatingRepository storeRatingRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IStoreRepository storeRepository)
        {
            _storeRatingRepository = storeRatingRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _storeRepository = storeRepository;
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var storeRatings = await _storeRatingRepository.GetAllAsync();

            if (storeRatings is null || !storeRatings.Any())
                return new CommandResult(false, storeRatings);

            return new CommandResult(true, _mapper.Map<StoreRatingDto[]>(storeRatings));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var storeRating = await _storeRatingRepository.GetByIdAsync(id);

            if (storeRating is null)
                return new CommandResult(false, storeRating);

            return new CommandResult(true, _mapper.Map<StoreRatingDto>(storeRating));
        }

        public async Task<CommandResult> GetByStoreIdAsync(int id)
        {
            var storeRatings = await _storeRatingRepository.GetByStoreIdAsync(id);

            if (storeRatings is null)
                return new CommandResult(false, storeRatings);

            return new CommandResult(true, _mapper.Map<StoreRatingDto[]>(storeRatings));
        }


        //TODO - Colocar validações de usuarios e loja antes de criar objeto
        public async Task<CommandResult> CreateAsync(StoreRatingCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetByIdAsync(command.UserId);

                if (user == null)
                {
                    return new CommandResult(true, null, "Usuário não encontrado");
                }

                var store = await _storeRepository.GetByIdAsync(command.StoreId);

                if (store == null)
                {
                    return new CommandResult(false, null, "Loja não encontrada");
                }

                var storeRating = new StoreRating(command.StoreId, command.UserId, command.ProfessionalId, command.Score, command.Comment);

                await _storeRatingRepository.AddAsync(storeRating);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, _mapper.Map<FavoriteStoreDto>(storeRating));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, null, ex.Message);
            }
        }
    }
}