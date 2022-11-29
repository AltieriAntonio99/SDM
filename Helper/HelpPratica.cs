﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDM.Models;
using SDM.Models.Database;

namespace SDM.Helper
{
    public class HelpPratica
    {
        private Logger _logger = new Logger();
        private Mail _mail = new Mail();

        #region Generale
        public bool ControlloCampi(Pratica pratica)
        {
            try
            {
                if (pratica.Nome != null && pratica.Nome != " " && pratica.Nome != "")
                {
                    if (pratica.Cognome != null && pratica.Cognome != " " && pratica.Cognome != "")
                    {
                        if (pratica.Sottocategoria != null && pratica.Sottocategoria != " " && pratica.Sottocategoria != "")
                        {
                            if (pratica.TipologiaPratica != null && pratica.TipologiaPratica != " " && pratica.TipologiaPratica != "")
                            {
                                if (pratica.Anno != null && pratica.Anno != " " && pratica.Anno != "")
                                {
                                    if (pratica.Note != null && pratica.Note != " " && pratica.Note != "")
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ControlloCampi", null, ex);
                throw;
            }
        }

        public bool ControlloCampiNoTipologia(Pratica pratica)
        {
            try
            {
                if (pratica.Nome != null && pratica.Nome != " " && pratica.Nome != "")
                {
                    if (pratica.Cognome != null && pratica.Cognome != " " && pratica.Cognome != "")
                    {
                        if (pratica.Sottocategoria != null && pratica.Sottocategoria != " " && pratica.Sottocategoria != "")
                        {
                            if (pratica.Anno != null && pratica.Anno != " " && pratica.Anno != "")
                            {
                                if (pratica.Note != null && pratica.Note != " " && pratica.Note != "")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ControlloCampi", null, ex);
                throw;
            }
        }

        public bool ControlloCampiNoCategoria(Pratica pratica)
        {
            try
            {
                if (pratica.Nome != null && pratica.Nome != " " && pratica.Nome != "")
                {
                    if (pratica.Cognome != null && pratica.Cognome != " " && pratica.Cognome != "")
                    {
                        if (pratica.TipologiaPratica != null && pratica.Sottocategoria != " " && pratica.Sottocategoria != "")
                        {
                            if (pratica.Anno != null && pratica.Anno != " " && pratica.Anno != "")
                            {
                                if (pratica.Note != null && pratica.Note != " " && pratica.Note != "")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ControlloCampi", null, ex);
                throw;
            }
        }

        public List<Categorie> GetCategorie(string type)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var result = context.Categorie.Where(i => i.Categoria.ToLower() == type.ToLower()).OrderBy(i => i.Nome).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetCategorie", null, ex);
                throw;
            }
        }

        public List<Status> GetStati()
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var result = context.Status.OrderBy(i => i.Stato).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetCategorie", null, ex);
                throw;
            }
        }

        public RiepilogoPratica GetRiepilogoPratica(int id, string type)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    RiepilogoPratica riepilogoPratica = new RiepilogoPratica();

                    switch (type)
                    {
                        case "Sindacato":
                            riepilogoPratica.pratica = PraticaSindacato(id, "get");
                            riepilogoPratica.attachment = AttachmentsSindacato(id, "getall");
                            riepilogoPratica.type = "Sindacato";
                            break;
                        case "Patronato":
                            riepilogoPratica.pratica = PraticaPatronato(id, "get");
                            riepilogoPratica.attachment = AttachmentsPatronato(id, "getall");
                            riepilogoPratica.type = "Patronato";
                            break;
                        case "PraticheAuto":
                            riepilogoPratica.pratica = PraticaEventi(id, "get");
                            riepilogoPratica.attachment = AttachmentsEventi(id, "getall");
                            riepilogoPratica.type = "PraticheAuto";
                            break;
                        case "Eventi":
                            riepilogoPratica.pratica = PraticaEventi(id, "get");
                            riepilogoPratica.attachment = AttachmentsEventi(id, "getall");
                            riepilogoPratica.type = "Eventi";
                            break;
                        case "StudioProfessionale":
                            riepilogoPratica.pratica = PraticaStudioProfessionale(id, "get");
                            riepilogoPratica.attachment = AttachmentsStudioProfessionale(id, "getall");
                            riepilogoPratica.type = "StudioProfessionale";
                            break;
                        case "Agenzia":
                            riepilogoPratica.pratica = PraticaEventi(id, "get");
                            riepilogoPratica.attachment = AttachmentsEventi(id, "getall");
                            riepilogoPratica.type = "Agenzia";
                            break;
                    }

                    return riepilogoPratica;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetRiepilogoPratica", null, ex);
                throw;
            }
        }
        #endregion

        #region Patronato
        public List<Pratica> PraticaPatronato(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<Patronato> items = new List<Patronato>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPratichePatronato";
                    items = GetPratiche<Patronato>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaPatronato";
                    items = GetPratiche<Patronato>(logMessage);
                    break;
            }

            return MapListPatronatoToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaPatronato(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaPatronato";
                    var item = GetPratica<Patronato>(idPratica, logMessage);
                    result = MapPatronatoToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaPatronato(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "Patronato";
            string logMessage;
            Patronato item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "segreteria@sdmservices.it";
            string mailSubject = "Pratica Patronato";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaPatronato";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewPatronato(pratica);
                    mailPratica = MapPatronatoToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaPatronato";
                    var prevPratica = GetById<Patronato>(pratica.Id);
                    item = MapPraticaToPatronato(prevPratica, pratica);
                    mailPratica = MapPatronatoToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsPatronato(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsPatronato> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFilePatronato";
                    items = GetFile<AttachmentsPatronato>(idPratica, logMessage);
                    result = MapListAttachmentsPatronatoToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsPatronato(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFilePatronato";
                    var temp = MapListAttachmentsToAttachmentsPatronato(items);
                    result = LoadFile<AttachmentsPatronato>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsPatronato(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFilePatronato";
                    var item = DownloadFile<AttachmentsPatronato>(idFile, logMessage);
                    result = MapAttachmentsPatronatoToAttachments(item);
                    break;
            }

            return result;
        }

        public bool DeletePatronato(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Patronato.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsPatronato.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsPatronato.Remove(item);
                            }
                        }

                        context.Patronato.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeletePatronato", null, ex);
                throw;
            }
        }

        public bool DeleteFilePatronato(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsPatronato.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsPatronato.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFilePatronato", null, ex);
                throw;
            }
        }
        #endregion

        #region PraticheAuto
        public List<Pratica> PraticaPraticheAuto(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<PraticheAuto> items = new List<PraticheAuto>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPratichePraticheAuto";
                    items = GetPratiche<PraticheAuto>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaPraticheAuto";
                    items = GetPratiche<PraticheAuto>(logMessage);
                    break;
            }

            return MapListPraticheAutoToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaPraticheAuto(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaPraticheAuto";
                    var item = GetPratica<PraticheAuto>(idPratica, logMessage);
                    result = MapPraticheAutoToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaPraticheAuto(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "PraticheAuto";
            string logMessage;
            PraticheAuto item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "segreteria@sdmservices.it";
            string mailSubject = "Pratica PraticheAuto";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaPraticheAuto";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewPraticheAuto(pratica);
                    mailPratica = MapPraticheAutoToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaPraticheAuto";
                    var prevPratica = GetById<PraticheAuto>(pratica.Id);
                    item = MapPraticaToPraticheAuto(prevPratica, pratica);
                    mailPratica = MapPraticheAutoToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsPraticheAuto(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsPraticheAuto> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFilePraticheAuto";
                    items = GetFile<AttachmentsPraticheAuto>(idPratica, logMessage);
                    result = MapListAttachmentsPraticheAutoToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsPraticheAuto(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFilePraticheAuto";
                    var temp = MapListAttachmentsToAttachmentsPraticheAuto(items);
                    result = LoadFile<AttachmentsPraticheAuto>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsPraticheAuto(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFilePraticheAuto";
                    var item = DownloadFile<AttachmentsPraticheAuto>(idFile, logMessage);
                    result = MapAttachmentsPraticheAutoToAttachments(item);
                    break;
            }

            return result;
        }

        public bool DeletePraticheAuto(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.PraticheAuto.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsPraticheAuto.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsPraticheAuto.Remove(item);
                            }
                        }

                        context.PraticheAuto.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeletePraticheAuto", null, ex);
                throw;
            }
        }

        public bool DeleteFilePraticheAuto(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsPraticheAuto.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsPraticheAuto.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFilePraticheAuto", null, ex);
                throw;
            }
        }
                
        #endregion

        #region Eventi
        public List<Pratica> PraticaEventi(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<Eventi> items = new List<Eventi>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPraticheEventi";
                    items = GetPratiche<Eventi>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaEventi";
                    items = GetPratiche<Eventi>(logMessage);
                    break;
            }

            return MapListEventiToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaEventi(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaEventi";
                    var item = GetPratica<Eventi>(idPratica, logMessage);
                    result = MapEventiToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaEventi(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "Eventi";
            string logMessage;
            Eventi item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "segreteria@sdmservices.it";
            string mailSubject = "Pratica Eventi";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaEventi";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewEventi(pratica);
                    mailPratica = MapEventiToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaEventi";
                    var prevPratica = GetById<Eventi>(pratica.Id);
                    item = MapPraticaToEventi(prevPratica, pratica);
                    mailPratica = MapEventiToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsEventi(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsEventi> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFileEventi";
                    items = GetFile<AttachmentsEventi>(idPratica, logMessage);
                    result = MapListAttachmentsEventiToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsEventi(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFileEventi";
                    var temp = MapListAttachmentsToAttachmentsEventi(items);
                    result = LoadFile<AttachmentsEventi>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsEventi(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFileEventi";
                    var item = DownloadFile<AttachmentsEventi>(idFile, logMessage);
                    result = MapAttachmentsEventiToAttachments(item);
                    break;
            }

            return result;
        }

        public bool DeleteEventi(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Eventi.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsEventi.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsEventi.Remove(item);
                            }
                        }

                        context.Eventi.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteEventi", null, ex);
                throw;
            }
        }

        public bool DeleteFileEventi(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsEventi.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsEventi.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFileEventi", null, ex);
                throw;
            }
        }
        #endregion

        #region Sindacato
        public List<Pratica> PraticaSindacato(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<Sindacato> items = new List<Sindacato>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPraticheSindacato";
                    items = GetPratiche<Sindacato>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaSindacato";
                    items = GetPratiche<Sindacato>(logMessage);
                    break;
            }

            return MapListSindacatoToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaSindacato(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaSindacato";
                    var item = GetPratica<Sindacato>(idPratica, logMessage);
                    result = MapSindacatoToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaSindacato(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "Sindacato";
            string logMessage;
            Sindacato item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "segreteria@sdmservices.it";
            string mailSubject = "Pratica Sindacato";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaSindacato";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewSindacato(pratica);
                    mailPratica = MapSindacatoToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaSindacato";
                    var prevPratica = GetById<Sindacato>(pratica.Id);
                    item = MapPraticaToSindacato(prevPratica, pratica);
                    mailPratica = MapSindacatoToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsSindacato(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsSindacato> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFileSindacato";
                    items = GetFile<AttachmentsSindacato>(idPratica, logMessage);
                    result = MapListAttachmentsSindacatoToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsSindacato(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFileSindacato";
                    var temp = MapListAttachmentsToAttachmentsSindacato(items);
                    result = LoadFile<AttachmentsSindacato>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsSindacato(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFileSindacato";
                    var item = DownloadFile<AttachmentsSindacato>(idFile, logMessage);
                    result = MapAttachmentsSindacatoToAttachments(item);
                    break;
            }

            return result;
        }
                
        public bool DeleteSindacato(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Sindacato.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsSindacato.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsSindacato.Remove(item);
                            }
                        }

                        context.Sindacato.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteSindacato", null, ex);
                throw;
            }
        }

        public bool DeleteFileSindacato(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsSindacato.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsSindacato.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFileSindacato", null, ex);
                throw;
            }
        }

        #endregion

        #region StudioProfessionale
        public List<Pratica> PraticaStudioProfessionale(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<StudioProfessionale> items = new List<StudioProfessionale>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPraticheStudioProfessionale";
                    items = GetPratiche<StudioProfessionale>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaStudioProfessionale";
                    items = GetPratiche<StudioProfessionale>(logMessage);
                    break;
            }

            return MapListStudioProfessionaleToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaStudioProfessionale(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaStudioProfessionale";
                    var item = GetPratica<StudioProfessionale>(idPratica, logMessage);
                    result = MapStudioProfessionaleToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaStudioProfessionale(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "StudioProfessionale";
            string logMessage;
            StudioProfessionale item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "documenti@sdmservices.it"; //documenti@
            string mailSubject = "Pratica Studio Professionale";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaStudioProfessionale";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewStudioProfessionale(pratica);
                    mailPratica = MapStudioProfessionaleToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaStudioProfessionale";
                    var prevPratica = GetById<StudioProfessionale>(pratica.Id);
                    item = MapPraticaToStudioProfessionale(prevPratica, pratica);
                    mailPratica = MapStudioProfessionaleToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsStudioProfessionale(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsStudioProfessionale> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFileStudioProfessionale";
                    items = GetFile<AttachmentsStudioProfessionale>(idPratica, logMessage);
                    result = MapListAttachmentsStudioProfessionaleToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsStudioProfessionale(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFileStudioProfessionale";
                    var temp = MapListAttachmentsToAttachmentsStudioProfessionale(items);
                    result = LoadFile<AttachmentsStudioProfessionale>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsStudioProfessionale(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFileStudioProfessionale";
                    var item = DownloadFile<AttachmentsStudioProfessionale>(idFile, logMessage);
                    result = MapAttachmentsStudioProfessionaleToAttachments(item);
                    break;
            }

            return result;
        }

        public bool DeleteStudioProfessionale(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.StudioProfessionale.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsStudioProfessionale.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsStudioProfessionale.Remove(item);
                            }
                        }

                        context.StudioProfessionale.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteStudioProfessionale", null, ex);
                throw;
            }
        }
        
        public bool DeleteFileStudioProfessionale(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsStudioProfessionale.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsStudioProfessionale.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFileStudioProfessionale", null, ex);
                throw;
            }
        }
                
        #endregion

        #region Agenzia
        public List<Pratica> PraticaAgenzia(int idSede, string ruolo, string metodo, Pratica criteri)
        {
            #region variabili gestionali
            string logMessage;
            List<Agenzia> items = new List<Agenzia>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetPraticheAgenzia";
                    items = GetPratiche<Agenzia>(logMessage);
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminSupportoNoSmartJob")
                    {
                        criteri = new Pratica();
                    }
                    else criteri = new Pratica() { IdSede = idSede };
                    break;
                case "search":
                    logMessage = "RicercaAgenzia";
                    items = GetPratiche<Agenzia>(logMessage);
                    break;
            }

            return MapListAgenziaToPraticaFiltered(items, criteri);
        }

        public Pratica PraticaAgenzia(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            Pratica result = new Pratica();
            #endregion

            switch (metodo)
            {
                case "get":
                    logMessage = "GetPraticaAgenzia";
                    var item = GetPratica<Agenzia>(idPratica, logMessage);
                    result = MapAgenziaToPratica(item);
                    break;

            }

            return result;
        }

        public bool PraticaAgenzia(Pratica pratica, string metodo)
        {
            #region variabili gestionali
            string tipoPratica = "Agenzia";
            string logMessage;
            Agenzia item;
            Pratica mailPratica;
            bool result = false;
            #endregion

            #region variabili mail
            string mailTos = "segreteria@sdmservices.it";
            string mailSubject = "Pratica Agenzia";
            #endregion

            switch (metodo)
            {
                case "save":
                    logMessage = "SalvaPraticaAgenzia";
                    pratica.NumPratica = GetNextNumeroPratica(pratica.NumPratica, tipoPratica);
                    item = MapPraticaToNewAgenzia(pratica);
                    mailPratica = MapAgenziaToPraticaMail(item);
                    result = SalvaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
                case "update":
                    logMessage = "ModificaPraticaAgenzia";
                    var prevPratica = GetById<Agenzia>(pratica.Id);
                    item = MapPraticaToAgenzia(prevPratica, pratica);
                    mailPratica = MapAgenziaToPraticaMail(item);
                    result = ModificaPratica(item, tipoPratica, mailTos, mailSubject, mailPratica, logMessage);
                    break;
            }

            return result;
        }

        public List<Attachment> AttachmentsAgenzia(int idPratica, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            List<AttachmentsAgenzia> items;
            List<Attachment> result = new List<Attachment>();
            #endregion

            switch (metodo)
            {
                case "getall":
                    logMessage = "GetFileAgenzia";
                    items = GetFile<AttachmentsAgenzia>(idPratica, logMessage);
                    result = MapListAttachmentsAgenziaToAttachments(items);
                    break;
            }

            return result;
        }

        public bool AttachmentsAgenzia(List<Attachment> items, string metodo)
        {
            #region variabili gestionali
            string logMessage;
            bool result = false;
            #endregion

            switch (metodo)
            {
                case "upload":
                    logMessage = "LoadFileAgenzia";
                    var temp = MapListAttachmentsToAttachmentsAgenzia(items);
                    result = LoadFile<AttachmentsAgenzia>(temp, logMessage);
                    break;
            }

            return result;
        }

        public Attachment AttachmentsAgenzia(string metodo, int idFile)
        {
            #region variabili gestionali
            string logMessage;
            Attachment result = new Attachment();
            #endregion

            switch (metodo)
            {
                case "download":
                    logMessage = "DownloadFileAgenzia";
                    var item = DownloadFile<AttachmentsAgenzia>(idFile, logMessage);
                    result = MapAttachmentsAgenziaToAttachments(item);
                    break;
            }

            return result;
        }

        public bool DeleteAgenzia(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Agenzia.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsAgenzia.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsAgenzia.Remove(item);
                            }
                        }

