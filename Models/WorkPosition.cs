using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class WorkPosition
    {
        public WorkPosition()
        {
            Employees = new HashSet<Employee>();
        }

        public long IdWorkPosition { get; set; }
        public DateOnly StartDate { get; set; }
        public string PositionName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
