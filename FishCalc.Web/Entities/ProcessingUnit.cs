using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishCalc.Web.Entities;

public class ProcessingUnit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UnitId { get; set; }

    public required string UnitName { get; set; }

    public string? Contact { get; set; }

    //----------- Navigation
    public ICollection<SalaryPayment>? SalaryPayments { get; set; }
}
