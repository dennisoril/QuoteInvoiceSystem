namespace QuoteInvoiceAPI.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int QuoteID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public Quote Quote { get; set; } // Navigation Property
    }
}
