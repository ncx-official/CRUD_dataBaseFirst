using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class StoreAddress
    {
        public StoreAddress()
        {
            Stores = new HashSet<Store>();
        }

        public long IdStoreAddress { get; set; }
        public long IdStoreCity { get; set; }
        public string StreetName { get; set; } = null!;
        public string StreetNumberCode { get; set; } = null!;

        public virtual StoreCity IdStoreCityNavigation { get; set; } = null!;
        public virtual ICollection<Store> Stores { get; set; }
    }
}
