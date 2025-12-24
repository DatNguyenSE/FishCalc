using FishCalc.Web.DTOs;

namespace FishCalc.Web.DTOs;
public class ReceiptListDto
    {
        // Danh sách các cột (Ngày)
        public List<DateOnly> Headers { get; set; } = new();

        // Danh sách các dòng (Các tổ)
        public List<ProcessingUnitDto> Rows { get; set; } = new();

        // Dữ liệu ô: Key là (ProcessingUnitId, Date), Value là Tiền
        public Dictionary<(int UnitId, DateOnly Date), decimal> Data { get; set; } = new();

        // Tổng tiền theo hàng dọc (Tổng của 1 ngày cho tất cả các tổ)
        public Dictionary<DateOnly, decimal> ColumnTotals { get; set; } = new();
    }