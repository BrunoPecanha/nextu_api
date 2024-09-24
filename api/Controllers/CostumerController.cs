using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using uff.Domain;
using uff.Domain.Commands;
using uff.Domain.Dto;

namespace WeApi.Controllers
{
    [Route("costumer")]
    public class CostumerController : Controller {
        private readonly ICostumerService _service;
        private readonly ICostumerRepository _repository;
        private readonly IMapper _mapper;

        public CostumerController(ICostumerService service, ICostumerRepository repository, 
            IMapper mapper) {          
            _service = service;
            _mapper = mapper;
            _repository = repository; 
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CostumerCommand command)
        {
            var costumer = await _service.CreateAsync(command);
            if (costumer.Valid)
                return Ok(new CommandResult(true, costumer));
            else
                return BadRequest(costumer.Log);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ReadAsync(int id) {
            var costumer = await _repository.GetByIdAsync(id);

            if (costumer is not null)
                return Ok(new CommandResult(true, _mapper.Map<CostumerDto>(costumer)));
            else
                return BadRequest();
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> ReadAllAsync() {
            var costumers = await _repository.GetAllAsync();           

            if (costumers is not null)
                return Ok(new CommandResult(true, _mapper.Map<IEnumerable<CostumerDto>>(costumers)));
            else
                return BadRequest(new CommandResult(false, null));

                
        }      
       
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CostumerCommand command) {
            var costumer = await _service.UpdateAsync(command);
            if (costumer.Valid)
                return Ok(new CommandResult(true, _mapper.Map<CostumerDto>(costumer)));
            else
                return BadRequest(new CommandResult(false, costumer.Log));
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