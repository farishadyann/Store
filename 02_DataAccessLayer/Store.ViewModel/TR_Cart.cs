using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class TR_Cart_Request
    {
        public string PostType { get; set; }
        public int? CartID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public int? ProductID_FK { get; set; }
        public int? Quantity { get; set; }
        public bool? FlagActive { get; set; }
        public bool? FlagDekete { get; set; }
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
    }
    public class TR_Cart_Response
    {
        public int? CartID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public string UserName { get; set; }
        public int? ProductID_FK { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int? Stock { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }  
        public bool? FlagActive { get; set; }
        public bool? FlagDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RecordTotal { get; set; }
    }
    public class TR_Cart_Item
    {
        public int? CartID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public int? ProductID_FK { get; set; }
        public int? Quantity { get; set; }
        public bool? FlagActive { get; set; }
        public bool? FlagDekete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
