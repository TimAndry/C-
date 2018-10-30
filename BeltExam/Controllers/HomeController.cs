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

        [HttpGet]
        [Route ("show")]

        public IActionResult Show () {
            if (HttpContext.Session.GetInt32 ("ID") == null) {
                return RedirectToAction ("Index");
            } else {
                List<User> AllUsers = _context.Users.ToList();
                List<LoginReg.Models.Activity> Activities = _context.Activities
                    .OrderByDescending(a => a.Date)
                    .Include (p => p.Users)
                    .ThenInclude (s => s.User)
                    .ToList ();
                ViewBag.Activities = Activities;
                int? SessionId = HttpContext.Session.GetInt32 ("ID");
                ViewBag.SessionId = SessionId;
                User user = _context.Users.SingleOrDefault(a => a.UserId == SessionId);
                ViewBag.User = user;
                ViewBag.Users = AllUsers;
                return View ("show");
            }
        }

        public IActionResult AddActivity () {
            int? UserID = HttpContext.Session.GetInt32 ("ID");
            ViewBag.UserID = UserID;
            User User = _context.Users.SingleOrDefault(user => user.UserId == UserID);
            ViewBag.User = User;
            return View ("create");
        }

        public IActionResult AActivity (LoginReg.Models.Activity NewActivity) {
            int result = DateTime.Compare (DateTime.Now, NewActivity.Date);
            if (result <= 0) {
                if (ModelState.IsValid) {
                    _context.Add (NewActivity);
                    _context.SaveChanges ();
                    HttpContext.Session.SetInt32("ActivityId", NewActivity.ActivityId);
                    System.Console.WriteLine ("***************SUCCESS******************");
                    return RedirectToAction ("ShowActivity2");
                } else {
                    return View ("create");
                }
            }
            else{
                int? UserID = HttpContext.Session.GetInt32 ("ID");
                ViewBag.UserID = UserID;
                User User = _context.Users.SingleOrDefault(user => user.UserId == UserID);
                ViewBag.User = User;
                ViewBag.DateErrors = "The date must be after the current date";
                return View("create");
            }

        }

        public IActionResult Reserve (int id) {
            
            ViewBag.UserID = HttpContext.Session.GetInt32 ("ID");
            System.Console.WriteLine ("**********************************************" + id + "  and  " + ViewBag.UserID);

            Reservation NewReservation = new Reservation {
                ActivityId = id,
                UserId = ViewBag.UserID,
            };
            _context.Add (NewReservation);
            _context.SaveChanges ();
            return RedirectToAction ("Show");
        }

        public IActionResult UnReserve (int id){
            int? userid = HttpContext.Session.GetInt32("ID");
            Reservation reservation = _context.Reservations.SingleOrDefault (areservation => areservation.UserId == (int)userid && areservation.ActivityId == id);
            _context.Remove(reservation);
            _context.SaveChanges();
            return RedirectToAction("Show");
        }

        public IActionResult ShowActivity (int id) {
            int? UserId = HttpContext.Session.GetInt32("ID");
            List<LoginReg.Models.Activity> Activities = _context.Activities
                .Include (p => p.Users)
                .ThenInclude (s => s.User)
                .ToList ();
            ViewBag.Activities = Activities;
            ViewBag.SessionId = UserId;
            LoginReg.Models.Activity Activity = _context.Activities.SingleOrDefault (wed => wed.ActivityId == id);
            ViewBag.Activity = Activity;
            return View ("activity");
        }

        public IActionResult ShowActivity2 () {
            int? UserId = HttpContext.Session.GetInt32("ID");
            List<LoginReg.Models.Activity> Activities = _context.Activities
                .Include (p => p.Users)
                .ThenInclude (s => s.User)
                .ToList ();
            ViewBag.Activities = Activities;
            ViewBag.SessionId = UserId;
            int? ActivityId = HttpContext.Session.GetInt32("ActivityId");
            LoginReg.Models.Activity Activity = _context.Activities.SingleOrDefault (wed => wed.ActivityId == ActivityId);
            ViewBag.Activity = Activity;
            return View ("activity");
        }

        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

        public IActionResult Delete(int id){
            LoginReg.Models.Activity Activity = _context.Activities.SingleOrDefault(t => t.ActivityId == id);
            _context.Remove(Activity);
            _context.SaveChanges();
            return RedirectToAction ("Show");
        }

    }
}