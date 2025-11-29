namespace FishCalc.Web.DTOs
{
    public class FishTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UnitOfMeasure { get; set; } = "Kilogram";

        // Optional: latest price
        public decimal? LatestPrice { get; set; }
    }
}
