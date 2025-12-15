using FishCalc.Web.DTOs;
using FishCalc.Web.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FishCalc.Web.Pages.Fish;

// Class name phải là FishTypesModel (Hoặc FishTypes)
public class FishTypesModel(IFishTypeService _fishTypeService) : PageModel
{
    // Lưu tham số vào biến readonly để sử dụng trong các phương thức
    
    // Biến để chứa dữ liệu hiển thị ra màn hình
    public IReadOnlyList<FishTypeDto> FishTypes { get; set; } = new List<FishTypeDto>();

    // Hàm này chạy khi người dùng truy cập trang (GET request)
    public async Task OnGetAsync()
    {
        // Gọi Service lấy danh sách DTO
        FishTypes = await _fishTypeService.GetAllFishTypesAsync();
    }
}