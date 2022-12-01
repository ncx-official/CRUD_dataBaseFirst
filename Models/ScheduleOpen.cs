using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class ScheduleOpen
    {
        public ScheduleOpen()
        {
            IdStores = new HashSet<Store>();
        }

        public long IdScheduleOpen { get; set; }
        public long IdWeekDay { get; set; }
        public TimeOnly OpenAt { get; set; }
        public TimeOnly CloseAt { get; set; }

        public virtual WeekDay IdWeekDayNavigation { get; set; } = null!;

        public virtual ICollection<Store> IdStores { get; set; }
    }
}
