using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bankaccount.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace bankaccount.Controllers
{
    public class HomeController : Controller
    {
         private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/logpage")]
        public IActionResult LogPage(){
            return View("LogIn");
        }
        [HttpPost("/Register")]
        public IActionResult Register(User adduser){
            if(ModelState.IsValid){
                if(dbContext.User.Any(u => u.Email == adduser.Email))
        {
            // Manually add a ModelState error to the Email field, with provided
            // error message
            ModelState.AddModelError("Email", "Email already in use!");
            return View("Index");
            
            // You may consider returning to the View at this point
        }
         // Initializing a PasswordHasher object, providing our User class as its
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                adduser.Password = Hasher.HashPassword(adduser, adduser.Password);
                //Save your user object to the database
                dbContext.Add(adduser);
                dbContext.SaveChanges();
                // HttpContext.Session.SetInt32("ID",userInDb.Id);
                // HttpContext.Session.SetString("Name",adduser.FirstName);

                return View("LogIn");
                
            }
            return View("Index");
        }
        [HttpGet("Dashboard/{id}")]
        public IActionResult Dashboard(int id){
            if(HttpContext.Session.GetInt32("ID")==null){
                return View("Index");
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Id= HttpContext.Session.GetInt32("ID");
            List <Transaction> mytrans = dbContext.Transaction.Where(x=> x.Id ==id).ToList();
            decimal sum=0;

            foreach(var i in mytrans){
                sum +=i.Amount;
            }

            int stuff = Convert.ToInt32(sum);
            HttpContext.Session.SetInt32("Sum",stuff);
            ViewBag.Sum = Math.Round(sum,2);
            
            if(HttpContext.Session.GetString("Error") =="1"){
                ViewBag.Error = "You're Broke";
            }
            HttpContext.Session.SetString("Error","Test");
            
            return View("Dashboard",mytrans);
        }
        [HttpPost("Money")]
        public IActionResult Money(int Amount){
            int? myid = HttpContext.Session.GetInt32("ID");
            System.Console.WriteLine(myid);
            ViewBag.id = myid;
            // Transaction Transx = dbContext.Transaction.FirstOrDefault(u=>u.Id == myid);
                Transaction mytrans = new Transaction();
                mytrans.Amount = Amount ;
                mytrans.Id = ViewBag.id;
                int? currentb = HttpContext.Session.GetInt32("Sum");
                if(currentb + Amount <0){
                HttpContext.Session.SetString("Error","1");
                // ModelState.AddModelError("Amount", "You're Broke!");
                return RedirectToAction("Dashboard",new {id = mytrans.Id});

                    
                }

                // Transx.Amount = Transx.Amount + Amount;
                // Transx.Id = ViewBag.id;
                dbContext.Add(mytrans);
                dbContext.SaveChanges();
            return RedirectToAction("Dashboard",new {id = mytrans.Id});
        }

        [HttpPost("LogIn")]
        public IActionResult LogIn(Login thisuser){
        if(ModelState.IsValid)
        {
            // If inital ModelState is valid, query for a user with provided email
            var userInDb = dbContext.User.FirstOrDefault(u => u.Email == thisuser.Email);
            // If no user exists with provided email
            if(userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("LogIn");
            }
            
            // Initialize hasher object
            var hasher = new PasswordHasher<Login>();
            
            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(thisuser, userInDb.Password, thisuser.Password);
            
            // result can be compared to 0 for failure
            if(result == 0)
            {
                ModelState.AddModelError("Password","Invalid Email/Passwordz");
                return View("LogIn");
                // handle failure (this should be similar to how "existing email" is handled)
            }
            else{
                // HttpContext.Session.SetString("Email",thisuser.Email);
                HttpContext.Session.SetInt32("ID",userInDb.Id);
                HttpContext.Session.SetString("Name",userInDb.FirstName);

                
                return RedirectToAction("Dashboard", new {id = userInDb.Id});
            }
        }
        return View("LogIn");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
