namespace FishCalc.Web.DTOs
{
    public class FishTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImgFishUrl { get; set; }

        public decimal PricePerUnit { get; set; }
        public string UnitOfMeasure { get; set; } = "Kilogram";
    }
}
