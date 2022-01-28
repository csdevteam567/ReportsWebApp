using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportsWebApp.Models;
using Microsoft.AspNet.Identity;

namespace ReportsWebApp
{
    public class DatabaseAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            var isAuthorized = base.AuthorizeCore(httpContext);
            
            if (isAuthorized)
            {
                string userId = httpContext.User.Identity.GetUserId();

                using (var db = new ReportsWebAppDBEntities())
                {
                    try
                    {
                        result = db.AccessRights.Where(ar => ar.AspNetUserId == userId).First().Database;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                    }
                }

            }

            return result;
        }
    }

    public class GroupReportAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (isAuthorized)
            {
                string userId = httpContext.User.Identity.GetUserId();

                using (var db = new ReportsWebAppDBEntities())
                {
                    try
                    {
                        result = db.AccessRights.Where(ar => ar.AspNetUserId == userId).First().GroupReport;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                    }
                }

            }

            return result;
        }
    }

    public class PlanReportAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (isAuthorized)
            {
                string userId = httpContext.User.Identity.GetUserId();

                using (var db = new ReportsWebAppDBEntities())
                {
                    try
                    {
                        result = db.AccessRights.Where(ar => ar.AspNetUserId == userId).First().PlanReport;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                    }
                }

            }

            return result;
        }
    }

    public class AccessRightsAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (isAuthorized)
            {
                string userId = httpContext.User.Identity.GetUserId();

                using (var db = new ReportsWebAppDBEntities())
                {
                    try
                    {
                        result = db.AccessRights.Where(ar => ar.AspNetUserId == userId).First().ManageUsers;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                    }
                }

            }

            return result;
        }
    }
}