using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDM.Helper;
using SDM.Models;
using SDM.Models.Database;

namespace SDM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult ErrorAuth()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginResult(string username, string password, string viewName, string controllerName, string idPratica, string tipoPratica)
        {
            try
            {
                Authentication authentication = new Authentication();
                var user = authentication.GetLoginUser(username, password);
                if (user != null)
                {
                    Session["id"] = user.Id;
                    Session["username"] = user.Username;
                    Session["password"] = password;
                    Session["idsede"] = user.IdSede;
                    Session["sede"] = user.Sedi.Acronimo;
                    Session["role"] = user.Roles.Ruolo;

                    if (viewName != null && viewName != "" && controllerName != null && controllerName != "" && idPratica != null && idPratica != "" && tipoPratica != null && tipoPratica != "")
                    {
                        return RedirectToAction(viewName, controllerName, new { id = idPratica, type = tipoPratica});
                    }
                    else { return RedirectToAction("Index", "Home"); }
                }
                else {
                    TempData["loginError"] = "Username o password errata";
                    return RedirectToAction("Login", "Home"); 
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public RedirectResult Logout()
        {
            Session.Abandon();
            return Redirect("https://www.sdmservices.it/");
        }

        public ActionResult Index()
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), "admin"))
                    {
                        return View();
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("Login", "Home"); }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }        

        public ActionResult RiepilogoPratica(int id, string type)
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), "admin"))
                    {
                        HelpPratica _help = new HelpPratica();
                        RiepilogoPratica model = _help.GetRiepilogoPratica(id, type);
                        return View(model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else {
                    TempData["controllerName"] = "Home";
                    TempData["viewName"] = "RiepilogoPratica";
                    TempData["idPratica"] = id;
                    TempData["tipoPratica"] = type;

                    return RedirectToAction("Login", "Home"); 
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        public ActionResult GuidaPrimoAccesso()
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), "admin"))
                    {
                        return View();
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ResetPassword()
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), "admin"))
                    {
                        return View();
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                Authentication authentication = new Authentication();
                if(!string.IsNullOrWhiteSpace(oldPassword) && !string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(confirmPassword)) {
                    var user = authentication.GetLoginUser(Session["username"].ToString(), oldPassword);
                    if (user != null)
                    {
                        if(newPassword == confirmPassword)
                        {
                            bool reset = authentication.ResetPassword(Session["username"].ToString(), oldPassword, newPassword);

                            if (reset)
                            {
                                Session["password"] = newPassword;
                                TempData["resetSuccess"] = "Password cambiata correttamente";
                                return RedirectToAction("ResetPassword", "Home");
                            }
                            else
                            {
                                TempData["resetError"] = "Errore nel reset della password";
                                return RedirectToAction("ResetPassword", "Home");
                            }
                        }
                        else
                        {
                            TempData["resetError"] = "La nuova password non corrisponde con la conferma";
                            return RedirectToAction("ResetPassword", "Home");
                        }
                    }
                    else
                    {
                        TempData["resetError"] = "Password corrente errata";
                        return RedirectToAction("ResetPassword", "Home");
                    }
                }
                else
                {
                    TempData["resetError"] = "I campi non possono essere nulli";
                    return RedirectToAction("ResetPassword", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}