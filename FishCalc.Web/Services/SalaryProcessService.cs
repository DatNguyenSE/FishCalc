using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IRepositories;
using FishCalc.Web.Interfaces.IServices;
using FishCalc.Web.Extensions;

namespace FishCalc.Web.Services;

public class SalaryProcessService(ISalaryProcessRepository _repo) : ISalaryProcessService
{
    public async Task CreateSalaryProcessAsync(SalaryProcessDto dto)
    {
        var entity = dto.ToEntity();
        await _repo.CreateSalaryProcessAsync(dto.ToEntity());
    }

    public async Task CreateSalaryProcessesByList(List<SalaryProcessDto> dtos)
    {
         var entities = dtos.Select(dto => dto.ToEntity()).ToList();
        await _repo.CreateSalaryProcessesByList(entities);
    }

    public Task DeleteSalaryProcessAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SalaryProcessDto>> GetAllSalaryProcessesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SalaryProcessDto?> GetSalaryProcessesByDateAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSalaryProcessAsync(SalaryProcessDto dto)
    {
        throw new NotImplementedException();
    }
}
