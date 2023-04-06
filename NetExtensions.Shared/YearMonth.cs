using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtensions
{
    public class YearMonth
    {
        private int _year;
        public int Year { get { return _year; } }
        private int _month;
        public int Month { get { return _month; } }

        private List<DateTime> _days = new List<DateTime>();
        public List<DateTime> Days { get { return _days; } }

        private List<YearWeek> _weeks = new List<YearWeek>();
        public List<YearWeek> Weeks { get { return _weeks; } }

        public YearMonth(int Year, int Month)
        {
            _year = Year;
            _month = Month;

            DateTime dt = string.Format("{0}-{1}-1", _year, _month).ToDateTime();

            while (dt.Month == _month)
            {
                _days.Add(dt);
                dt = dt.AddDays(1);
            }

            dt = string.Format("{0}-{1}-1", _year, _month).ToDateTime();
            bool _continue = true;

            while (_continue)
            {
                YearWeek yw = dt.IsoWeek();
                if (yw.Days.Where(x => x.Month == _month).Count() > 0)
                    _weeks.Add(yw);
                else
                    _continue = false;

                dt = dt.AddDays(7);
            }

        }
    }
}

