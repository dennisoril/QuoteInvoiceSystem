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
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense([FromBody] ExpenseDTO expenseDto)
        {
            var expense = new Expense
            {
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                ExpenseDate = expenseDto.ExpenseDate,
                AttachmentPath = expenseDto.AttachmentPath
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenses), new { id = expense.ExpenseID }, expense);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDTO expenseDto)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            expense.Description = expenseDto.Description;
            expense.Amount = expenseDto.Amount;
            expense.ExpenseDate = expenseDto.ExpenseDate;
            expense.AttachmentPath = expenseDto.AttachmentPath;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
