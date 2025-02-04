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
    public class ReceiptsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts()
        {
            return await _context.Receipts.Include(r => r.Invoice).ToListAsync();
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(int id)
        {
            var receipt = await _context.Receipts.Include(r => r.Invoice).FirstOrDefaultAsync(r => r.ReceiptID == id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // POST: api/Receipts
        [HttpPost]
        public async Task<ActionResult<Receipt>> CreateReceipt([FromBody] ReceiptDTO receiptDto)
        {
            var invoiceExists = await _context.Invoices.AnyAsync(i => i.InvoiceID == receiptDto.InvoiceID);

            if (!invoiceExists)
            {
                return BadRequest(new { message = "Invalid InvoiceID. Invoice does not exist." });
            }

            var receipt = new Receipt
            {
                InvoiceID = receiptDto.InvoiceID,
                ReceiptDate = receiptDto.ReceiptDate,
                AmountPaid = receiptDto.AmountPaid
            };

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceipt), new { id = receipt.ReceiptID }, receipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
