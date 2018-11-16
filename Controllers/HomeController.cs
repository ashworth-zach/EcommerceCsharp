using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private Context dbContext;

        public HomeController(Context context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            lock (Console.Out)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            ViewBag.Products=dbContext.product.Include(x=>x.orders).ThenInclude(x=>x.user).Take(5).ToList();
            ViewBag.LatestUsers=dbContext.user.OrderByDescending(x=>x.created_at).Take(5).ToList();

            return View("index");
        }
        [HttpGet("orders")]
        public IActionResult Orders(){
            ViewBag.Users=dbContext.user.ToList();
            ViewBag.Products=dbContext.product.Include(x=>x.orders).ThenInclude(x=>x.user).ToList();
            return View("Orders");
        }
        [HttpPost("NewOrder")]
        public IActionResult NewOrder(Order order){
            if (ModelState.IsValid)
            {
                Product ProductBought=dbContext.product.FirstOrDefault(x=>x.ProductId==order.ProductId);
                ProductBought.quantity-=order.quantity;
                dbContext.order.Add(order);
                dbContext.SaveChanges();
                return RedirectToAction("Orders");
            }
            ViewBag.Users=dbContext.user.ToList();
            ViewBag.Products=dbContext.product.Include(x=>x.orders).ThenInclude(x=>x.user).ToList();
            return View("Orders");
        }
        [HttpGet("customers")]
        public IActionResult Customers(){
            ViewBag.AllUsers=dbContext.user.ToList();
            return View("Customers");
        }
        [HttpPost("NewUser")]
        public IActionResult NewUser(User user){
            if(dbContext.user.Any(x=>x.firstname==user.firstname && x.lastname==user.lastname)){
                ModelState.AddModelError("firstname", "user already exists");
                ModelState.AddModelError("lastname", "user already exists");
                ViewBag.AllUsers=dbContext.user.ToList();
            return View("Customers");
            }
            if (ModelState.IsValid)
            {
                dbContext.user.Add(user);
                dbContext.SaveChanges();
                return Redirect("/customers");
            }
            ViewBag.AllUsers=dbContext.user.ToList();
            return View("Customers");
        }
        [HttpGet("remove/{userid}")]
        public IActionResult RemoveUser(int userid){
            User userToDelete=dbContext.user.FirstOrDefault(x=>x.UserId==userid);
            foreach(var remove in dbContext.order.Where(x=>x.UserId==userid))
            {
                dbContext.order.Remove(remove);
            }
            dbContext.SaveChanges();
            dbContext.user.Remove(userToDelete);
            dbContext.SaveChanges();
            return Redirect("/customers");
        }
        [HttpGet("products")]
        public IActionResult Products(){
            ViewBag.Products=dbContext.product.ToList();
            return View();
        }
        [HttpPost("NewProduct")]
        public IActionResult NewProduct(Product product){
            if(ModelState.IsValid)
            {
                dbContext.product.Add(product);
                dbContext.SaveChanges();
                return Redirect("/products");
            }
            ViewBag.Products=dbContext.product.ToList();
            return View("Products");
        }
    }

}