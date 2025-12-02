using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces;
using FishCalc.Web.Interfaces.IServices;
using FishCalc.Web.Mapping;


namespace FishCalc.Web.Services;

public class ProcessingUnitService(IProcessingUnitRepository _processingUnitRepo)  : IProcessingUnitService
{
    public async Task CreateProcessingUnitAsync(ProcessingUnitDto dto)
    {
        var entity = dto.ToEntity();
        await _processingUnitRepo.CreateAsync(entity);
    }

    public async Task DeleteProcessingUnitAsync(int id)
    {
        var entityToDelete = await _processingUnitRepo.GetProcessingUnitByIdAsync(id);
        if(entityToDelete == null)
        {
            return;
        }
        await _processingUnitRepo.DeleteAsync(entityToDelete);
    }

    public async Task<IReadOnlyList<ProcessingUnitDto>> GetAllProcessingUnitsAsync()
    {
        var entities = await _processingUnitRepo.GetProcessingUnitsAsync();
        
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ProcessingUnitDto>> GetListUnitsByIdsAsync(List<int> ids)
    {
        var entities = await _processingUnitRepo.GetListUnitsByIdsAsync(ids);
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<ProcessingUnitDto?> GetProcessingUnitByIdAsync(int id)
    {
        var entity = await _processingUnitRepo.GetProcessingUnitByIdAsync(id);
        if (entity == null) {
            return null;
        }

        return entity.ToDto();
    }

    public async Task UpdateProcessingUnitAsync(ProcessingUnitDto dto)
    {
       var entity = dto.ToEntity();
       await _processingUnitRepo.UpdateAsync(entity);
    }
}
