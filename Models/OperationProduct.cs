using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class OperationProduct
    {
        public long IdOperation { get; set; }
        public long IdProduct { get; set; }
        public decimal OperationProductPrice { get; set; }
        public uint OperationProductCount { get; set; }

        public virtual Operation IdOperationNavigation { get; set; } = null!;
        public virtual Product IdProductNavigation { get; set; } = null!;
    }
}
