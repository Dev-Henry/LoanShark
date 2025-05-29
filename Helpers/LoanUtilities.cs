using LoanShark.Models;
using System.Security.Cryptography.X509Certificates;

namespace LoanShark.Helpers
{
    public static class LoanUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>loan object</returns>
        public static Loan GetPayment(Loan loan)
        {
            loan.Payments.Clear();


            //Calculate the monthly payment
            loan.Payment = CalculatePayment(loan.PurchaseAmount, loan.Rate, loan.Term);

            var loanMonths = loan.Term * 12;

            //variables to hold the total interest and balance
            double balance = loan.PurchaseAmount;
            double totalInterest = 0;
            double monthlyPrincipal = 0;
            double monthlyInterest = 0;
            double monthlyRate = CalculateMonthlyRate(loan.Rate);

            for (int month = 1; month <= loanMonths; month++)
            {
                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new LoanPayment();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance < 0 ? 0 : balance;

                //Add the payment to the list
                loan.Payments.Add(loanPayment);
            };

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.PurchaseAmount + totalInterest;

            return loan;

        }

        /// <summary>
        /// calculate the payment for a simple interest loan
        /// </summary>
        /// <param name="amount">Loan Amount</param>
        /// <param name="rate">Annualized Loan Rate</param>
        /// <param name="term">Term in years</param>
        /// <returns>A monthly payment as a double </returns>

        public static double CalculatePayment(double amount, double rate, double term)
        {
            var monthlyRate = CalculateMonthlyRate(rate);

            var payment = (amount * monthlyRate) / (1 - Math.Pow(1 + monthlyRate, -term * 12));

            return payment;
        }

        public static double CalculateMonthlyRate(double rate)
        {
            return rate / 1200;
        }

        public static double CalculateMonthlyInterest(double balance, double monthlyrate)
        {
            return balance * monthlyrate;
        }

    }
}
