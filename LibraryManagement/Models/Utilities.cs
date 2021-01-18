using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public static class Utilities
    {

        public static bool IsHoliday(DateTime dateTime)
        {
            return Constants.Weekends.Contains(dateTime.DayOfWeek);
        }
        public static DateTime AddBusinessDays(DateTime originalDateTime, int noOfDays)
        {
            var result = originalDateTime;

            for (var iCntr = 0; iCntr < noOfDays; iCntr++)
            {
                do
                {
                    result = result.AddDays(1);
                } while (IsHoliday(result));
            }

            return result;
        }

        public static int CountBusinessDays(DateTime ReturnedDatedateTime1, DateTime RequiredReturnedDuedateTime2)
        {
            int CountOfDays = (ReturnedDatedateTime1 - RequiredReturnedDuedateTime2).Days;
            if (CountOfDays <= 0) { return 0; }
            ApplicationDbContext db = new ApplicationDbContext();
            List<BusinessHoliday> bizHolidays = new List<BusinessHoliday>();
            
            var result = 0;
            try
            {
                bizHolidays = db.BusinessHolidays.Where(m => m.FromDate >= DateTime.Now).ToList();
                if(bizHolidays != null)
                {
                    var holiDatesRangeAddition = new List<DateTime>();
                    bizHolidays.ForEach(m => {
                        if((m.ToDate - m.FromDate).Days >= 0 )
                        {
                            for (var dt = m.FromDate; dt <= m.ToDate; dt = dt.AddDays(1))
                            {
                                holiDatesRangeAddition.Add(dt);
                            }
                        }
                    });
                   
                    // Days inclusivie of holidasy for which penalty is due
                    var delayedDatesAddition = new List<DateTime>();
                    for (var dt = RequiredReturnedDuedateTime2; dt <= ReturnedDatedateTime1; dt = dt.AddDays(1))
                    {
                        delayedDatesAddition.Add(dt);
                    }

                    var distinctHolidays = holiDatesRangeAddition.Distinct().ToList();

                    var countOfHolidaysInPenaltyDays = distinctHolidays.Where(m => delayedDatesAddition.Contains(m.Date)).Count();

                    // Actual penal days that are not holidays
                    result = delayedDatesAddition.Count - countOfHolidaysInPenaltyDays;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}