using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Areas.Admin.Controller
{
    [Area("admin")]
    [Route("admin/homepage")]
    public class HomePageController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
