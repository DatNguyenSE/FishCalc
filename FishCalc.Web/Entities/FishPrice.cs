using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class FishPrice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PriceId { get; set; } // - Auto Increment

    public decimal PricePerUnit { get; set; }
    public string? UnitOfMeasure {get;set;} = "Kilogram";
    public DateTime EffectiveDate { get; set; } = DateTime.Now;


    [Required]
    public int FishTypeId { get; set; }

}
