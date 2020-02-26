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
    public class ProductController : ApiController
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> POSTProduct(Product product)
        {
            if (this.ModelState.IsValid && product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            else if (product == null)
            {
                return BadRequest("Request body was empty. Please provide a model.");

            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> content = await _context.Products.ToListAsync();
            return Ok(content);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Product model)
        {
            if (ModelState.IsValid && model != null)
            {
                Product product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Brand = model.Brand;
                    product.Price = model.Price;
                    product.QuantityInStock = model.QuantityInStock;

                    await _context.SaveChangesAsync();
                    return Ok(product);
                }
                return NotFound();
            }

            return BadRequest(ModelState);
        }
    }
}
