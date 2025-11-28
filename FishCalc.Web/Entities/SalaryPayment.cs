using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class SalaryPayment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    public int FishId { get; set; }

    [Required]
    public int UnitId { get; set; }

    [Required]
    public decimal TotalQuantityProcessed { get; set; }  // decimal để tính chính xác hơn

    public string? Notes { get; set; }

    // ---------------- Navigation Properties---------------

    public required FishType FishType { get; set; } 

    public required ProcessingUnit ProcessingUnit { get; set; }
}