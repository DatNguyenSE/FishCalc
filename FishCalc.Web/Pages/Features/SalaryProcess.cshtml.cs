using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;

namespace FishCalc.Web.Pages.Features;

public class SalaryProcessModel(
    IFishTypeService _fishTypeService,
    IProcessingUnitService _unitService,
    ISalaryProcessService _salaryService
) : PageModel
{
    /* =======================
     * METADATA HIỂN THỊ
     * ======================= */
    public IReadOnlyList<FishTypeDto> SelectedFishTypes { get; private set; } = [];
    public IReadOnlyList<ProcessingUnitDto> SelectedProcessingUnits { get; private set; } = [];

    [BindProperty(SupportsGet = true)]
    public DateTime ProcessDate { get; set; } = DateTime.Today;

    [BindProperty]
    public string? NoteOption { get; set; }

    /* =======================
     * DỮ LIỆU NGƯỜI DÙNG NHẬP
     * unitId -> (fishId -> quantity)
     * ======================= */
    [BindProperty]
    public Dictionary<int, Dictionary<int, decimal?>> Data { get; set; } = new();

    /* ============================================================
     * LOAD METADATA DÙNG CHUNG (GET + POST)
     * ============================================================ */
    private async Task LoadMetadataAsync()
    {
        List<int> fishIds = new();
        List<int> unitIds = new();

        if (TempData.Peek("SelectedFishIds") is string fishJson)
            fishIds = JsonSerializer.Deserialize<List<int>>(fishJson) ?? [];

        if (TempData.Peek("SelectedUnitIds") is string unitJson)
            unitIds = JsonSerializer.Deserialize<List<int>>(unitJson) ?? [];

        if (fishIds.Any())
        {
            var fishList = await _fishTypeService.GetListFishTypeByIdAsync(fishIds);
            SelectedFishTypes = fishList.OrderBy(f => f.Id).ToList();
        }

        if (unitIds.Any())
        {
            var unitList = await _unitService.GetListUnitsByIdsAsync(unitIds);
            SelectedProcessingUnits = unitList.OrderBy(u => u.UnitId).ToList();
        }
    }

    /* =======================
     * GET
     * ======================= */
    public async Task OnGetAsync()
    {
        await LoadMetadataAsync();

        if (!SelectedFishTypes.Any() || !SelectedProcessingUnits.Any())
            return;

        try
        {
            var existing =
                await _salaryService.GetSalaryProcessesListByDateAsync(
                    DateOnly.FromDateTime(ProcessDate));

            if (existing == null || !existing.Any())
                return;

            foreach (var item in existing)
            {
                if (!Data.ContainsKey(item.UnitId))
                    Data[item.UnitId] = new Dictionary<int, decimal?>();

                Data[item.UnitId][item.FishTypeId] = item.TotalQuantityProcessed;
            }

            NoteOption = existing
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Notes))
                ?.Notes;
        }
        catch
        {
            // Ignore nếu chưa có dữ liệu
        }
    }

    /* =======================
     * POST
     * ======================= */
    public async Task<IActionResult> OnPostProcessData()
    {
        TempData.Keep("SelectedFishIds");
        TempData.Keep("SelectedUnitIds");

        // 🔑 QUAN TRỌNG: load lại metadata trước khi return Page()
        await LoadMetadataAsync();

        /* ===== CHECK TRÙNG NGÀY ===== */
        var existed =
            await _salaryService.GetSalaryProcessesListByDateAsync(
                DateOnly.FromDateTime(ProcessDate));

        if (existed != null && existed.Any())
        {
            ModelState.AddModelError(
                string.Empty,
                $"Ngày {ProcessDate:dd/MM/yyyy} đã được tính lương. Vui lòng chọn ngày khác."
            );

            return Page(); // ✅ KHÔNG MẤT DATA
        }

        /* ===== XỬ LÝ LƯU ===== */
        var allFishIds = Data.SelectMany(u => u.Value.Keys).Distinct().ToList();
        var fishList = await _fishTypeService.GetListFishTypeByIdAsync(allFishIds);
        var priceMap = fishList.ToDictionary(f => f.Id, f => f.PricePerUnit);

        var salaryList = new List<SalaryProcessDto>();

        foreach (var unitEntry in Data)
        {
            int unitId = unitEntry.Key;

            foreach (var fishEntry in unitEntry.Value)
            {
                if (!fishEntry.Value.HasValue || fishEntry.Value <= 0)
                    continue;

                if (!priceMap.TryGetValue(fishEntry.Key, out var price) || price <= 0)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        $"Giá cá ID {fishEntry.Key} không hợp lệ."
                    );
                    return Page();
                }

                salaryList.Add(new SalaryProcessDto
                {
                    Date = DateOnly.FromDateTime(ProcessDate),
                    UnitId = unitId,
                    FishTypeId = fishEntry.Key,
                    PricePerUnit = price,
                    TotalQuantityProcessed = fishEntry.Value.Value,
                    SalaryPayment = price * fishEntry.Value.Value,
                    Notes = NoteOption
                });
            }
        }

        if (!salaryList.Any())
        {
            ModelState.AddModelError(
                string.Empty,
                "Chưa nhập số lượng cá làm của các tổ"
            );
            return Page();
        }

        await _salaryService.CreateSalaryProcessesByList(salaryList);
        return RedirectToPage("/Features/Receipt");
    }
}
