using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/queue")]
    public class QueueController : Controller
    {
        private readonly IQueueService _service;

        public QueueController(IQueueService service)
        {
            _service = service;
        }

        /// <summary>
        /// Inclui cliente na fila
        /// </summary> 
        [HttpPost("addCustomer")]
        //[Authorize]
        public async Task<IActionResult> AddCustomerToQueueAsync([FromBody] QueueAddCustomerCommand command)
        {
            var response = await _service.AddCustomerToQueueAsync(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateQueueAsync([FromBody] QueueCreateCommand command)
        {
            var response = await _service.CreateQueueAsync(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        /// <summary>
        /// Inicia atendimento
        /// </summary> 
        [HttpGet("start-service/{customerId}")]
        public async Task<IActionResult> StartCustomerService([FromRoute] int customerId)
        {
            var customer = await _service.StartCustomerService(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        /// <summary>
        /// Envia notificação para o cliente que é a vez dele
        /// </summary> 
        [HttpGet("notify-customer/{customerId}")]
        public async Task<IActionResult> NotifyTimeCustomerWasCalledInTheQueue([FromRoute] int customerId)
        {
            var customer = await _service.SetTimeCustomerWasCalledInTheQueue(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        /// <summary>
        /// Finaliza o serviço de um cliente
        /// </summary>  
        [HttpGet("finish-service/{customerId}")]
        public async Task<IActionResult> NotifyTimeCustomerServiceWasCompleted([FromRoute] int customerId)
        {
            var customer = await _service.SetTimeCustomerServiceWasCompleted(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        /// <summary>
        /// Exclui o cliente da fila e marca como "ausente"
        /// </summary>  
        [HttpPut("remove")]
        public async Task<IActionResult> RemoveMissingCustomer([FromBody] CustomerRemoveFromQueueCommand command)
        {
            var customer = await _service.RemoveMissingCustomer(command);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        [HttpPatch("{customerId}/name")]
        public async Task<IActionResult> UpdateCustomerName([FromRoute] int customerId, [FromBody] CustomerEditNameCommand command)
        {
            var customer = await _service.UpdateCustomerName(customerId, command.Name);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        /// <summary>
        /// Recupera as filas de um determinado dia para uma determinada loja
        /// </summary>  
        [HttpGet("store/owners-queue/{storeId}")]
        //[Authorize]
        public async Task<IActionResult> GetllQueuesOfStoreForOwner([FromRoute] int storeId)
        {
            var queues = await _service.GetllQueuesOfStoreForOwner(storeId);

            if (queues == null)
                BadRequest(queues);

            return Ok(queues);
        }

        /// <summary>
        /// Recupera as filas que estã em aberto para um determinado funcionário
        /// </summary>  
        [HttpGet("{id}/employee")]
        //[Authorize]
        public async Task<IActionResult> GetOpenedQueueByEmployeeId([FromRoute] int id)
        {
            var queues = await _service.GetOpenedQueueByEmployeeId(id);

            if (queues == null)
                BadRequest(queues);

            return Ok(queues);
        }

        /// <summary>
        /// Recupera todos os clientes para uma fila de um determinado funcionário.
        /// </summary>  
        [HttpGet("{storeId}/{employeeId}/customers-in-queue")]
        //[Authorize]
        public async Task<IActionResult> GetAllCustomersInQueueByEmployeeAndStoreId([FromRoute] int storeId, int employeeId)
        {
            var customers = await _service.GetAllCustomersInQueueByEmployeeAndStoreId(storeId, employeeId);

            if (customers == null)
                BadRequest(customers);

            return Ok(customers);
        }


        /// <summary>
        /// Recupera as filas que o cliente está 
        /// </summary>  
        [HttpGet("{userId}/card")]
        //[Authorize]
        public async Task<IActionResult> GetCustomerInQueueCardByCustomerId([FromRoute] int userId)
        {
            var queueUserIsIn = await _service.GetCustomerInQueueCardByCustomerId(userId);

            if (queueUserIsIn == null)
                BadRequest(queueUserIsIn);

            return Ok(queueUserIsIn);
        }

        /// <summary>
        /// Recupera dados da fila para montagem de relatório
        /// </summary>  
        [HttpGet("{id}/report")]
        //[Authorize]
        public async Task<IActionResult> GetQueueReport([FromRoute] int id)
        {
            var report = await _service.GetQueueReport(id);

            if (report == null)
                BadRequest(report);

            return Ok(report);
        }

        /// <summary>
        /// Recupera detalhes de uma fila que o cliente está
        /// </summary>  
        [HttpGet("{customerId}/{queueId}/card/details")]
        //[Authorize]
        public async Task<IActionResult> GetCustomerInQueueCardDetailsByCustomerId([FromRoute] int customerId, int queueId)
        {
            var customers = await _service.GetCustomerInQueueCardDetailsByCustomerId(customerId, queueId);

            if (customers == null)
                BadRequest(customers);

            return Ok(customers);
        }


        /// <summary>
        /// Recupera todas as filas de um estabelecimento
        /// </summary>  
        [HttpGet("{idStore}")]
        //[Authorize]
        public async Task<IActionResult> GetAllByStoreIdAsync([FromRoute] int idStore)
        {
            var queue = await _service.GetAllByStoreIdAsync(idStore);

            if (!queue.Valid)
                BadRequest(queue.Data);

            return Ok(queue);
        }

        /// <summary>
        /// Recupera todas as filas de um estabelecimento
        /// </summary>  
        [HttpPost("{storeId}/filter")]
        //[Authorize]
        public async Task<IActionResult> GetAllByDateAndStoreIdAsync([FromRoute] int storeId, [FromBody] QueueFilterRequestCommand command)
        {
            var queue = await _service.GetAllByDateAndStoreIdAsync(storeId, command);

            if (!queue.Valid)
                BadRequest(queue.Data);

            return Ok(queue);
        }

        /// <summary>
        /// Remove o cliente da fila da fila (Feito pelo próprio cliente)
        /// </summary>  
        [HttpDelete("{customerId}/{queueId}/exit")]
        //[Authorize]
        public async Task<IActionResult> ExitQueueAsync([FromRoute] int customerId, int queueId)
        {
            var result = await _service.ExitQueueAsync(customerId, queueId);
            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(result);
        }

        /// <summary>
        /// Encerra a fila
        /// </summary>  
        [HttpPut("close")]
        //[Authorize]
        public async Task<IActionResult> CloseQueue([FromBody] QueueCloseCommand command)
        {
            var result = await _service.CloseQueueAsync(command);

            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, result));
        }

        /// <summary>
        /// Pausar a fila
        /// </summary>  
        [HttpPut("pause")]
        //[Authorize]
        public async Task<IActionResult> PauseQueue([FromBody] QueuePauseCommand command)
        {
            var result = await _service.PauseQueueAsync(command);

            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(result);
        }

        /// <summary>
        /// Pausar a fila
        /// </summary>  
        [HttpGet("{queueId}/waiting")]
        //[Authorize]
        public async Task<IActionResult> ExistCustuomerInQueueWaiting([FromRoute] int queueId)
        {
            var result = await _service.ExistCustuomerInQueueWaiting(queueId);
            return Ok(result);
        }

        /// <summary>
        /// Remove a fila , desde que não tenha sido usada por ninguém 
        /// </summary>  
        [HttpDelete("{queueId}")]
        //[Authorize]
        public async Task<IActionResult> Delete([FromRoute] int queueId)
        {
            var result = await _service.Delete(queueId);
            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(result);
        }
    }
}