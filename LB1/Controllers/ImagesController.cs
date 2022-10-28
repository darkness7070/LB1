using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LB1.Models;
using LB1.data;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace LB1.Controllers
{
    public class ImagesController : Controller
    {
        IHostingEnvironment _appEnvironment;
        private readonly MVCMobileContext _context;

        public ImagesController(MVCMobileContext context, IHostingEnvironment appEnvironment)
        { 
            _appEnvironment = appEnvironment;
            _context = context;
        }

        // GET: Images


        // GET: Images/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Path")] Image image, IFormFile F)
        {
            if (F != null)
            {
                // путь к папке Files
                string path = "/img/" + F.FileName;
                // сохраняем файл в папку Images в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    F.CopyTo(fileStream); //копируем файл в папку
                }
            }
            image.Path = F.FileName;
            _context.Add(image);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return _context.Images.Any(e => e.Id == id);
        }
    }
}
