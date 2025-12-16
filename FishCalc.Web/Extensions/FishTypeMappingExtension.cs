using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Extensions;

// Lớp tĩnh chứa các extension method
public static class FishTypeMappingExtension
{
    // 1. Ánh xạ Entity (nguồn) sang DTO (đích) - Dùng cho Truy vấn (Get)
    public static FishTypeDto ToDto(this FishType entity)
        {
            if (entity == null) return null;

            return new FishTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ImgFishUrl = entity.ImgFishUrl,
                UnitOfMeasure = "Kilogram", 

                PricePerUnit = entity.FishPrices?
                    .OrderByDescending(fp => fp.EffectiveDate) 
                    .FirstOrDefault()?.PricePerUnit ?? 0
            };
        }

    // - Dùng cho Tạo mới (Create)
    public static FishType ToEntity(this FishTypeDto dto)
        {
            if (dto == null) return null;

            var entity = new FishType
            {

                Name = dto.Name,
                ImgFishUrl = dto.ImgFishUrl
            
            };

            if (dto.PricePerUnit > 0)
            {
                entity.FishPrices.Add(new FishPrice
                {
                    PricePerUnit = dto.PricePerUnit,
                    EffectiveDate = DateTime.Now, 
                    UnitOfMeasure = "Kilogram"
                });
            }

            return entity;
        }
    
    // 3. Ánh xạ DTO (nguồn) vào Entity đã tồn tại (đích) - Dùng cho Cập nhật (Update)
    // Phương thức này CẬP NHẬT TRỰC TIẾP lên đối tượng entity
}