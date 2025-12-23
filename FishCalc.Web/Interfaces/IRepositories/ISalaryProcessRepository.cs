using System;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Interfaces.IRepositories;

public interface ISalaryProcessRepository
{
    Task CreateSalaryProcessAsync(SalaryProcess salaryProcess);
    Task DeleteSalaryProcessAsync(int salaryProcessId);
    Task<IReadOnlyList<SalaryProcess?>> GetSalaryProcessesListByDateAsync(DateOnly date);
    Task<IReadOnlyList<SalaryProcess>> GetSalaryProcessesAsync();
    Task UpdateSalaryProcessAsync(SalaryProcess salaryProcess);
    Task CreateSalaryProcessesByList(List<SalaryProcess> salaryProcesses);

}
