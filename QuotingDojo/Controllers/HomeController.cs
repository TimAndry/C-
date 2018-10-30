using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Models;

namespace QuotingDojo.Controllers {
    public class HomeController : Controller {

        //Database example query...
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {    
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query ("SELECT * FROM quotes");
            ViewBag.AllQuotes = AllQuotes;
            return View ("index");
        }

        [HttpPost ("addquote")]
        public IActionResult addquote (Quote newquote) {

            //Did it this way being lazy because index.cshtml renders on this route so viewbag.allquotes is necessary
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query ("SELECT * FROM quotes");
            ViewBag.AllQuotes = AllQuotes;

            var query = $"INSERT INTO QUOTES(name, quote) VALUES ('{newquote.Name}', '{newquote.QuoteText}')";
            DbConnector.Execute (query);
            return RedirectToAction("Index");
        }
    }
}
