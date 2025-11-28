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
    public DbSet<SalaryPayment> SalaryPayments { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    
}
