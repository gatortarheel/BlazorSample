namespace BlazorSample.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlazorSample.Shared;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    public class BillsController : Controller
    {
        [HttpGet("[action]/{id}")]
        public IEnumerable<Bill> Analyze(int id)
        {
            int month = id;
            if(month > 0 && month <= 12)
            {
                var now = default(DateTime);
                var dateToCheck = new DateTime(now.Year, month, now.Day);
                var bills = Bill.RetrieveMonthlyBills(dateToCheck);
                return bills;
            }

            return new List<Bill>();

        }

        public DateTime CalculateDateTimeFromMonth(int month)
        {
            return new DateTime(DateTime.Now.Year, month, 15);
        }

        [HttpPost("[action]/{id}")]

        // http://localhost:62977/api/Bills/AnalyzeStatement/1/
        public List<Bill> AnalyzeStatement([FromBody] Statement s2)
        {
            string json2 = JsonConvert.SerializeObject(s2);
            System.Diagnostics.Debug.WriteLine(json2);
            var dateToCheck = DateTime.Parse(s2.StartDate);
            var analyzedBills = Bill.RetrievePaydays(dateToCheck);
            var firstPayday = analyzedBills[0];
            var firstPaydayRemaining = firstPayday.Credit;
            var secondPayday = analyzedBills[1];
            var secondPaydayRemaining = secondPayday.Credit;

            // assign monthly bills to paychecks
            var bills = this.GetBills(dateToCheck.Month);
            // int sum = (from x in list where x > 4 select x).Sum();  // sum: 1
            // var firstSum = (from x in bills where x.Date > firstPayday.Date && x.Date < secondPayday.Date).Sum();
            var total = bills.Sum(x => x.Debit);

            var totalFirst = bills.Where(x => x.Date > firstPayday.Date && x.Date < secondPayday.Date && x.IsSplit == false).Sum(x => x.Debit);

            var totalSecond = bills.Where(x => x.Date > secondPayday.Date && x.IsSplit == false).Sum(x => x.Debit);

        System.Diagnostics.Debug.WriteLine($"first paycheck balance {firstPaydayRemaining}");
            System.Diagnostics.Debug.WriteLine($"second paycheck balance {secondPaydayRemaining}");
            if (firstPaydayRemaining > secondPaydayRemaining)
            {

            }
            else
            {

            }
            return analyzedBills;

        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<Bill> GetBills(int id)
        {
            int month = id;
            var dateToCheck = this.CalculateDateTimeFromMonth(month);
            var bills = Bill.RetrieveMonthlyBills(dateToCheck);

            // pay checks
            var paydays = Bill.RetrievePaydays(dateToCheck);
            foreach (var payday in paydays)
            {
                bills.Add(payday);
            }

            foreach (var gwenPayment in Bill.RetrieveGwenPayments(dateToCheck))
            {
                bills.Add(gwenPayment);
            }

            if (month % 2 == 0)
            {
                bills.Add(this.RetrieveWaterBill(month));
            }

            // wyndham foundation is Jan/Apr/Jul/Oct
            if (month % 3 == 1)
            {
                bills.Add(this.RetrieveWyndham(month));
            }

            // all the regular bills have been added -- calculate Avalon payments for each pay cycle
            var total = bills.Sum(x => x.Debit);
            var totalFirst = bills.Where(x => x.Date > paydays[0].Date && x.Date < paydays[1].Date && x.IsSplit == false).Sum(x => x.Debit);
            var totalSecond = bills.Where(x => x.Date > paydays[1].Date && x.IsSplit == false).Sum(x => x.Debit);

            Console.WriteLine($"total: {total}");
            Console.WriteLine($"totalFirst: {totalFirst}");
            Console.WriteLine($"totalSecond: {totalSecond}");

            if (totalFirst > totalSecond)
            {
                decimal percentFirst = (totalFirst / totalSecond) / 100;
                decimal percentSecond = 1 - percentFirst;
                bills.Add(new Bill(paydays[0].Date.AddDays(3), Constants.Amount.Avalon * percentFirst, 0, Constants.Message.TransferVACU));
                bills.Add(new Bill(paydays[1].Date.AddDays(3), Constants.Amount.Avalon * percentSecond, 0, Constants.Message.TransferVACU));
                bills.Add(new Bill(paydays[0].Date.AddDays(3), Constants.Amount.Citibank * percentFirst, 0, Constants.Message.CitiBank));
                bills.Add(new Bill(paydays[1].Date.AddDays(3), Constants.Amount.Citibank * percentSecond, 0, Constants.Message.CitiBank));

            }



            return bills.OrderBy(b => b.Date).ToArray();
        }

        [HttpGet("[action]")]
        public IEnumerable<Bill> GetBills()
        {
            // old school
            return this.GetBills(DateTime.Now.Month);
        }

        public Bill RetrieveWaterBill(int month)
        {
            var billDate = this.CalculateDateTimeFromMonth(month);
            return new Bill(billDate, Constants.Amount.WaterBill, 0, $"Water Bill - {Constants.Message.ManualPay} ~ 15th"); ;
        }

        public Bill RetrieveWyndham(int month)
        {
            var billDate = this.CalculateDateTimeFromMonth(month);
            return new Bill(billDate, Constants.Amount.Wyndham, 0, $"Wyndham Foundation - {Constants.Message.AutoPay} ~ 3rd"); ;
        }
    }
}
