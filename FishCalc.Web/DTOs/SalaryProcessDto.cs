using FishCalc.Web.Helpers.Enums;

namespace FishCalc.Web.DTOs
{
    public class SalaryProcessDto
    {
        public int SalaryProcessId { get; set; }
        public DateOnly Date { get; set; }
        public int UnitId { get; set; }
        public int FishTypeId { get; set; }
        public decimal PricePerKg { get; set; }
        
        // Dữ liệu quan trọng
        public decimal TotalQuantityProcessed { get; set; } 
        public decimal SalaryPayment { get; set; }          
        public string? Notes { get; set; }
        public SalaryStatus Status { get; set; } = SalaryStatus.Unpaid;

        // Tên hiển thị (đã Map từ bảng liên kết)
        public string FishTypeName { get; set; } = "Chưa xác định";
        public string UnitName { get; set; } = "Chưa xác định";
    }
}