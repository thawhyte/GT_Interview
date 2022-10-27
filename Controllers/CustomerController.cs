using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRULogic.Interface;
using ShopsRUsModel.DTOs.CustomerDTOs;

namespace ShopsRU.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomer _customer;

        public CustomerController(ICustomer customer)
        {
            this._customer = customer;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerGetDTO>>> GetAllCustomers()
        {
            try
            {
                return Ok( await _customer.GetAllCustomersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerDTO input)
        {
            try
            {
                await _customer.CreateCustomerAsync(input);
                return Ok("Customer added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[HttpPost]
        //public async Task<ActionResult> CreateCustomerBulk([FromBody] List<CreateCustomerDTO> input)
        //{
        //    try
        //    {
        //        await _customer.CreateCustomerBulkAsync(input);
        //        return Ok("Customer added successfully.");
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest("An Error occured during this process");
        //    }
        //}

        [HttpGet("{id}", Name = "GetCustomerById")]
        public async Task<ActionResult<CustomerGetDTO>> GetCustomerById(int id)
        {
            try
            {
                return Ok(await _customer.GetCustomerByIdAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //A specific customer
        [HttpGet("{name}", Name = "GetCustomerByName")]
        public async Task<ActionResult<CustomerGetDTO>> GetCustomerByName(string name)
        {
            try
            {
                return Ok(await _customer.GetCustomerByNameAsync(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
