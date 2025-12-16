using System;
using System.Collections.Generic;
using System.Linq;

namespace FishCalc.Web.DTOs;

    // 1. DTO cho từng dòng chi tiết (Từng loại cá trong tổ)
    public class ReceiptGroupViewModel
    {
        public string UnitName { get; set; } = string.Empty;

        public List<ReceiptItemViewModel> Items { get; set; } = new();

        // Tổng của cả tổ
        public decimal TotalWeight => Items.Sum(x => x.Quantity);
        public decimal TotalMoney => Items.Sum(x => x.TotalPrice);
    }

    // 2. Đại diện cho 1 dòng Cá (Item)
    public class ReceiptItemViewModel
    {
        public string FishName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }  

        public decimal TotalPrice { get; set; } 
        public string? Notes { get; set; }

        public decimal PricePerUnit { get; set; }
    }
