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

        // Sử dụng GUID cho Id
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Gán giá trị GUID mới khi khởi tạo

        private DateTime _checkOut = DateTime.Now;
        public DateTime CheckOut
        {
            get { return _checkOut; }
            set
            {
                if (value <= DateTime.Now && value >= CheckIn)
                    _checkOut = value;
            }
        }

        private DateTime _checkIn = DateTime.Now;
        public DateTime CheckIn
        {
            get { return _checkIn; }
            set
            {
                if (value <= DateTime.Now && value <= CheckOut)
                    _checkIn = value;
            }
        }

        private int _idCustomer;
        public int IdCustomer
        {
            get { return _idCustomer; }
            set
            {
                if (value >= 1)
                    _idCustomer = value;
            }
        }

        [ForeignKey(nameof(IdCustomer))]
        public virtual Customer Customer { get; set; }

        private int _idUser;
        public int IdUser
        {
            get { return _idUser; }
            set
            {
                if (value >= 1)
                    _idUser = value;
            }
        }

        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; }

        // Sử dụng ICollection để lưu danh sách BillDetails
        public virtual ICollection<BillDetail> BillDetail { get; set; }
    }
}
