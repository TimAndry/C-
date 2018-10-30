 using Microsoft.AspNetCore.Mvc;
 namespace RazorFun //be sure to use your own project's namespace!
 {
     public class List : Controller //remember inheritance??
     {
         //for each route this controller is to handle:
         [HttpGet] //type of request
         [Route ("")] //associated route string (exclude the leading /)
         public IActionResult Home () {
             return View ("Index");
         }

         [HttpPost ("")]
         public IActionResult Index (Survey user) {
             if (ModelState.IsValid) {
                 Survey newuser = new Survey () {
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                     Age = user.Age,
                     Email = user.Email,
                     Password = user.Password,
                 };
                 return View ("results", user);
             }else{
                 System.Console.WriteLine("we have errors");
                 return View("index");
             }
         }
     }
 }