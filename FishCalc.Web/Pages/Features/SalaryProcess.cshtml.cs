using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using System;

namespace FishCalc.Web.Pages.Features
{
    // Cấu trúc DTO để nhận dữ liệu nhập liệu từ bảng (nếu bạn muốn lưu)
    // Tôi sẽ không dùng nó trong ví dụ này để giữ đơn giản, nhưng bạn nên định nghĩa nó.
    
    public class SalaryProcessModel(
        IFishTypeService _fishTypeService,
        IProcessingUnitService _unitService) : PageModel
    {

        // Dữ liệu đã Tái tạo (sẽ được sắp xếp)
        public IReadOnlyList<FishTypeDto> SelectedFishTypes { get; set; } = new List<FishTypeDto>();
        public IReadOnlyList<ProcessingUnitDto> SelectedProcessingUnits { get; set; } = new List<ProcessingUnitDto>();
        
        public DateTime ProcessDate { get; set; }

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

        public async Task<IActionResult> OnPostProcessData()
        {


            return RedirectToPage("/Features/CalculationComplete");
        }
    }
}