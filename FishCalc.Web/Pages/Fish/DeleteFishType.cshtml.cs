using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Fish
{
    public class DeleteFishTypeModel(IFishTypeService fishTypeService, IWebHostEnvironment environment) : PageModel
    {
        [BindProperty]
        public FishTypeDto FishType { get; set; } = new();

        // GET: Hiển thị trang xác nhận xóa
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0) return NotFound();

            var fish = await fishTypeService.GetFishTypeByIdAsync(id);
            if (fish == null) return NotFound();

            FishType = fish;
            return Page();
        }

        // POST: Thực hiện xóa
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Kiểm tra tồn tại
            var fish = await fishTypeService.GetFishTypeByIdAsync(id);
            if (fish == null) return NotFound();

            // 1. Xóa ảnh vật lý trong thư mục uploads (nếu có)
            if (!string.IsNullOrEmpty(fish.ImgFishUrl))
            {
                // Chuyển URL web (/uploads/...) thành đường dẫn ổ cứng
                var relativePath = fish.ImgFishUrl.TrimStart('/', '\\');
                var absolutePath = Path.Combine(environment.WebRootPath, relativePath);

                if (System.IO.File.Exists(absolutePath))
                {
                    try
                    {
                        System.IO.File.Delete(absolutePath);
                    }
                    catch
                    {
                        // Log lỗi nếu cần, nhưng không chặn quy trình xóa data
                    }
                }
            }

            // 2. Gọi service xóa dữ liệu trong DB
            await fishTypeService.DeleteFishTypeAsync(id);

            return RedirectToPage("./FishTypes");
        }
    }
}