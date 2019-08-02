namespace BlazorSample.Shared
{
    using System.Collections.Generic;

    public class BillStub
    {
         public BillStub(string name, string message, decimal debit, int dayOfMonth, bool isSplit)
        {
            this.Name = name;
            this.Message = name + " " + message;
            this.Debit = debit;
            this.DayOfMonth = dayOfMonth;
            this.IsSplit = isSplit;
        }

        public string Name { get; set; }

        public int Interval { get; set; }

        public string Message { get; set; }

        public decimal Credit { get; set; }

        public decimal Debit { get; set; }

        public int DayOfMonth { get; set; }

        public bool IsSplit { get; set; }

        // business logic
        public static List<BillStub> RetrieveMonthlyBillStubs()
        {
            var monthly = new List<BillStub>();
            monthly.Add(new BillStub("Mortgage", Constants.Message.AutoPay, Constants.Amount.Mortgage, Constants.DayOfMonth.Mortgage, false));
            monthly.Add(new BillStub("YMCA", Constants.Message.TransferHodor, Constants.Amount.YMCA, Constants.DayOfMonth.YMCA, true));
            monthly.Add(new BillStub("Comcast", Constants.Message.TransferHodor, Constants.Amount.Comcast, Constants.DayOfMonth.Comcast, true));
            monthly.Add(new BillStub("Sprint", Constants.Message.CitiBank, Constants.Amount.Sprint, Constants.DayOfMonth.Sprint, true));
            monthly.Add(new BillStub("VirginiaPower", Constants.Message.ManualPay, Constants.Amount.VirginiaPower, Constants.DayOfMonth.VirginiaPower, false));
            monthly.Add(new BillStub("City of Richmond", Constants.Message.ManualPay, Constants.Amount.CityOfRichmond, Constants.DayOfMonth.CityOfRichmond, false));
            monthly.Add(new BillStub("Highlander", Constants.Message.AutoPay, Constants.Amount.Highlander, Constants.DayOfMonth.Highlander, false));
            monthly.Add(new BillStub("StateFarm", Constants.Message.CitiBank, Constants.Amount.StateFarm, Constants.DayOfMonth.StateFarm, true));
            return monthly;
        }

    }
}
