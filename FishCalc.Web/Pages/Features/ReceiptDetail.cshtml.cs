using FishCalc.Web.DTOs;
using FishCalc.Web.Helpers.Enums;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Features
{
    public class ReceiptDetailModel(ISalaryProcessService _salaryService) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; } = DateTime.Today;

        public List<ReceiptGroupViewModel> ReceiptData { get; set; } = new();

        public decimal GrandTotalMoney { get; set; }
        public decimal GrandTotalWeight { get; set; }
        public string DailyNote { get; set; } = string.Empty;
        public SalaryStatus Status { get; set; }


        public async Task OnGetAsync()
        {
            var queryDate = DateOnly.FromDateTime(SearchDate);

            //  Lấy dữ liệu thô từ Service
            var rawData = await _salaryService.GetSalaryProcessesListByDateAsync(queryDate);

            if (rawData == null || !rawData.Any()) return;

            // Nhóm dữ liệu theo UnitId (Theo tổ) có value là danh sách các dòng cá trong tổ đó
            var unitGroupData = rawData.GroupBy(x => x.UnitId);

            //  Duyệt qua từng nhóm để tạo ViewModel
            foreach (var unit in unitGroupData)
            {
                
                // Lấy tên tổ từ phần tử đầu tiên trong nhóm
                // Lưu ý: Dùng null coalescing (??) đề phòng tên null
                var firstItem = unit.First();
                DailyNote = firstItem.Notes ?? string.Empty;
                Status = firstItem.Status;;

                var unitName = firstItem.UnitName ?? $"Tổ #{firstItem.UnitId}";
                
                var groupDisplay = new ReceiptGroupViewModel
                {
                    UnitName = unitName,
                    Items = new List<ReceiptItemViewModel>()
                };

                // Duyệt qua từng dòng cá trong tổ này
                foreach (var item in unit)
                {
                    groupDisplay.Items.Add(new ReceiptItemViewModel
                    {
                        FishName = item.FishTypeName ,
                        Quantity = item.TotalQuantityProcessed,
                        PricePerUnit = item.PricePerUnit,
                        TotalPrice = item.SalaryPayment,
                        Notes = item.Notes,
                        
                    });
                }

                ReceiptData.Add(groupDisplay);
            }

            // 4. Tính tổng toàn cục (Grand Total)
            GrandTotalMoney = ReceiptData.Sum(x => x.TotalMoney);
            GrandTotalWeight = ReceiptData.Sum(x => x.TotalWeight);
        }
    }
}