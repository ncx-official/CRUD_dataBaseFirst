using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class WeekDay
    {
        public WeekDay()
        {
            ScheduleOpens = new HashSet<ScheduleOpen>();
        }

        public long IdWeekDay { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<ScheduleOpen> ScheduleOpens { get; set; }
    }
}
