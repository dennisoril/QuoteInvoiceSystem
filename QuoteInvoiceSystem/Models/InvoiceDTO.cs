namespace QuoteInvoiceAPI.Models
{
    public class InvoiceDTO
    {
        public int QuoteID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
