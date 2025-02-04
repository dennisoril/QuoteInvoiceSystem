namespace QuoteInvoiceAPI.Models
{
    public class ReceiptDTO
    {
        public int InvoiceID { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
