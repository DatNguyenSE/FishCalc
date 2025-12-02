using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces;
public interface IProcessingUnitRepository
{
    Task CreateAsync(ProcessingUnit unit);
    Task UpdateAsync(ProcessingUnit unit);
    Task DeleteAsync(ProcessingUnit unit);
    Task<IReadOnlyList<ProcessingUnit>> GetProcessingUnitsAsync();
    Task<ProcessingUnit?> GetProcessingUnitByIdAsync(int id);
    Task<IReadOnlyList<ProcessingUnit>>GetListUnitsByIdsAsync(List<int> ids);
    


}