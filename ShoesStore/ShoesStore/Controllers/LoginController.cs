using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ShoesStore.Data;
using BC = BCrypt.Net.BCrypt;

namespace ShoesStore.Areas.User.Controller
{
    [Route("login")]
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        [Route("signup")]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Models.User user, string passwordConfirm)
        {
            var userInDb = _db.Users.SingleOrDefault(u => u.Email.Equals(user.Email));
            if (userInDb != null)
            {
                ModelState.AddModelError("Email", "This email is existed!!!!");
            }

            if (user.Phone != null)
            {
                if (_db.Users.SingleOrDefault(u => u.Phone == user.Phone) != null)
                {
                    ModelState.AddModelError("Phone", "This phone number is existed!!!");
                }
            }

            if (passwordConfirm == null)
            {
                @ViewBag.passwordConfirm = "Password Confirm must not be emptied!!!";
            }
            else if (!passwordConfirm.Equals(user.Password))
            {
                ModelState.AddModelError("Password", "Password and confirm password does not matched!!!");
                @ViewBag.passwordConfirm = "Password and confirm password does not matched!!!";
            }

            if (ModelState.IsValid)
            {
                user.RoleId = 2;
                user.Password = BC.HashString(user.Password);
                user.RegistrationDate = DateTime.Now;
                user.Status = true;
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("LoginForm");
            }

            return View("SignUp", user);
        }

        [HttpGet]
        [Route("")]
        [Route("loginform")]
        public IActionResult LoginForm()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("")]
        [Route("loginform")]
        public IActionResult LoginForm(string email, string password)
        {
            var userInDb = _db.Users.SingleOrDefault(u => u.Email == email);
            if (userInDb == null)
            {
                ModelState.AddModelError("Email", "Email or password is incorrect!!!");
                @ViewBag.email = "Email or password is incorrect!!!";
            }
            else if (!BC.Verify(password, userInDb.Password))

            {
                ModelState.AddModelError("Password", "Password is incorrect!!!");
                @ViewBag.password = "Password is incorrect!!!";
            }



            if (ModelState.IsValid)
            {
                if (userInDb.Status == false)
                {
                    ModelState.AddModelError("Email", "This account is inactivated!!!");
                }
                else
                {
                    if (userInDb.RoleId == 2)
                    {
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(userInDb));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("admin", JsonConvert.SerializeObject(userInDb));
                        return RedirectToAction("Index", "HomePage", new { area = "Admin" });
                    }
                }
            }

            return View("Login", userInDb);
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                HttpContext.Session.Remove("user");
            }
            else if (HttpContext.Session.GetString("admin") != null)
            {
                HttpContext.Session.Remove("admin");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
