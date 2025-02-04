using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuoteInvoiceAPI.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }  // Ensure only ClientID is required

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]  // Ensures API ignores this property in requests
        public virtual Client Client { get; set; }
    }
}
