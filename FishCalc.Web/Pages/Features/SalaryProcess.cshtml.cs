using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using System;
using Humanizer;

namespace FishCalc.Web.Pages.Features
{
    // Cấu trúc DTO để nhận dữ liệu nhập liệu từ bảng (nếu bạn muốn lưu)
    // Tôi sẽ không dùng nó trong ví dụ này để giữ đơn giản, nhưng bạn nên định nghĩa nó.

    public class SalaryProcessModel(
        IFishTypeService _fishTypeService,
        IProcessingUnitService _unitService,
        ISalaryProcessService _salaryService
        ) : PageModel
    {

        // Dữ liệu đã Tái tạo (sẽ được sắp xếp)
        public IReadOnlyList<FishTypeDto> SelectedFishTypes { get; set; } = new List<FishTypeDto>();
        public IReadOnlyList<ProcessingUnitDto> SelectedProcessingUnits { get; set; } = new List<ProcessingUnitDto>();
        [BindProperty]
        public DateTime ProcessDate { get; set; }
        [BindProperty]
        public string? NoteOption { get; set; }
        public string? SuccessMessage { get; set; } 
        public async Task OnGetAsync()
        {
            List<int> fishIds = new List<int>();
            List<int> unitIds = new List<int>();

            ProcessDate = DateTime.Today;

            // === 1. ĐỌC DỮ LIỆU TỪ TempData VÀ GIẢI MÃ JSON ===

            if (TempData.ContainsKey("SelectedFishIds"))
            {
                var fishIdsJson = TempData["SelectedFishIds"] as string;
                if (!string.IsNullOrEmpty(fishIdsJson))
                {
                    fishIds = JsonSerializer.Deserialize<List<int>>(fishIdsJson) ?? new List<int>();
                }
            }

            if (TempData.ContainsKey("SelectedUnitIds"))
            {
                var unitIdsJson = TempData["SelectedUnitIds"] as string;
                if (!string.IsNullOrEmpty(unitIdsJson))
                {
                    unitIds = JsonSerializer.Deserialize<List<int>>(unitIdsJson) ?? new List<int>();
                }
            }


            //  Fish Types SẮP XẾP theo Id tăng dần
            if (fishIds.Any())
            {
                var fishList = await _fishTypeService.GetListFishTypeByIdAsync(fishIds);
                SelectedFishTypes = fishList.OrderBy(f => f.Id).ToList();
            }

            // Processing Units SẮP XẾP theo UnitId tăng dần
            if (unitIds.Any())
            {
                var unitList = await _unitService.GetListUnitsByIdsAsync(unitIds);
                SelectedProcessingUnits = unitList.OrderBy(u => u.UnitId).ToList();
            }
        }

        private async Task<Decimal> CalculateSalaryPayment(int fishTypeId, decimal quantityProcessed)
        {
            var fishCalc = await _fishTypeService.GetFishTypeByIdAsync(fishTypeId);
            if (fishCalc == null)
            {
                throw new ArgumentException($"FishType with id {fishTypeId} not found.");
            }
            decimal PricePerUnitOfMeasure = fishCalc.FishPrice.PricePerUnitOfMeasure;
            return PricePerUnitOfMeasure * quantityProcessed;
        }

        [BindProperty] // Dữ liệu nhập liệu từ bảng sẽ được liên kết vào đây
        public Dictionary<int, Dictionary<int, decimal>> Data { get; set; }
        public async Task<IActionResult> OnPostProcessData()
        {
            var salaryProcessesList = new List<SalaryProcessDto>();
            // Xử lý dữ liệu nhập liệu từ bảng ở đây
            foreach (var unitEntry in Data)
            {
                int unitId = unitEntry.Key;
                var fishList = unitEntry.Value;
                foreach (var fishEntry in fishList)
                {
                    int fishTypeId = fishEntry.Key;
                    decimal totalQuantityProcessed = fishEntry.Value;
                    if (totalQuantityProcessed <= 0) continue;
                    
                    var salaryPayment = await CalculateSalaryPayment(fishTypeId, totalQuantityProcessed);
                    var dto = new SalaryProcessDto
                    {
                        PaymentDate = DateOnly.FromDateTime(ProcessDate),
                        FishId = fishTypeId,
                        UnitId = unitId,
                        TotalQuantityProcessed = totalQuantityProcessed,
                        SalaryPayment = salaryPayment,
                        Notes = NoteOption
                    };
                    salaryProcessesList.Add(dto);

                }
            }
            // Gửi dữ liệu xuống Service
    if (salaryProcessesList.Any()) 
    {
        await _salaryService.CreateSalaryProcessesByList(salaryProcessesList);
        
        // --- THAY ĐỔI Ở ĐÂY ---
        
        // 1. Thiết lập thông báo thành công
        SuccessMessage = $"Đã lưu thành công {salaryProcessesList.Count} bản ghi!";

        // 2. (Tùy chọn) Xóa dữ liệu cũ để Form trống trơn cho lần nhập tiếp theo
        Data = new Dictionary<int, Dictionary<int, decimal>>();
        NoteOption = string.Empty; 
        
        // 3. Ở lại trang hiện tại (Render lại trang này)
        return Page(); 
    }
    else 
    {
        // Trường hợp người dùng bấm lưu mà không nhập gì
        ModelState.AddModelError(string.Empty, "Bạn chưa nhập số liệu nào cả!");
        return Page();
    }
        }
    }
    

    /*  CHUYEN TRANG
            // 1. Biến Dictionary thành chuỗi String (JSON)
        var jsonString = JsonSerializer.Serialize(Data);

        // 2. Nhét vào túi TempData
        TempData["SalaryData"] = jsonString;

        // 3. Chuyển sang trang kết quả
        return RedirectToPage("/SalaryResult");
            }
        }
    */

}