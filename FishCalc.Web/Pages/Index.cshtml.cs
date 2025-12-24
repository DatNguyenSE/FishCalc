using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFishTypeService _fishService;
        private readonly ISalaryProcessService _salaryService;
        private readonly IProcessingUnitService _unitService;

        public IndexModel(
            IFishTypeService fishService, 
            ISalaryProcessService salaryService, 
            IProcessingUnitService unitService)
        {
            _fishService = fishService;
            _salaryService = salaryService;
            _unitService = unitService;
        }

        // --- Data Properties ---
        public int TotalFishTypesCount { get; set; }
        
        public decimal TotalWeightToday { get; set; }
        public decimal WeightGrowth { get; set; } 

        public decimal TotalSalaryMonth { get; set; }
        
        public int ActiveUnitsCount { get; set; }
        public int TotalUnitsCount { get; set; }

        public List<SalaryProcessDto> RecentActivities { get; set; } = new();

        public List<string> ChartLabels { get; set; } = new();
        public List<decimal> ChartValues { get; set; } = new();

        public async Task OnGetAsync()
        {
      
            var allFish = await _fishService.GetAllFishTypesAsync();
            TotalFishTypesCount = allFish.Count;

            var today = DateOnly.FromDateTime(DateTime.Now);
            var todayList = await _salaryService.GetSalaryProcessesListByDateAsync(today);
            
            if (todayList != null)
            {
                TotalWeightToday = todayList.Sum(x => x.TotalQuantityProcessed );
                
                RecentActivities = todayList
                    .OrderByDescending(x => x.UnitId) 
                    .ToList();
            }

            decimal previousWeight = 0;

            for (int i = 1; i <= 10; i++)
            {
                var checkDate = today.AddDays(-i);
                var checkList = await _salaryService.GetSalaryProcessesListByDateAsync(checkDate);
                var weight = checkList?.Sum(x => x.TotalQuantityProcessed) ?? 0;

                if (weight > 0)
                {
                    previousWeight = weight;
                    break;
                }
            }

            if (previousWeight > 0)
                WeightGrowth = ((TotalWeightToday - previousWeight) / previousWeight) * 100;
            else
                WeightGrowth = TotalWeightToday > 0 ? 100 : 0;


            var firstDayOfMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            var monthMatrixData = await _salaryService.GetListSalaryProcessesByDatesAsync(firstDayOfMonth, lastDayOfMonth);
            
            if (monthMatrixData != null)
            {

                TotalSalaryMonth = monthMatrixData.ColumnTotals.Values.Sum();
            }

            var units = await _unitService.GetAllProcessingUnitsAsync();
            TotalUnitsCount = units.Count;
            ActiveUnitsCount = units.Count; 

            for (int i = 9; i >= 0; i--)
            {
                var d = today.AddDays(-i);
                ChartLabels.Add(d.ToString("dd/MM"));
                
                var dailyList = await _salaryService.GetSalaryProcessesListByDateAsync(d);
                decimal dailyWeight = dailyList?.Sum(x => x.TotalQuantityProcessed ) ?? 0;
                ChartValues.Add(dailyWeight);
            }
        }
    }
}