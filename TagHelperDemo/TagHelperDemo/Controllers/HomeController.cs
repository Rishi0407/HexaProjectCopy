using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using TagHelperDemo.Models;
using Microsoft.AspNetCore.Http;
using TagHelperDemo.Services;

namespace TagHelperDemo.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache memChache;
        private IDistributedCache dcache;
        private StateDataService stateDataService;
        public HomeController(IMemoryCache memCach, IDistributedCache cache,
            StateDataService stateDataService)
        {
            this.memChache = memCach;
            this.dcache = cache;
            this.stateDataService = stateDataService;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Message", "This is my message from session");
            if (string.IsNullOrEmpty(memChache.Get<string>("time")))
            {
                memChache.Set<string>("time", DateTime.Now.ToString());
            }
            ViewBag.Time = memChache.Get("time");
            ViewBag.DatabaseName = HttpContext.Items["DatabaseName"];
            stateDataService.Data = "This is some state data service.";

            /* For Distributed System:
             * It should be either byte array or string to store the
             * value in distributed system.
             */ 
            if (string.IsNullOrEmpty(dcache.GetString("dtime")))
            {
                dcache.SetString("dtime", DateTime.Now.ToString(),
                    new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(60)
                    });
            }
            ViewBag.DTime = dcache.GetString("dtime");            
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Data = stateDataService.Data;
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
