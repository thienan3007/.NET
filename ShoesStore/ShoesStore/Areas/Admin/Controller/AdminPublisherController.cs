using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Data;
using ShoesStore.Models;

namespace ShoesStore.Areas.Admin.Controller
{
    [Area("admin")]
    [Route("admin/publisher")]
    public class AdminPublisherController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminPublisherController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var publishers = _db.Publishers.ToList();

            @ViewBag.index = "index";
            return View(publishers);
        }

        [HttpGet]
        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            var publisher = _db.Publishers.SingleOrDefault(p => p.Id == id);
            
            if (publisher == null)
            {
                return NotFound();
            }
            publisher.Shoes = _db.Shoes.Where(s => s.PublisherId == publisher.Id).ToList();
            List<SelectListItem> shoeListItems = new List<SelectListItem>();
            foreach (var shoe in publisher.Shoes)
            {
                shoeListItems.Add(new SelectListItem() {Value = shoe.Id.ToString(), Text = shoe.Name});
            }

            @ViewBag.shoes = shoeListItems;
            return View(publisher);

        }

        [HttpPost]
        [Route("deletepost")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var publisher = _db.Publishers.SingleOrDefault(p => p.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            _db.Publishers.Remove(publisher);
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

            var publisher = _db.Publishers.SingleOrDefault(p => p.Id == id);
            if (publisher == null)
            {
                return NotFound();;
            }

            return View(publisher);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("updatepost")]
        public IActionResult  UpdatePost(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _db.Publishers.Update(publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", publisher);
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
        public IActionResult CreatePost(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _db.Publishers.Add(publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            @ViewBag.create = "create";
            return View("Create", publisher);
        }
    }
}
