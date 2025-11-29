using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces;

public interface IFishTypeRepository
{
    Task Update(FishType fish);
    Task Create(FishType fish);
    Task<IReadOnlyList<FishType>> GetFishTypesAsync();
    Task Delete(FishType fish);
    Task<FishType?> GetFishForUpdate(int id);


}