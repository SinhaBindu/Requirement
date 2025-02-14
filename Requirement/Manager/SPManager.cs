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
        public static DataTable Usp_GetUserlist(int? RoleId)
        {
            StoredProcedure sp = new StoredProcedure("Usp_GetUserlist");
            sp.Command.AddParameter("@RoleId", RoleId, DbType.Int32);
            sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}