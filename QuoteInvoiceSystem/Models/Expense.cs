namespace QuoteInvoiceAPI.Models
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string AttachmentPath { get; set; } // Stores file path of uploaded receipts
    }
}
