using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class CustomerList
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public List<Customer> Customers { get; set; }
        public bool DisplayMailChimpButton { get; set; }
    }
}
