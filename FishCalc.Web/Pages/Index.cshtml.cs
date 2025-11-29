using System.Threading.Tasks;
using FishCalc.Web.Entities;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages;

public class IndexModel(IFishTypeService _fishTypeService,ILogger<IndexModel> _logger) : PageModel
{
    public int TotalFishTypesCount { get; set; }
    public async Task OnGet()
    {
        var fishTypes = await _fishTypeService.GetAllFishTypesAsync();
        TotalFishTypesCount = fishTypes.Count;
    }
}
