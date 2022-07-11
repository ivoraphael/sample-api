using CustomerIntegrationApp.Models;
using CustomerIntegrationApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerIntegrationApp.Controllers
{
    [Route("[controller]")]
    public class CustomerCardController : ControllerBase
    {
        private readonly ICustomerIntegrationService _service;

        public CustomerCardController(ICustomerIntegrationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Registrates a customer card
        /// </summary>
        /// <param name="customerCard"></param>
        /// <returns></returns>
        [HttpPost("Registrate")]
        public IActionResult Registrate([FromBody] CustomerCard customerCard)
        {
            try
            {
                return Ok(_service.Registrate(customerCard));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Validates a customer card
        /// </summary>
        /// <param name="customerCardValidation"></param>
        /// <returns></returns>
        [HttpPost("Validate")]
        public IActionResult Validate([FromBody] CustomerCardValidation customerCardValidation)
        {
            try
            {
                return Ok(_service.Validate(customerCardValidation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
