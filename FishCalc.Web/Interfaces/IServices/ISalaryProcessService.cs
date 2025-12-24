using System;
using FishCalc.Web.DTOs;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces.IServices;

public interface ISalaryProcessService
{
    Task<IReadOnlyList<SalaryProcessDto>> GetAllSalaryProcessesAsync();
    Task<IReadOnlyList<SalaryProcessDto?>> GetSalaryProcessesListByDateAsync(DateOnly date);
   
    Task CreateSalaryProcessAsync(SalaryProcessDto dto);
    Task UpdateSalaryProcessAsync(SalaryProcessDto dto);
    Task DeleteSalaryProcessAsync(int id);
    Task CreateSalaryProcessesByList(List<SalaryProcessDto> dtos);
    Task<ReceiptListDto> GetListSalaryProcessesByDatesAsync(DateOnly startDate, DateOnly endDate);

}
