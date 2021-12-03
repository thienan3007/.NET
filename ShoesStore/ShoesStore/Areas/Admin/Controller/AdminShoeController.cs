using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using ShoesStore.Data;
using ShoesStore.Models;



namespace ShoesStore.Areas.Admin.Controller
{
    [Area("admin")]
    [Route("admin/shoe")]
    public class AdminShoeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _webHostEnvironment;

        public AdminShoeController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var shoe = _db.Shoes;
            foreach (var s in shoe)
            {
                s.Publisher = _db.Publishers.SingleOrDefault(p => p.Id == s.PublisherId);
                s.Category = _db.Categories.SingleOrDefault(c => c.Id == s.CategoryId);
            }

            @ViewBag.index = "index";
            return View(shoe);
        }


        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var shoe = LoadShoe(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }


        [HttpPost]
        [Route("delete/{id?}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var shoe = _db.Shoes.Find(id);
            if (shoe == null)
            {
                return NotFound();
            }

            _db.Shoes.Remove(shoe);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update/{id?}")]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = LoadShoe(id);
            if (shoe == null)
            {
                return NotFound();
            }

            List<SelectListItem> puListItems = new List<SelectListItem>();
            foreach (var publisher in _db.Publishers)
            {
                if (publisher.Id == shoe.PublisherId)
                {
                    puListItems.Add(new SelectListItem() {Value = publisher.Id.ToString(), Text = publisher.Name, Selected = true});
                }
                else
                {
                    puListItems.Add(new SelectListItem() { Value = publisher.Id.ToString(), Text = publisher.Name });
                }
            }
            @ViewBag.publisher = puListItems;
            List<SelectListItem> categoryListItems = new List<SelectListItem>();
            foreach (var category in _db.Categories)
            {
                if (category.Id == shoe.CategoryId)
                {
                    categoryListItems.Add(new SelectListItem() {Value = category.Id.ToString(), Text = category.Name, Selected = true});
                }
                else
                {
                    categoryListItems.Add(new SelectListItem() {Value = category.Id.ToString(), Text = category.Name});
                }
            }
            @ViewBag.category = categoryListItems;
            return View(shoe);
        }

        [HttpPost]
        [ ValidateAntiForgeryToken]
        public IActionResult UpdatePost(Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _db.Shoes.Update(shoe);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", shoe);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            List<SelectListItem> publisherListItems = new List<SelectListItem>();
            foreach (var publisher in _db.Publishers)
            {
                publisherListItems.Add(new SelectListItem() {Value = publisher.Id.ToString(), Text = publisher.Name});
            }

            @ViewBag.publisher = publisherListItems;
            List<SelectListItem> categoryListItems = new List<SelectListItem>();
            foreach (var category in _db.Categories)
            {
                categoryListItems.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Name});
            }

            @ViewBag.create = "create";
            @ViewBag.category = categoryListItems;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createpost")]
        public IActionResult CreatePost(Shoe shoe, IFormFile file)
        {
            if (file == null)
            {
                ModelState.AddModelError("Image", "Image should not be emptied!!!");
            }
            else
            {
                var fileName = GenerateFileName(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                file.CopyTo(fileStream);
                shoe.Image = fileName;
            }
            if (ModelState.IsValid)
            {
                var des = shoe.ShortDes.Replace("<p>", "");
                shoe.ShortDes = des.Replace("</p>", "");

                _db.Shoes.Add(shoe);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> publisherListItems = new List<SelectListItem>();
            foreach (var publisher in _db.Publishers)
            {
                publisherListItems.Add(new SelectListItem() { Value = publisher.Id.ToString(), Text = publisher.Name });
            }

            @ViewBag.publisher = publisherListItems;
            List<SelectListItem> categoryListItems = new List<SelectListItem>();
            foreach (var category in _db.Categories)
            {
                categoryListItems.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Name });
            }

            @ViewBag.category = categoryListItems;
            return View("Create", shoe);
        }

        public string GenerateFileName(string fileName)
        {
            var lastIndex = fileName.LastIndexOf(".", StringComparison.Ordinal);
            var ext = fileName.Substring(lastIndex);
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            return guid + ext;
        }

        private Shoe LoadShoe(int? id)
        {
            var shoe = _db.Shoes.SingleOrDefault(s => s.Id == id);
            if (shoe != null)
            {
                shoe.Publisher = _db.Publishers.SingleOrDefault(p => p.Id == shoe.PublisherId);
                shoe.Category = _db.Categories.SingleOrDefault(s => s.Id == shoe.CategoryId);
                
            }

            return shoe;
        }
    }

}
