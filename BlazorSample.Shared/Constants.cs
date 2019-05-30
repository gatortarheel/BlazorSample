namespace BlazorSample.Shared
{
    public static class Constants
    {
        public static class Message
        {
            public const string TransferHodor = "Transfer to Hodor";
            public const string AutoPay = "AutoPay";
            public const string ManualPay = "Manual Pay";
            public const string TransferGwen = "Transfer to Gwen";
            public const string TransferVACU = "Transfer to VACU";
            public const string PayCheck = "Taradel Payday";
            public const string CitiBank = "Citibank Visa";
        }

        // business logic
        private const int daysInYear = 365;
        public const int DaysInPaycycle = 14;
        public const int GwenStartDay = 15;
        public const int GwenRegularPay = 60;
        public const int GwenFridayPay = 200;

        public static int DaysInYear => daysInYear;


        // BillStub is how a Bill is built
        public static class DayOfMonth
        {
            public const int Mortgage = 9;
            public const int Sprint = 24;
            public const int YMCA = 14;
            public const int Comcast = 21;
            public const int VirginiaPower = 1;
            public const int CityOfRichmond = 19;
            public const int Highlander = 15;
            public const int StateFarm = 24;
            public const int Citibank = 15;
            public const int Avalon = 6;
        }

        public static class Amount
        {
            public const decimal Mortgage = 1594.22M;
            public const decimal Sprint = 235.99M;
            public const decimal YMCA = 82;
            public const decimal PayCheck = 3950.93M;
            public const decimal Comcast = 171.46M;
            public const decimal VirginiaPower = 111.99M;
            public const decimal CityOfRichmond = 99;
            public const decimal Highlander = 373.59M;
            public const decimal StateFarm = 240.99M;
            public const decimal Citibank = 2599.99M;
            public const decimal Avalon = 368.50M;
            public const decimal WaterBill = 129;
            public const decimal Wyndham = 206;
        }
    }
}
