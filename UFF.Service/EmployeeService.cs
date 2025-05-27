using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Employee;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class EmployeeStoreService : IEmployeeStoreService
    {
        private readonly IEmployeeStoreRepository _employeeStoreRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;

        public EmployeeStoreService(IEmployeeStoreRepository employeeStoreRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IStoreRepository storeRepository)
        {
            _employeeStoreRepository = employeeStoreRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _storeRepository = storeRepository;
        }

        public async Task<CommandResult> SendInviteToEmployee(EmployeeStoreSendInviteCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var user = await _userRepository.GetUserByCpf(command.Cpf);

                if (user == null)
                {
                    return new CommandResult(false, Resources.OwnerNotFound);
                }

                var store = await _storeRepository.GetByIdAsync(command.StoreId);

                if (store == null)
                {
                    return new CommandResult(false, Resources.NotFound);
                }

                var relationshipExist = await _employeeStoreRepository.GetByEmployeeAndStoreAndActivatedIds(user.Id, store.Id);

                if (relationshipExist != null && relationshipExist.IsActive)
                {
                    return new CommandResult(false, "Usuário já associado");
                }
                else if (relationshipExist != null)
                {
                    relationshipExist.MarkAsNotAnswered();
                    _employeeStoreRepository.Update(relationshipExist);
                }
                else
                {
                    var employeeXStore = new EmployeeStore(user.Id, store.Id);
                    await _employeeStoreRepository.AddAsync(employeeXStore);
                    // Mudar o perfil do usuário para "Professional"
                }

                await _unitOfWork.CommitAsync();

                return new CommandResult(true, true);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex, ex.Message);
            }
        }

        public async Task<CommandResult> RespondInvite(EmployeeStoreAswerInviteCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var invite = await _employeeStoreRepository.GetByIdsAsync(command.UserId, command.StoreId);

                if (invite == null)
                {
                    return new CommandResult(false, "Convite não encontrado");
                }

                if (command.Answer)
                {
                    invite.ActivateRelation();
                    invite.MarkAsAnswered();
                }
                else
                {
                    invite.InactivateRelation();
                    invite.MarkAsAnswered();
                }       

                _employeeStoreRepository.Update(invite);
                await _unitOfWork.CommitAsync();

                return new CommandResult(true, true);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex, ex.Message);
            }
        }

        public async Task<CommandResult> GetPendingAndAcceptedInvitesByUser(int id)
        {
            var invites = await _employeeStoreRepository.GetPendingAndAcceptedInvitesByUser(id);

            if (invites is null)
                return new CommandResult(false, invites);

            return new CommandResult(true, _mapper.Map<EmployeeStoreDto>(invites));
        }

        public async Task<CommandResult> GetPendingAndAcceptedInvitesByStore(int id)
        {
            var invites = await _employeeStoreRepository.GetPendingAndAcceptedInvitesByStore(id);

            if (invites is null)
                return new CommandResult(false, invites);

            return new CommandResult(true, _mapper.Map<EmployeeStoreDto>(invites));
        }
    }
}