using S5_UESAN_JQUERYMVCEF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S5_UESAN_JQUERYMVCEF.DatabaseFirst.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public IEnumerable<Order> Order { get; set; }
    }
}
