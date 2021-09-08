using FiorelloAsP.DAL;
using FiorelloAsP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        public AppDbContext _context { get; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExistCategory = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExistCategory)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur");
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task <IActionResult> Update(int?id)
        {
            if (id == null) 
                return NotFound();
            Category category = await _context.Categories.FindAsync(id);
            if (category == null) 
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null) return NotFound();
            Category category1 = await _context.Categories.FindAsync(id);
            if (category1 == null) return NotFound();
            Category categoryCheck = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower());

            if (categoryCheck != null)
            {
                if (category1.Name != categoryCheck.Name)
                {
                    ModelState.AddModelError("Name", "Bu adda kateqoriya artıq mövcuddur");
                    return View();
                }
            }
            category1.Name = category.Name;
            category1.Description = category.Description;
            //_context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       public  IActionResult Delete(int?id)
        {
            if (id == null) 
                NotFound();
            Category category =  _context.Categories.FirstOrDefault(c=>c.Id==id);
            if (category == null) 
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCategory(int?id)
        {
            if (id == null) 
                return NotFound();
            Category category =   _context.Categories.FirstOrDefault(c=>c.Id==id);
            if (category == null)
                return NotFound();
            if (category.HasDeleted)
            {
                category.HasDeleted = false;
            }
            else
            {
                category.HasDeleted = true;
            }
             //_context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int?id)
        {
            if (id == null)
                return NotFound();
            Category category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();
            return View(category);
        }
    }
}
