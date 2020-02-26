using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        //POST
        [HttpPost]
        public async Task<IHttpActionResult> POSTCustomer(Customer customer)
        {
            if (this.ModelState.IsValid && customer != null)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            else if (customer == null)
            {
                return BadRequest("Request body was empty. Please provide a model.");

            }
            return BadRequest(ModelState);
        }
        //GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }
        //GET ALL
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> content = await _context.Customers.ToListAsync();
            return Ok(content);
        }
        //PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Customer model)
        {
            if (ModelState.IsValid && model != null)
            {
                Customer customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    customer.Address = model.Address;
                    customer.Email = model.Email;

                    await _context.SaveChangesAsync();
                    return Ok(customer);
                }
                return NotFound();
            }

            return BadRequest(ModelState);
        }
    }
}
