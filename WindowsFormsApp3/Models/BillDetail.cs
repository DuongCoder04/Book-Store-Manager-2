using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp.Models
{
    [Table("BillDetail")]
    public class BillDetail
    {
        public BillDetail()
        {
        }

        // Khóa chính thứ nhất: Sử dụng GUID cho IdBill
        [Key]
        [Column(Order = 1)]
        public string IdBill { get; set; }

        [ForeignKey(nameof(IdBill))]
        public virtual Bill Bill { get; set; }

        // Khóa chính thứ hai: IdBook
        private int _idBook = 1;
        [Key]
        [Column(Order = 2)]
        public int IdBook
        {
            get { return _idBook; }
            set { if (value >= 1) _idBook = value; }
        }

        [ForeignKey(nameof(IdBook))]
        public virtual Book Book { get; set; }

        // Thuộc tính Quantity (Số lượng)
        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { if (value >= 1) _quantity = value; }
        }

        // Thuộc tính Price (Giá)
        public int Price { get; set; }
    }
}
