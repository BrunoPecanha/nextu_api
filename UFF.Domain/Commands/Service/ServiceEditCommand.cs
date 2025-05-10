using Microsoft.AspNetCore.Http;
using System;

namespace UFF.Domain.Commands.Service
{
    public class ServiceEditCommand
    {
        public ServiceEditCommand()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Activated { get; set; }
        public bool VariablePrice { get; set; }
        public bool VariableTime { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}