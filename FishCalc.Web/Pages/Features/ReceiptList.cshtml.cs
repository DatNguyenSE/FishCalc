using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Features
{
    public class ReceiptListModel : PageModel
    {
     private readonly ISalaryProcessService _salaryService;

    public ReceiptListModel(ISalaryProcessService salaryService)
    {
        _salaryService = salaryService;
    }

    public ReceiptListDto MatrixData { get; set; }

    public async Task OnGetAsync(DateOnly? startDate, DateOnly? endDate)
    {
        // Xử lý ngày mặc định nếu null
        var from = startDate ?? DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
        var to = endDate ?? DateOnly.FromDateTime(DateTime.Now);

        // Gọi Service lấy cục dữ liệu đã được chế biến sẵn
        MatrixData = await _salaryService.GetListSalaryProcessesByDatesAsync(from, to);
    }
    }
}
