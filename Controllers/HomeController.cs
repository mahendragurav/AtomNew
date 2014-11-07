using AtomConfiguratorModel.Models;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.AuthenticationServices;
using Flextronics.QMSCC.Commons.SystemIntegrations.FlexWare.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ATOMv0.Controllers
{
  public class HomeController : Controller
  {
    bool isValidUser = false;
    [AuthorizeEnum(RolesEnum.Roles.AtomAdministrator, RolesEnum.Roles.SiteAdministrator)]
    public ActionResult Index()
    {
      ViewBag.Title = "Home Page";

      userrolesws userRoles = (userrolesws)TempData["Roles"];

      if (userRoles != null)
      {

        if (userRoles.roles[0].name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.AtomAdministrator))
        {
          return RedirectToAction("DimNavigation");
         
        }
        else if (userRoles.roles[0].name == AuthorizeEnumAttribute.GetEnumDescription(RolesEnum.Roles.SiteAdministrator))
        {
          return RedirectToAction("Index", "FFSite");
        }
        else
        {
          return RedirectToAction("Login");
        }
      }
      else
      {
        return RedirectToAction("Login");
      }

    }
    [AuthorizeEnum(RolesEnum.Roles.AtomAdministrator)]
    public ActionResult DimNavigation()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
    [AllowAnonymous]
    public ActionResult LoginRedirect()
    {
      return RedirectToAction("Login");
    }
    [AllowAnonymous]
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
      isValidUser = ValidateUser(model);
      if (isValidUser)
      {
        return RedirectToAction("Index");

      }

      else
      {
        ModelState.AddModelError("Error", "Please enter valid user name or password");
        return View();
      }

    }
    public ActionResult Logout()
    {
      if (Request.IsAuthenticated)
      {
        FormsAuthentication.SignOut();
        ClearSessions();
      }

      return RedirectToAction("Login");
    }

    private void ClearSessions()
    {
      //if (Session["userRoles"] != null)
      //{
      //  Session["userRoles"] = null;
      //}
    }

    private bool ValidateUser(LoginModel model)
    {
      try
      {
        bool isValidUser = false;
        string Roleserrors = string.Empty;
        string MasterDataErrors = string.Empty;
        string url = ConfigurationManager.AppSettings["FlexwareUrlQA"];
        string strSolutionCode = Convert.ToString(ConfigurationManager.AppSettings["SolutionCode"]);
        Authentication authentication = new Authentication(url);

        string tokenAuthentication = authentication.AuthenticateUser(model.UserName, model.Password, strSolutionCode);
        userrolesws userRoles = FlexWareGetUserRoles(authentication);

        if (userRoles != null)
        {
          isValidUser = true;
          FormsAuthentication.SetAuthCookie(model.UserName, false);
          TempData["Roles"] = userRoles;
          string roles = "";



          foreach (var item in userRoles.roles)
          {
            if (roles == "")
            {
              roles = item.name;
            }
            else
            {
              roles = roles + ";" + item.name;
            }
          }
          //roles = "ATOM_ADMINISTRATOR;SALES_USER";
          FormsAuthentication.SetAuthCookie(model.UserName + "|" + roles, false);

        }
        else
        {
          isValidUser = false;
        }

        masterdataelementws[] masterData = GetMasterData(authentication, out MasterDataErrors);

        if (masterData != null)
        {
          foreach (var item in masterData)
          {

          }
        }
        return isValidUser;
      }
      catch (Exception ex)
      {
        return isValidUser = false;
      }
    }

    private masterdataelementws[] GetMasterData(Authentication authentication, out string errors)
    {
      try
      {
        errors = string.Empty;
        return authentication.GetMasterData();
      }
      catch (Exception ex)
      {
        errors = "Error on MasterData: " + ReadException(ex);
      }

      return null;
    }

    private string ReadException(Exception ex)
    {
      if (ex.InnerException != null)
      {
        return ReadException(ex.InnerException);
      }
      else
      {
        return ex.Message;
      }
    }

    private userrolesws FlexWareGetUserRoles(Authentication authentication)
    {
      try
      {
        // errors = string.Empty;
        return authentication.GetUserRoles();
      }
      catch (Exception ex)
      {
        // errors = "Error on GetRoles: " + ReadException(ex);
      }
      return null;
    }
  }
}