using System;
using System.Threading.Tasks;
using FishCalc.Web.Data;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FishCalc.Web.Repositories;

public class ProcessingUnitRepository(AppDbContext _context) : IProcessingUnitRepository
{
    public async Task CreateAsync(ProcessingUnit unit)
    {
        _context.ProcessingUnits.Add(unit);
       await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var unit = await _context.ProcessingUnits.FindAsync(id);
        if (unit == null)
        {
            throw new ArgumentException($"ProcessingUnit with id {id} not found.");
        }
         _context.ProcessingUnits.Remove(unit);
       await  _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<ProcessingUnit>> GetProcessingUnitsAsync()
    {
        return await _context.ProcessingUnits.ToListAsync();
    }

    public async Task<ProcessingUnit?> GetProcessingUnitByIdAsync(int id)
    {
       return await _context.ProcessingUnits
                                .SingleOrDefaultAsync(x => x.UnitId == id);
    }

    public async Task UpdateAsync(ProcessingUnit unit)
    {
        _context.Entry(unit).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<ProcessingUnit>> GetListUnitsByIdsAsync(List<int> ids)
    {
        return await _context.ProcessingUnits.Where(x => ids.Contains(x.UnitId)).ToListAsync();
    }
}
