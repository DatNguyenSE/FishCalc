using System;
using FishCalc.Web.Data;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FishCalc.Web.Repositories;

public class FishTypeRepository(AppDbContext context) : IFishTypeRepository
{
    public async Task Create(FishType fish)
    {
        context.FishTypes.Add(fish);
        await context.SaveChangesAsync();

    }

    public async Task  Delete(int id)
    {
        var fish = await context.FishTypes.FindAsync(id);
        if (fish == null)
        {
            return;
        }
       context.FishTypes.Remove(fish);
        await context.SaveChangesAsync();

    }

    public async Task<FishType?> GetFishTypeByIdAsync(int? id)
    {
       return await context.FishTypes.Include(x => x.FishPrice)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<FishType>> GetFishTypesAsync()
    {
        return await context.FishTypes.Include(x => x.FishPrice).ToListAsync();
    }

    public async Task<IReadOnlyList<FishType>> GetListFishTypeByIdAsync(List<int> ids)
    {
        return await context.FishTypes.Include(x => x.FishPrice).Where(x => ids.Contains(x.Id)).ToListAsync();
           
    }

    public async Task UpdateAsync(FishType fish)
    {
        var existingFish = await context.FishTypes.FindAsync(fish.Id);
    
    if (existingFish == null)
    {
        throw new Exception("Không tìm thấy loại cá này!");
    }
        existingFish.FishPrice.PricePerUnit = fish.FishPrice.PricePerUnit;
        existingFish.FishPrice.PricePerUnit = fish.FishPrice.PricePerUnit;

        var currentPrice = await context.FishPrices
            .FirstOrDefaultAsync(fp => fp.FishTypeId == fish.Id);
        if (currentPrice != null)
        {
            currentPrice.PricePerUnit = fish.FishPrice.PricePerUnit;
            // currentPrice.EffectiveDate = DateTime.Now; // Cập nhật ngày hiệu lực nếu cần
        }
        else
        {
            var newPrice = new FishPrice
            {
                FishTypeId = fish.Id,
                PricePerUnit = fish.FishPrice.PricePerUnit,
                // EffectiveDate = DateTime.Now
            };
        }
        // vieet them ham priceFish
        context.Entry(fish).State = EntityState.Modified;
        await context.SaveChangesAsync();

    }
}
