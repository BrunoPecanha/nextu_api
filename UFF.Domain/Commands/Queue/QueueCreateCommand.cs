using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UFF.Domain.Commands.Queue
{
    public class QueueCreateCommand : Notifiable<Notification>
    {
        public int StoreId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int EmployeeId { get; set; }

        public string Date { get; set; } = string.Empty;
        public string OpeningTime { get; set; } = string.Empty;
        public string ClosingTime { get; set; } = string.Empty;

        public string Type { get; set; } = "normal"; 

        public List<string>? EligibleGroups { get; set; }

        public int? MaxServiceTime { get; set; }

        public bool IsRecurring { get; set; } = false;
        public List<int>? RecurringDays { get; set; }
        public string? RecurringEndDate { get; set; }

        public void Validate()
        {
            var contract = new Contract<QueueCreateCommand>()
                .Requires()                
                .IsNotNullOrWhiteSpace(Description, "Description", "O nome da fila é obrigatório.")
                .IsGreaterThan(Description, 2, "Description", "O nome da fila deve ter pelo menos 3 caracteres.")                
                .IsNotNullOrWhiteSpace(Date, "Date", "A data é obrigatória.")
                .IsNotNullOrWhiteSpace(OpeningTime, "OpeningTime", "O horário de abertura é obrigatório.")
                .IsNotNullOrWhiteSpace(ClosingTime, "ClosingTime", "O horário de fechamento é obrigatório.")               
                .IsTrue(
                    Type == "normal" || Type == "priority" || Type == "express",
                    "Type",
                    "O tipo da fila deve ser 'normal', 'priority' ou 'express'."
                )
                .IsFalse(
                    IsRecurring && (RecurringDays == null || !RecurringDays.Any()),
                    "RecurringDays",
                    "Se a fila é recorrente, é necessário informar os dias da semana."
                )
                .IsFalse(
                    IsRecurring && string.IsNullOrWhiteSpace(RecurringEndDate),
                    "RecurringEndDate",
                    "Se a fila é recorrente, a data de fim da recorrência é obrigatória."
                );

            if (DateTime.TryParse(OpeningTime, out var opening) &&
                DateTime.TryParse(ClosingTime, out var closing))
            {
                contract.IsTrue(
                    closing > opening,
                    "ClosingTime",
                    "O horário de fechamento deve ser após o horário de abertura."
                );
            }

            if (Type == "express")
            {
                contract.IsTrue(
                    MaxServiceTime.HasValue && MaxServiceTime.Value > 0,
                    "MaxServiceTime",
                    "Tempo máximo é obrigatório e deve ser maior que zero para fila expressa."
                );
            }

            if (Type == "priority")
            {
                contract.IsTrue(
                    EligibleGroups != null && EligibleGroups.Any(),
                    "EligibleGroups",
                    "Grupos elegíveis são obrigatórios para fila prioritária."
                );
            }

            AddNotifications(contract);
        }
    }
}