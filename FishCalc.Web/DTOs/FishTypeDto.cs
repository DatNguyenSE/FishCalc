namespace FishCalc.Web.DTOs
{
    public class FishTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UnitOfMeasure { get; set; } = "Kilogram";
        
        public string? ImgFishUrl { get; set; }

        public decimal PricePerUnitOfMeasure { get; set; }
        
        public FishPriceDto FishPrice { get; set; } = null!;
    }
}
