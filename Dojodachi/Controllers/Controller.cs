using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dojodachi {
    public class Pets : Controller {
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            if (HttpContext.Session.GetInt32 ("happiness") == null) {
                HttpContext.Session.SetInt32 ("happiness", 20);
            }
            if (HttpContext.Session.GetInt32 ("fullness") == null) {
                HttpContext.Session.SetInt32 ("fullness", 20);
            }
            if (HttpContext.Session.GetInt32 ("energy") == null) {
                HttpContext.Session.SetInt32 ("energy", 50);
            }
            if (HttpContext.Session.GetInt32 ("meals") == null) {
                HttpContext.Session.SetInt32 ("meals", 3);
            }

            ViewBag.happiness = HttpContext.Session.GetInt32 ("happiness");
            int? happiness = ViewBag.happiness;
            if(happiness <= 0){
                ViewBag.gamestatus = "GAME OVER";
                ViewBag.type = "button type='submit'";
                ViewBag.play = "input type=hidden style=color: white;";
            }else if(happiness >= 100){
                ViewBag.gamestatus = "YOU WIN";
                ViewBag.type = "button type='submit'";
                ViewBag.play = "input type=hidden style=color: white;";              
            }else{
                ViewBag.type = "input type=hidden style=color: white;";
                ViewBag.play = "button type='submit'";
            }

            ViewBag.fullness = HttpContext.Session.GetInt32 ("fullness");
            int? fullness = ViewBag.fullness;
            if(fullness <= 0){
                ViewBag.gamestatus = "GAME OVER";
                ViewBag.type = "button type='submit'";
                ViewBag.play = "input type=hidden style=color: white;";
            }else if(fullness >= 100){
                ViewBag.gamestatus = "YOU WIN";
                ViewBag.type = "button type='submit'";
                ViewBag.play = "input type=hidden style=color: white;";              
            }else{
                ViewBag.type = "input type=hidden style=color: white;";
                ViewBag.play = "button type='submit'";
            }

            ViewBag.energy = HttpContext.Session.GetInt32 ("energy");
            ViewBag.meals = HttpContext.Session.GetInt32 ("meals");
            ViewBag.status = HttpContext.Session.GetString ("status");
            return View ("index");
        }

        [HttpPost]
        [Route ("feed")]
        public IActionResult Feed () {

            int? meals = HttpContext.Session.GetInt32 ("meals");
            if (meals > 0) {
                Random like = new Random ();
                int likeornot = like.Next (1, 5);
                if (likeornot == 3) {
                    HttpContext.Session.SetString("status", "The pet doesn't want food, it wants to eat you... because it's pissed");
                    HttpContext.Session.SetInt32 ("meals", (int) meals - 1);
                } else {
                    HttpContext.Session.SetInt32 ("meals", (int) meals - 1);
                    Random numb = new Random ();
                    int number = numb.Next (5, 11);
                    int? fullness = HttpContext.Session.GetInt32 ("fullness");
                    HttpContext.Session.SetInt32 ("fullness", (int) fullness + number);
                    HttpContext.Session.SetString("status", "This pet ate like a pig");
                }
            }
            return RedirectToAction ("Index");
        }

        [HttpPost]
        [Route ("play")]
        public IActionResult Play () {
            int? energy = HttpContext.Session.GetInt32 ("energy");
            if (energy > 0) {
                Random like = new Random ();
                int likeornot = like.Next (1, 5);
                if (likeornot == 3) {
                    HttpContext.Session.SetString("Status", "The pet says 'don't touch me'");
                    HttpContext.Session.SetInt32 ("energy", (int) energy - 5);
                } else {
                    HttpContext.Session.SetInt32 ("energy", (int) energy - 5);
                    Random numb = new Random ();
                    int number = numb.Next (5, 11);
                    int? happiness = HttpContext.Session.GetInt32 ("happiness");
                    HttpContext.Session.SetInt32 ("happiness", (int) happiness + number);
                    HttpContext.Session.SetString("status", "This pet jumps for joy");
                }
            }
            return RedirectToAction ("Index");
        }

        [HttpPost]
        [Route ("work")]
        public IActionResult Work () {
            int? energy = HttpContext.Session.GetInt32 ("energy");
            int? happiness = HttpContext.Session.GetInt32 ("happiness");
            int? fullness = HttpContext.Session.GetInt32 ("fullness");
            if (energy > 0) {
                HttpContext.Session.SetInt32 ("energy", (int) energy - 5);
                HttpContext.Session.SetInt32 ("happiness", (int) happiness - 5);
                HttpContext.Session.SetInt32 ("fullness", (int) fullness - 5);
                Random numb = new Random ();
                int number = numb.Next (1, 4);
                int? meals = HttpContext.Session.GetInt32 ("meals");
                HttpContext.Session.SetInt32 ("meals", (int) meals + number);
                HttpContext.Session.SetString("status", "This pet is tired, but at least it earned some food");
            }
            return RedirectToAction ("Index");
        }

        [HttpPost]
        [Route ("sleep")]
        public IActionResult Sleep () {
            int? energy = HttpContext.Session.GetInt32 ("energy");
            int? happiness = HttpContext.Session.GetInt32 ("happiness");
            int? fullness = HttpContext.Session.GetInt32 ("fullness");
            if (energy < 200) {
                HttpContext.Session.SetInt32 ("energy", (int) energy + 15);
                HttpContext.Session.SetInt32 ("happiness", (int) happiness - 5);
                HttpContext.Session.SetInt32 ("fullness", (int) fullness - 5);
                HttpContext.Session.SetString("status", "This pet is rested, bored, and probably hungry");
            }
            return RedirectToAction ("Index");
        }

        [HttpPost]
        [Route ("reset")]
        public IActionResult Reset () {
            HttpContext.Session.Clear();
            return RedirectToAction ("Index");
        }


    }
}