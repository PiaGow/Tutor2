
namespace GS.Models
{
    public class RevenueComparisonViewModel
    {
        public List<string> Labels { get; set; }
        public List<float> Revenues { get; set; }
        public Dictionary<string, float> MonthlyRevenueDetails { get; set; } // Detailed revenue information per month
    }

}
