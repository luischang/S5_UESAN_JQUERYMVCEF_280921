using System;
using System.Collections.Generic;

#nullable disable

namespace S5_UESAN_JQUERYMVCEF.DatabaseFirst.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
