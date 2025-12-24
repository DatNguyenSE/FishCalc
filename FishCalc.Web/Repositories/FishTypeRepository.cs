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
            PricePerUnit = fish.FishPrices.FirstOrDefault()?.PricePerUnit ?? 0,
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
        await _context.FishTypes.Where(f => f.Id == id).ExecuteDeleteAsync();
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

    public async Task UpdateAsync(FishType currentFish)
    {
        var existingFish = await _context.FishTypes
        .Include(f => f.FishPrices)
        .FirstOrDefaultAsync(f => f.Id == currentFish.Id);

        if (existingFish == null)
        {
            throw new Exception("Không tìm thấy loại cá này!");
        }

        var oldFishPriceObj = existingFish.FishPrices
             .OrderByDescending(fp => fp.EffectiveDate)
             .FirstOrDefault();
        decimal oldPrice = oldFishPriceObj?.PricePerUnit ?? 0;
        
        var newFishPriceObj = currentFish.FishPrices.FirstOrDefault();
        decimal newPrice = newFishPriceObj?.PricePerUnit ?? 0;
        if (newPrice != oldPrice && newPrice > 0)
        {
            // Tạo một bản ghi giá mới và Add vào list hiện có
            // EF Core sẽ tự hiểu đây là lệnh INSERT vào bảng FishPrices
            existingFish.FishPrices.Add(new FishPrice //INSERT
            {
                UnitOfMeasure = newFishPriceObj?.UnitOfMeasure ?? "Kilogram",
                PricePerUnit = newPrice,
                EffectiveDate = DateTime.Now, // Set thời gian đổi giá là hiện tại
                FishTypeId = existingFish.Id
            });
        }

        _context.Entry(existingFish).CurrentValues.SetValues(currentFish); //UPDATE
        await _context.SaveChangesAsync();

    }

    public async Task UpdatePriceByIdAsync(int id, FishPrice fishPrice)
    {
        var fish = await _context.FishTypes.FirstAsync(x => x.Id == id) ?? throw new Exception("Không tìm thấy loại cá này!");
        fish.FishPrices.Add(fishPrice);
        await _context.SaveChangesAsync();
    }

}
