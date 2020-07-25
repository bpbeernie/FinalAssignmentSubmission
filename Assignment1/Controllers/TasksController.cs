using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksWebService.Models;

namespace TasksWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            _context.Database.EnsureCreated();
            return await _context.TaskItems.ToListAsync<Tasks>();
        }


        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks([FromBody] Tasks tasks)
        {
            _context.TaskItems.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.ID }, tasks);
        }

    }
}
