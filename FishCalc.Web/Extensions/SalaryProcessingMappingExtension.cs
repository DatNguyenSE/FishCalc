using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.My.Extensions;


public static class SalaryProcessMappingExtension
{
    public static SalaryProcessDto ToDto(this SalaryProcess entity)
    {
        return new SalaryProcessDto
        {
            SalaryProcessId= entity.SalaryProcessId,
            Date = entity.Date,
            FishTypeId= entity.FishTypeId,
            UnitId= entity.ProcessingUnitId,
            UnitName = entity.ProcessingUnit?.UnitName,
            FishTypeName = entity.FishType?.Name,
            PricePerUnit = entity.PricePerUnit,
            SalaryPayment = entity.SalaryPayment,
            TotalQuantityProcessed= entity.TotalQuantityProcessed,
            Notes= entity.Notes,
            Status = entity.Status
            
        };
    }
    

    // 2. Ánh xạ DTO (nguồn) sang Entity (đích mới) - Dùng cho Tạo mới (Create)
    public static SalaryProcess ToEntity(this SalaryProcessDto dto)
    {
        return new SalaryProcess
        {
            SalaryProcessId = dto.SalaryProcessId,
            Date= dto.Date,
            FishTypeId= dto.FishTypeId,
            ProcessingUnitId= dto.UnitId,
            TotalQuantityProcessed= dto.TotalQuantityProcessed,
            SalaryPayment= dto.SalaryPayment,
            PricePerUnit= dto.PricePerUnit,
            Notes= dto.Notes,
            Status = dto.Status
        };
    }
}
