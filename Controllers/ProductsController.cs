using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    //може да разглеждаш продуктите ако си логнат
    public class ProductsController : Controller
    {
        private readonly OnlineShopDbContext context;
        public ProductsController(OnlineShopDbContext context)
        {
            this.context = context;
        }
        // GET /products
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = context.Products.OrderByDescending(x => x.Id)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Count() / pageSize);

            return View(await products.ToListAsync());
        }

        //SearchEngine
        [HttpGet]

        public async Task<IActionResult> Index(string Psearch)
        {
            ViewData["Getproductsdetails"] = Psearch;
            var pquery = from x in context.Products select x;
            if (!String.IsNullOrEmpty(Psearch))
            {
                pquery = pquery.Where(x => x.Name.Contains(Psearch));
            }
            return View(await pquery.AsNoTracking().ToListAsync());
        }

        //GET /products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {
            Category category = await context.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");

            int pageSize = 6;//брой продукти на страница 
            var products = context.Products.OrderByDescending(x => x.Id)
                                           .Where(x => x.CategoryId == category.Id)
                                           .Skip((p - 1) * pageSize)
                                           .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products.Where(x => x.CategoryId == 
              category.Id).Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = categorySlug;

            return View(await products.ToListAsync());

        }
        // GET /products/IndexAPI
        public async Task<IActionResult> IndexAPI()
        {
            List<Product> products = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using var request = await httpClient.GetAsync("https://localhost:44373/api/values/getproducts");
                string response = await request.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(response);
            }

            return View(products);
        }
       

    }
}
