using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Fish
{
    public class AddFishTypeModel(IFishTypeService fishTypeService, IWebHostEnvironment _environment) : PageModel
    {
        [BindProperty]
        public FishTypeDto FishType { get; set; } = new(); // Khởi tạo để tránh null

        [BindProperty]
        public IFormFile? ImageUpload { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Loại bỏ Id và ImgFishUrl khỏi kiểm tra validation vì:
            // 1. Id sẽ do Database tự sinh (Identity)
            // 2. ImgFishUrl sẽ được tạo sau khi upload ảnh thành công ở bên dưới
            ModelState.Remove(nameof(FishType) + "." + nameof(FishType.Id));
            ModelState.Remove(nameof(FishType) + "." + nameof(FishType.ImgFishUrl));

            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            // Xử lý Upload ảnh
            if (ImageUpload != null)
            {
                // Sanitize tên file để tránh lỗi ký tự đặc biệt
                var safeName = Path.GetFileNameWithoutExtension(FishType.Name)
                                   .Replace(" ", "").ToLower();
                
                var fileName = $"{safeName}.png";

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "fish-images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                FishType.ImgFishUrl = $"/uploads/fish-images/{fileName}";
            }
            else
            {
                FishType.ImgFishUrl = "/img/default-fish.png"; 
            }

            await fishTypeService.CreateFishTypeAsync(FishType);
            
            return RedirectToPage("./FishTypes");
        }
    }
}