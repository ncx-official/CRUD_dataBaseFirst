using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class StoreCountry
    {
        public StoreCountry()
        {
            StoreCities = new HashSet<StoreCity>();
        }

        public long IdStoreCountry { get; set; }
        public string CountryName { get; set; } = null!;

        public virtual ICollection<StoreCity> StoreCities { get; set; }
    }
}
