using System;
using System.Collections.Generic;

namespace NetExtensions
{
    public class YearWeek
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public int YW { get { return Year * 100 + Week; } }
        public DateTime Monday { get; set; }
        public DateTime Tuesday { get { return Monday.AddDays(1); } }
        public DateTime Wednesday { get { return Monday.AddDays(2); } }
        public DateTime Thursday { get { return Monday.AddDays(3); } }
        public DateTime Friday { get { return Monday.AddDays(4); } }
        public DateTime Saturday { get { return Monday.AddDays(5); } }
        public DateTime Sunday { get { return Monday.AddDays(6); } }
        public DateTime WeekStartDate { get { return Monday; } }
        public DateTime WeekEndDate { get { return Sunday; } }

        private List<DateTime> _days = null;
        public List<DateTime> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = new List<DateTime>();
                    _days.Add(Monday);
                    _days.Add(Tuesday);
                    _days.Add(Wednesday);
                    _days.Add(Thursday);
                    _days.Add(Friday);
                    _days.Add(Saturday);
                    _days.Add(Sunday);
                }

                return _days;
            }
        }

        public string DisplayName { get { return string.Format("{0}-{1} ({2} - {3})", Year, Week, WeekStartDate.ToShortDateString(), WeekEndDate.ToShortDateString()); } }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (obj == null) return false;
                YearWeek cmp = (YearWeek)obj;
                return (YW == cmp.YW);
            }
            catch { return false; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}

