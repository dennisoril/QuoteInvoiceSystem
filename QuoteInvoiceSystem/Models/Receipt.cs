namespace QuoteInvoiceAPI.Models
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public int InvoiceID { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal AmountPaid { get; set; }

        public Invoice Invoice { get; set; } // Navigation Property
    }
}
