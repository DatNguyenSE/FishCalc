using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class Receipt
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReceiptId { get; set; }

    [Required]
    public int PaymentId { get; set; }     // FK -> SalaryPayment

    [Required]
    public int FishTypeId { get; set; }    // FK -> FishType

    [Required]
    public int UnitId { get; set; }        // FK -> ProcessingUnit

    public string? Notes { get; set; }


}

