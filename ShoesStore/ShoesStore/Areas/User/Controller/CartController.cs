using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoesStore.Data;
using ShoesStore.Models;

namespace ShoesStore.Areas.User.Controller
{
    [Area("user")]
    [Route("user/cart")]
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("cart");

            if (cart != null)
            {
                var carts = JsonConvert.DeserializeObject<Order>(cart);
                if (carts.OrderDetails.Count > 0)
                {
                    var subtotal = carts.OrderDetails.Sum(s => s.Shoe.Price * s.Quantity);

                    var vat = subtotal * 0.1;
                    var total = subtotal + vat;
                    carts.OrderAmount = total;
                    ViewBag.total = $"{total:0.00}";
                    ViewBag.subtotal = $"{subtotal:0.00}";
                    ViewBag.vat = $"{vat:0.00}";
                    return View(carts);
                }
                else
                {
                    HttpContext.Session.Remove("cart");
                }
            }

            ViewBag.cartNull = "Has no item!!!!";
            return View();
        }

        [Route("addtocart/{id}")]
        public IActionResult AddToCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            var shoe = _db.Shoes.SingleOrDefault(s => s.Id == id);
            //shoe.Category = _db.Categories.SingleOrDefault(c => c.Id == shoe.CategoryId);
            //shoe.Publisher = _db.Publishers.SingleOrDefault(p => p.Id == shoe.PublisherId);

            if (cart == null)
            {
                var order = new Order
                {
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail()
                        {
                            Shoe = shoe,
                            ShoeId = shoe.Id,
                            Quantity = 1
                        }
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(order));
            }
            else
            {
                var order = JsonConvert.DeserializeObject<Order>(cart);
                var index = Existed(order.OrderDetails, id);
                if (index == -1)
                {
                    order.OrderDetails.Add(new OrderDetail()
                    {
                        Shoe = shoe,
                        ShoeId = shoe.Id,
                        Quantity = 1
                    });

                }
                else
                {
                    order.OrderDetails[index].Quantity++;
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(order));
            }

            return RedirectToAction("Index");
        }

        [Route("decrease/{id?}")]
        public IActionResult Decrease(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var carts = JsonConvert.DeserializeObject<Order>(cart);
                var index = Existed(carts.OrderDetails, id);
                if (carts.OrderDetails[index].Quantity == 1)
                {
                    carts.OrderDetails.RemoveAt(index);
                }
                else
                {
                    carts.OrderDetails[index].Quantity--;
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts));
            }

            return RedirectToAction("Index");
        }

        [Route("increase/{id?}")]
        public IActionResult Increase(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var carts = JsonConvert.DeserializeObject<Order>(cart);
                carts.OrderDetails.ForEach(d =>
                {
                    if (d.Shoe.Id == id)
                    {
                        d.Quantity++;
                    }
                });
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts));
            }

            return RedirectToAction("Index");
        }


        [Route("delete/{id?}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                var carts = JsonConvert.DeserializeObject<Order>(cart);
                var index = Existed(carts.OrderDetails, id);
                if (index == -1)
                {
                    return NotFound();
                }
                else
                {
                    carts.OrderDetails.RemoveAt(index);
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts));
            }

            return RedirectToAction("Index");
        }

        [Route("checkout")]
        public IActionResult CheckOut(Order order)
        {
            var cart = HttpContext.Session.GetString("cart");

            if (ModelState.IsValid)
            {
                var carts = JsonConvert.DeserializeObject<Order>(cart);
                order.OrderDetails = carts.OrderDetails;
                order.OrderDate = DateTime.Now;
                _db.Orders.Add(order);
                _db.SaveChanges();
                //var oderDetail = new OrderDetail()
                //{
                //    OrderId = 2,
                //    ShoeId = 1,
                //    Quantity = 2
                //};

                //_db.OrderDetails.Add(oderDetail);
                //_db.SaveChanges();

                //foreach (var orderDetail in carts.OrderDetails)
                //{
                //    orderDetail.OrderId = order.Id;
                //    _db.OrderDetails.Add(orderDetail);

                //}
                //order.OrderDetails.First().OrderId = order.Id;
                //_db.OrderDetails.Add(order.OrderDetails.First());
                //_db.SaveChanges();


                HttpContext.Session.Remove("cart");
                return RedirectToAction("Index", "Home");
            }

            return View("Index", order);
        }

        private int Existed(List<OrderDetail> details, int? id)
        {

            for (var i = 0; i < details.Count; i++)
            {
                if (details[i].Shoe.Id == id)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
