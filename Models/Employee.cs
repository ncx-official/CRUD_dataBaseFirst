using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Operations = new HashSet<Operation>();
        }

        public long IdEmployee { get; set; }
        public long IdPerson { get; set; }
        public long? IdStore { get; set; }
        public long? IdWorkPosition { get; set; }

        public virtual Person IdPersonNavigation { get; set; } = null!;
        public virtual Store IdStoreNavigation { get; set; }
        public virtual WorkPosition IdWorkPositionNavigation { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
