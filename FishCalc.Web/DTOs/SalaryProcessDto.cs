using System;

namespace FishCalc.Web.DTOs
{
    public class SalaryProcessDto
    {
        public int PaymentId { get; set; }
        public DateOnly PaymentDate { get; set; }
        public int FishId { get; set; }
        public int UnitId { get; set; }
        public decimal TotalQuantityProcessed { get; set; }
        public string? Notes { get; set; }

        // Optional: include names for display
        public string FishTypeName { get; set; } = null!;
        public string ProcessingUnitName { get; set; } = null!;
    }
}
