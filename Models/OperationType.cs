using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class OperationType
    {
        public OperationType()
        {
            Operations = new HashSet<Operation>();
        }

        public long IdOperationType { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Operation> Operations { get; set; }
    }
}
