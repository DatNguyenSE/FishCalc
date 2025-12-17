using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.My.Extensions;

public static class FishPriceMappingExtension
{
    public static FishPriceDto ToDto( this FishPrice entity)
    {
        var dto = new FishPriceDto
        {
            PriceId = entity.PriceId,
            FishTypeId = entity.FishTypeId,
            PricePerUnit = entity.PricePerUnit,
            EffectiveDate = entity.EffectiveDate,
            UnitOfMeasure = entity.UnitOfMeasure

        };
        return dto;
    }
    public static FishPrice ToEntity(this FishPriceDto dto)
    {
        var entity = new FishPrice
        {
            PriceId = dto.PriceId,
            FishTypeId = dto.FishTypeId,
            PricePerUnit = dto.PricePerUnit,
            EffectiveDate = dto.EffectiveDate,
            UnitOfMeasure = dto.UnitOfMeasure
        };
        return entity;
    }

}
