using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoginReg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginReg.Controllers {
    public class HomeController : Controller {
        private LRContext _context;
        public HomeController (LRContext context) {
            _context = context;
        }

        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ("Index");
        }

        public IActionResult Create (User NewUser) {
            List<User> dupe = _context.Users.Where (user => user.Email == NewUser.Email).ToList ();
            if(dupe.Count() > 0){
                ViewBag.EmailErrors = "This email already exists in this database";
            }
            if (ModelState.IsValid && NewUser.Password == NewUser.ConfirmPassword && dupe.Count () < 1) {
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                NewUser.Password = Hasher.HashPassword (NewUser, NewUser.Password);
                User user = new User {
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    Email = NewUser.Email,
                    Password = NewUser.Password,
                    ConfirmPassword = NewUser.Password,
                };
                _context.Add (user);
                _context.SaveChanges ();
                System.Console.WriteLine ("****************SUCCESS*******************");
                HttpContext.Session.SetInt32 ("ID", user.UserId);
                return RedirectToAction ("Show");
            } else if (ModelState.IsValid && NewUser.Password != NewUser.ConfirmPassword) {
                System.Console.WriteLine ("We have errors");
                ViewBag.PassErrors = "Passwords Must Match";
                return View ("Index");
            } else {
                System.Console.WriteLine ("We have errors");
                return View ("Index");
            }
        }

        // [HttpGet]
        // [Route ("LoginPage")]
        // public IActionResult LoginPage () {
        //     return View ("loginpage");
        // }

        // public IActionResult Login (User OldUser) {
        //     System.Console.WriteLine ("*******************" + OldUser.Email + " " + OldUser.Password + "**********************");
        //     User user = _context.Users.SingleOrDefault (auser => auser.Email == OldUser.Email);
        //     PasswordHasher<User> Hasher = new PasswordHasher<User> ();
        //     var result = Hasher.VerifyHashedPassword (user, user.Password, OldUser.Password);
        //     if (result != 0) {
        //         System.Console.WriteLine ("Succuess");
        //         return View ("LoginPage");
        //     } else {
        //         System.Console.WriteLine ("Try Again");
        //         return View ("LoginPage");
        //     }
        // }

        [HttpPost]
        [Route ("LoginHome")]
        public IActionResult LoginHome (string Mail, string Password) {
            if (Mail == null) {
                ViewBag.Errors = "Please enter a valid email address";
                return View ("Index");
            } else if (Password == null) {
                ViewBag.Errors = "Please enter a valid password";
                return View ("Index");
            } else {
                System.Console.WriteLine (Mail + ": " + Password);
                User user = _context.Users.SingleOrDefault (auser => auser.Email == Mail);
                if (user == null) {
                    ViewBag.Errors = "This email is not registered";
                    System.Console.WriteLine ("We have errors");
                    return View ("Index");
                } else {
                    PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                    var result = Hasher.VerifyHashedPassword (user, user.Password, Password);
                    if (result != 0) {
                        HttpContext.Session.SetInt32 ("ID", user.UserId);
                        System.Console.WriteLine ("******************************Success: userID = " + user.UserId);
                        return RedirectToAction ("Show");
                    } else {
                        System.Console.WriteLine ("******************************Try Again");
                        ViewBag.Errors = "This username/password combination does not exist";
                        return View ("Index");
                    }
                }
            }
        }


        public IActionResult Show(){
            //Check for session before rendering "sensitive" pages
            if(HttpContext.Session.GetInt32("ID") == null){
                return RedirectToAction("Index");
            }
            return View("show");
        }


        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}