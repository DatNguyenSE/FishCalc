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
            UnitOfMeasure = entity.UnitOfMeasure ?? string.Empty,
            ImgFishUrl = entity.ImgFishUrl,
            PricePerUnitOfMeasure = entity.FishPrice?.PricePerUnitOfMeasure ?? 0
        };
    }

    // 2. Ánh xạ DTO (nguồn) sang Entity (đích mới) - Dùng cho Tạo mới (Create)
    public static FishType ToEntity(this FishTypeDto dto)
    {
        return new FishType
        {
            Id = dto.Id,
            Name = dto.Name,
            UnitOfMeasure = dto.UnitOfMeasure ?? string.Empty,
            ImgFishUrl = dto.ImgFishUrl
        };
    }
    
    // 3. Ánh xạ DTO (nguồn) vào Entity đã tồn tại (đích) - Dùng cho Cập nhật (Update)
    // Phương thức này CẬP NHẬT TRỰC TIẾP lên đối tượng entity
}