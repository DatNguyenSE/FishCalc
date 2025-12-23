using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FishCalc.Web.Helpers.Enums;

namespace FishCalc.Web.Entities;

public class SalaryProcess
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SalaryProcessId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public int FishTypeId { get; set; }

    [Required]
    public int ProcessingUnitId { get; set; }
    
    [Required]
    public decimal PricePerUnit { get; set; }

    [Required]
    public decimal TotalQuantityProcessed { get; set; }  // decimal để tính chính xác hơn

    [Required]
    public decimal SalaryPayment { get; set; }
    

    public string? Notes { get; set; }
    [Required]
    public SalaryStatus Status { get; set; } = SalaryStatus.Unpaid;
    // ---------------- Navigation Properties---------------

    public  FishType? FishType { get; set; } 

    public  ProcessingUnit? ProcessingUnit { get; set; }
}