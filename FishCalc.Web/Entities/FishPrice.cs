using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class FishPrice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PriceId { get; set; } // - Auto Increment

    public decimal PricePerUnitOfMeasure { get; set; }

    [Required]
    public int FishTypeId { get; set; }

    public virtual FishType? FishType { get; set; }
}
