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
    }

    public async Task Delete(FishType fish)
    {
        context.FishTypes.Remove(fish);
    }

    public async Task<FishType?> GetFishForUpdate(int id)
    {
       return context.FishTypes.SingleOrDefault(x => x.Id == id);
    }

    public async Task<IReadOnlyList<FishType>> GetFishTypesAsync()
    {
        return await context.FishTypes.ToListAsync();
    }

    public async Task Update(FishType fish)
    {
        context.Entry(fish).State = EntityState.Modified;
    }
}
