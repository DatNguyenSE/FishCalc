namespace FishCalc.Web.DTOs
{
    public class ProcessingUnitDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = null!;
        public string? Contact { get; set; }
    }
}
