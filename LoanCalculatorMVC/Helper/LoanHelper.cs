using LoanCalculatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculatorMVC.Helper
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            //calculate monthly payment
            int termMonthly = loan.Term;
            loan.Payment = calcPayment(loan.Amount, loan.Rate, termMonthly);

            //create a loop from 1 to term
            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalcMonthlyRate(loan.Rate);

            for (int month = 1; month <= termMonthly; month++)
            {
                monthlyInterest = balance * monthlyRate;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                totalInterest += monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.LoanTerm = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                //push object
                loan.Payments.Add(loanPayment);
            }
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            //calculate a payment schedule
            return loan;
        }
        private decimal calcPayment(decimal Amount, decimal Rate, int Term)
        {
            var rateD = Convert.ToDouble(CalcMonthlyRate(Rate));
            var amountD = Convert.ToDouble(Amount);

            var paymentD = (amountD * rateD) / (1 - Math.Pow(1 + rateD, -Term));
            return Convert.ToDecimal(paymentD);
        }
        private decimal CalcMonthlyRate(decimal rate)
        {
            return (rate / 1200);
        }
    }
}
