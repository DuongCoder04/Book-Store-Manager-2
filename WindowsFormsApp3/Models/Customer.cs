using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp.Models
{
    [Table("Customer")]
        public class Customer
        {
            public Customer()
            {
                Bills = new HashSet<Bill>();
            }
            private int _id = 1;
            public int Id
            {
                get { return _id; }
                set { if (value >= 1) _id = value; }
            }
            private string _nameCustomer = "Unknown name";
            public string NameCustomer
            {
                get { return _nameCustomer; }
                set { if (!string.IsNullOrEmpty(value)) _nameCustomer = value; }
            }
            private string _phone = "Unknown";
            public string Phone
            {
                get { return _phone; }
                set { if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value)) _phone = value; }
            }
            public string Address { get; set; }
            public virtual ICollection<Bill> Bills { get; set; }
        }
}
