namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Helpers
{
    public struct DateTimeSpan
    {
        private readonly int _Years;
        public int Years
        {
            get
            {
                return _Years;
            }
        }

        private readonly int _Months;
        public int Months
        {
            get
            {
                return _Months;
            }
        }

        private readonly int _Days;
        public int Days
        {
            get
            {
                return _Days;
            }
        }

        private readonly int _Hours;
        public int Hours
        {
            get
            {
                return _Hours;
            }
        }

        private readonly int _Minutes;
        public int Minutes
        {
            get
            {
                return _Minutes;
            }
        }

        private readonly int _Seconds;
        public int Seconds
        {
            get
            {
                return _Seconds;
            }
        }

        private readonly int _MilliSeconds;
        public int MilliSeconds
        {
            get
            {
                return _MilliSeconds;
            }
        }

        // the ctor for the result
        private DateTimeSpan(int y, int mm, int d, int h, int m, int s, int ms)
        {
            _Years = y;
            _Months = mm;
            _Days = d;
            _Hours = h;
            _Minutes = m;
            _Seconds = s;
            _MilliSeconds = ms;
        }

        // private time unit tracker when counting
        private enum Unit
        {
            Year,
            Month,
            Day,
            Complete
        }

        public static DateTimeSpan DateSpan(DateTime dt1, DateTime dt2)
        {
            // we dont do negatives
            if (dt2 < dt1)
            {
                (dt2, dt1) = (dt1, dt2);
            }
            DateTime thisDT = dt1;
            int years = 0;
            int months = 0;
            int days = 0;

            Unit level = Unit.Year;
            DateTimeSpan span = new();

            while (level != Unit.Complete)
            {
                switch (level)
                {
                    case Unit.Year:
                        {
                            if (thisDT.AddYears(years + 1) > dt2)
                            {
                                level = Unit.Month;
                                thisDT = thisDT.AddYears(years);
                            }
                            else
                                years += 1;
                            break;
                        }

                    case Unit.Month:
                        {
                            if (thisDT.AddMonths(months + 1) > dt2)
                            {
                                level = Unit.Day;
                                thisDT = thisDT.AddMonths(months);
                            }
                            else
                                months += 1;
                            break;
                        }

                    case Unit.Day:
                        {
                            if (thisDT.AddDays(days + 1) > dt2)
                            {
                                thisDT = thisDT.AddDays(days);
                                var thisTS = dt2 - thisDT;
                                // create a new DTS from the values caluclated
                                span = new DateTimeSpan(years, months, days, thisTS.Hours, thisTS.Minutes, thisTS.Seconds, thisTS.Milliseconds);
                                level = Unit.Complete;
                            }
                            else
                                days += 1;
                            break;
                        }
                }
            }

            return span;
        }
    }
}
