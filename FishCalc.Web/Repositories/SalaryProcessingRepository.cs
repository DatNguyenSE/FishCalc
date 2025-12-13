using System;
using FishCalc.Web.Data;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FishCalc.Web.Repositories;

public class SalaryProcessingRepository(AppDbContext _context) : ISalaryProcessRepository
{
    public async Task CreateSalaryProcessAsync(SalaryProcess salaryProcess)
    {
        await _context.SalaryProcesses.AddAsync(salaryProcess);
        await _context.SaveChangesAsync();
    }

    public async Task CreateSalaryProcessesByList(List<SalaryProcess> salaryProcesses)
    {
        await _context.SalaryProcesses.AddRangeAsync(salaryProcesses);
    
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSalaryProcessAsync(int salaryProcessId)
    {
        var salaryProcess =await _context.SalaryProcesses.FindAsync(salaryProcessId);
        if (salaryProcess == null)  
        {
            throw new ArgumentException($"SalaryProcess with id {salaryProcessId} not found.");
        }
        _context.SalaryProcesses.Add(salaryProcess);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<SalaryProcess?>> GetProcessesListByDateAsync(DateOnly date)
    {
        var salaryProcessList = await _context.SalaryProcesses
        .Include(f => f.FishType)
        .Include(p => p.ProcessingUnit)
        .Where(x => x.Date == date)
        .ToListAsync();
        return salaryProcessList;
    }

    public async Task<IReadOnlyList<SalaryProcess>> GetSalaryProcessesAsync()
    {
        var salaryProcesses =await  _context.SalaryProcesses.ToListAsync();
        return salaryProcesses;
    }

    public Task UpdateSalaryProcessAsync(SalaryProcess salaryProcess)
    {
        _context.Entry(salaryProcess).State = EntityState.Modified;
        return _context.SaveChangesAsync();
    }
}
