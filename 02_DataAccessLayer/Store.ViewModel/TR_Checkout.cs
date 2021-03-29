using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class TR_Checkout_Request
    {
        public int? CheckoutID_PK { get; set; }
        public int? CartID_FK { get; set; }
        public int? ProductID_FK { get; set; }
        public int? BuyerID_FK { get; set; }
        public string UserName { get; set; }
        public string BuyerEmail { get; set; }
        public Cities City { get; set; }
        public Districs Districs { get; set; }
        public string PostCode { get; set; }
        public int? SellerID_FK { get; set; }
        public int? TotalPaymet { get; set; }
        public string ShippingAddress { get; set; }
        public int? PaymentTypeID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<TR_Cart_Response> Cart { get; set; }
        
    }

    
    public class TR_Checkout_Response
    {
        public int? CheckoutID_PK { get; set; }
        public List<TR_Cart_Response> Cart { get; set; }
        public int? ProductID_FK { get; set; }
        public int? BuyerID_FK { get; set; }
        public int? SellerID_FK { get; set; }
        public int? TotalPaymet { get; set; }
        public string ShippingAddress { get; set; }
        public int? PaymentTypeID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class TR_Checkout_Item
    {
        public int? CheckoutID_PK { get; set; }
        public int? CartID_FK { get; set; }
        public int? ProductID_FK { get; set; }
        public int? BuyerID_FK { get; set; }
        public int? SellerID_FK { get; set; }
        public int? TotalPaymet { get; set; }
        public string ShippingAddress { get; set; }
        public int? PaymentTypeID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public enum Cities
    {
        Jakarta,
        Bandung,
        Surabaya,
        Yogyakarta,
        Semarang,
        Malang
    }

    public enum Districs
    {
        Menteng,
        Pasteur,
        Malioboro,
        Sleman
    }

}
