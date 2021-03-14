using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModel
{
    public class MS_ActivityLog
    {
        public int? ActivityLogID_PK { get; set; }
        public int? UserID_FK { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
        public DateTime? ActivityDate { get; set; }
    }
}
