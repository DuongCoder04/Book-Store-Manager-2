using System.Collections.Generic;
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
        private int _idbill = 1;
        [Key]
        [Column(Order =1)]
        public int IdBill
        {
            get { return _idbill; }
            set { if (value >= 1) _idbill = value; }
        }
        [ForeignKey(nameof(IdBill))]
        public virtual Bill Bill { get; set; }
        private int _idbook = 1;
        [Key]
        [Column(Order = 2)]
        public int IdBook
        {
            get { return _idbook; }
            set { if (value >= 1) _idbook = value; }
        }
        [ForeignKey(nameof(IdBook))]
        public virtual Book Book { get; set; }
        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { if (value >= 1) _quantity = value; }
        }
        public int Price {
            get; set;
        }
    }
}
