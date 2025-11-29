namespace FishCalc.Web.DTOs
{
    public class ReceiptDto
    {
        public int ReceiptId { get; set; }
        public int PaymentId { get; set; }
        public int FishTypeId { get; set; }
        public int UnitId { get; set; }
        public string? Notes { get; set; }

        // Optional: display names
        public string? FishTypeName { get; set; }
        public string? UnitName { get; set; }
    }
}
