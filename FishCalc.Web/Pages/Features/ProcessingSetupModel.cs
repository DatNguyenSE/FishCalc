using FishCalc.Web.DTOs;
using FishCalc.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FishCalc.Web.Interfaces.IServices;
using System.Text.Json;
namespace FishCalc.Web.Pages.Features
{
    public class ProcessingSetupModel(IFishTypeService fishTypeService, IProcessingUnitService processingUnitService) : PageModel
    {
        public IReadOnlyList<FishTypeDto> FishTypes { get; set; } = new List<FishTypeDto>();
        public IReadOnlyList<ProcessingUnitDto> ProcessingUnit { get; set; } = new List<ProcessingUnitDto>();


        [BindProperty]
        public List<int> SelectedFishIds { get; set; } = new List<int>();
        [BindProperty]
        public List<int> SelectedUnitIds { get; set; } = new List<int>();

        public async Task OnGet()
        {
            FishTypes = await fishTypeService.GetAllFishTypesAsync();
            ProcessingUnit = await processingUnitService.GetAllProcessingUnitsAsync();
        }
        public async Task<IActionResult> OnPost()
        {
          if (SelectedFishIds == null || SelectedFishIds.Count == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn ít nhất 1 loại cá để tiếp tục.");
                // Phải gọi lại OnGet() để tải lại dữ liệu FishTypes nếu có lỗi
                await OnGet(); 
                return Page();
            }
            if (SelectedUnitIds == null || SelectedUnitIds.Count == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn ít nhất 1 đơn vị để tiếp tục.");
                await OnGet(); 
                return Page();
            }
            var fishIdsJson = JsonSerializer.Serialize(SelectedFishIds);
            var unitIdsJson = JsonSerializer.Serialize(SelectedUnitIds);
            TempData["SelectedFishIds"] = fishIdsJson;
            TempData["SelectedUnitIds"] = unitIdsJson;
            
            return RedirectToPage("/Features/SalaryProcess");
            // return RedirectToPage("/Features/SalaryProcess", new { 
            //     SelectedFishIds = SelectedFishIds, 
            //     SelectedUnitIds = SelectedUnitIds 
            // });
        }
    }
}
