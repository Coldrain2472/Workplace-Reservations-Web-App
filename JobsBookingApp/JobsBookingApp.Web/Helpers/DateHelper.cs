namespace JobsBookingApp.Web.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetNextWorkingDay()
        {
            DateTime day = DateTime.Today.AddDays(1);
            while (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
            {
                day = day.AddDays(1);
            }
            return day;
        }
    }
}
