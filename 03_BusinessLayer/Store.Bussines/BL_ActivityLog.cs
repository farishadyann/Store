using Store.DataAccess;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Bussines
{
    public class BL_ActivityLog
    {
        DAL_ActivityLog  DAL = new DAL_ActivityLog();
        public bool ActivityLog(MS_ActivityLog req)
        {
            bool Retval = DAL.ActivityLog(req);
            return Retval;
        }
    }
}
