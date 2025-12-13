namespace FishCalc.Web.DTOs
{
    public class SalaryProcessDto
    {
        public int SalaryProcessId { get; set; }
        public DateOnly Date { get; set; }
        public int UnitId { get; set; }
        public int FishTypeId { get; set; }
        
        // Dữ liệu quan trọng
        public decimal TotalQuantityProcessed { get; set; } // Tổng Kg
        public decimal SalaryPayment { get; set; }          // Tổng tiền (Thành tiền)
        public string? Notes { get; set; }

        // Tên hiển thị (đã Map từ bảng liên kết)
        public string FishTypeName { get; set; } = "Chưa xác định";
        public string UnitName { get; set; } = "Chưa xác định";
    }
}