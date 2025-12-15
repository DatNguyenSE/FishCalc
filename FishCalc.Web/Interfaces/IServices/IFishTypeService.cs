using System.Collections.Generic;
using System.Threading.Tasks;
using FishCalc.Web.DTOs;

namespace FishCalc.Web.Interfaces.IServices;

public interface IFishTypeService
{
    // Queries
    Task<IReadOnlyList<FishTypeDto>> GetAllFishTypesAsync();
    Task<FishTypeDto?> GetFishTypeByIdAsync(int? id); 
    Task<IReadOnlyList<FishTypeDto>> GetListFishTypeByIdAsync(List<int> ids);

    // Commands
    Task CreateFishTypeAsync(FishTypeDto dto);
    Task UpdateFishTypeAsync(FishTypeDto dto);
    Task DeleteFishTypeAsync(int id);
}