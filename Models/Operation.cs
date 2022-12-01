using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Operation
    {
        public Operation()
        {
            OperationProducts = new HashSet<OperationProduct>();
        }

        public long IdOperation { get; set; }
        public long IdOperationType { get; set; }
        public long? IdEmployee { get; set; }
        public long IdProduct { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual OperationType IdOperationTypeNavigation { get; set; } = null!;
        public virtual ICollection<OperationProduct> OperationProducts { get; set; }
    }
}
