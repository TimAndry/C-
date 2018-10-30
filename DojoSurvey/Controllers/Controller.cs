 using Microsoft.AspNetCore.Mvc;
    namespace RazorFun     //be sure to use your own project's namespace!
    {
        public class List : Controller   //remember inheritance??
        {
            //for each route this controller is to handle:
            [HttpGet]       //type of request
            [Route("")]     //associated route string (exclude the leading /)
            public IActionResult Index()
            {
                return View("Index");
            }

            [HttpPost]
            [Route("method")]
            public IActionResult Method(string Name, string Location, string Language, string Comment)
            {
               ViewBag.name = Name;
               ViewBag.location = Location;
               ViewBag.language = Language;
               ViewBag.comment = Comment;
               return View("Method");
            }
        }
    }