                        context.Agenzia.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteAgenzia", null, ex);
                throw;
            }
        }

        public bool DeleteFileAgenzia(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsAgenzia.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsAgenzia.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFileAgenzia", null, ex);
                throw;
            }
        }
        #endregion

        #region DownloadFileArea
        public List<Download> GetFileDownload(int idSezione)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<DownloadFile> files = context.DownloadFile.Where(i => i.IdSezione == idSezione).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Download> result = new List<Download>();
                    foreach (var file in files)
                    {
                        result.Add(new Download
                        {
                            Id = file.Id,
                            Nome = file.Nome,
                            Blob = file.Blob,
                            LastUpdate = file.LastUpdate,
                            Type = file.Type,
                            IdSezione = file.IdSezione.Value,
                            IdUserUpdate = file.IdUserUpdate
                        });
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileDownload", null, ex);
                throw;
            }
        }

        public bool LoadFileDownload(List<Download> att, int idSezione)
        {
            try
            {
                _logger.LogInfo("LoadFileDownload", null, "Entra nel try");
                using (var context = new SDMEntities())
                {
                    _logger.LogInfo("LoadFileDownload", null, "Entra nel using");
                    foreach (var item in att)
                    {
                        _logger.LogInfo("LoadFileDownload", null, "Faccio add file");

                        context.DownloadFile.Add(new DownloadFile
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdSezione = item.IdSezione,
                            LastUpdate = item.LastUpdate,
                        });
                        _logger.LogInfo("LoadFileDownload", null, "Finisco add file");
                    }

                    _logger.LogInfo("LoadFileDownload", null, "Faccio save");
                    var result = context.SaveChanges() > 0;

                    if (result && idSezione == 9)
                    {
                        List<string> emails = new List<string>();

                        var users = context.Users.Where(x => x.Username != "amministrazione"
                                                            && x.Username != "assistenza"
                                                            && x.Username != "pratiche"
                                                            && x.Email != null).ToList();

                        foreach (var user in users)
                        {
                            emails.Add(user.Email);
                        }

                        _mail.SendMail(emails);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("LoadFileDownload", null, "Save non riuscito" + ex.Message + ex.InnerException.Message);
                _logger.LogWrite("LoadFileDownload", null, ex);
                throw;
            }
        }

        public Download DownloadFileDownload(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    DownloadFile att = context.DownloadFile.FirstOrDefault(i => i.Id == idFile);

                    Download result = new Download
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdSezione = att.IdSezione.Value,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileDownload", null, ex);
                throw;
            }
        }

        public bool DeleteFileDownload(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.DownloadFile.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.DownloadFile.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DeleteFileDownload", null, ex);
                throw;
            }
        }
        #endregion

        #region Generic Methods Pratiche
        
        public List<T> GetPratiche<T>(string logMessage) where T : class
        {
            try
            {
                return GetAll<T>();
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public T GetPratica<T>(int idPratica, string logMessage) where T : class
        {
            try
            {
               return GetById<T>(idPratica);                
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public bool SalvaPratica<T>(T item, string tipoPratica, string mailTos, string mailSubject, Pratica mailPratica, string logMessage) where T : class
        {
            try
            {
                var result = Add<T>(item, tipoPratica);
                if (result) _mail.SendMail(mailPratica, tipoPratica, mailTos, mailSubject);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public bool ModificaPratica<T>(T item, string tipoPratica, string mailTos, string mailSubject, Pratica mailPratica, string logMessage) where T : class
        {
            try
            {
                var result = Update(item);
                if (result)
                {
                    using (var context = new SDMEntities())
                    {
                        Users userFrom = GetById<Users>(mailPratica.IdUserUpdate.Value);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == mailPratica.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminCasoria")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email)) _mail.SendMail(mailPratica, tipoPratica, email, mailSubject);
                                    }
                                }
                                else _mail.SendMail(mailPratica, tipoPratica, mailTos, mailSubject);
                            }

                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public List<T> GetFile<T>(int idPratica, string logMessage) where T : class
        {
            try
            {
                return GetAll<T>();
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public bool LoadFile<T>(List<T> items, string logMessage) where T : class
        {
            try
            {
                return AddRange<T>(items);
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public T DownloadFile<T>(int idFile, string logMessage) where T : class
        {
            try
            {
                return GetById<T>(idFile);
            }
            catch (Exception ex)
            {
                _logger.LogWrite(logMessage, null, ex);
                throw;
            }
        }

        public string GetNextNumeroPratica(string numeroPratica, string tipoPratica)
        {
            using (var context = new SDMEntities())
            {
                var result = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == tipoPratica)?.NumPratica.ToString();
                return numeroPratica + result;
            }
        }

        #endregion

        #region Generic Methods DB
        public List<T> GetAll<T>() where T : class
        {
            using (var context = new SDMEntities())
            {
                return context.Set<T>().ToList();
            }
        }

        public T GetById<T>(int id) where T : class
        {
            using (var context = new SDMEntities())
            {
                return context.Set<T>().Find(id);
            }
        }

        public bool Add<T>(T newItem, string tipoPratica = null) where T : class
        {
            using (var context = new SDMEntities())
            {
                context.Set<T>().Add(newItem);
                bool result = context.SaveChanges() > 0;
                if (result && tipoPratica != null)
                {
                    var numeroPratica = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == tipoPratica);
                    numeroPratica.NumPratica += 1;
                    context.SaveChanges();
                }
                return result;
            }
        }

        public bool AddRange<T>(List<T> newItem) where T : class
        {
            using (var context = new SDMEntities())
            {
                context.Set<T>().AddRange(newItem);
                bool result = context.SaveChanges() > 0;
                return result;
            }
        }

        public bool Update<T>(T item) where T : class
        {
            using (var context = new SDMEntities())
            {
                context.Set<T>().Add(item);
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }
        #endregion

        #region Mapper

            #region StudioProfessionale
            private StudioProfessionale MapPraticaToNewStudioProfessionale(Pratica item)
            {
                return new StudioProfessionale
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private StudioProfessionale MapPraticaToStudioProfessionale(StudioProfessionale prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapStudioProfessionaleToPraticaMail(StudioProfessionale item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListStudioProfessionaleToPratica(List<StudioProfessionale> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListStudioProfessionaleToPraticaFiltered(List<StudioProfessionale> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapStudioProfessionaleToPratica(StudioProfessionale item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsStudioProfessionaleToAttachments(AttachmentsStudioProfessionale item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsStudioProfessionaleToAttachments(List<AttachmentsStudioProfessionale> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsStudioProfessionale> MapListAttachmentsToAttachmentsStudioProfessionale(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsStudioProfessionale> result = new List<AttachmentsStudioProfessionale>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsStudioProfessionale
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
            #endregion

            #region Sindacato
            private Sindacato MapPraticaToNewSindacato(Pratica item)
            {
                return new Sindacato
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Sindacato MapPraticaToSindacato(Sindacato prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapSindacatoToPraticaMail(Sindacato item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListSindacatoToPratica(List<Sindacato> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListSindacatoToPraticaFiltered(List<Sindacato> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapSindacatoToPratica(Sindacato item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsSindacatoToAttachments(AttachmentsSindacato item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsSindacatoToAttachments(List<AttachmentsSindacato> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsSindacato> MapListAttachmentsToAttachmentsSindacato(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsSindacato> result = new List<AttachmentsSindacato>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsSindacato
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
            #endregion

            #region Patronato
            private Patronato MapPraticaToNewPatronato(Pratica item)
            {
                return new Patronato
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Patronato MapPraticaToPatronato(Patronato prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapPatronatoToPraticaMail(Patronato item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListPatronatoToPratica(List<Patronato> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListPatronatoToPraticaFiltered(List<Patronato> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapPatronatoToPratica(Patronato item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsPatronatoToAttachments(AttachmentsPatronato item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsPatronatoToAttachments(List<AttachmentsPatronato> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsPatronato> MapListAttachmentsToAttachmentsPatronato(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsPatronato> result = new List<AttachmentsPatronato>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsPatronato
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
            #endregion
        
            #region Eventi
            private Eventi MapPraticaToNewEventi(Pratica item)
            {
                return new Eventi
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Eventi MapPraticaToEventi(Eventi prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapEventiToPraticaMail(Eventi item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListEventiToPratica(List<Eventi> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListEventiToPraticaFiltered(List<Eventi> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapEventiToPratica(Eventi item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsEventiToAttachments(AttachmentsEventi item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsEventiToAttachments(List<AttachmentsEventi> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsEventi> MapListAttachmentsToAttachmentsEventi(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsEventi> result = new List<AttachmentsEventi>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsEventi
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
        #endregion

            #region Agenzia
            private Agenzia MapPraticaToNewAgenzia(Pratica item)
            {
                return new Agenzia
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Agenzia MapPraticaToAgenzia(Agenzia prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapAgenziaToPraticaMail(Agenzia item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListAgenziaToPratica(List<Agenzia> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListAgenziaToPraticaFiltered(List<Agenzia> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapAgenziaToPratica(Agenzia item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsAgenziaToAttachments(AttachmentsAgenzia item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsAgenziaToAttachments(List<AttachmentsAgenzia> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsAgenzia> MapListAttachmentsToAttachmentsAgenzia(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsAgenzia> result = new List<AttachmentsAgenzia>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsAgenzia
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
        #endregion

            #region PraticheAuto
            private PraticheAuto MapPraticaToNewPraticheAuto(Pratica item)
            {
                return new PraticheAuto
                {
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private PraticheAuto MapPraticaToPraticheAuto(PraticheAuto prev, Pratica item)
            {
                prev.Nome = item.Nome;
                prev.Cognome = item.Cognome;
                prev.Sottocategoria = item.Sottocategoria;
                prev.Anno = item.Anno;
                prev.LastUpdate = item.LastUpdate;
                prev.IdUserUpdate = item.IdUserUpdate;
                //newPratica.IdSede = pratica.IdSede;
                prev.IdStato = item.IdStato;
                prev.Note = item.Note;
                prev.TipologiaPratica = item.TipologiaPratica;

                return prev;
            }

            private Pratica MapPraticheAutoToPraticaMail(PraticheAuto item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Sottocategoria = item.Sottocategoria,
                    Anno = item.Anno,
                    LastUpdate = item.LastUpdate,
                    IdUserUpdate = item.IdUserUpdate,
                    NumPratica = item.NumPratica,
                    IdSede = item.IdSede,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private List<Pratica> MapListPraticheAutoToPratica(List<PraticheAuto> items)
            {
                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in items)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private List<Pratica> MapListPraticheAutoToPraticaFiltered(List<PraticheAuto> items, Pratica criteri)
            {
                var temp = items.Where(i =>
                            (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                            && (criteri.Anno == null || i.Anno == criteri.Anno)
                            && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                            && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                            && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                            && (criteri.IdSede == null || i.IdSede == criteri.IdSede)
                            )
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Pratica> result = new List<Pratica>();
                foreach (var pratica in temp)
                {
                    result.Add(new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = pratica.Sottocategoria,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    });
                }
                return result;
            }

            private Pratica MapPraticheAutoToPratica(PraticheAuto item)
            {
                return new Pratica
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Cognome = item.Cognome,
                    Anno = item.Anno,
                    Sottocategoria = item.Sottocategoria,
                    IdUserUpdate = item.Id,
                    IdSede = item.IdSede,
                    NumPratica = item.NumPratica,
                    LastUpdate = item.LastUpdate,
                    IdStato = item.IdStato,
                    Note = item.Note,
                    TipologiaPratica = item.TipologiaPratica
                };
            }

            private Attachment MapAttachmentsPraticheAutoToAttachments(AttachmentsPraticheAuto item)
            {
                return new Attachment
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Blob = item.Blob,
                    LastUpdate = item.LastUpdate,
                    Type = item.Type,
                    IdPratica = item.IdPratica,
                    IdUserUpdate = item.IdUserUpdate
                };
            }

            private List<Attachment> MapListAttachmentsPraticheAutoToAttachments(List<AttachmentsPraticheAuto> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<Attachment> result = new List<Attachment>();
                foreach (var file in temp)
                {
                    result.Add(new Attachment
                    {
                        Id = file.Id,
                        Nome = file.Nome,
                        Blob = file.Blob,
                        LastUpdate = file.LastUpdate,
                        Type = file.Type,
                        IdPratica = file.IdPratica,
                        IdUserUpdate = file.IdUserUpdate
                    });
                }

                return result;
            }

            private List<AttachmentsPraticheAuto> MapListAttachmentsToAttachmentsPraticheAuto(List<Attachment> items)
            {
                var temp = items
                            .OrderByDescending(i => i.LastUpdate)
                            .ToList();

                List<AttachmentsPraticheAuto> result = new List<AttachmentsPraticheAuto>();
                foreach (var file in temp)
                {
                    result.Add(new AttachmentsPraticheAuto
                    {
                        Nome = file.Nome,
                        Blob = file.Blob,
                        Type = file.Type,
                        IdUserUpdate = file.IdUserUpdate,
                        IdPratica = file.IdPratica,
                        LastUpdate = file.LastUpdate
                    });
                }

                return result;
            }
            #endregion

        #endregion
    }
}