using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class StorePhone
    {
        public StorePhone()
        {
            Stores = new HashSet<Store>();
        }

        public long IdStorePhone { get; set; }
        public string? PhoneValue { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
