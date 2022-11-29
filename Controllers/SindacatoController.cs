﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SDM.Helper;
using SDM.Models;
using SDM.Models.Database;

namespace SDM.Controllers
{
    public class SindacatoController : Controller
    {
        private readonly Logger _logger = new Logger();
        private readonly HelpPratica _help = new HelpPratica();

        #region Pratica
        public ActionResult Index()
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), Session["role"].ToString()))
                    {
                        PraticaIndex model = new PraticaIndex
                        {
                            Pratiche = _help.PraticaSindacato(Convert.ToInt32(Session["idsede"].ToString()), Session["role"].ToString(), "getall", null),
                            Categorie = Session["role"].ToString() != "smartJob" ? _help.GetCategorie("sindacato") : _help.GetCategorie("sindacatoSmartJob")
                        };

                        return View(model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult AggiungiPratica()
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), Session["role"].ToString(), true))
                    {
                        Pratica model = new Pratica
                        {
                            SottocategoriaList = Session["role"].ToString() != "smartJob" ? _help.GetCategorie("sindacato") : _help.GetCategorie("sindacatoSmartJob")
                        };

                        return View(model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ModificaPratica(int idPratica)
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), Session["role"].ToString(), true))
                    {

                        Pratica model = _help.PraticaSindacato(idPratica, "get");
                        model.SottocategoriaList = Session["role"].ToString() != "smartJob" ? _help.GetCategorie("sindacato") : _help.GetCategorie("sindacatoSmartJob");
                        model.StatoList = _help.GetStati();

                        return View(model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("Login", "Home"); }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult SavePratica(Pratica pratica)
        {
            try
            {

                if (_help.ControlloCampi(pratica))
                {
                    pratica.LastUpdate = DateTime.Now;
                    pratica.IdUserUpdate = Convert.ToInt32(Session["Id"].ToString());
                    pratica.IdSede = Convert.ToInt32(Session["idsede"].ToString());
                    pratica.NumPratica = Session["sede"].ToString() + "-" + pratica.Anno + "-";

                    if (_help.PraticaSindacato(pratica, "save"))
                    {
                        TempData["erroreInserimentoPatronatoHome"] = "Pratica salvata correttamente";
                        return RedirectToAction("Index", "Sindacato");
                    }
                }

                TempData["erroreInserimentoPatronato"] = "true";
                return RedirectToAction("AggiungiPratica", "Sindacato");
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                TempData["ExceptionError"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditPratica(Pratica pratica)
        {
            try
            {
                if (_help.ControlloCampi(pratica))
                {
                    pratica.LastUpdate = DateTime.Now;
                    pratica.IdUserUpdate = Convert.ToInt32(Session["Id"].ToString());
                    pratica.IdSede = Convert.ToInt32(Session["idsede"].ToString());

                    if (_help.PraticaSindacato(pratica, "update"))
                    {
                        TempData["erroreInserimentoPatronatoHome"] = "Pratica modificata correttamente";
                        return RedirectToAction("Index", "Sindacato");
                    }
                }

                TempData["erroreInserimentoPatronato"] = "true";
                return RedirectToAction("ModificaPratica", "Sindacato", new { idPratica = pratica.Id });
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                TempData["ExceptionError"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult DeletePratica(int idPratica)
        {
            try
            {

                PraticaIndex model = new PraticaIndex();
                if (_help.DeleteSindacato(idPratica))
                {
                    model.Pratiche = _help.PraticaSindacato(Convert.ToInt32(Session["idsede"].ToString()), Session["role"].ToString(), "getall", null);
                    return PartialView("TableIndex", model.Pratiche);
                }

                TempData["message"] = "Errore nell'eliminazione della pratica";
                model.Pratiche = _help.PraticaSindacato(Convert.ToInt32(Session["idsede"].ToString()), Session["role"].ToString(), "getall", null);
                return PartialView("TableIndex", model.Pratiche);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult SearchPratica(Pratica pratica)
        {
            try
            {
                pratica.IdSede = Convert.ToInt32(Session["idsede"].ToString());
                List<Pratica> result = _help.PraticaSindacato(0, Session["role"].ToString(), "search", pratica);

                TempData["patronatoList"] = result;

                return PartialView("TableIndex", result);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                TempData["ExceptionError"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region File
        public ActionResult CaricaAllegati(int idPratica)
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), Session["role"].ToString(), true))
                    {

                        List<Attachment> model = _help.AttachmentsSindacato(idPratica, "getall");
                        return View(model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }                
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult UploadFile(int id, List<HttpPostedFileBase> files)
        {
            try
            {

                if (Request.Files != null && Request.Files.Count > 0 && Request.Files.Count <= 4)
                {
                    List<Attachment> ListFile = new List<Attachment>();

                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var content = new byte[file.ContentLength];
                            file.InputStream.Read(content, 0, file.ContentLength);

                            Attachment loadFile = new Attachment
                            {
                                Nome = file.FileName,
                                Blob = content,
                                Type = file.ContentType,
                                IdUserUpdate = Convert.ToInt32(Session["id"].ToString()),
                                IdPratica = id,
                                LastUpdate = DateTime.Now,
                            };

                            ListFile.Add(loadFile);
                        }
                    }

                    if (ListFile.Count > 0)
                    {
                        if (!_help.AttachmentsSindacato(ListFile, "upload"))
                        {
                            TempData["messaggioErroreInserimentoFile"] = "Errore nell'inserimento del documento";
                        }
                    }
                }

                List<Attachment> model = _help.AttachmentsSindacato(id, "getall");
                return PartialView("TableAllegati", model);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult DownloadFile(int idFile)
        {
            try
            {
                Attachment loadFile = _help.AttachmentsSindacato("download", idFile);

                return File(loadFile.Blob, loadFile.Type, loadFile.Nome);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult DeleateFile(int idPratica, int idFile)
        {
            try
            {

                List<Attachment> model = new List<Attachment>();
                if (_help.DeleteFileSindacato(idFile))
                {
                    model = _help.AttachmentsSindacato(idPratica, "getall");
                    return PartialView("TableAllegati", model);
                }

                TempData["messaggioErroreInserimentoFile"] = "Errore nell'eliminazione del file";
                model = _help.AttachmentsSindacato(idPratica, "getall");
                return PartialView("TableAllegati", model);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SindacatoController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
    }
}