using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Fish
{
    public class UpdateFishTypeModel(IFishTypeService fishTypeService,IWebHostEnvironment _environment) : PageModel
    {
        [BindProperty]
        public FishTypeDto FishType { get; set; }
        [BindProperty] 
        public IFormFile? ImageUpload { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {

            FishType = await fishTypeService.GetFishTypeByIdAsync(id);
            if(FishType == null)
            {
                return(NotFound());
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if(ImageUpload != null)
            {
                var fileName = $"{FishType.Name}_img_{ImageUpload.FileName}.png";
               
                // 2. Xác định thư mục lưu: wwwroot/uploads/fish-images 
                // Lưu ý: Bạn cần tạo tay thư mục này trong project trước hoặc code tự tạo
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "fish-images");
                
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                // 3. Lưu file vật lý vào ổ cứng
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                // 4. Cập nhật đường dẫn vào DTO để lưu xuống DB
                // Đường dẫn web bắt đầu từ /
                FishType.ImgFishUrl = $"/uploads/fish-images/{fileName}";
            }

             await fishTypeService.UpdateFishTypeAsync(FishType);
            return RedirectToPage("./FishTypes");
        }
    }
}
