using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class MS_Product_Request
    {
        public int? ProductID_PK { get; set; }
        public int? SellerID_FK { get; set; }
        public int? CategoryID_FK { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public int? Price { get; set; }
        public string ProductImage { get; set; }
        public int? ProductSeen { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class MS_Product_Response
    {
        public int? ProductID_PK { get; set; }
        public int? SellerID_FK { get; set; }
        public int? CategoryID_FK { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public int? Price { get; set; }
        public string ProductImage { get; set; }
        public int? ProductSeen { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int? RecordTotal { get; set; }
    }
    public class MS_Product_Item
    {
        public int? ProductID_PK { get; set; }
        public int? SellerID_FK { get; set; }
        public int? CategoryID_FK { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Stock { get; set; }
        public int? Price { get; set; }
        public string ProductImage { get; set; }
        public int? ProductSeen { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

