using FiorelloAsP.DAL;
using FiorelloAsP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.ViewComponents
{
    public class ProductViewComponent:ViewComponent
    {
        public AppDbContext _context { get; }
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Product> products = _context.Products
                                               .Where(p => p.HasDeleted == false)
                                                 .Take(take).ToList();
            return View(await Task.FromResult(products));
        }
    }
}
