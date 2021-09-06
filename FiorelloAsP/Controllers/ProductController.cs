using FiorelloAsP.DAL;
using FiorelloAsP.Models;
using FiorelloAsP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Controllers
{
    public class ProductController : Controller
    {
        public AppDbContext _context { get; }
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.PrCount = _context.Products.Count();
            return View();
        }
        public IActionResult GetProducts(int skip)
        {
            List<Product> model = _context.Products
                                             .Where(p => p.HasDeleted == false)
                                              .Skip(skip)
                                                .Take(8)
                                                  .ToList();
            return PartialView("_ProductPartial", model);
        }
        public IActionResult GetBasketCount()
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            return Content(basket.Count.ToString());
        }
        public async Task<IActionResult> AddToBasket(int id)
        {

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            List<BasketVM> basket = new List<BasketVM>();
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            BasketVM repeatBasket = basket.FirstOrDefault(p => p.Id == id);
            if (repeatBasket == null)
            {
                basket.Add(new BasketVM()
                {
                    Id = id,
                    Count = 1,
                });
            }
            else
            {
                repeatBasket.Count += 1;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Basket()
        {

            ViewBag.Total = 0;
            ViewBag.BasketCount = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> dbBasket = new List<BasketVM>();
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (BasketVM product in basket)
                {
                    Product pro = await _context.Products.FindAsync(product.Id);
                    product.Title = pro.Title;
                    product.Image = pro.Image;
                    product.Price = pro.Price;
                    ViewBag.Total += pro.Price * product.Count;
                    dbBasket.Add(product);
                }
                ViewBag.BasketCount = basket.Count();
                return View(dbBasket);
            }

            return View();
        }
        public IActionResult DeleteProduct(int id)
        {
            List<BasketVM> basket = new List<BasketVM>();
            basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            if (basket == null)
                return NotFound();
            BasketVM removedItem = basket.FirstOrDefault(b => b.Id == id);
            if (removedItem != null)
                basket.Remove(removedItem);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction(nameof(Basket));
        }
        public IActionResult IncProductCount(int id)
        {
            List<BasketVM> basket = new List<BasketVM>();
            basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM incPro = basket.FirstOrDefault(b => b.Id == id);
            if (incPro != null)
            {
                incPro.Count++;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction(nameof(Basket));

        }
        public IActionResult DecProductCount(int id)
        {
            List<BasketVM> basket = new List<BasketVM>();
            basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM incPro = basket.FirstOrDefault(b => b.Id == id);
            if (incPro != null && incPro.Count > 1)
            {
                incPro.Count--;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction(nameof(Basket));

        }
    }
}
