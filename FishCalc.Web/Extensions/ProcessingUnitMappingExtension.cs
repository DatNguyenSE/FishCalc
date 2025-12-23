using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.My.Extensions;


// Lớp tĩnh chứa các extension method
public static class ProcessingUnitMappingExtension
{
    // 1. Ánh xạ Entity (nguồn) sang DTO (đích) - Dùng cho Truy vấn (Get)
    public static ProcessingUnitDto ToDto(this ProcessingUnit entity)
    {
        return new ProcessingUnitDto
        {
            UnitId = entity.UnitId,
            UnitName = entity.UnitName,
            // Thêm các thuộc tính khác (nếu có)
            // ExampleProperty = entity.ExampleProperty
        };
    }

    // 2. Ánh xạ DTO (nguồn) sang Entity (đích mới) - Dùng cho Tạo mới (Create)
    public static ProcessingUnit ToEntity(this ProcessingUnitDto dto)
    {
        return new ProcessingUnit
        {
            // Không set ID, để DB tự cấp phát
            UnitName = dto.UnitName,
            // Thêm các thuộc tính khác (nếu có)
        };
    }
    
    // 3. Ánh xạ DTO (nguồn) vào Entity đã tồn tại (đích) - Dùng cho Cập nhật (Update)
    // Phương thức này CẬP NHẬT TRỰC TIẾP lên đối tượng entity
}