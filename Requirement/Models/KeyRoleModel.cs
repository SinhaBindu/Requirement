using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Requirement.Models
{
    public class KeyRoleModel
    {
        public KeyRoleModel()
        {
            KeyRoleId_pk = 0;
        }
        public int KeyRoleId_pk { get; set; }
        public string KeyRoleName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
}