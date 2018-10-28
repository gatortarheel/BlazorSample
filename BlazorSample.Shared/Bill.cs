using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorSample.Shared
{
    public static class DecimalExtensions
    {
        public static string EmptyIfZero(this decimal value)
        {
            if (value == 0)
                return string.Empty;

            return "$" + value.ToString("0.00");
        }
    }

    public class Bill
    {
        public DateTime Date { get; set; }
        public decimal Debit { get; set; }
        public string Description { get; set; }
        public decimal Credit { get; set; }
    }
}
