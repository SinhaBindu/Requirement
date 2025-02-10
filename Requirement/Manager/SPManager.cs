using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Requirement.Models;
using System.Data;
using System.Web.Security;
using SubSonic.Schema;

namespace Requirement.Manager
{
    public static partial class SPManager
    {
        public static DataTable Get_USP_GetHiredList()
        {
            StoredProcedure sp = new StoredProcedure("USP_GetHiredList");
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}