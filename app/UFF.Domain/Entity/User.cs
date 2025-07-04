﻿using System;
using System.Collections.Generic;
using UFF.Domain.Commands.User;
using UFF.Domain.Enum;

namespace UFF.Domain.Entity
{
    public class User : To
    {
        private User()
        {
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public string StateId { get; private set; }
        public string Cpf { get; set; }
        public StatusEnum Status { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ProfileEnum Profile { get; private set; }
        public string Subtitle { get; set; }
        public string ServicesProvided { get; set; }
        public bool AcceptAwaysMinorQueue { get; set; }
        public string ImageUrl { get; set; }
        public bool LooseCustomer { get; set; } = false;
        public ICollection<FavoriteProfessional> Favorites { get; set; } = new List<FavoriteProfessional>();
        public ICollection<Customer> CustomerInstances { get; private set; } = new List<Customer>();
        public ICollection<Store> Stores { get; private set; } = new List<Store>();
        public virtual ICollection<EmployeeStore> EmployeeStore { get; private set; } = new List<EmployeeStore>();
        public virtual ICollection<Queue> Queues { get; private set; } = new List<Queue>();

        public User(UserCreateCommand user)
        {
            Name = user.Name;
            Email = user.Email;
            LastName = user.LastName;
            Phone = user.Phone;
            Address = user.Address;
            Password = user.Password;
            StateId = user.StateId;
            Number = user.Number;
            Cpf = user.Cpf;
            Status = StatusEnum.Enabled;
            City = user.City;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            Profile = ProfileEnum.Customer;
        }

        public void UpdateAllUserInfo(UserEditCommand user)
        {
            Name = !string.IsNullOrWhiteSpace(user.Name) ? user.Name : this.Name;
            LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : this.LastName;
            Phone = !string.IsNullOrWhiteSpace(user.Phone) ? user.Phone : this.Phone;
            Address = !string.IsNullOrWhiteSpace(user.Address) ? user.Address : this.Address;
            Number = !string.IsNullOrWhiteSpace(user.Number) ? user.Number : this.Number;
            City = !string.IsNullOrWhiteSpace(user.City) ? user.City : this.City;
            Status = user.DeleteAccount ? StatusEnum.Disabled : StatusEnum.Enabled;
            StateId = user.StateId;
            Subtitle = user.Subtitle;
            ServicesProvided = user.ServicesProvided;
            AcceptAwaysMinorQueue = user.AcceptAwaysMinorQueue;
            Phone = string.Concat(user.DDD, user.Phone);
        }

        public void UpdatePassWord(string passWord)
        {
            Password = passWord;
        }


        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Phone)
                || string.IsNullOrEmpty(Password));

        }

        public void ChageUserToCustomer()
            => Profile = ProfileEnum.Customer;

        public void ChageUserToProfessional()
            => Profile = ProfileEnum.Employee;

        public void ChageUserToOwner()
            => Profile = ProfileEnum.Owner;

        public void Disable()
        {
            Status = StatusEnum.Disabled;
            Cpf = null;

            foreach (var es in EmployeeStore)
            {
                es.InactivateRelation();
            }

            foreach (var store in Stores)
            {
                store.Disable();
            }
        }

        public void Enable()
            => Status = StatusEnum.Enabled;

        private void UpdateCpf(string cpfcnpj)
        {
            if (!string.IsNullOrWhiteSpace(cpfcnpj) && (cpfcnpj.Length == 11))
                this.Cpf = cpfcnpj;
        }
    }
}
