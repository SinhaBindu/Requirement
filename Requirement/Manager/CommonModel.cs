using Requirement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Requirement.Manager
{
    public class CommonModel
    {
        private static Recruitment_DBEntities _db = new Recruitment_DBEntities();

        public static List<SelectListItem> GetALLHyringM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_HiringType.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.HiringTypeId.ToString(), Text = x.HiringType}).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLApplicableM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Applicable.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.ApplicableId.ToString(), Text = x.ApplicableType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }

        public static List<SelectListItem> GetALLPositionM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Position.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.PositionId.ToString(), Text = x.PositionType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLJobLocationM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_JobLocation.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.JobLocationId.ToString(), Text = x.JobLocationType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLCCM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_CostCentreCode.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.CostCentreCodeId.ToString(), Text = x.CostCentreCodeType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLSPM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_SelectionProcess.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.SelectionProcessId.ToString(), Text = x.SelectionProcessType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }

        public static List<SelectListItem> GetALLBSM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_SelectionProcess.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.SelectionProcessId.ToString(), Text = x.SelectionProcessType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLGradeM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Grade.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.GradeId.ToString(), Text = x.GradeType }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLYesNoM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_TypeYesNo.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Type }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLRoles(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.AspNetRoles./*OrderBy(x => x.OrderBy).*/Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLLocation(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Location.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Id_pk.ToString(), Text = x.Location }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLDesignation(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Designation.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Id_pk.ToString(), Text = x.Designation }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLEmp(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_EmpCode.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Id_pk.ToString(), Text = x.Emp_Code }).ToList();
            if (IsSelect == 0)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
    }
}