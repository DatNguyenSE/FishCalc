using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Fish
{
    public class UpdateFishTypeModel(IFishTypeService fishTypeService) : PageModel
    {
        [BindProperty]
        public FishTypeDto FishType { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if(id == null)
            {
                return(NotFound());
            }
            FishType = await fishTypeService.GetFishTypeByIdAsync(id);
            if(FishType == null)
            {
                return(NotFound());
            }
            return Page();
        }
    }
}
