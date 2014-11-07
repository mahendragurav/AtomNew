using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ATOMv0
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


    protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
    {
      if (FormsAuthentication.CookiesSupported == true)
      {
        if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
        {
          try
          {
            string userDetails = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            string roles = "";

            string[] userName;
            if (userDetails != null && userDetails.Length > 0)
            {
              userName = userDetails.Split('|');


              //userrolesws userRoles = (userrolesws)Context.Items["Test"];
              //if (Session["userRoles"] != null)
              //{
              //  userrolesws userRoles = (userrolesws)Session["userRoles"];
              //  if (userRoles != null && userRoles.roles.Count() > 0)
              //  {
              //    foreach (var item in userRoles.roles)
              //    {
              //      if (roles == "")
              //      {
              //        roles = item.name;
              //      }
              //      else
              //      {
              //        roles = roles + ";" + item.name;
              //      }
              //    }
              //  }
              //}
              // FormsAuthentication.SetAuthCookie(userName, false);
              //if (Session["userRoles"] != null)
              //  Session["userRoles"] = null;
              //Context.User = new System.Security.Principal.GenericPrincipal(
              //     new System.Security.Principal.GenericIdentity(userName, "Forms"), roles.Split(';'));

              HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                    new System.Security.Principal.GenericIdentity(userName[0], "Forms"), userName[1].Split(';'));

            }

          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
      }
    }




    //void Application_AcquireRequestState(object sender, EventArgs e)
    //{
    //  if (System.Web.HttpContext.Current.Session != null)
    //  {
    //    if (FormsAuthentication.CookiesSupported == true)
    //    {
    //      if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
    //      {
    //        try
    //        {
    //          string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
    //          string roles = "";

    //          if (Session["userRoles"] != null)
    //          {
    //            userrolesws userRoles = (userrolesws)Session["userRoles"];
    //            if (userRoles != null && userRoles.roles.Count() > 0)
    //            {
    //              foreach (var item in userRoles.roles)
    //              {
    //                if (roles == "")
    //                {
    //                  roles = item.name;
    //                }
    //                else
    //                {
    //                  roles = roles + ";" + item.name;
    //                }
    //              }
    //            }
    //          }
    //          // FormsAuthentication.SetAuthCookie(userName, false);
    //          if (Session["userRoles"] != null)
    //            Session["userRoles"] = null;
    //          //Context.User = new System.Security.Principal.GenericPrincipal(
    //          //     new System.Security.Principal.GenericIdentity(userName, "Forms"), roles.Split(';'));

    //          HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
    //                new System.Security.Principal.GenericIdentity(userName, "Forms"), roles.Split(';'));

    //        }
    //        catch (Exception ex)
    //        {
    //          throw ex;
    //        }
    //      }
    //    }
    //  }
    //}
  }
}
