using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class FishType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? UnitOfMeasure {get;set;} ="Kilogram";
    public string? ImgFishUrl { get; set; } 


    // -----------Nav property
    public FishPrice? FishPrice {get;set;} =null!;

}



