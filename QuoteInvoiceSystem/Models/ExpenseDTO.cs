namespace QuoteInvoiceAPI.Models
{
    public class ExpenseDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string AttachmentPath { get; set; } // File path for attached receipts/documents
    }
}
