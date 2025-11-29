using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FishCalc.Web.Data;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using FishCalc.Web.Interfaces.IServices;
using System.Linq;
using FishCalc.Web.Mapping;

namespace FishCalc.Web.Services;

// Bỏ IMapper ra khỏi constructor
public class FishTypeService(
    IFishTypeRepository _fishTypeRepository, 
    AppDbContext _context) : IFishTypeService
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
    

    public async Task CreateFishTypeAsync(FishTypeDto dto)
    {
        var entity = dto.ToEntity();

        _fishTypeRepository.Create(entity);

        await _context.SaveChangesAsync();
        
    }

    public async Task UpdateFishTypeAsync(FishTypeDto dto)
    {
        var existingEntity = await _fishTypeRepository.GetFishTypeByIdAsync(dto.Id);

        if (existingEntity == null) 
        {
            throw new Exception($"Không tìm thấy loại cá có ID: {dto.Id}");
        }

      
        existingEntity.MapToEntity(dto); 

        _fishTypeRepository.Update(existingEntity); 

        await _context.SaveChangesAsync();
    }

    public async Task DeleteFishTypeAsync(int id)
    {
        var entityToDelete = await _fishTypeRepository.GetFishTypeByIdAsync(id);

        if (entityToDelete == null)
        {
            return; 
        }

        _fishTypeRepository.Delete(entityToDelete);

        await _context.SaveChangesAsync();
    }
}