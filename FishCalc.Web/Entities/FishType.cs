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
    public string? ImgFishUrl { get; set; } 


    // -----------Nav property
    //1-N vì một loại cá có thể có nhiều giá khác nhau theo thời gian
    public virtual ICollection<FishPrice> FishPrices { get; set; } = new List<FishPrice>();
}



