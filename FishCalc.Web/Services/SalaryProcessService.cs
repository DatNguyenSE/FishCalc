using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IRepositories;
using FishCalc.Web.Interfaces.IServices;
using FishCalc.Web.My.Extensions;

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

   public async Task<IReadOnlyList<SalaryProcessDto?>> GetSalaryProcessesListByDateAsync(DateOnly date)
{
    // 1. Lấy Entity từ Repo (đã có Include)
    var entities = await _repo.GetSalaryProcessesListByDateAsync(date);

    // 2. Map sang DTO
    var dtos = entities
                .Select(entity => entity?.ToDto())
                .ToList();

    return dtos;
}

    public Task UpdateSalaryProcessAsync(SalaryProcessDto dto)
    {
        throw new NotImplementedException();
    }
}
