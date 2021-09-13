using FiorelloAsP.DAL;
using FiorelloAsP.Helpers;
using FiorelloAsP.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SlidesController : Controller
    {

        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public SlidesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            ViewBag.SlideCount = _context.Sliders.Count();
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            if (_context.Sliders.Count() >= 4)
            {
                return Content("Şəkil sayı 4-dən artıq ola bilməz");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slides)
        {
            if (slides.Photos == null)
                return View();
            foreach (IFormFile photo in slides.Photos)
            {
                if (photo == null)
                {
                    ModelState.AddModelError("Photos", "Zəhmət olmasa şəkil daxil edin");
                    return View();
                }
                if (!photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photos", "Yüklədiyiniz fayl şəkil tipində olmalıdır");
                    return View();
                }
                if (photo.Length / 1024 > 200)
                {
                    ModelState.AddModelError("Photos", "Şəkil ölçüsü 200kb-dan böyük ola bilməz");
                    return View();
                }
                string fileName = Guid.NewGuid().ToString() + photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                Slider slider = new Slider();
                slider.Image = fileName;
                await _context.Sliders.AddAsync(slider);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider == null)
                return NotFound();
            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSlider(int? id)
        {
            if (id == null)
                return NotFound();
            Slider slider = _context.Sliders.FirstOrDefault(c => c.Id == id);
            if (slider == null)
                return NotFound();
            Helper.DeleteImage(_env.WebRootPath, "img", slider.Image);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
                return NotFound();
            if(slider.Photo!=null)
            {
                if (!slider.Photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa sekil elave edin");
                    return View();
                }
                if (slider.Photo.Length / 1024 > 200)
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu 200kb-dan az olmalidir");
                    return View();
                }
                Slider slider1 = await _context.Sliders.FindAsync(id);
                if (slider1 == null)
                    return NotFound();
                string path = Path.Combine(_env.WebRootPath, "img", slider1.Image);
                string fileName = Guid.NewGuid().ToString() + slider.Photo.FileName;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);
                using(FileStream fileStream=new FileStream(newPath, FileMode.Create))
                {
                    await slider.Photo.CopyToAsync(fileStream);
                }
                slider1.Image = fileName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
      
    }
}
