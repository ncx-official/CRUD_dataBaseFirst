using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Person
    {
        public Person()
        {
            Employees = new HashSet<Employee>();
        }

        public long IdPerson { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public uint IdSex { get; set; }
        public DateOnly Birthdate { get; set; }

        public virtual PersonSex IdSexNavigation { get; set; } = null!;
        public virtual Authorization Authorization { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
