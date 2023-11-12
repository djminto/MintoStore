using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MintoStore.Data;
using MintoStore.Migrations;
using MintoStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace MintoStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreInventoryController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public StoreInventoryController(ProductDbContext context) => _context = context;

        //Get all Products
        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            try
            {
                var pProducts = _context.Products.Include(b => b.Category).Include(b => b.Size).ToList();
                if (pProducts == null)
                {
                    return BadRequest();
                }
                return Ok(pProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Get Product details by Id
        [HttpGet ("{id}")]
           public IActionResult GetProductsById(int id)
        {
            try
            {
                var pProducts = _context.Products.FirstOrDefault(x => x.Id == id);
                if (pProducts == null)
                {
                    return NotFound($"Product details not found with Id {id}");

                }
                return Ok(pProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Post Product details
        [HttpPost]
          public IActionResult PostProducts(Product model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Product created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Update Product Details
        [HttpPut]
       public IActionResult PutProducts(Product model)
        {
            if(model == null || model.Id == 1) 
            {
                if(model == null)
                {
                    return BadRequest("Model data is invalid.");
                   
                }
                else if(model.Id == 1)
                    {
                        return BadRequest($"Product Id {model.Id} is invalid");
                    } 
            }
            try
            {
                var pProducts = _context.Products.FirstOrDefault(x => x.Id == model.Id);
                if (pProducts == null)
                {
                    return NotFound($"Product not found with id {model.Id}");
                }
                pProducts.ProductName = model.ProductName;
                pProducts.Quantity = model.Quantity;
                pProducts.Size = model.Size;
                pProducts.Category = model.Category;
                pProducts.CategoryId = model.CategoryId;
                pProducts.SizeId = model.SizeId;
                _context.SaveChanges();
                return Ok("Product details updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsById(int id)
        {
            try
            {
                var pProducts = await _context.Products.FindAsync(id);

                if (pProducts == null)
                {
                    return NotFound($"Product not found with id {id}");
                }
                _context.Products.Remove(pProducts);
                _context.SaveChanges();
                return Ok("Product details deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
    }

}
}
