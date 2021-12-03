
#nullable enable
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using PagedList;
using PagedList.Core.Mvc;
using ShoesStore.Data;
using ShoesStore.Models;
using X.PagedList;

namespace ShoesStore.Controllers
{

    [Route("shoe")]
    public class ShoeController : Controller
    {
        private readonly int _pageSize = 6;
        private readonly ApplicationDbContext _db;

        public ShoeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("")]
        [Route("allshoe")]
        public IActionResult AllShoe(int? page)
        {
            var shoes = _db.Shoes.OrderByDescending(p => p.Id).ToPagedList(page ?? 1, _pageSize);
            ViewBag.allShoe = "AllShoe";
            ViewBag.pageSize = _pageSize;
            return View(shoes);
        }

        [Route("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var shoe = _db.Shoes.SingleOrDefault(p => p.Id == id);

            if (shoe != null)
            {
                Debug.WriteLine(shoe.PublisherId);

                shoe.Publisher = _db.Publishers.SingleOrDefault(p => p.Id == shoe.PublisherId);
                shoe.Category = _db.Categories.SingleOrDefault(p => p.Id == shoe.CategoryId);
            }
            return View(shoe);
        }

        [Route("find/{keyword?}")]
        public IActionResult Find(string? keyword, int? page)
        {
            if (keyword == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (keyword.Length == 0)
                {
                    var shoes = _db.Shoes.OrderByDescending(p => p.Id).ToPagedList(page ?? 1, _pageSize);
                    @ViewBag.find = "Find";
                    @ViewBag.keyword = keyword;
                    return View("AllShoe", shoes);
                }
                else
                {
                    var shoes = _db.Shoes.Where(p => p.Name.Contains(keyword)).OrderByDescending(p => p.Id).ToPagedList(page ?? 1, _pageSize);
                    @ViewBag.find = "Find";
                    @ViewBag.keyword = keyword;
                    return View("AllShoe", shoes);
                }
            }
        }
    }
}
