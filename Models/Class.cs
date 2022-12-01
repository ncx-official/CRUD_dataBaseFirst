using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Class
    {
        public Class()
        {
            Products = new HashSet<Product>();
        }

        public long IdClass { get; set; }
        public string ClassName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
