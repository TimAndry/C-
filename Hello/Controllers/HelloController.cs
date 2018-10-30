    using Microsoft.AspNetCore.Mvc;
    namespace Hello //be sure to use your own project's namespace!
    {
        public class HelloController : Controller //remember inheritance??
        {
            //for each route this controller is to handle:
            [HttpGet] //type of request
            [Route ("index")] //associated route string (exclude the leading /)
            public string Index () {
                return "Hello World from HelloController!";
            }

            // Other code
            [HttpGet]
            [Route ("template/{name}")]
            public JsonResult Method (string name) {
                
            }

            [HttpGet]
            [Route ("template/{id}/{action}")]
            public JsonResult Method (int id, string action) {
                
            }

        }
    }