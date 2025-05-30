﻿using System.ComponentModel.DataAnnotations;

namespace LoanShark.Models
{
    public class Loan
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Loan amount must be greater than ₵1.")]
        public double PurchaseAmount { get; set; }

        [Range(0.0, 100, MinimumIsExclusive = true, ErrorMessage = "Interest Range must be between 0 and 100")]
        public double Rate { get; set; }

        //Term is expressed in years 
        [Range(1, 100, ErrorMessage = "Loan Term must be between 1 and 100 years")]
        public int Term { get; set; }

        public double Payment { get; set; }

        public double TotalInterest { get; set; }

        public double TotalCost { get; set; }

        public List<LoanPayment> Payments { get; set; } = new List<LoanPayment>();
    }
}
