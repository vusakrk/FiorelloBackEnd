using FiorelloAsP.DAL;
using FiorelloAsP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                SliderContent = _context.SliderContents.FirstOrDefault(),
                Categories = _context.Categories.Where(c => c.HasDeleted == false).ToList(),
                Products = _context.Products.Include(p => p.Category).Where(p => p.HasDeleted == false).Take(8).ToList(),
                About = _context.Abouts.FirstOrDefault(),
                AboutInfos = _context.AboutInfo.ToList(),
                Experts = _context.Experts.Include(e => e.Profession).ToList(),
                Blogs = _context.Blogs.ToList()
            };
            return View(homeVM);
        }
    }
}
