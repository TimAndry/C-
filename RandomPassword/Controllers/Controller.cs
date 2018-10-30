using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
    namespace RandomPassword
    {
        public class RandoDando : Controller
        {
             [HttpGet]
             [Route("")]
             public IActionResult Index()
             {
                 if(HttpContext.Session.GetInt32("passnum") == null){
                     HttpContext.Session.SetInt32("passnum", 0);
                 }
                 return View("index");
             }

             [HttpPost]
             [Route("")]
             public IActionResult Randomize()
             {
                int? numb = HttpContext.Session.GetInt32("passnum");
                HttpContext.Session.SetInt32("passnum", (int)numb + 1);
                int? numbr = HttpContext.Session.GetInt32("passnum");
                ViewBag.number = numbr;
                string[] chars = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","0","1","2","3","4","5","6","7","8","9",};
                ViewBag.randompass = "";
                for(var i = 0; i< 14; i++){
                    Random rand = new Random();
                    int r = rand.Next(0, chars.Length -1);
                    ViewBag.randompass += chars[r];
                }
                System.Console.WriteLine(ViewBag.randompass);
                return View("index"); 
             }

             [HttpGet]
             [Route("clear")]
             public IActionResult Clear()
             {
                 HttpContext.Session.Clear();
                 HttpContext.Session.SetInt32("passnum", 0);
                 return RedirectToAction("index");
             }             
        }
    }