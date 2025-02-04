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
    public class QuotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            return await _context.Quotes.Include(q => q.Client).Include(q => q.Project).ToListAsync();
        }

        // GET: api/Quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.Include(q => q.Client).Include(q => q.Project)
                                             .FirstOrDefaultAsync(q => q.QuoteID == id);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        // POST: api/Quotes
        [HttpPost]
        public async Task<ActionResult<Quote>> CreateQuote([FromBody] QuoteDTO quoteDto)
        {
            var clientExists = await _context.Clients.AnyAsync(c => c.ClientID == quoteDto.ClientID);
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectID == quoteDto.ProjectID);

            if (!clientExists || !projectExists)
            {
                return BadRequest(new { message = "Invalid ClientID or ProjectID." });
            }

            var quote = new Quote
            {
                ClientID = quoteDto.ClientID,
                ProjectID = quoteDto.ProjectID,
                QuoteDate = quoteDto.QuoteDate,
                Amount = quoteDto.Amount,
                Status = quoteDto.Status
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuote), new { id = quote.QuoteID }, quote);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, QuoteDTO quoteDto)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            quote.ClientID = quoteDto.ClientID;
            quote.ProjectID = quoteDto.ProjectID;
            quote.QuoteDate = quoteDto.QuoteDate;
            quote.Amount = quoteDto.Amount;
            quote.Status = quoteDto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
