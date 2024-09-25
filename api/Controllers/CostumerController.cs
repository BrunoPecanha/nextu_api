using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using uff.Domain;
using uff.Domain.Commands;

namespace WeApi.Controllers
{
    [Route("costumer")]
    public class CostumerController : Controller
    {
        private readonly ICostumerService _service;
        

        public CostumerController(ICostumerService service)
        {
            _service = service;          
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CostumerCreateCommand command)
        {
            var response = await _service.CreateAsync(command);

            if (response.Valid)
                return Ok(response);

            return BadRequest(response.Log);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var costumer = await _service.GetByIdAsync(id);

            if (costumer is null)
                BadRequest(costumer);

            return Ok(costumer);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var costumers = await _service.GetAllAsync();

            if (!costumers.Valid)
                BadRequest(costumers.Log);

            return Ok(costumers);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CostumerEditCommand command)
        {
            var costumer = await _service.UpdateAsync(command);

            if (!costumer.Valid)
                return BadRequest(costumer.Log);

            return Ok(costumer);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.Valid)
                return Ok(new CommandResult(true, null));
            else
                return BadRequest(new CommandResult(false, result.Log));

        }
    }
}