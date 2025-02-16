using Requirement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Requirement.Manager
{
    public class CommonModel
    {
        private static Recruitment_DBEntities _db = new Recruitment_DBEntities();

        #region BaseUrl
        //public static string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    // throw new Exception("No network adapters with an IPv4 address in the system!");
        //    return "Error";
        //}
        //public static string GetPublicIPAddress()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string publicIPAddress = client.GetStringAsync("https://api.ipify.org").Result;
        //        return publicIPAddress;
        //    }
        //}

        public static string GetBaseUrl()
        {
            var str = HttpContext.Current.Request.Url.Host;
            //return str;
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            string host = HttpContext.Current.Request.Url.Host;
            if (host == "localhost")
            {
                host = HttpContext.Current.Request.Url.Authority;
                return HttpContext.Current.Request.Url.Scheme + "://" + host;
            }
            //return urlHelper.Content("~/");
            return HttpContext.Current.Request.Url.Scheme + "://" + str;
        }
        public static string GetWebUrl()
        {
            return ConfigurationManager.AppSettings["WebUrl"];
        }
        public static bool ValidateImageSizeMemory(MemoryStream ms)
        {
            const int maxSizeInBytes = 1 * 1024 * 1024; // 1 MB in bytes

            // Check if the stream size is less than or equal to 1 MB
            if (ms.Length <= maxSizeInBytes)
            {
                return true; // Valid size
            }

            return false; // Invalid size
        }
        public static bool ValidateImageSize(HttpPostedFileBase image)
        {
            const int maxSizeInBytes = 1 * 1024 * 1024; // 1 MB in bytes

            // Check if the stream size is less than or equal to 1 MB
            if (image.ContentLength <= maxSizeInBytes)
            {
                return true; // Valid size
            }

            return false; // Invalid size
        }

        public static bool IsEmailConfiguredToLive()
        {
            var isLive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsEmailSetLive"].ToString());
            return isLive;
        }
        public static string GetLocalEmailAddress()
        {
            var emailAddr = ConfigurationManager.AppSettings["LocalEmailAddress"].ToString();
            return emailAddr;
        }

        public static string GetFileUrl(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return CommonModel.GetBaseUrl() + filePath.ToString().Replace("~", "");
            else
                return string.Empty;
        }

        public static string GetMultipleFileUrlFromComma(string filePaths)
        {
            //string filePath = string.Empty;
            //var filePathArray = filePaths.Split(',');
            List<string> filePathList = new List<string>();
            foreach (var path in filePaths.Split(','))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    //return CommonModel.GetBaseUrl() + path.ToString().Replace("~", "");
                    filePathList.Add(CommonModel.GetBaseUrl() + path.Trim().ToString().Replace("~", ""));
                }
                //else
                //    return string.Empty;
            }
            //filePathList=.Join(',');
            return string.Join(",", filePathList);
        }

        public static string GetHeaderUSLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/USAID_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/USAID_Template.png\"");
            else
                return string.Empty;
        }
        public static string GetHeaderCareLogo(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                return filePath.ToString().Replace("src=\"..//Content/images/Care_Template.png\"", "src=\"" + CommonModel.GetWebUrl() + "/Content/images/Care_Template.png\"");
            else
                return string.Empty;
        }
        #endregion

        public static List<SelectListItem> GetALLHyringM(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_HiringType.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.HiringTypeId.ToString(), Text = x.HiringType }).ToList();
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
        public static List<SelectListItem> GetALLIT(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_TypeofInterview.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.InterviewId.ToString(), Text = x.InterviewType }).ToList();
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
        public static List<SelectListItem> GetALLYesNoMId(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_TypeYesNo.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Type }).ToList();
            if (IsSelect == 0)
            {
                //list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
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
            list = _db.mst_TypeYesNo.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.Type.ToString(), Text = x.Type }).ToList();
            if (IsSelect == 0)
            {
                //list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
            }
            else if (IsSelect == 1)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "All" });
            }

            return list;
        }
        public static List<SelectListItem> GetALLAdvertisement(int IsSelect = 0)
        {
            Recruitment_DBEntities _db = new Recruitment_DBEntities();
            List<SelectListItem> list = new List<SelectListItem>();
            list = _db.mst_Advertisement.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.AdvertisementId.ToString(), Text = x.AdvertisementType }).ToList();
            if (IsSelect == 0)
            {
                //list.Insert(0, new SelectListItem { Value = "", Text = "Select" });
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
            list = _db.mst_State.OrderBy(x => x.OrderBy).Select(x => new SelectListItem { Value = x.StateId_pk.ToString(), Text = x.StateName }).ToList();
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
        #region Upload files
        public static bool ValidateImageSizeDocoument(HttpPostedFileBase file)
        {
            byte[] image = new byte[file.ContentLength];
            file.InputStream.Read(image, 0, image.Length);
            // Convert MB to bytes (1 MB = 1024 * 1024 bytes)
            //int maxSizeInBytes = 5242880;//maxMB * 1024 * 1024;
            int maxSizeInBytes = 20971520;//maxMB * 1024 * 1024;/20mb

            // Check if the image size is less than or equal to the specified limit
            if (image.Length <= maxSizeInBytes)
            {
                return true; // Valid size
            }

            return false; // Invalid size
        }
        public static FileModel saveFile(HttpPostedFileBase item, string Fldpath, string FileName)
        {
            FileModel fileModel = new FileModel();
            if (ValidateImageSizeDocoument(item))
            {
                string URL = "";
                string filepath = string.Empty;
                if (item != null && item.ContentLength > 0)
                {
                    if (Fldpath != URL)
                    {
                        URL = Fldpath;
                    }
                    URL = "/Uploads/" + Fldpath + "/";
                    string folderPath = HttpContext.Current.Server.MapPath("~" + URL);

                    var supportedTypes = new[] { "pdf", "xls", "xlsx", "jpeg", "png", "jpg" };

                    var fileName = Path.GetFileName(item.FileName);
                    // var rondom = Guid.NewGuid() + fileName;

                    // var fileExt = System.IO.Path.GetExtension(rondom).Substring(1).ToLower();

                    //if (!supportedTypes.Contains(fileExt.ToLower()))
                    //{
                    //   // Danger("File Extension Is InValid - Upload Only PDF/EXCEL/JPEG/PNG/JPG File");
                    //   // return RedirectToAction("VendorDetails", new { id = d.guid });
                    //}

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // string Document = Path.Combine("~/Uploads/VendorDoc/" + rondom);

                    item.SaveAs(folderPath + fileName);
                    filepath = URL + fileName;
                    fileModel.FolderPath = URL;
                }
                fileModel.IsvalidFile = true;
                fileModel.FilePathFull = filepath;
                return fileModel;
            }
            else
            {
                fileModel.IsvalidFile = false;
                return fileModel;
            }

        }

        public class FileModel
        {
            public string FilePathFull { get; set; }
            public string FolderPath { get; set; }
            public bool IsvalidFile { get; set; }
        }
        #endregion
    }
}