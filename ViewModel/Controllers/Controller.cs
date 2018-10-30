 using Microsoft.AspNetCore.Mvc;
 using System.Collections.Generic;
 namespace RazorFun //be sure to use your own project's namespace!
 {
     public class List : Controller {
         //for each route this controller is to handle:
         [HttpGet] //type of request
         [Route ("")] //associated route string (exclude the leading /)
         public IActionResult Index () {
             Message message = new Message () {
                 message = "This is a message that doesn't really say anything. Why would this be here? Because you will read it and it's fact that you really wanted to waste some time because, why else would you read a random paragraph on a random site with literally NO use."
             };
             return View ("Index", message);
         }

         [HttpGet] //type of request
         [Route ("numbers")]
         public IActionResult Numbers () {
            Numbers nums = new Numbers{
                numbers = new int[3]{1,2,3}  
            };
             return View("numbers", nums);
         }

         [HttpGet] //type of request
         [Route ("users")]
         public IActionResult Users(){
             Users newusers = new Users{
                 users = new string[5]{"Michael", "Jermaine", "Tito", "Randy", "Marlon"}
             };
             return View("Users", newusers);
         }

         [HttpGet]
         [Route ("user")]
         public IActionResult UserPage(){
             User user = new User(){
                 FirstName = "James",
                 LastName = "Harden"
             };
             return View("user", user);
         }
     }

 }