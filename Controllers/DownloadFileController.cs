using System;
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
    public class DownloadFileController : Controller
    {
        private readonly Logger _logger = new Logger();
        private readonly HelpPratica _help = new HelpPratica();

        #region Pratica
        public ActionResult Index(int idSezione)
        {
            try
            {
                Authentication authentication = new Authentication();
                if (Session["username"] != null && Session["password"] != null)
                {
                    if (authentication.Login(Session["username"].ToString(), Session["password"].ToString(), Session["role"].ToString(), true))
                    {
                        DownloadIndex model = new DownloadIndex
                        {
                            Files = _help.GetFileDownload(idSezione)
                        };

                        return View("Download", model);
                    }
                    else { return RedirectToAction("ErrorAuth", "Home"); }
                }
                else { return RedirectToAction("ErrorAuth", "Home"); }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region File
        [HttpPost]
        public ActionResult UploadFile(int id, List<HttpPostedFileBase> files)
        {
            try
            {
                if (Request.Files != null && Request.Files.Count > 0 && Request.Files.Count <= 4)
                {
                    List<Download> ListFile = new List<Download>();

                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var content = new byte[file.ContentLength];
                            file.InputStream.Read(content, 0, file.ContentLength);

                            Download loadFile = new Download
                            {
                                Nome = file.FileName,
                                Blob = content,
                                Type = file.ContentType,
                                IdUserUpdate = Convert.ToInt32(Session["id"].ToString()),
                                IdSezione = id,
                                LastUpdate = DateTime.Now,
                            };

                            ListFile.Add(loadFile);
                        }
                    }

                    if (ListFile.Count > 0)
                    {
                        if (!_help.LoadFileDownload(ListFile, id))
                        {
                            TempData["messaggioErroreInserimentoFile"] = "Errore nell'inserimento del documento";
                        }
                    }
                }

                List<Download> model = _help.GetFileDownload(id);
                return PartialView("DownloadTableAllegati", model);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult DownloadFile(int idFile)
        {
            try
            {
                Download loadFile = _help.DownloadFileDownload(idFile);

                return File(loadFile.Blob, loadFile.Type, loadFile.Nome);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFile", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult DeleateFile(int idSezione, int idFile)
        {
            try
            {
                List<Download> model = new List<Download>();
                if (_help.DelateFileDownload(idFile))
                {
                    model = _help.GetFileDownload(idSezione);
                    return PartialView("DownloadTableAllegati", model);
                }

                TempData["messaggioErroreInserimentoFile"] = "Errore nell'eliminazione del file";
                model = _help.GetFileDownload(idSezione);
                return PartialView("DownloadTableAllegati", model);
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileController", null, ex);
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
    }
}