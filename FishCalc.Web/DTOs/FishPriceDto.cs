using System;

namespace FishCalc.Web.DTOs
{
    public class FishPriceDto
    {
        public int PriceId { get; set; }
        public decimal PricePerUnitOfMeasure { get; set; }
        public int FishTypeId { get; set; }
        public DateTime EffectiveDate { get; set; }

        // Optional: include FishType name
        public string? FishTypeName { get; set; }
    }
}
