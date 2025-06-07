using AutoMapper;
using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteStoreRepository _favoriteStoreRepository;
        private readonly IFavoriteProfessionalRepository _favoriteProfessionalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IFavoriteStoreRepository favoriteStoreRepository, IUnitOfWork unitOfWork, IStoreRepository storeRepository, 
            IUserRepository userRepository, IFavoriteProfessionalRepository favoriteProfessionalRepository, IMapper mapper)
        {
            _favoriteStoreRepository = favoriteStoreRepository;
            _favoriteProfessionalRepository = favoriteProfessionalRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> LikeStore(int storeId, int userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new CommandResult(true, null, "Usuário não encontrado");
                }

                var store = await _storeRepository.GetByIdAsync(storeId);

                if (store == null)
                {
                    return new CommandResult(false, null, "Loja não encontrada");
                }

                var likeStore = new FavoriteStore(user.Id, store.Id);
                await _favoriteStoreRepository.AddAsync(likeStore);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, _mapper.Map<FavoriteStoreDto>(likeStore));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, null, ex.Message);
            }
        }
        public async Task<CommandResult> DislikeStore(int storeId, int userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new CommandResult(true, null, "Usuário não encontrado");
                }

                var store = await _storeRepository.GetByIdAsync(storeId);

                if (store == null)
                {
                    return new CommandResult(false, null, "Loja não encontrada");
                }

                var likedStore = await _favoriteStoreRepository.GetStoreLikedByUser(userId, storeId);

                if (likedStore != null)
                    _favoriteStoreRepository.Remove(likedStore);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, _mapper.Map<FavoriteStoreDto>(likedStore));

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, null, ex.Message);
            }
        }
        public async Task<CommandResult> LikeProfessional(int professionalId, int userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new CommandResult(true, null, "Usuário não encontrado");
                }

                var professional = await _userRepository.GetByIdAsync(professionalId, "Favorites");

                if (professional == null)
                {
                    return new CommandResult(true, null, "Profissional não encontrado");
                }

                var likeProfessional = new FavoriteProfessional(user.Id, professional.Id);
                await _favoriteProfessionalRepository.AddAsync(likeProfessional);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, _mapper.Map<FavoriteProfessionalDto>(likeProfessional));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, null, ex.Message);
            }
        }
        public async Task<CommandResult> DislikeProfessional(int professionalId, int userId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new CommandResult(true, null, "Usuário não encontrado");
                }

                var professional = await _userRepository.GetByIdAsync(professionalId, "Favorites");

                if (professional == null)
                {
                    return new CommandResult(false, null, "Profissional não encontrado");
                }

                var dislikedProfessional = await _favoriteProfessionalRepository.GetProfessionalLikedByUser(userId, professionalId);

                if (dislikedProfessional != null)
                    _favoriteProfessionalRepository.Remove(dislikedProfessional);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, _mapper.Map<FavoriteProfessionalDto>(dislikedProfessional));

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, null, ex.Message);
            }
        }   
    }
}