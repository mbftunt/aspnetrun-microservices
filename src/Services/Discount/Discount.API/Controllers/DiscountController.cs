using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<DiscountController>
        [HttpGet("{productName}", Name = "GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            return Ok(await _repository.GetDiscount(productName));
        }

        // POST api/<DiscountController>
        [HttpPost]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repository.CreateDiscount(coupon));
        }

        // PUT api/<DiscountController>/5
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        // DELETE api/<DiscountController>/5
        [HttpDelete("{productName}")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
        }
    }
}
