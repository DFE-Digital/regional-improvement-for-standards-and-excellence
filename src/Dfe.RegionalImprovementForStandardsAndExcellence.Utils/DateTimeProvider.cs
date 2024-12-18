﻿namespace Dfe.RegionalImprovementForStandardsAndExcellence.Utils
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
    }
}
