namespace Webservice_Laundrygest.Models
{
    public class MonthlyStatsDTO
    {
        public string dateName {  get; set; }
        public decimal totalAmount { get; set; }
        public decimal taxAmount { get; set; }
        public decimal baseAmount { get; set; }
    }
}
