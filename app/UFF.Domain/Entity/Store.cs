using System;
using System.Collections.Generic;
using System.Linq;
using UFF.Domain.Commands.Store;
using UFF.Domain.Enum;

namespace UFF.Domain.Entity
{
    public class Store : To
    {
        private Store() { }

        public string Cnpj { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string PhoneNumber { get; set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public bool OpenAutomatic { get; private set; }
        public bool AttendSimultaneously { get; private set; }
        public string StoreSubtitle { get; private set; }
        public bool AcceptOtherQueues { get; private set; }
        public bool AnswerOutOfOrder { get; private set; }
        public bool AnswerScheduledTime { get; private set; }
        public int? TimeRemoval { get; private set; }
        public bool WhatsAppNotice { get; private set; }
        public string LogoPath { get; private set; }
        public string LogoHash { get; private set; }
        public string WallPaperPath { get; private set; }
        public string WallPaperHash { get; private set; }
        public Category Category { get; private set; }
        public int CategoryId { get; private set; }
        public decimal Rating { get; private set; }
        public int Votes { get; private set; }
        public string Instagram { get; private set; }
        public string Facebook { get; private set; }
        public string Youtube { get; private set; }
        public string Site { get; private set; }
        public virtual ICollection<OpeningHours> OpeningHours { get; private set; } = new List<OpeningHours>();
        public virtual ICollection<HighLight> HighLights { get; private set; } = new List<HighLight>();
        public virtual ICollection<Queue> Queues { get; private set; } = new List<Queue>();
        public virtual ICollection<Service> Services { get; private set; } = new List<Service>();
        public virtual ICollection<EmployeeStore> EmployeeStore { get; private set; } = new List<EmployeeStore>();
        public int OwnerId { get; private set; }
        public bool Verified { get; set; }
        public User Owner { get; private set; }
        public StatusEnum Status { get; private set; }

        public Store(StoreCreateCommand command)
        {
            Cnpj = command.Cnpj;
            Name = command.Name;
            Address = command.Address;
            Number = command.Number;
            City = command.City;
            State = command.State;
            PhoneNumber = command.PhoneNumber;
            Status = StatusEnum.Enabled;
            RegisteringDate = DateTime.UtcNow;
            LastUpdate = DateTime.UtcNow;
            StoreSubtitle = command.StoreSubtitle;
            AttendSimultaneously = command.AttendSimultaneously;
            OpenAutomatic = command.OpenAutomatic;
            AcceptOtherQueues = command.AcceptOtherQueues;
            AnswerOutOfOrder = command.AnswerOutOfOrder;
            AnswerScheduledTime = command.AnswerScheduledTime;
            TimeRemoval = command.TimeRemoval;
            WhatsAppNotice = command.WhatsAppNotice;

            foreach (var hour in command.OpeningHours)
            {
                OpeningHours.Add(new OpeningHours(hour));
            }

            foreach (var highLight in command.HighLights)
            {
                HighLights.Add(new HighLight(highLight));
            }
        }

        public void UpdateStoreInfo(StoreEditCommand command)
        {
            Name = !string.IsNullOrWhiteSpace(command.Name) ? command.Name : Name;
            Address = !string.IsNullOrWhiteSpace(command.Address) ? command.Address : Address;
            Number = !string.IsNullOrWhiteSpace(command.Number) ? command.Number : Number;
            City = !string.IsNullOrWhiteSpace(command.City) ? command.City : City;
            State = !string.IsNullOrWhiteSpace(command.State) ? command.State : State;
            StoreSubtitle = !string.IsNullOrWhiteSpace(command.StoreSubtitle) ? command.StoreSubtitle : StoreSubtitle;           
            CategoryId = command.CategoryId;
            Facebook = command.Facebook;
            Instagram = command.Instagram;
            PhoneNumber = command.PhoneNumber;
            Site = command.WebSite;
            Youtube = command.Youtube;
            OpenAutomatic = command.OpenAutomatic;
            AttendSimultaneously = command.AttendSimultaneously;
            AcceptOtherQueues = command.AcceptOtherQueues;
            AnswerOutOfOrder = command.AnswerOutOfOrder;
            AnswerScheduledTime = command.AnswerScheduledTime;
            WhatsAppNotice = command.WhatsAppNotice;
            TimeRemoval = command.TimeRemoval;
            AttendSimultaneously = command.AttendSimultaneously;
            CategoryId = command.CategoryId;

            UpdateOpeningHours(command.OpeningHours);
            UpdateHighLights(command.HighLights);
        }

        public void UpdateLogo(string logoPath, string logoHash)
        {
            LogoPath = logoPath;
            LogoHash = logoHash;
        }

        public void UpdateWallPaper(string wallPaperPath, string wallPaperHash)
        {
            WallPaperPath = wallPaperPath;
            WallPaperHash = wallPaperHash;
        }


        private void UpdateOpeningHours(ICollection<OpeningHoursCreateCommand> newOpeningHours)
        {
            var existingHours = OpeningHours.ToList();

            foreach (var existing in existingHours)
            {
                if (!newOpeningHours.Any(h => h.WeekDay == existing.WeekDay))
                {
                    OpeningHours.Remove(existing);
                }
            }

            foreach (var newHours in newOpeningHours)
            {
                var existing = OpeningHours.FirstOrDefault(h => h.WeekDay == newHours.WeekDay);
                if (existing != null)
                {
                    existing.SetStart(newHours.Start);
                    existing.SetEnd(newHours.End);
                }
                else
                {
                    OpeningHours.Add(new OpeningHours(
                        new OpeningHoursCreateCommand(newHours.WeekDay, newHours.Start, newHours.End, true)
                    ));
                }
            }
        }

        public void SetSmallestQueue(List<Queue> queues)
        {
            this.Queues = queues;
        }

        private void UpdateHighLights(ICollection<HighLightCreateCommand> newHighLights)
        {
            HighLights.Clear();

            foreach (var highlightCommand in newHighLights)
            {
                HighLights.Add(new HighLight(highlightCommand));
            }
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name)
                && !(string.IsNullOrEmpty(Address))
                && !(string.IsNullOrEmpty(Number))
                && !(string.IsNullOrEmpty(City))
                && (string.IsNullOrEmpty(Cnpj) || Cnpj.Length == 14));
        }
        public void SetOwner(User owner)
        {
            OwnerId = owner.Id;
        }
        public void SetCategory(Category category)
        {
            CategoryId = category.Id;
        }
        public void Disable()
            => Status = StatusEnum.Disabled;
        public void Enable()
            => Status = StatusEnum.Enabled;
        public void UpdateCnpj(string cnpj)
        {
            if (!string.IsNullOrWhiteSpace(cnpj) && cnpj.Length == 14)
                Cnpj = cnpj;
        }
    }
}