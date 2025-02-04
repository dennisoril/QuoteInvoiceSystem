using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteInvoiceAPI.Data;
using QuoteInvoiceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteInvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.Include(p => p.Client).ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.Include(p => p.Client).FirstOrDefaultAsync(p => p.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromBody] ProjectDTO projectDto)
        {
            // Ensure the ClientID exists
            var clientExists = await _context.Clients.AnyAsync(c => c.ClientID == projectDto.ClientID);
            if (!clientExists)
            {
                return BadRequest(new { message = "Invalid ClientID. Client does not exist." });
            }

            // Create a new Project entity without the Client object
            var project = new Project
            {
                ClientID = projectDto.ClientID,
                Name = projectDto.Name,
                Description = projectDto.Description,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectID }, project);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectID)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Projects.Any(e => e.ProjectID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
