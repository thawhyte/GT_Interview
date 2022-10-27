using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopsRULogic.Interface;
using ShopsRUsModel.DTOs.DiscountsDTOs;

namespace ShopsRU.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscounts _discounts;
        public DiscountsController(IDiscounts discounts)
        {
            this._discounts = discounts;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetDiscountsDTO>>> GetAllDiscounts()
        {
            try
            {
                return Ok(await _discounts.GetAllDiscountsAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{type}", Name = "GetDiscountByType")]
        public async Task<ActionResult<GetDiscountsDTO>> GetDiscountByType(string type)
        {
            try
            {
                return Ok(await _discounts.GetDiscountByTypeAsync(type));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddNewDiscountType(AddDiscountDTO input)
        {
            try
            {
                await _discounts.AddNewDiscountTypeAsync(input);
                return Ok("Added Successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
