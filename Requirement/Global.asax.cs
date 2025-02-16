using Requirement.Models;
using SubSonic.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Requirement
{
    public class MvcApplication : System.Web.HttpApplication
    {
      
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_BeginRequest()
        //{
        //    if (HttpContext.Current.Request.Url.AbsolutePath == "/")
        //    {
        //        Response.Redirect("~/Account/Login");
        //    }
        //}

        public static UserViewModel CUser
        {
            get
            {
                if (HttpContext.Current?.User?.Identity?.IsAuthenticated ?? false)
                {
                    // Retrieve user from session if exists
                    if (HttpContext.Current.Session["CUser"] is UserViewModel cachedUser)
                    {
                        return cachedUser;
                    }

                    // Fetch user from database if not in session
                    var user = new UserViewModel();
                    StoredProcedure sp = new StoredProcedure("SP_GetCurrentLogin");
                    sp.Command.AddParameter("@UN", HttpContext.Current.User.Identity.Name, DbType.String);
                    DataTable dt = sp.ExecuteDataSet().Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0]; // Get first row
                        user.UserId = dr["UserId"].ToString();
                        user.Name = dr["Name"].ToString();
                        user.Email = dr["Email"].ToString();
                        user.UserName = dr["UserName"].ToString();
                        user.PhoneNumber = dr["PhoneNumber"].ToString();
                        user.LockoutEnabled = dr["LockoutEnabled"].ToString();
                        user.RoleId = Convert.ToInt32(dr["RoleId"]);
                        user.RoleName = dr["RoleName"].ToString();
                        user.Location = dr["Location"].ToString();
                        user.Designation = dr["Designation"].ToString();
                    }

                    // Cache user details in session
                    HttpContext.Current.Session["CUser"] = user;
                    return user;
                }
                else
                {
                    RouteCollection routes = new RouteCollection();
                    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                    routes.MapRoute(
                        name: "Default",
                        url: "{controller}/{action}/{id}",
                        defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                    );
                    return null;
                    // Redirect to login page if not authenticated
                    //HttpContext.Current.Response.Redirect("~/Account/Login", false);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //return null;
                }
            }
        }

       

        //public static UserViewModel CUser
        //{

        //  get
        //    {
        //        if (HttpContext.Current.User.Identity.IsAuthenticated)
        //        {
        //            var User = new UserViewModel();
        //            if (HttpContext.Current.Session["CUser"] != null)
        //            {
        //                return (UserViewModel)HttpContext.Current.Session["CUser"];
        //            }
        //            else
        //            {
        //                StoredProcedure sp = new StoredProcedure("SP_GetCurrentLogin");
        //                sp.Command.AddParameter("@UN", HttpContext.Current.User.Identity.Name, DbType.String);
        //                DataTable dt = sp.ExecuteDataSet().Tables[0];

        //                if (dt.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in dt.Rows)
        //                    {
        //                        User.UserId = dr["UserId"].ToString();
        //                        User.Name = dr["Name"].ToString();
        //                        User.Email = dr["Email"].ToString();
        //                        User.UserName = dr["UserName"].ToString();
        //                        User.PhoneNumber = dr["PhoneNumber"].ToString();
        //                        User.LockoutEnabled = dr["LockoutEnabled"].ToString();
        //                        User.RoleId = Convert.ToInt32(dr["RoleId"]);
        //                        User.RoleName = dr["RoleName"].ToString();
        //                        User.Location = dr["Location"].ToString();
        //                        User.Designation = dr["Designation"].ToString();
        //                    }
        //                }

        //                HttpContext.Current.Session["CUser"] = User;
        //                return CUser;
        //            }
        //        }
        //        else
        //        {
        //            RouteCollection routes = new RouteCollection();
        //            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //            routes.MapRoute(
        //                name: "Default",
        //                url: "{controller}/{action}/{id}",
        //                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
        //            );
        //            //HttpContext.Current.Response.RedirectToRoute("~/Account/Login", false);

        //            //HttpContext.Current.Response.Redirect("~/Account/Login", false);
        //            //RewritePath
        //            return null;
        //        }
        //    }
        //}


    }
}
