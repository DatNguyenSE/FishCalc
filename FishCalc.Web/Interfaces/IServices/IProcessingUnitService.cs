using System;
using FishCalc.Web.DTOs;

namespace FishCalc.Web.Interfaces.IServices;

public interface IProcessingUnitService
{
    Task<IReadOnlyList<ProcessingUnitDto>> GetAllProcessingUnitsAsync();
    Task<ProcessingUnitDto?> GetProcessingUnitByIdAsync(int id); 
    Task<IReadOnlyList<ProcessingUnitDto>>GetListUnitsByIdsAsync(List<int> ids);

    Task CreateProcessingUnitAsync(ProcessingUnitDto dto);
    Task UpdateProcessingUnitAsync(ProcessingUnitDto dto);
    Task DeleteProcessingUnitAsync(int id);
}
