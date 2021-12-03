using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using ShoesStore.Data;
using BC = BCrypt.Net.BCrypt;

namespace ShoesStore.Areas.Admin.Controller
{
    [Area("admin")]
    [Route("admin/usermanage")]
    public class AdminUserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminUserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var userList = _db.Users.Where(u => u.RoleId == 2).ToList();
            foreach (var user in userList)
            {
                user.Role = _db.Roles.SingleOrDefault(r => r.Id == user.RoleId);
            }

            ViewBag.index = "index";
            return View(userList);
        }

        [HttpGet]
        [Route("delete/{email?}")]
        public IActionResult Delete([CanBeNull] string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("deletepost")]
        public IActionResult DeletePost([CanBeNull] string email)
        {
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = false;
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("update/{email?}")]
        public IActionResult Update([CanBeNull] string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdatePost")]
        public IActionResult UpdatePost(Models.User user)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Update", user);
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
        public IActionResult CreatePost(Models.User user, string confirmPassword)
        {
            var userInDb = _db.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userInDb != null)
            {
                ModelState.AddModelError("Email", "This email is existed!!!");
            }

            if (user.Phone != null)
            {
                if (_db.Users.SingleOrDefault(u => u.Phone == user.Phone) != null)
                {
                    ModelState.AddModelError("Phone", "This phone is existed!!!");
                }
            }
            if (confirmPassword == null)
            {
                ViewBag.password = "Confirm password must not be emptied!!!";
            } else if (!confirmPassword.Equals(user.Password))
            {
                ViewBag.password = "Confirm password and password are not matched!!!";
                ModelState.AddModelError("Password", "Confirm password and password are not matched!!!");
            }

            if (ModelState.IsValid)
            {
                user.RoleId = 2;
                user.Password = BC.HashString(user.Password);
                user.RegistrationDate = DateTime.Now;
                user.Status = true;
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View("Create", user);
        }
    }
}
