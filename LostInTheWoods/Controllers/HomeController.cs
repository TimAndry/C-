using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LostInTheWoods.Models;
using LostInTheWoods.Factories;

namespace LostInTheWoods.Controllers
{
    public class HomeController : Controller
    {
        private readonly TrailsFactory trailsFactory;
        public HomeController(TrailsFactory tFactory)
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailsFactory = tFactory;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.AllTrails = trailsFactory.FindAll();
            return View ("index");
        }


        [HttpGet]
        [Route("new")]
        public IActionResult NewTrail()
        {
            return View("new");
        }

        [HttpPost("create")]
        public IActionResult create(Trail newTrail)
        {
            if(ModelState.IsValid){
                trailsFactory.Add(newTrail);
                return RedirectToAction("Index");
            }else{
                System.Console.WriteLine("we have errors");
                return View("new");
            }
        }

        [HttpGet]
        [Route("show")]
        public IActionResult Show(int id){
            ViewBag.Trail = trailsFactory.FindByID(id);
            return View("show");
        }

    }
}
