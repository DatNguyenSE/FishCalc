using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Features
{
    public class ReceiptModel(ISalaryProcessService _salaryService) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; } = DateTime.Today;

        public List<ReceiptGroupViewModel> ReceiptData { get; set; } = new();

        public decimal GrandTotalMoney { get; set; }
        public decimal GrandTotalWeight { get; set; }
        public string DailyNote { get; set; } = string.Empty;


        public async Task OnGetAsync()
        {
            var queryDate = DateOnly.FromDateTime(SearchDate);

            // 1. Lấy dữ liệu thô từ Service
            var rawData = await _salaryService.GetProcessesListByDateAsync(queryDate);

            if (rawData == null || !rawData.Any()) return;

            // 2. Nhóm dữ liệu theo UnitId
            var groupedData = rawData.GroupBy(x => x.UnitId);

            // 3. Duyệt qua từng nhóm để tạo ViewModel
            foreach (var group in groupedData)
            {
                
                // Lấy tên tổ từ phần tử đầu tiên trong nhóm
                // Lưu ý: Dùng null coalescing (??) đề phòng tên null
                var firstItem = group.First();
                DailyNote = firstItem.Notes ?? string.Empty;
                var unitName = firstItem.UnitName ?? $"Tổ #{firstItem.UnitId}";
                
                var groupVm = new ReceiptGroupViewModel
                {
                    UnitName = unitName,
                    Items = new List<ReceiptItemViewModel>()
                };

                // Duyệt qua từng dòng cá trong tổ này
                foreach (var item in group)
                {
                    groupVm.Items.Add(new ReceiptItemViewModel
                    {
                        FishName = item.FishTypeName ,
                        Quantity = item.TotalQuantityProcessed,
                        TotalPrice = item.SalaryPayment,
                        Notes = item.Notes
                        // PricePerKg sẽ tự động được tính trong class ViewModel
                    });
                }

                ReceiptData.Add(groupVm);
            }

            // 4. Tính tổng toàn cục (Grand Total)
            GrandTotalMoney = ReceiptData.Sum(x => x.TotalMoney);
            GrandTotalWeight = ReceiptData.Sum(x => x.TotalWeight);
        }
    }
}