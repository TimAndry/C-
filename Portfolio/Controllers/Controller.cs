    using Microsoft.AspNetCore.Mvc;
    namespace portfolio     //be sure to use your own project's namespace!
    {
        public class Hello : Controller   //remember inheritance??
        {
            //for each route this controller is to handle:
            [HttpGet]       //type of request
            [Route("")]     //associated route string (exclude the leading /)
            public IActionResult Index()
            {
                ViewBag.Greeting = "Hellp World!";
                return View("Index");
            }

            [HttpGet]       //type of request
            [Route("projects")]     //associated route string (exclude the leading /)
            public IActionResult Projects()
            {
                return View("projects");
            }

            [HttpGet]       //type of request
            [Route("contact")]     //associated route string (exclude the leading /)
            public IActionResult Contact()
            {
                return View("contact");
            }

        }
    }
