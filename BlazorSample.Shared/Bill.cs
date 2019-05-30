namespace BlazorSample.Shared
{
    using System;
    using System.Collections.Generic;

    public class Bill
    {
        public DateTime Date { get; set; }

        public decimal Debit { get; set; }

        public string Description { get; set; }

        public decimal Credit { get; set; }

        public decimal Type { get; set; }

        public string Message { get; set; }

        public int Frequency { get; set; }

        public bool IsSplit { get; set; }

        public Bill()
        {

        }

        public Bill(DateTime date, decimal debit, decimal credit, string message)
        {
            this.Date = date;
            this.Debit = debit;
            this.Credit = credit;
            this.Description = message;
        }

        public Bill(DateTime date, decimal debit, decimal credit, string message, bool Is)
        {
            this.Date = date;
            this.Debit = debit;
            this.Credit = credit;
            this.Description = message;
        }

        public Bill(int month, int dayOfMonth, decimal debit, decimal credit, string message, int frequency)
        {
            if (frequency == 12)
            {
                this.Date = DateTime.Parse($"{month}/{dayOfMonth}/{DateTime.Now.Year}");
                this.Debit = debit;
                this.Credit = credit;
                if (message == Constants.Message.ManualPay)
                {
                    this.Description = message + $" *** {this.Date.ToShortDateString()} ***";
                }
                else
                {
                    this.Description = message + $" sch {this.Date.ToShortDateString()}";
                }
            }
        }

        public static List<Bill> RetrieveMonthlyBills(DateTime dateToCheck)
        {
            var listOfBills = new List<Bill>();
            var monthlyBillStubs = BillStub.RetrieveMonthlyBillStubs();

            // all the monthly bills
            foreach (var stub in monthlyBillStubs)
            {
                var bill = new Bill(DateTime.Parse($"{dateToCheck.Month}/{stub.DayOfMonth}/{dateToCheck.Year}"), stub.Debit, stub.Credit, stub.Message, stub.IsSplit);
                listOfBills.Add(bill);
            }

            return listOfBills;
        }


        public static List<Bill> RetrievePaydays(DateTime dateToCalculate)
        {
            var payDays = new List<DateTime>();
            var paychecks = new List<Bill>();
            try
            {
                var paydayAnchor = Convert.ToDateTime("10/26/2018"); // dateToCalculate;

                // how many years since 10/26/2018 ?  -- calculate that far + 1
                var now = DateTime.Now;
                var span = now - paydayAnchor;
                var days = span.TotalDays;
                int years = Convert.ToInt32(days / Constants.DaysInYear) + 1;
                payDays = Utility.BuildListOfDates(paydayAnchor, Constants.DaysInPaycycle, years);

                foreach (var payday in payDays)
                {
                    var bill = new Bill(payday, 0, Constants.Amount.PayCheck, Constants.Message.PayCheck, false);
                    if (bill.Date.Month == dateToCalculate.Month && bill.Date.Year == dateToCalculate.Year)
                    {
                        paychecks.Add(bill);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return paychecks;
        }


        public static List<Bill> RetrieveGwenPayments(DateTime dateToCheck)
        {
            var payGwen = new List<Bill>();

            // start with the 15th - do not pay last day of month
            // pay $60 every Mon/Tue/Wed/Thu
            // pay $200 every Fri
            int year = dateToCheck.Year;
            var startDate = DateTime.Parse($"{dateToCheck.Month}/{Constants.GwenStartDay}/{dateToCheck.Year}");
            var loopDate = startDate;
            while (loopDate.Month == dateToCheck.Month)
            {
                var lastDayOfMonth = Utility.GetLastDayOfMonth(startDate);

                // do not pay on the last day of the month
                if (loopDate == lastDayOfMonth)
                {
                    break;
                }

                if (loopDate.DayOfWeek == DayOfWeek.Friday)
                {
                    var bill = new Bill(loopDate, Constants.GwenFridayPay, 0, $"{Constants.Message.TransferGwen} {loopDate.DayOfWeek}", false);
                    payGwen.Add(bill);
                }
                else if (loopDate.DayOfWeek == DayOfWeek.Monday || loopDate.DayOfWeek == DayOfWeek.Tuesday || loopDate.DayOfWeek == DayOfWeek.Wednesday
                    || loopDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    var bill = new Bill(loopDate, Constants.GwenRegularPay, 0, $"{Constants.Message.TransferGwen} {loopDate.DayOfWeek}", false);
                    payGwen.Add(bill);
                }

                loopDate = loopDate.AddDays(1);

                // System.Diagnostics.Trace.WriteLine(loopDate);
            }

            return payGwen;
        }
    }

}
