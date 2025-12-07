using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Extensions;

public static class SalaryProcessMappingExtension
{
    public static SalaryProcessDto ToDto(this SalaryProcess entity)
    {
        return new SalaryProcessDto
        {
            PaymentId= entity.SalaryProcessId,
            PaymentDate = entity.Date,
            FishId= entity.FishTypeId,
            UnitId= entity.ProcessingUnitId,
            TotalQuantityProcessed= entity.TotalQuantityProcessed,
            Notes= entity.Notes
        };
    }
    

    // 2. Ánh xạ DTO (nguồn) sang Entity (đích mới) - Dùng cho Tạo mới (Create)
    public static SalaryProcess ToEntity(this SalaryProcessDto dto)
    {
        return new SalaryProcess
        {
            SalaryProcessId = dto.PaymentId,
            Date= dto.PaymentDate,
            FishTypeId= dto.FishId,
            ProcessingUnitId= dto.UnitId,
            TotalQuantityProcessed= dto.TotalQuantityProcessed,
            Notes= dto.Notes
        };
    }
}
