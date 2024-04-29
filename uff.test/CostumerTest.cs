using uff.Domain.Commands;
using uff.Domain.Entity;
using Xunit;

namespace uff.Test
{
    public class CostumerTest
    {
        [Fact]
        public void Should_Create_New_Costumer()
        {
            var command = new CostumerCommand()
            {
                Name = "Bruno",
                LastName = "Peçanha",
                Phone = "21981970792",
                Street = "Rua Amadeu Soares",
                Number = "44",
                City = "Nova Iguaçu"
            };

            var costumer = new Costumer(command);           
            Assert.True(costumer.IsValid());
        }


        [Fact]
        public void Should_Not_Create_New_Costumer_Missing_Phone()
        {
            var command = new CostumerCommand()
            {
                Name = "Bruno",
                LastName = "Peçanha",
                Street = "Rua Amadeu Soares",
                Number = "44",
                City = "Nova Iguaçu"
            };

            var costumer = new Costumer(command);
            Assert.False(costumer.IsValid());
        }


        [Fact]
        public void Should_Not_Create_New_Costumer_Missing_Street()
        {
            var command = new CostumerCommand()
            {
                Name = "Bruno",
                LastName = "Peçanha",
                Phone = "21981970792",
                Number = "44",
                City = "Nova Iguaçu"
            };

            var costumer = new Costumer(command);
            Assert.False(costumer.IsValid());
        }


        [Fact]
        public void Should_Not_Create_New_Costumer_Missing_Number()
        {
            var command = new CostumerCommand()
            {
                Name = "Bruno",
                LastName = "Peçanha",
                Phone = "21981970792",
                City = "Nova Iguaçu"
            };

            var costumer = new Costumer(command);
            Assert.False(costumer.IsValid());
        }


        [Fact]
        public void Should_Not_Create_New_Costumer_Missing_City()
        {
            var command = new CostumerCommand()
            {
                Name = "Bruno",
                LastName = "Peçanha",
                Phone = "21981970792",
                Number = "44"
            };

            var costumer = new Costumer(command);
            Assert.False(costumer.IsValid());
        }
    }
}
