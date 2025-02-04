namespace QuoteInvoiceAPI.Models
{
    public class ProjectDTO
    {
        public int ClientID { get; set; }  // Only requires ClientID, NOT Client object
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
