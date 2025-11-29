using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Mapping;

// Lớp tĩnh chứa các extension method
public static class FishTypeMappingExtension
{
    // 1. Ánh xạ Entity (nguồn) sang DTO (đích) - Dùng cho Truy vấn (Get)
    public static FishTypeDto ToDto(this FishType entity)
    {
        return new FishTypeDto
        {
            Id = entity.Id,
            Name = entity.Name,
            // Thêm các thuộc tính khác (nếu có)
            // ExampleProperty = entity.ExampleProperty
        };
    }

    // 2. Ánh xạ DTO (nguồn) sang Entity (đích mới) - Dùng cho Tạo mới (Create)
    public static FishType ToEntity(this FishTypeDto dto)
    {
        return new FishType
        {
            // Không set ID, để DB tự cấp phát
            Name = dto.Name,
            // Thêm các thuộc tính khác (nếu có)
        };
    }
    
    // 3. Ánh xạ DTO (nguồn) vào Entity đã tồn tại (đích) - Dùng cho Cập nhật (Update)
    // Phương thức này CẬP NHẬT TRỰC TIẾP lên đối tượng entity
    public static void MapToEntity(this FishType entity, FishTypeDto dto)
    {
        // KHÔNG CẬP NHẬT ID
        entity.Name = dto.Name;
        // Cập nhật các thuộc tính khác có thể thay đổi
        // entity.AnotherProperty = dto.AnotherProperty;
    }
}