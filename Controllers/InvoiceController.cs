using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRULogic.Interface;
using ShopsRUsModel.DTOs;
using ShopsRUsModel.DTOs.InvoiceDTOs;

namespace ShopsRU.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        public readonly IInvoice _invoice;
        public InvoiceController(IInvoice invoice)
        {
            this._invoice = invoice;
        }


        [HttpPost]
        public async Task<ActionResult<GetTotalInvoiceDetailsDTO>> GetInvoiceDetail([FromBody] InvoiceRequest input)
        {
            try
            {
                return Ok(await _invoice.GetInvoiceDetailsAsync(input));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}
