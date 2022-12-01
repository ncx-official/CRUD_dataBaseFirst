using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Store
    {
        public Store()
        {
            Employees = new HashSet<Employee>();
            Products = new HashSet<Product>();
            IdScheduleOpens = new HashSet<ScheduleOpen>();
        }

        public long IdStore { get; set; }
        public long IdScheduleOpen { get; set; }
        public string StoreName { get; set; } = null!;
        public long IdStoreAddress { get; set; }
        public long IdStorePhone { get; set; }

        public virtual StoreAddress IdStoreAddressNavigation { get; set; } = null!;
        public virtual StorePhone IdStorePhoneNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ScheduleOpen> IdScheduleOpens { get; set; }
    }
}
