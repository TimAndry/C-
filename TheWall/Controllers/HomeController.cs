using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Controllers {
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

        public IActionResult Show () {
            //Check for session before rendering "sensitive" pages
            if (HttpContext.Session.GetInt32 ("ID") == null) {    
                return RedirectToAction ("Index");
            } else {
                int? UserId = HttpContext.Session.GetInt32 ("ID");
                int? MessageId = HttpContext.Session.GetInt32("MessageId");
                List<Message> AllMessages = _context.Messages.Include(message => message.Comments).ToList ();
                List<Comment> AllComments = _context.Comments.ToList();
                User user = _context.Users.SingleOrDefault (auser => auser.UserId == (int) UserId);
                ViewBag.User = user;
                ViewBag.AllMessages = AllMessages;
                ViewBag.AllComments = AllComments;
                ViewBag.MessageId = MessageId;
                return View ("thewall");
            }
        }

        public IActionResult AddMessage (VModel NewMessage) {
            ViewBag.WallError = TempData["WallMessage"];
            ViewBag.WallErrorC = TempData["WallComment"];
            if (ModelState.IsValid) {
                Message AMessage = new Message {
                    UserId = NewMessage.MessageForm.UserId,
                    AuthorName = NewMessage.MessageForm.AuthorName,
                    WallMessage = NewMessage.MessageForm.WallMessage,
                    Date = DateTime.Now,
                };
                _context.Add (AMessage);
                _context.SaveChanges();
                return RedirectToAction ("Show");
            } else {
                foreach (var MSkey in ModelState.Keys) {
                    var val = ModelState[MSkey];
                    foreach (var error in val.Errors) {
                        var key = MSkey;
                        var EM = error.ErrorMessage;
                        TempData[key] = EM;
                        System.Console.WriteLine("This is the temp data key***************************************"+ key);
                    }
                }
                return RedirectToAction ("Show");
            }
        }

        public IActionResult AddComment (VModel NewComment) {
            System.Console.WriteLine("This is the submited information: " + NewComment.CommentForm.UserId + " AND " + NewComment.CommentForm.MessageId + " AND " + NewComment.CommentForm.AuthorName);
            ViewBag.WallError = TempData["WallComment"];
            if(ModelState.IsValid){
                Comment AComment = new Comment {
                  UserId = NewComment.CommentForm.UserId,
                  MessageId = NewComment.CommentForm.MessageId,
                  AuthorName = NewComment.CommentForm.AuthorName,
                  WallComment = NewComment.CommentForm.WallComment,
                  Date = DateTime.Now  
                };
                _context.Add(AComment);
                _context.SaveChanges();
                System.Console.WriteLine("********************SUCCESS******************");
                return RedirectToAction("Show");
            }else{
                foreach (var MSkey in ModelState.Keys) {
                    var val = ModelState[MSkey];
                    foreach (var error in val.Errors) {
                        var key = MSkey;
                        var EM = error.ErrorMessage;
                        TempData[key] = EM;
                    }
                    HttpContext.Session.SetInt32("MessageId", NewComment.CommentForm.MessageId);
                }
                System.Console.WriteLine("****************************TRY AGAIN*****************");
                return RedirectToAction ("Show");
            }
        }

        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return RedirectToAction ("Index");
        }

    }

}