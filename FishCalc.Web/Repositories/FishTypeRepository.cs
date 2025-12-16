using System;
using FishCalc.Web.Data;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FishCalc.Web.Repositories;

public class FishTypeRepository(AppDbContext _context) : IFishTypeRepository
{
    public async Task Create(FishType fish)
    {
        var initPrice = new FishPrice
        {
            UnitOfMeasure = "Kilogram",
            PricePerUnit = 0,
            EffectiveDate = DateTime.Now,
            FishTypeId = fish.Id
        };
        fish.FishPrices.Add(initPrice);

        _context.FishTypes.Add(fish);
        await _context.SaveChangesAsync();

    }

    public async Task Delete(int id)
    {
        var fish = await _context.FishTypes.FindAsync(id);
        if (fish == null)
        {
            return;
        }
        await _context.FishTypes.Where(f => f.Id == id ).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task<FishType?> GetFishTypeByIdAsync(int? id)
    {
       return await _context.FishTypes.Include(x => x.FishPrices.OrderByDescending(fp => fp.EffectiveDate)) 
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<FishType>> GetFishTypesAsync()
    {
        return await _context.FishTypes.Include(x => x.FishPrices.OrderByDescending(fp => fp.EffectiveDate).Take(1))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<FishType>> GetListFishTypeByIdAsync(List<int> ids)
    {
        return await _context.FishTypes.Include(x => x.FishPrices.OrderByDescending(fp => fp.EffectiveDate).Take(1)).Where(x => ids.Contains(x.Id)).ToListAsync();
           
    }

    public async Task UpdateAsync(FishType fish)
    {
        var existingFish = await _context.FishTypes.FindAsync(fish.Id);
    
    if (existingFish == null)
    {
        throw new Exception("Không tìm thấy loại cá này!");
    }
        
        _context.Entry(fish).State = EntityState.Modified;
        await _context.SaveChangesAsync();

    }

    public async Task UpdatePriceByIdAsync(int id, FishPrice fishPrice)
    {
       var fish = await _context.FishTypes.FirstAsync(x => x.Id == id) ?? throw new Exception("Không tìm thấy loại cá này!");
        fish.FishPrices.Add(fishPrice);
        await _context.SaveChangesAsync();
    }

}
