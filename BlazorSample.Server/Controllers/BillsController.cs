using BlazorSample.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorSample.Server.Controllers
{
    [Route("api/[controller]")]
    public class BillsController : Controller
    {
        [HttpGet("[action]/{id}")]
        public IEnumerable<Bill> GetBills(int id)
        {
            int month = id;
            List<Bill> Bills = new List<Bill>();
            Bills.Add(AddBestBuy(month));
            Bills.Add(AddCitibank(month));
            Bills.Add(AddCityOfRichmond(month));
            Bills.Add(AddHighlander(month));
            Bills.Add(AddMortgage(month));
            Bills.Add(AddStateFarm(month));
            Bills.Add(AddSprintTransfer(month));
            Bills.Add(AddVerizon(month));
            Bills.Add(AddVirginiaPower(month));
            if (month % 2 == 0)
            {
                Bills.Add(AddWaterBill(month));
            }
            Bills.Add(AddWellsFargo(month));
            Bills.Add(AddYMCATransfer(month));

            foreach (Bill bill in AddAvalon(month))
            {
                Bills.Add(bill);
            }

            foreach (Bill bill in AddPayday(month))
            {
                Bills.Add(bill);
            }
            return Bills.OrderBy(b => b.Date).ToArray();
        }

        [HttpGet("[action]")]
        public IEnumerable<Bill> GetBills()
        {
            //old school
            List<Bill> Bills = new List<Bill>();
            int month = DateTime.Now.Month + 1; //human month jan = 1; feb = 2
            Bills.Add(AddBestBuy(month));
            Bills.Add(AddCitibank(month));
            Bills.Add(AddCityOfRichmond(month));
            Bills.Add(AddHighlander(month));
            Bills.Add(AddMortgage(month));
            Bills.Add(AddStateFarm(month));
            Bills.Add(AddSprintTransfer(month));
            Bills.Add(AddVerizon(month));
            Bills.Add(AddVirginiaPower(month));
            if(month % 2 == 0)
            {
                Bills.Add(AddWaterBill(month));
            }
            Bills.Add(AddWellsFargo(month));
            Bills.Add(AddYMCATransfer(month));

            foreach (Bill bill in AddPayday(month))
            {
                Bills.Add(bill);
            }

            foreach (Bill bill in AddAvalon(month))
            {
                Bills.Add(bill);
            }
            return Bills.OrderBy(b => b.Date).ToArray();
            //end old school
        }

        /// <summary>
        /// month is for humans 1- Jan, 2- Feb etc.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Bill AddMortgage(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/9/{DetermineYear(month)}"),
                Debit = BillStub.Mortgage,
                Credit = 0,
                Description = $"Mortgage - {Message.AutoPay} ~ 9th"
            };
            return bill;
        }

        public Bill AddVerizon(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/20/{DetermineYear(month)}"),
                Debit = (Decimal)BillStub.Verizon,
                Credit = (Decimal)0,
                Description = $"Verizon - {Message.TransferHodor} ~ 20th"
            };
            return bill;
        }

        
        public Bill AddWaterBill(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/15/{DetermineYear(month)}"),
                Debit = (Decimal)129.99,
                Credit = (Decimal)0,
                Description = $"Water Bill - {Message.ManualPay} ~ 15th"
            };
            return bill;
        }

        public List<Bill> AddPayday(int month)
        {
            List<DateTime> payDays = new List<DateTime>();
            List<Bill> paychecks = new List<Bill>();

            payDays = BuildListOfDates(DateTime.Parse("10/26/2018"), 14, 2);

            foreach(DateTime payday in payDays)
            {
                Bill bill = new Bill
                {
                    Date = payday,
                    Debit = 0,
                    Credit = BillStub.PayCheck,
                    Description = "Taradel Payday"
                };
                if (bill.Date.Month == month)
                {
                    paychecks.Add(bill);
                }
            }
            return paychecks;
        }

        public Bill AddVirginiaPower(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/28/{DetermineYear(month)}"),
                Debit = BillStub.VirginiaPower,
                Credit = 0,
                Description = $"Dominion VA Power {Message.ManualPay} due ~ 28th"
            };
            return bill;
        }

        public Bill AddCityOfRichmond(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/5/{DetermineYear(month)}"),
                Debit = BillStub.CityOfRichmond,
                Credit = 0,
                Description = $"City of Richmond {Message.ManualPay} due ~ 5th"
            };
            return bill;
        }

        public Bill AddHighlander(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/16/{DetermineYear(month)}"),
                Debit = BillStub.Highlander,
                Credit = 0,
                Description = $"Highlander {Message.AutoPay} sch ~ 16th"
            };
            return bill;
        }

        public Bill AddYMCATransfer(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/14/{DetermineYear(month)}"),
                Debit = BillStub.YMCA,
                Credit = 0,
                Description = $"YMCA {Message.TransferHodor} sch ~ 14th"
            };
            return bill;
        }

        public Bill AddSprintTransfer(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/24/{DetermineYear(month)}"),
                Debit = BillStub.Sprint,
                Credit = 0,
                Description = $"Sprint {Message.TransferHodor} sch ~ 24th"
            };
            return bill;
        }

        public Bill AddStateFarm(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/22/{DetermineYear(month)}"),
                Debit = BillStub.StateFarm,
                Credit = 0,
                Description = $"State Farm {Message.TransferHodor} sch ~ 22nd"
            };
            return bill;
        }

        public Bill AddBestBuy(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/5/{DetermineYear(month)}"),
                Debit = BillStub.BestBuy,
                Credit = 0,
                Description = $"Best Buy {Message.AutoPay} sch ~ 5th ends 1/1/19"
            };
            return bill;
        }

        public Bill AddCitibank(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/14/{DetermineYear(month)}"),
                Debit = BillStub.Citibank,
                Credit = 0,
                Description = $"Citibank {Message.ManualPay}  ~ 14th "
            };
            return bill;
        }

        public Bill AddWellsFargo(int month)
        {
            Bill bill = new Bill
            {
                Date = DateTime.Parse($"{month}/15/{DetermineYear(month)}"),
                Debit = BillStub.WellsFargo,
                Credit = 0,
                Description = $"Wells Fargo {Message.AutoPay}  ~ 15th "
            };
            return bill;
        }

        public List<DateTime> BuildListOfDates(DateTime startDate, int intervalInDays, int loopMonths)
        {
            List<DateTime> paymentDates = new List<DateTime>();
            paymentDates.Add(startDate);
            int monthEnd = startDate.Month + loopMonths;
            while(loopMonths <= monthEnd)
            {
                startDate = startDate.AddDays(intervalInDays);
                paymentDates.Add(startDate);
                loopMonths++;
            }

            return paymentDates;
        }

        public List<Bill> AddAvalon(int month)  //biweekly payments
        {
            List<Bill> bills = new List<Bill>();
            DateTime start = Convert.ToDateTime("11/5/2018");
            List<DateTime> paymentDates = new List<DateTime>();
            paymentDates.Add(start);
           
            //go 4 months from now 
            int monthEnd = month + 4;
            while(month <= monthEnd)
            {
                start.AddDays(14);
                Bill bill = new Bill
                {
                    Date = start,
                    Debit = BillStub.Avalon,
                    Credit = 0,
                    Description = $"Avalon recurring {Message.TransferVACU}"
                };
                if (start.Month == month)
                {
                    bills.Add(bill);
                }
                month++;
            }
            return bills;
        }

        public int DetermineYear(int month)
        {
            DateTime now = DateTime.Now;
            if(month <= 6)
            {
                return now.Year + 1;
            }
            else
            {
                return now.Year;
            }
        }
    }
}
