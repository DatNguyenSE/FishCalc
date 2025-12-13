using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FishCalc.Web.Entities;

namespace FishCalc.Web.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }
    
    public DbSet<FishType> FishTypes { get; set; }
    public DbSet<FishPrice> FishPrices { get; set; }
    public DbSet<ProcessingUnit> ProcessingUnits { get; set; }
    public DbSet<SalaryProcess> SalaryProcesses { get; set; }
    
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     base.OnModelCreating(modelBuilder);

//     // --- BẮT ĐẦU SỬA TỪ ĐÂY ---

//     // 1. Chỉ định rõ mối quan hệ và KHÓA NGOẠI
//     modelBuilder.Entity<FishType>()
//         .HasOne(a => a.FishPrice)
//         .WithOne(b => b.FishType)
//         .HasForeignKey<FishPrice>(b => b.FishTypeId); // <--- LỆNH QUAN TRỌNG NHẤT

//     // 2. Fix lỗi warning decimal và đảm bảo không bị làm tròn về 0
//     modelBuilder.Entity<FishPrice>()
//         .Property(p => p.PricePerUnitOfMeasure)
//         .HasColumnType("decimal(18,2)"); 
//         // Hoặc .HasPrecision(18, 2);

//     // --- KẾT THÚC SỬA ---
    
// }
}
