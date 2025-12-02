using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces;

public interface IFishTypeRepository
{
    Task Create(FishType fish);
    Task UpdateAsync(FishType fish);
    Task Delete(FishType fish);
    Task<IReadOnlyList<FishType>> GetFishTypesAsync();
    Task<FishType?> GetFishTypeByIdAsync(int id);
    Task<IReadOnlyList<FishType>> GetListFishTypeByIdAsync(List<int> ids);


    


}