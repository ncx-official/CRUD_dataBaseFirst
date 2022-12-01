using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class PersonSex
    {
        public PersonSex()
        {
            People = new HashSet<Person>();
        }

        public uint IdSex { get; set; }
        public string SexValue { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
