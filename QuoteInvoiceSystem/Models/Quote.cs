namespace QuoteInvoiceAPI.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        public int ClientID { get; set; }
        public int ProjectID { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        // Navigation Properties
        public Client Client { get; set; }
        public Project Project { get; set; }
    }
}
