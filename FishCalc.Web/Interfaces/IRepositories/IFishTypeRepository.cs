using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces;

public interface IFishTypeRepository
{
    void Create(FishType fish);
    void Update(FishType fish);
    void Delete(FishType fish);
    Task<IReadOnlyList<FishType>> GetFishTypesAsync();
    Task<FishType?> GetFishTypeByIdAsync(int id);


}