using FiorelloAsP.DAL;
using FiorelloAsP.Models;
using FiorelloAsP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        public AppDbContext _context { get; }
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketTotal = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert
                        .DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (BasketVM pr in basket)
                {
                    Product pro = await _context.Products.FindAsync(pr.Id);
                    ViewBag.BasketTotal += pro.Price * pr.Count;
                }

            }

            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
