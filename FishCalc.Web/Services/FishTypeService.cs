using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FishCalc.Web.Data;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using FishCalc.Web.Interfaces.IServices;
using System.Linq;
using FishCalc.Web.Extensions;

namespace FishCalc.Web.Services;

// Bỏ IMapper ra khỏi constructor
public class FishTypeService(IFishTypeRepository _fishTypeRepository) : IFishTypeService
{
    public async Task<IReadOnlyList<FishTypeDto>> GetAllFishTypesAsync()
    {
        var entities = await _fishTypeRepository.GetFishTypesAsync();
        
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<FishTypeDto?> GetFishTypeByIdAsync(int id)
    {
        var entity = await _fishTypeRepository.GetFishTypeByIdAsync(id);

        if (entity == null) return null;

        return entity.ToDto();
    }
    public async Task<IReadOnlyList<FishTypeDto?>> GetListFishTypeByIdAsync(List<int> ids)
    {
        var entities =await _fishTypeRepository.GetListFishTypeByIdAsync(ids);
        
        if (entities == null) return null;

        return entities.Select(e => e.ToDto()).ToList();
 
    }
    

    public async Task CreateFishTypeAsync(FishTypeDto dto)
    {
        var entity = dto.ToEntity();

        await _fishTypeRepository.Create(entity);
    }

    public async Task UpdateFishTypeAsync(FishTypeDto dto)
    {
        var entiy = dto.ToEntity();

        await _fishTypeRepository.UpdateAsync(entiy); 

    }
    public async Task DeleteFishTypeAsync(int id)
    {
        var entityToDelete = await _fishTypeRepository.GetFishTypeByIdAsync(id);

        if (entityToDelete == null)
        {
            return; 
        }

        await _fishTypeRepository.Delete(entityToDelete);

    }

    
}