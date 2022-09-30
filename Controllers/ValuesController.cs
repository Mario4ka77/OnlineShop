using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly OnlineShopDbContext context;
        public ValuesController(OnlineShopDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetProducts")]
        public IActionResult Get()
        {
            try
            {
                var products = context.Products.ToList();
                if(products.Count == 0)
                {
                    return StatusCode(404, "No products found");
                }
                return Ok(products);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
           // var products = GetProducts();
            
        }
        //// GET /pages
        //public async Task<IActionResult> Index()
        //{
        //    List<Product> products = new List<Product>();

        //    using (var httpClient = new HttpClient())
        //    {
        //        using var request = await httpClient.GetAsync("https://localhost:44305/api/pages");
        //        string response = await request.Content.ReadAsStringAsync();

        //        products = JsonConvert.DeserializeObject<List<Product>>(response);
        //    }

        //    return View(products);
        //}


        //[HttpPost("CreateProducts")]
        //public IActionResult Create([FromBody] Product request)
        //{
        //    Product product = new Product();
        //    product.Name = request.Name;
        //    product.Description = product.Description;
        //    product.Price = product.Price;
        //    product.CategoryId = product.CategoryId;
        //    product.Image = product.Image;
        //    product.Category = product.Category;
        //    product.ImageUpload = product.ImageUpload;

        //    try
        //    {
        //        context.Products.Add(product);
        //        context.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, "An error has occurred");
        //    }

        //    //Get all products
        //    var products = context.Products.ToList();
        //    return Ok(products);

        //}

        //[HttpPut("UpdateProducts")]
        //public IActionResult Update([FromBody] Product request)
        //{
        //    try
        //    {
        //        var product = context.Products.FirstOrDefault(x => x.Id == request.Id);
        //        if(product == null)
        //        {
        //            return StatusCode(404, "User not found");
        //        }

        //        product.Name = request.Name;
        //        product.Description = product.Description;
        //        product.Price = product.Price;
        //        product.CategoryId = product.CategoryId;
        //        product.Image = product.Image;
        //        product.Category = product.Category;
        //        product.ImageUpload = product.ImageUpload;

        //        context.Entry(product).State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, "An error has occurred");

        //    }

        //    //get all products
        //    var products = context.Products.ToList();
        //    return Ok(products);
        //}

        //[HttpDelete("DeleteProducts/{Id}")]
        //public IActionResult Detele([FromRoute]int Id)
        //{
        //    try
        //    {
        //        var product = context.Products.FirstOrDefault(x => x.Id == Id);
        //        if (product == null)
        //        {
        //            return StatusCode(404, "User not found");
        //        }

        //        context.Entry(product).State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, "An error has occurred");

        //    }

        //get all products
        //    var products = context.Products.ToList();
        //    return Ok(products);
        //}

        //private List<Product> GetProducts()
        // {
        // return new List<Product>{
        //    new Product { Name = "abc", Description="dfdfsdfsd" },
        //    new Product { Name = "cdf", Description = "Maryanad" },
        //    new Product { Name = "XYZ", Description = "SimpleThing" },

        // };
        // }
    }
}
