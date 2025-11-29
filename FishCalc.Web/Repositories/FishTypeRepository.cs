using System;
using FishCalc.Web.Data;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FishCalc.Web.Repositories;

public class FishTypeRepository(AppDbContext context) : IFishTypeRepository
{
    public void Create(FishType fish)
    {
        context.FishTypes.Add(fish);
    }

    public void  Delete(FishType fish)
    {
       context.FishTypes.Remove(fish);
    }

    public async Task<FishType?> GetFishTypeByIdAsync(int id)
    {
       return await context.FishTypes
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<FishType>> GetFishTypesAsync()
    {
        return await context.FishTypes.ToListAsync();
    }

    public void Update(FishType fish)
    {
        context.Entry(fish).State = EntityState.Modified;
    }
}
