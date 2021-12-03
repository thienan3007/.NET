using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Data;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Controller
{
    [Area("admin")]
    [Route("admin/category")]
    public class AdminCategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var categories = _db.Categories.ToList();
            @ViewBag.index = "index";
            return View(categories);
        }

        [HttpGet]
        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var category = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Shoes = _db.Shoes.Where(s => s.CategoryId == category.Id).ToList();
            List<SelectListItem> categoryListItems = new List<SelectListItem>();
            foreach (var shoe in category.Shoes)
            {
                categoryListItems.Add(new SelectListItem() {Value = shoe.Id.ToString(), Text = shoe.Name});
            }

            @ViewBag.shoes = categoryListItems;
            return View(category);
        }


        [HttpPost]
        [Route("deletepost")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var category = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("update/{id?}")]
        public IActionResult Update(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var category = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [Route("updatepost")]
        public IActionResult UpdatePost(Category category)
        {
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", category);
        }


        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("createpost")]
        public IActionResult CreatePost(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", category);
        }
    }
}
