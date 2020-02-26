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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        [HttpPost]
        public async Task<IHttpActionResult> POSTTransaction (Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("The request body was empty.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer targetCustomer = await _context.Customers.FindAsync(transaction.CustomerId);
            if (targetCustomer == null)
            {
                return BadRequest("Invalid CustomerId. No customer by that ID.");
            }
            Product targetProduct = await _context.Products.FindAsync(transaction.ProductId);
            if (targetProduct == null)
            {
                return BadRequest("Invalid ProductId. No product by that ID.");
            }
            transaction.DateOfTransaction = DateTime.Now;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok(transaction);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GETAll()
        {
            return Ok(await _context.Transactions.ToListAsync());
        }
    }
}
