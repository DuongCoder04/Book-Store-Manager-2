using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp.Models
{
    [Table("Bill")]
    public class Bill
    {
        public Bill()
        {
            BillDetail = new HashSet<BillDetail>();
        }
        private int _id = 1;
        public int Id
        {
            get { return _id; }
            set { if (value >= 1) _id = value; }
        }
        
        private DateTime _checkout = DateTime.Now;
        public DateTime CheckOut
        {
            get { return _checkout; }
            set { if (value <= DateTime.Now && value >= _checkin) _checkout = value; }
        }
        private DateTime _checkin = DateTime.Now;
        public DateTime CheckIn
        {
            get { return _checkin; }
            set { if (value <= DateTime.Now && value <= _checkout) _checkin = value; }
        }
        private int _idCustomer = 1;
        public int IdCustomer
        {
            get { return _idCustomer; }
            set { if (value >= 1) _idCustomer = value; }
        }
        [ForeignKey(nameof(IdCustomer))]
        public virtual Customer Customer { get; set; }
        private int _idUser = 1;
        public int IdUser
        {
            get { return _idUser; }
            set { if (value >= 1) _idUser = value; }
        }
        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
    }
}
