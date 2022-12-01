using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class StoreCity
    {
        public StoreCity()
        {
            StoreAddresses = new HashSet<StoreAddress>();
        }

        public long IdStoreCity { get; set; }
        public long IdStoreCountry { get; set; }
        public string CityName { get; set; } = null!;

        public virtual StoreCountry IdStoreCountryNavigation { get; set; } = null!;
        public virtual ICollection<StoreAddress> StoreAddresses { get; set; }
    }
}
