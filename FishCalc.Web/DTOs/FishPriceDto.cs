using System;

namespace FishCalc.Web.DTOs
{
    public class FishPriceDto
    {
        public int PriceId { get; set; }
        public int FishTypeId { get; set; }
        public decimal PricePerUnit { get; set; }
        public string? UnitOfMeasure { get; set; } = "Kilogram";
        public DateTime EffectiveDate { get; set; }


        // Optional: include FishType name
        public string? FishTypeName { get; set; }
    }
}
