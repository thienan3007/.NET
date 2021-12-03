using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ShoesStore.Data;

namespace ShoesStore.Controllers
{

    [Route("home")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _logger = logger;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            //var category = new Category()
            //{
            //    Name = "Jogging Shoe"
            //};
            //var publisher = new Publisher()
            //{
            //    Name = "Adidas"
            //};
            //var shoe = new Shoe()
            //{
            //    ShoeLive = true,
            //    Name = "Shoe A",
            //    Publisher = publisher,
            //    Category = category,
            //    ShortDes = "This is a good shoe for jogging on the hill",
            //    Price = 100000,
            //    Quantity = 100,
            //    Image = "shoes-img1.png"
            //};
            //_db.Categories.Add(category);
            //_db.Publishers.Add(publisher);
            //_db.Shoes.Add(shoe);
            //_db.SaveChanges();
            IEnumerable<Shoe> shoes = _db.Shoes;
            var shoe = shoes.First();
            return View(shoe);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
