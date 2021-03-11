﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class MS_UserInfo_Request
    {
        public int? UserInfoID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public string FullName { get; set; }
        public DateTime? BornDate { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string PostType { get; set; }
    }
    public class MS_UserInfo_Response
    {
        public int? UserInfoID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public string FullName { get; set; }
        public DateTime? BornDate { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string UserName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        

        public string ResponseMessage { get; set; }
        public bool? ResponseAction { get; set; }
    }
    public class MS_UserInfo_Item
    {
        public int? UserInfoID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public string FullName { get; set; }
        public DateTime? BornDate { get; set; }
        public string Email { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
