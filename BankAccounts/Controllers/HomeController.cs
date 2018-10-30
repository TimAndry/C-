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
            if (dupe.Count () > 0) {
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
        public IActionResult Edit () {
            int? UserId = HttpContext.Session.GetInt32 ("ID");
            User Auser = _context.Users.SingleOrDefault (user => user.UserId == UserId);
            ViewBag.User = Auser;
            return View ("Edit");
        }
        public IActionResult Update (User EditUser) {
            if (ModelState.IsValid && EditUser.Password == EditUser.ConfirmPassword) {
                int? UserId = HttpContext.Session.GetInt32 ("ID");
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                EditUser.Password = Hasher.HashPassword (EditUser, EditUser.Password);
                User ThisUser = _context.Users.SingleOrDefault (Auser => Auser.UserId == UserId);
                ThisUser.FirstName = EditUser.FirstName;
                ThisUser.LastName = EditUser.LastName;
                ThisUser.Email = EditUser.Email;
                ThisUser.Password = EditUser.Password;
                ThisUser.ConfirmPassword = EditUser.Password;
                _context.SaveChanges ();
                return RedirectToAction ("Show");
            } else if (ModelState.IsValid && EditUser.Password != EditUser.ConfirmPassword) {
                System.Console.WriteLine ("We have errors");
                ViewBag.PassErrors = "Passwords Must Match";
                int? UserId = HttpContext.Session.GetInt32 ("ID");
                User Auser = _context.Users.SingleOrDefault (user => user.UserId == UserId);
                ViewBag.User = Auser;
                return View ("Edit");
            } else {
                int? UserId = HttpContext.Session.GetInt32 ("ID");
                User Auser = _context.Users.SingleOrDefault (user => user.UserId == UserId);
                ViewBag.User = Auser;
                return View ("Edit");
            }
        }

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
                        return RedirectToAction ("Show");
                    } else {
                        ViewBag.Errors = "This username/password combination does not exist";
                        return View ("Index");
                    }
                }
            }
        }

        public IActionResult Show () {
            //Check for session before rendering "sensitive" pages
            if (HttpContext.Session.GetInt32 ("ID") == null) {
                return RedirectToAction ("Index");
            }
            int? ThisId = HttpContext.Session.GetInt32 ("ID");
            User Auser = _context.Users.SingleOrDefault (User => User.UserId == ThisId);
            foreach (var transaction in Auser.Transactions) { }
            List<Transaction> Transactions = _context.Transactions.Where (t => t.UserId == Auser.UserId).OrderByDescending (t => t.Date).ToList ();
            string success = HttpContext.Session.GetString ("success");
            string failure = HttpContext.Session.GetString ("failure");
            ViewBag.User = Auser;
            ViewBag.Transactions = Transactions;
            ViewBag.Success = success;
            ViewBag.Failure = failure;
            return View ("show");
        }

        public IActionResult AddMoney (Transaction NewTransaction) {
            int? ThisId = HttpContext.Session.GetInt32 ("ID");
            User Auser = _context.Users.SingleOrDefault (User => User.UserId == ThisId);
            if (Auser.Balance >= NewTransaction.Withdrawl) {
                Auser.Balance = Auser.Balance + NewTransaction.Deposit - NewTransaction.Withdrawl;
                _context.SaveChanges ();
                if (ModelState.IsValid) {
                    Transaction transaction = new Transaction {
                        UserId = Auser.UserId,
                        Deposit = NewTransaction.Deposit,
                        Withdrawl = NewTransaction.Withdrawl,
                        Date = DateTime.Now,
                    };
                    _context.Transactions.Add (transaction);
                    _context.SaveChanges ();
                    System.Console.WriteLine ("success, transaction complete");
                    HttpContext.Session.SetString ("success", "Transaction Complete!");
                    HttpContext.Session.SetString ("failure", "");
                    return RedirectToAction ("Show");
                } else {
                    foreach (var MSkey in ModelState.Keys) {
                        var val = ModelState[MSkey];
                        foreach (var error in val.Errors) {
                            var key = MSkey;
                            var EM = error.ErrorMessage;
                            TempData[key] = EM;
                        }
                    }
                    HttpContext.Session.SetString ("failure", "We cannot complete this transaction at this time");
                    HttpContext.Session.SetString ("success", "");
                    return RedirectToAction ("Show");
                }
            }
            HttpContext.Session.SetString ("failure", "We cannot complete this transaction at this time");
            HttpContext.Session.SetString ("success", "");
            return RedirectToAction ("Show");
        }

        public IActionResult Delete () {
            int? UserId = HttpContext.Session.GetInt32 ("ID");
            User Auser = _context.Users.SingleOrDefault (User => User.UserId == UserId);
            List<Transaction> Transactions = _context.Transactions.Where (t => t.UserId == Auser.UserId).OrderByDescending (t => t.Date).ToList ();
            foreach (Transaction Transaction in Transactions) {
                System.Console.WriteLine ("*************Transaction: " + Transaction.TransactionId);
                _context.Transactions.Remove (Transaction);
                _context.SaveChanges ();
            }
            return RedirectToAction ("Show");
        }

        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

    }
}