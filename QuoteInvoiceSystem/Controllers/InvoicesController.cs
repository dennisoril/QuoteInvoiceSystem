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
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.Include(i => i.Quote).ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.Include(i => i.Quote).FirstOrDefaultAsync(i => i.InvoiceID == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice([FromBody] InvoiceDTO invoiceDto)
        {
            var quoteExists = await _context.Quotes.AnyAsync(q => q.QuoteID == invoiceDto.QuoteID);

            if (!quoteExists)
            {
                return BadRequest(new { message = "Invalid QuoteID. Quote does not exist." });
            }

            var invoice = new Invoice
            {
                QuoteID = invoiceDto.QuoteID,
                InvoiceDate = invoiceDto.InvoiceDate,
                Amount = invoiceDto.Amount,
                Status = invoiceDto.Status
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceID }, invoice);
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceDTO invoiceDto)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            invoice.QuoteID = invoiceDto.QuoteID;
            invoice.InvoiceDate = invoiceDto.InvoiceDate;
            invoice.Amount = invoiceDto.Amount;
            invoice.Status = invoiceDto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
