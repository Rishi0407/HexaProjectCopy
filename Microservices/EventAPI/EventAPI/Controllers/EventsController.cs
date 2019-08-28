using EventAPI.Infrastructure;
using EventAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EventAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private EventDbContext eventDbContext;

        public EventsController(EventDbContext eventDbContext)
        {
            this.eventDbContext = eventDbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<EventInfo>> GetEvents()
        {
            return eventDbContext.Events.ToList();
        }

        [HttpGet("{id}", Name = "GetEventById")]
        [AllowAnonymous]
        public ActionResult<EventInfo> GetEventById(int id)
        {
            var item = eventDbContext.Events.Find(id);
            if (item != null)
                return Ok(item);
            else
                return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<EventInfo>> AddEventAsync(EventInfo model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                var result = await eventDbContext.Events.AddAsync(model);
                await eventDbContext.SaveChangesAsync();
                //return Created($"/api/events/{result.Entity.Id}", result.Entity);
                //return CreatedAtAction(nameof(GetEventById), new { id = result.Entity.Id }, result.Entity);
                return CreatedAtRoute("GetEventById", new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        
    }
}