using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customercrud.Data;
using customercrud.Models;

namespace customercrud.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Customer>>> GetAction([FromServices] DataContext context)
        {
            var customers = await context.Customers.ToListAsync();
            return customers;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Customer>> Post(
            [FromServices] DataContext context,
            [FromBody]Customer model
        )
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            
            return BadRequest(ModelState);
        }
    }
}