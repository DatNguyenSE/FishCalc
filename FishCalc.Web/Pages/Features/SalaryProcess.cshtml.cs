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

        [BindProperty] // Dữ liệu nhập liệu từ bảng sẽ được liên kết vào đây
        public Dictionary<int, Dictionary<int, decimal>> Data { get; set; }
        // Key: UnitId, Value: (Key: FishId, Value: TotalQuantityProcessed)

        public async Task<IActionResult> OnPostProcessData()
        {
            try
            {

                // === TỐI ƯU HÓA: LẤY GIÁ CÁ 1 LẦN DUY NHẤT ===

                // B1: Lấy tất cả FishId có trong dữ liệu nhập lên (loại bỏ trùng lặp)
                var allFishIds = Data.SelectMany(unit => unit.Value.Keys).Distinct().ToList(); // return fishTypeId

                var fishList = await _fishTypeService.GetListFishTypeByIdAsync(allFishIds);

                // B3: Tạo Dictionary (Key: FishId, Value: Price)
                var fishPriceDict = fishList.ToDictionary(f => f.Id, f => f.PricePerUnitOfMeasure);

                var salaryProcessesList = new List<SalaryProcessDto>();

                // Xử lý dữ liệu nhập liệu
                foreach (var unitEntry in Data)
                {
                    int unitId = unitEntry.Key;
                    var fishMap = unitEntry.Value;

                    foreach (var fishEntry in fishMap)
                    {
                        int fishTypeId = fishEntry.Key;
                        decimal totalQuantityProcessed = fishEntry.Value;

                        if (totalQuantityProcessed <= 0) continue;

                        // TRA CỨU GIÁ TỪ DICTIONARY (Không gọi DB ở đây nữa)
                        if (fishPriceDict.TryGetValue(fishTypeId, out decimal pricePerUnit))
                        {
                            // --- CODE KIỂM TRA ---
                            // In thẳng giá ra màn hình web để bạn nhìn thấy
                            if (pricePerUnit == 0)
                            {
                                ModelState.AddModelError("", $"LỖI: Cá ID {fishTypeId} đang có giá là 0 đồng!");
                                return Page(); // Dừng lại để bạn đọc lỗi
                            }
                            // ---------------------

                            decimal salaryPayment = pricePerUnit * totalQuantityProcessed;

                            var dto = new SalaryProcessDto
                            {
                                Date = DateOnly.FromDateTime(ProcessDate),
                                FishTypeId = fishTypeId,
                                UnitId = unitId,
                                PricePerKg = pricePerUnit,
                                TotalQuantityProcessed = totalQuantityProcessed,
                                SalaryPayment = salaryPayment,
                                Notes = NoteOption,

                            };
                            Console.WriteLine("-----------------XXXXXXXXXXXXXXXXXXXXXXXXXXXXX-----------------------------------------------------------------");
                            Console.WriteLine($"DEBUG: Cá ID {fishTypeId}, Giá {pricePerUnit}, SL {totalQuantityProcessed}, Thành tiền); {salaryPayment}");

                            salaryProcessesList.Add(dto);
                        }

                    }
                }

                // Gửi dữ liệu xuống Service
                if (salaryProcessesList.Any())
                {
                    await _salaryService.CreateSalaryProcessesByList(salaryProcessesList);

                    return RedirectToPage("/Features/Receipt");
                }
                else
                {
                    foreach (var key in ModelState.Keys)
                    {
                        ModelState[key].Errors.Clear();
                    }

                    ModelState.AddModelError("", "Không thể lưu do chưa nhập số lượng");

                    return Page();
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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