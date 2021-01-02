using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.CMS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult File([FromForm] IFormFileCollection file)
        {
            var data = Request;
            var a = Request.Form.Files;
            if (a.Count > 0)
            {
                return Ok(new { success = 1, message = "成功", url = "http://5b0988e595225.cdn.sohucs.com/images/20200420/b0ab35ee97b54d6b8963e9c989c57388.png" });
            }
            return Ok(new { success = 0, message = "成功" });
        }


        [HttpPost]
        public IActionResult File2([FromForm] IFormFileCollection file)
        {
            var data = Request;
            var a = Request.Form.Files;
           
             return Ok(new { code = 0, msg = "成功", data= new {src = "http://5b0988e595225.cdn.sohucs.com/images/20200420/b0ab35ee97b54d6b8963e9c989c57388.png" }});
           
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
