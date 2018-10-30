using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTauranter.Models;

namespace RESTauranter.Controllers {
    public class HomeController : Controller {
        private RestauranterContext _context;
        public HomeController(RestauranterContext context){
            _context = context;
        }
        public IActionResult Index () {
            List<Restaurant> restaurants = _context.Review.ToList();
            ViewBag.restaurants = restaurants; 
            return View ();
        }

        public IActionResult Create (Restaurant NewReview) {

            if (ModelState.IsValid) {
                Restaurant review = new Restaurant {
                    ReviewerName = NewReview.ReviewerName,
                    RestaurantName = NewReview.RestaurantName,
                    ReviewDate = NewReview.ReviewDate,
                    StarRating = NewReview.StarRating,
                    ReviewComment = NewReview.ReviewComment,
                };
                _context.Add(review);
                _context.SaveChanges();
                System.Console.WriteLine("****************SUCCESS*******************");
                return RedirectToAction ("Index");
            }else{
                System.Console.WriteLine("we have errors");
                return View ("Index");
            }
        }

        public IActionResult Show (int id){
            Restaurant OneReview = _context.Review.SingleOrDefault(review => review.Id == id);
            ViewBag.review = OneReview;
            return View("Show");
        }

        public IActionResult Edit (int id){
            Restaurant OneReview = _context.Review.SingleOrDefault(review => review.Id == id);
            ViewBag.review = OneReview;
            return View("Edit");
        }

        public IActionResult Update (Restaurant AReview){

            if (ModelState.IsValid) {
                Restaurant OneReview = _context.Review.SingleOrDefault(review => review.Id == AReview.Id);
                Restaurant reviewed = new Restaurant {
                    ReviewerName = AReview.ReviewerName,
                    RestaurantName = AReview.RestaurantName,
                    ReviewDate = AReview.ReviewDate,
                    StarRating = AReview.StarRating,
                    ReviewComment = AReview.ReviewComment,
                };
                OneReview.ReviewerName = reviewed.ReviewerName;
                OneReview.RestaurantName = reviewed.RestaurantName;
                OneReview.ReviewDate = reviewed.ReviewDate;
                OneReview.StarRating = reviewed.StarRating;
                OneReview.ReviewComment = reviewed.ReviewComment;
                _context.SaveChanges();
                System.Console.WriteLine("****************SUCCESS*******************");
                return RedirectToAction ("Index");
            }else{
                System.Console.WriteLine("we have errors");
                return View ("Edit");
            }
        }

        public IActionResult Delete (int id){
            Restaurant DeleteMe = _context.Review.SingleOrDefault(reveiew => reveiew.Id == id);
            _context.Review.Remove(DeleteMe);
            _context.SaveChanges();
            return RedirectToAction ("Index");
        }
    }
}