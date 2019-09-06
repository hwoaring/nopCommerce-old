using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Helpers
{
    public partial class DateTimeHelper
    {
        public static readonly DateTime StartTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Utc); //Unix起始时间
 
        public static DateTime ConvertToDateTime(long dateTime)
        {
            return StartTime.AddSeconds(dateTime).ToLocalTime();
        }

        public static DateTime ConvertToDateTime(string dateTime)
        {
            return ConvertToDateTime(Convert.ToInt64(dateTime));
        }

        public static long ConvertToDateTimeLong(DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - StartTime).TotalSeconds;
        }

    }
}
