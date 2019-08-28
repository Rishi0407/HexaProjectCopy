using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventManagementApplication.Models;
using EventManagementApplication.Infrastructure;

namespace EventManagementApplication.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext db;
        public EventsController(EventDbContext db)
        {
            this.db = db;
        }

        [Route("eventlist")]
        public IActionResult Index()
        {
            var model = db.Events;
            return View(model);
        }

        [HttpGet("new", Name ="AddEvent")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("new", Name = "AddEvent")]
        public async Task<IActionResult> CreateAsync(EventInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await db.Events.AddAsync(model);
                await db.SaveChangesAsync();
                return Redirect("eventlist");
            }
        }
    }
}
