using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorSample.Shared
{
    public static class Utility
    {
        public static DateTime GetLastDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static List<DateTime> BuildListOfDates(DateTime startDate, int intervalInDays, int yearsToGo)
        {
            var paymentDates = new List<DateTime>();
            paymentDates.Add(startDate);
            var loopDate = startDate;
            var endDate = startDate.AddYears(yearsToGo);
            while (loopDate <= endDate)
            {
                startDate = startDate.AddDays(intervalInDays);
                paymentDates.Add(startDate);
                loopDate = loopDate.AddDays(intervalInDays);
                System.Diagnostics.Trace.WriteLine(loopDate);
            }

            return paymentDates;
        }
    }
}
