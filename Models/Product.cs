using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Product
    {
        public Product()
        {
            OperationProducts = new HashSet<OperationProduct>();
        }

        public long IdProduct { get; set; }
        public long IdStore { get; set; }
        public long IdClass { get; set; }
        public decimal UsdPrice { get; set; }
        public string DefineCode { get; set; }
        public bool IsAvailable { get; set; }
        public string Name { get; set; } = null!;
        public uint Count { get; set; }

        public virtual Class IdClassNavigation { get; set; }
        public virtual Store IdStoreNavigation { get; set; }
        public virtual ICollection<OperationProduct> OperationProducts { get; set; }
    }
}
