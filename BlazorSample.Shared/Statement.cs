using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSample.Shared
{
    [Serializable]
    public class Statement
    {
        public string StartDate { get; set; }
        public string CloseDate { get; set; }
        public decimal Balance { get; set; }
    }

   
}
