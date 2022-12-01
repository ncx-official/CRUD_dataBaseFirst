using System;
using System.Collections.Generic;

namespace DataBaseManagerApplication.Models
{
    public partial class Authorization
    {
        public long IdPerson { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Person IdPersonNavigation { get; set; } = null!;
    }
}
