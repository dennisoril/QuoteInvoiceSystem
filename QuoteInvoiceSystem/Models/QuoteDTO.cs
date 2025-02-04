namespace QuoteInvoiceAPI.Models
{
    public class QuoteDTO
    {
        public int ClientID { get; set; }
        public int ProjectID { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
