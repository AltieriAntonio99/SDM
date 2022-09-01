using System;
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
                    var result = context.Categorie.Where(i => i.Categoria == type).OrderBy(i => i.Nome).ToList();

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

                    if (type == "Sindacato")
                    {
                        riepilogoPratica.pratica = GetPraticaSindacato(id);
                        riepilogoPratica.attachment = GetFileSindacato(id);
                        riepilogoPratica.type = "Sindacato";
                    }
                    if (type == "Patronato")
                    {
                        riepilogoPratica.pratica = GetPraticaPatronato(id);
                        riepilogoPratica.attachment = GetFilePatronato(id);
                        riepilogoPratica.type = "Patronato";
                    }
                    else if (type == "Noleggio")
                    {
                        riepilogoPratica.pratica = GetPraticaNoleggio(id);
                        riepilogoPratica.attachment = GetFileNoleggio(id);
                        riepilogoPratica.type = "Noleggio";
                    }
                    else if (type == "Credito")
                    {
                        riepilogoPratica.pratica = GetPraticaCredito(id);
                        riepilogoPratica.attachment = GetFileCredito(id);
                    }
                    else if (type == "Eventi")
                    {
                        riepilogoPratica.pratica = GetPraticaEventi(id);
                        riepilogoPratica.attachment = GetFileEventi(id);
                        riepilogoPratica.type = "Eventi";
                    }
                    else if (type == "AssistenzaContabile")
                    {
                        riepilogoPratica.pratica = GetPraticaAssistenzaContabile(id);
                        riepilogoPratica.attachment = GetFileAssistenzaContabile(id);
                        riepilogoPratica.type = "AssistenzaContabile";
                    }
                    else if (type == "Archivio")
                    {
                        riepilogoPratica.pratica = GetPraticaArchivio(id);
                        riepilogoPratica.attachment = GetFileArchivio(id);
                        riepilogoPratica.type = "Archivio";
                    }
                    else if (type == "Agenzia")
                    {
                        riepilogoPratica.pratica = GetPraticaAgenzia(id);
                        riepilogoPratica.attachment = GetFileAgenzia(id);
                        riepilogoPratica.type = "Agenzia";
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

        #region Sindacato
        public bool SalvaPraticaSindacato(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "sindacato");
                    Sindacato newPratica = new Sindacato
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Sindacato.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "Sindacato", "segreteria@sdmservices.it", "Pratica Sindacato");  //segreteria@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaSindacato", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaSindacato(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Sindacato newPratica = context.Sindacato.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria" 
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto" 
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob" 
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Sindacato", email, "Pratica Sindacato");
                                        }
                                    }
                                }
                                else
                                {
                                    _mail.SendMail(praticaMail, "Sindacato", "segreteria@sdmservices.it", "Pratica Sindacato");
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaSindacato", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheSindacato(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Sindacato> pratiche = new List<Sindacato>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                     || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                     || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                     || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                     || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                     || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Sindacato.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Sindacato.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheSindacato", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaSindacato(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Sindacato pratica = context.Sindacato.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaSindacato", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaSindacato(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Sindacato> pratiche = context.Sindacato.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede)).OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaSindacato", null, ex);
                throw;
            }
        }

        public bool DelateSindacato(int id)
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
                _logger.LogWrite("DelateSindacato", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileSindacato(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsSindacato> files = context.AttachmentsSindacato.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileSindacato", null, ex);
                throw;
            }
        }

        public bool LoadFileSindacato(List<Attachment> att)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    foreach (var item in att)
                    {
                        context.AttachmentsSindacato.Add(new AttachmentsSindacato
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("LoadFileSindacato", null, ex);
                throw;
            }
        }

        public bool DelateFileSindacato(int id)
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
                _logger.LogWrite("DelateFileSindacato", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileSindacato(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsSindacato att = context.AttachmentsSindacato.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileSindacato", null, ex);
                throw;
            }
        }
        #endregion

        #region Patronato
        public bool SalvaPraticaPatronato(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "patronato");
                    Patronato newPratica = new Patronato
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Patronato.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        var getSedeUser = context.Sedi.SingleOrDefault(x => x.Id == newPratica.IdSede);

                        if (getSedeUser != null && getSedeUser.Nome.Contains("SDM Services Casavatore"))
                        {
                            //_mail.SendMail(praticaMail, "Patronato", "amministrazione@sdmservices.it", "Pratica Patronato");
                        }
                        else
                        {
                            _mail.SendMail(praticaMail, "Patronato", "assistenza@sdmservices.it", "Pratica Patronato");
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaPatronato", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaPatronato(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Patronato newPratica = context.Patronato.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Patronato", email, "Pratica Patronato");  //supporto@
                                        }
                                    }
                                }
                                else
                                {
                                    var getSedeUser = context.Sedi.SingleOrDefault(x => x.Id == newPratica.IdSede);

                                    if (getSedeUser != null && getSedeUser.Nome.Contains("SDM Services Casavatore"))
                                    {
                                        //_mail.SendMail(praticaMail, "Patronato", "amministrazione@sdmservices.it", "Pratica Patronato");
                                    }
                                    else
                                    {
                                        _mail.SendMail(praticaMail, "Patronato", "supporto@sdmservices.it", "Pratica Patronato");  //supporto@
                                    }
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaPatronato", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPratichePatronato(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Patronato> pratiche = new List<Patronato>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Patronato.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Patronato.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPratichePatronato", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaPatronato(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Patronato pratica = context.Patronato.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaPatronato", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaPatronato(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Patronato> pratiche = context.Patronato.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede))
                                                    .OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaPatronato", null, ex);
                throw;
            }
        }

        public bool DelatePatronato(int id)
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
                _logger.LogWrite("DelatePatronato", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFilePatronato(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsPatronato> files = context.AttachmentsPatronato.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFilePatronato", null, ex);
                throw;
            }
        }

        public bool LoadFilePatronato(List<Attachment> att)
        {
            try
            {
                _logger.LogInfo("LoadFilePatronato", null, "Entra nel try");
                using (var context = new SDMEntities())
                {
                    _logger.LogInfo("LoadFilePatronato", null, "Entra nel using");
                    foreach (var item in att)
                    {
                        _logger.LogInfo("LoadFilePatronato", null, "Faccio add file");
                        context.AttachmentsPatronato.Add(new AttachmentsPatronato
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                        _logger.LogInfo("LoadFilePatronato", null, "Finisco add file");
                    }

                    _logger.LogInfo("LoadFilePatronato", null, "Faccio save");
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("LoadFilePatronato", null, "Save non riuscito" + ex.Message + ex.InnerException.Message);
                _logger.LogWrite("LoadFilePatronato", null, ex);
                throw;
            }
        }

        public bool DelateFilePatronato(int id)
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
                _logger.LogWrite("DelateFilePatronato", null, ex);
                throw;
            }
        }

        public Attachment DownloadFilePatronato(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsPatronato att = context.AttachmentsPatronato.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFilePatronato", null, ex);
                throw;
            }
        }
        #endregion

        #region Credito
        public bool SalvaPraticaCredito(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "credito");
                    Credito newPratica = new Credito
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Credito.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "Credito", "documenti@sdmservices.it", "Pratica Credito e Assicurazioni");   //documenti@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaCredito", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaCredito(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Credito newPratica = context.Credito.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Credito", email, "Pratica Credito e Assicurazioni");   //documenti@
                                        }
                                    }
                                }
                                else
                                {
                                    _mail.SendMail(praticaMail, "Credito", "documenti@sdmservices.it", "Pratica Credito e Assicurazioni");   //documenti@
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaCredito", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheCredito(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Credito> pratiche = new List<Credito>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Credito.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Credito.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheCredito", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaCredito(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Credito pratica = context.Credito.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaCredito", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaCredito(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Credito> pratiche = context.Credito.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede)).OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaCredito", null, ex);
                throw;
            }
        }

        public bool DelateCredito(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Credito.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsCredito.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsCredito.Remove(item);
                            }
                        }

                        context.Credito.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateCredito", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileCredito(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsCredito> files = context.AttachmentsCredito.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileCredito", null, ex);
                throw;
            }
        }

        public bool LoadFileCredito(List<Attachment> att)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    foreach (var item in att)
                    {
                        context.AttachmentsCredito.Add(new AttachmentsCredito
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("LoadFileCredito", null, ex);
                throw;
            }
        }

        public bool DelateFileCredito(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsCredito.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsCredito.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateFileCredito", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileCredito(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsCredito att = context.AttachmentsCredito.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileCredito", null, ex);
                throw;
            }
        }
        #endregion

        #region Noleggio
        public bool SalvaPraticaNoleggio(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "noleggio");
                    Noleggio newPratica = new Noleggio
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Noleggio.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "Noleggio", "documenti@sdmservices.it", "Pratica Noleggio");        //documenti@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaNoleggio", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaNoleggio(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Noleggio newPratica = context.Noleggio.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Noleggio", email, "Pratica Noleggio");        //documenti@
                                        }
                                    }
                                }
                                else
                                {
                                    _mail.SendMail(praticaMail, "Noleggio", "documenti@sdmservices.it", "Pratica Noleggio");        //documenti@
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaNoleggio", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheNoleggio(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Noleggio> pratiche = new List<Noleggio>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Noleggio.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Noleggio.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheNoleggio", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaNoleggio(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Noleggio pratica = context.Noleggio.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaNoleggio", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaNoleggio(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Noleggio> pratiche = context.Noleggio.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede)).OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaNoleggio", null, ex);
                throw;
            }
        }

        public bool DelateNoleggio(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Noleggio.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsNoleggio.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsNoleggio.Remove(item);
                            }
                        }

                        context.Noleggio.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateNoleggio", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileNoleggio(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsNoleggio> files = context.AttachmentsNoleggio.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileNoleggio", null, ex);
                throw;
            }
        }

        public bool LoadFileNoleggio(List<Attachment> att)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    foreach (var item in att)
                    {
                        context.AttachmentsNoleggio.Add(new AttachmentsNoleggio
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("LoadFileNoleggio", null, ex);
                throw;
            }
        }

        public bool DelateFileNoleggio(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsNoleggio.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsNoleggio.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateFileNoleggio", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileNoleggio(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsNoleggio att = context.AttachmentsNoleggio.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileNoleggio", null, ex);
                throw;
            }
        }
        #endregion

        #region Eventi
        public bool SalvaPraticaEventi(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "eventi");
                    Eventi newPratica = new Eventi
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = null,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Eventi.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = null,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        //_mail.SendMail(praticaMail, "Eventi", "amministrazione@sdmservices.it", "Pratica Eventi");      amministrazione@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaEventi", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaEventi(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Eventi newPratica = context.Eventi.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = null;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Eventi", email, "Pratica Eventi");
                                        }
                                    }
                                }
                                else
                                {
                                    //_mail.SendMail(praticaMail, "Eventi", "amministrazione@sdmservices.it", "Pratica Eventi");      amministrazione@
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaEventi", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheEventi(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Eventi> pratiche = new List<Eventi>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Eventi.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Eventi.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
                    {
                        result.Add(new Pratica
                        {
                            Id = pratica.Id,
                            Nome = pratica.Nome,
                            Cognome = pratica.Cognome,
                            Anno = pratica.Anno,
                            Sottocategoria = null,
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheEventi", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaEventi(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Eventi pratica = context.Eventi.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = null,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaEventi", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaEventi(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Eventi> pratiche = context.Eventi.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede)).OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
                    {
                        result.Add(new Pratica
                        {
                            Id = pratica.Id,
                            Nome = pratica.Nome,
                            Cognome = pratica.Cognome,
                            Anno = pratica.Anno,
                            Sottocategoria = null,
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaEventi", null, ex);
                throw;
            }
        }

        public bool DelateEventi(int id)
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
                _logger.LogWrite("DelateEventi", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileEventi(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsEventi> files = context.AttachmentsEventi.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileEventi", null, ex);
                throw;
            }
        }

        public bool LoadFileEventi(List<Attachment> att)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    foreach (var item in att)
                    {
                        context.AttachmentsEventi.Add(new AttachmentsEventi
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("LoadFileEventi", null, ex);
                throw;
            }
        }

        public bool DelateFileEventi(int id)
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
                _logger.LogWrite("DelateFileEventi", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileEventi(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsEventi att = context.AttachmentsEventi.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileEventi", null, ex);
                throw;
            }
        }
        #endregion

        #region AssistenzaContabile
        public bool SalvaPraticaAssistenzaContabile(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "assistenza_legale");
                    AssistenzaContabile newPratica = new AssistenzaContabile
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = null,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.AssistenzaContabile.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = null,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "AssistenzaContabile", "documenti@sdmservices.it", "Pratica Assistenza Contabile");       //documenti@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaAssistenzaContabile", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaAssistenzaContabile(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AssistenzaContabile newPratica = context.AssistenzaContabile.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = null;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "AssistenzaContabile", email, "Pratica Assistenza Contabile");
                                        }
                                    }
                                }
                                else
                                {
                                    _mail.SendMail(praticaMail, "AssistenzaContabile", "assistenza@sdmservices.it", "Pratica Assistenza Contabile");       //assistenza@
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaAssistenzaContabile", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheAssistenzaContabile(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AssistenzaContabile> pratiche = new List<AssistenzaContabile>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.AssistenzaContabile.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.AssistenzaContabile.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
                    {
                        result.Add(new Pratica
                        {
                            Id = pratica.Id,
                            Nome = pratica.Nome,
                            Cognome = pratica.Cognome,
                            Anno = pratica.Anno,
                            Sottocategoria = null,
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheAssistenzaContabile", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaAssistenzaContabile(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AssistenzaContabile pratica = context.AssistenzaContabile.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
                    {
                        Id = pratica.Id,
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Anno = pratica.Anno,
                        Sottocategoria = null,
                        IdUserUpdate = pratica.Id,
                        IdSede = pratica.IdSede,
                        NumPratica = pratica.NumPratica,
                        LastUpdate = pratica.LastUpdate,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaAssistenzaContabile", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaAssistenzaContabile(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AssistenzaContabile> pratiche = context.AssistenzaContabile.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede)).OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
                    {
                        result.Add(new Pratica
                        {
                            Id = pratica.Id,
                            Nome = pratica.Nome,
                            Cognome = pratica.Cognome,
                            Anno = pratica.Anno,
                            Sottocategoria = null,
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaAssistenzaContabile", null, ex);
                throw;
            }
        }

        public bool DelateAssistenzaContabile(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AssistenzaContabile.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsAssistenzaContabile.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsAssistenzaContabile.Remove(item);
                            }
                        }

                        context.AssistenzaContabile.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateAssistenzaContabile", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileAssistenzaContabile(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsAssistenzaContabile> files = context.AttachmentsAssistenzaContabile.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileAssistenzaContabile", null, ex);
                throw;
            }
        }

        public bool LoadFileAssistenzaContabile(List<Attachment> att)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    foreach (var item in att)
                    {
                        context.AttachmentsAssistenzaContabile.Add(new AttachmentsAssistenzaContabile
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                    }

                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("LoadFileAssistenzaContabile", null, ex);
                throw;
            }
        }

        public bool DelateFileAssistenzaContabile(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsAssistenzaContabile.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsAssistenzaContabile.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateFileAssistenzaContabile", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileAssistenzaContabile(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsAssistenzaContabile att = context.AttachmentsAssistenzaContabile.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileAssistenzaContabile", null, ex);
                throw;
            }
        }
        #endregion

        #region Archivio
        public bool SalvaPraticaArchivio(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "archivio");
                    Archivio newPratica = new Archivio
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Archivio.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "Archivio", "info@sdmservices.it", "Pratica Archivio");  //info@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaArchivio", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaArchivio(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Archivio newPratica = context.Archivio.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Archivio", email, "Pratica Archivio");
                                        }
                                    }
                                }
                                else
                                {
                                    var getSedeUser = context.Sedi.SingleOrDefault(x => x.Id == newPratica.IdSede);
                                    
                                    _mail.SendMail(praticaMail, "Archivio", "info@sdmservices.it", "Pratica Archivio");  //info@                                    
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaArchivio", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheArchivio(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Archivio> pratiche = new List<Archivio>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria" 
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto" 
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob" 
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Archivio.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Archivio.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheArchivio", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaArchivio(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Archivio pratica = context.Archivio.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaArchivio", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaArchivio(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Archivio> pratiche = context.Archivio.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede))
                                                    .OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaArchivio", null, ex);
                throw;
            }
        }

        public bool DelateArchivio(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.Archivio.SingleOrDefault(x => x.Id == id);
                    var itemToRemoveAttachment = itemToRemove.AttachmentsArchivio.ToList();

                    if (itemToRemove != null)
                    {
                        if (itemToRemoveAttachment != null)
                        {
                            foreach (var item in itemToRemoveAttachment)
                            {
                                context.AttachmentsArchivio.Remove(item);
                            }
                        }

                        context.Archivio.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateArchivio", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileArchivio(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsArchivio> files = context.AttachmentsArchivio.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileArchivio", null, ex);
                throw;
            }
        }

        public bool LoadFileArchivio(List<Attachment> att)
        {
            try
            {
                _logger.LogInfo("LoadFileArchivio", null, "Entra nel try");
                using (var context = new SDMEntities())
                {
                    _logger.LogInfo("LoadFileArchivio", null, "Entra nel using");
                    foreach (var item in att)
                    {
                        _logger.LogInfo("LoadFileArchivio", null, "Faccio add file");

                        context.AttachmentsArchivio.Add(new AttachmentsArchivio
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                        _logger.LogInfo("LoadFileArchivio", null, "Finisco add file");
                    }

                    _logger.LogInfo("LoadFileArchivio", null, "Faccio save");
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("LoadFileArchivio", null, "Save non riuscito" + ex.Message + ex.InnerException.Message);
                _logger.LogWrite("LoadFileArchivio", null, ex);
                throw;
            }
        }

        public bool DelateFileArchivio(int id)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var itemToRemove = context.AttachmentsArchivio.SingleOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        context.AttachmentsArchivio.Remove(itemToRemove);
                        return context.SaveChanges() > 0;
                    }

                    return false; ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DelateFileArchivio", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileArchivio(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsArchivio att = context.AttachmentsArchivio.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileArchivio", null, ex);
                throw;
            }
        }
        #endregion

        #region Agenzia
        public bool SalvaPraticaAgenzia(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    var numPraticaAut = context.NumeroPratiche.SingleOrDefault(i => i.TipoPratica == "agenzia");
                    Agenzia newPratica = new Agenzia
                    {
                        Nome = pratica.Nome,
                        Cognome = pratica.Cognome,
                        Sottocategoria = pratica.Sottocategoria,
                        Anno = pratica.Anno,
                        LastUpdate = pratica.LastUpdate,
                        IdUserUpdate = pratica.IdUserUpdate,
                        NumPratica = pratica.NumPratica + numPraticaAut.NumPratica.ToString(),
                        IdSede = pratica.IdSede,
                        IdStato = pratica.IdStato,
                        Note = pratica.Note,
                        TipologiaPratica = pratica.TipologiaPratica
                    };

                    context.Agenzia.Add(newPratica);

                    var result = context.SaveChanges() > 0;

                    if (result)
                    {
                        numPraticaAut.NumPratica += 1;
                        context.SaveChanges();

                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        _mail.SendMail(praticaMail, "Agenzia", "amministrazione@sdmservices.it", "Pratica Agenzia");  //amministrazione@
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("SalvaPraticaAgenzia", null, ex);
                throw;
            }
        }

        public bool ModificaPraticaAgenzia(Pratica pratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Agenzia newPratica = context.Agenzia.SingleOrDefault(i => i.Id == pratica.Id);

                    if (newPratica != null)
                    {
                        newPratica.Nome = pratica.Nome;
                        newPratica.Cognome = pratica.Cognome;
                        newPratica.Sottocategoria = pratica.Sottocategoria;
                        newPratica.Anno = pratica.Anno;
                        newPratica.LastUpdate = pratica.LastUpdate;
                        newPratica.IdUserUpdate = pratica.IdUserUpdate;
                        //newPratica.IdSede = pratica.IdSede;
                        newPratica.IdStato = pratica.IdStato;
                        newPratica.Note = pratica.Note;
                        newPratica.TipologiaPratica = pratica.TipologiaPratica;
                    }

                    if (context.SaveChanges() > 0)
                    {
                        Pratica praticaMail = new Pratica
                        {
                            Id = newPratica.Id,
                            Nome = newPratica.Nome,
                            Cognome = newPratica.Cognome,
                            Sottocategoria = newPratica.Sottocategoria,
                            Anno = newPratica.Anno,
                            LastUpdate = newPratica.LastUpdate,
                            IdUserUpdate = newPratica.IdUserUpdate,
                            NumPratica = newPratica.NumPratica,
                            IdSede = newPratica.IdSede,
                            IdStato = newPratica.IdStato,
                            Note = newPratica.Note,
                            TipologiaPratica = newPratica.TipologiaPratica
                        };

                        Users userFrom = context.Users.FirstOrDefault(x => x.Id == praticaMail.IdUserUpdate);

                        if (userFrom != null)
                        {
                            List<Users> userFromList = context.Users.Where(x => x.IdSede == praticaMail.IdSede && x.Roles.Ruolo != "admin" && x.Roles.Ruolo != "adminArchivioNoSmartJob"
                                    && x.Roles.Ruolo != "adminNoSmartJob" && x.Roles.Ruolo != "adminCasoria"
                                    && x.Roles.Ruolo != "adminSegreteria" && x.Roles.Ruolo != "adminSupporto"
                                    && x.Roles.Ruolo != "adminDocumenti" && x.Roles.Ruolo != "adminArchivio"
                                    && x.Roles.Ruolo != "adminCasoriaNoSmartJob" && x.Roles.Ruolo != "adminSegreteriaNoSmartJob"
                                    && x.Roles.Ruolo != "adminSupportoNoSmartJob" && x.Roles.Ruolo != "adminDocumentiNoSmartJob").ToList();

                            string ruolo = userFrom.Roles?.Ruolo;
                            if (!string.IsNullOrWhiteSpace(ruolo))
                            {
                                if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                                {
                                    if (userFromList.Count > 0)
                                    {
                                        string email = userFromList.FirstOrDefault().Email;
                                        if (!string.IsNullOrWhiteSpace(email))
                                        {
                                            _mail.SendMail(praticaMail, "Agenzia", email, "Pratica Agenzia");  //supporto@
                                        }
                                    }
                                }
                                else
                                {
                                    var getSedeUser = context.Sedi.SingleOrDefault(x => x.Id == newPratica.IdSede);

                                    _mail.SendMail(praticaMail, "Agenzia", "amministrazione@sdmservices.it", "Pratica Agenzia");  //amministrazione@
                                }
                            }

                        }

                        return true;
                    }
                    else { return false; }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("ModificaPraticaAgenzia", null, ex);
                throw;
            }
        }

        public List<Pratica> GetPraticheAgenzia(int idSede, string ruolo)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<Agenzia> pratiche = new List<Agenzia>();
                    if (ruolo == "admin" || ruolo == "adminArchivioNoSmartJob"
                                    || ruolo == "adminNoSmartJob" || ruolo == "adminCasoria"
                                    || ruolo == "adminSegreteria" || ruolo == "adminSupporto"
                                    || ruolo == "adminDocumenti" || ruolo == "adminArchivio"
                                    || ruolo == "adminCasoriaNoSmartJob" || ruolo == "adminSegreteriaNoSmartJob"
                                    || ruolo == "adminSupportoNoSmartJob" || ruolo == "adminDocumentiNoSmartJob")
                    {
                        pratiche = context.Agenzia.OrderByDescending(i => i.LastUpdate).ToList();
                    }
                    else
                    {
                        pratiche = context.Agenzia.Where(i => i.IdSede == idSede).OrderByDescending(i => i.LastUpdate).ToList();
                    }

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticheAgenzia", null, ex);
                throw;
            }
        }

        public Pratica GetPraticaAgenzia(int idPratia)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Agenzia pratica = context.Agenzia.SingleOrDefault(i => i.Id == idPratia);
                    Pratica result = new Pratica
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
                    };
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetPraticaAgenzia", null, ex);
                throw;
            }
        }

        public List<Pratica> RicercaAgenzia(Pratica criteri, string role)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    if (role == "admin" || role == "adminArchivioNoSmartJob"
                                    || role == "adminNoSmartJob" || role == "adminCasoria"
                                    || role == "adminSegreteria" || role == "adminSupporto"
                                    || role == "adminDocumenti" || role == "adminArchivio"
                                    || role == "adminCasoriaNoSmartJob" || role == "adminSegreteriaNoSmartJob"
                                    || role == "adminSupportoNoSmartJob" || role == "adminDocumentiNoSmartJob") { criteri.IdSede = null; }
                    List<Agenzia> pratiche = context.Agenzia.Where(i => (criteri.NumPratica == null || i.NumPratica.Contains(criteri.NumPratica))
                                                    && (criteri.Anno == null || i.Anno == criteri.Anno)
                                                    && (criteri.Nome == null || i.Nome.Contains(criteri.Nome))
                                                    && (criteri.Cognome == null || i.Cognome.Contains(criteri.Cognome))
                                                    && (criteri.Sottocategoria == null || i.Sottocategoria == criteri.Sottocategoria)
                                                    && (criteri.IdSede == null || i.IdSede == criteri.IdSede))
                                                    .OrderBy(i => i.LastUpdate).ToList();

                    List<Pratica> result = new List<Pratica>();
                    foreach (var pratica in pratiche)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("RicercaAgenzia", null, ex);
                throw;
            }
        }

        public bool DelateAgenzia(int id)
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
                _logger.LogWrite("DelateAgenzia", null, ex);
                throw;
            }
        }

        public List<Attachment> GetFileAgenzia(int idPratica)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    List<AttachmentsAgenzia> files = context.AttachmentsAgenzia.Where(i => i.IdPratica == idPratica).OrderByDescending(i => i.LastUpdate).ToList();

                    List<Attachment> result = new List<Attachment>();
                    foreach (var file in files)
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
            }
            catch (Exception ex)
            {
                _logger.LogWrite("GetFileAgenzia", null, ex);
                throw;
            }
        }

        public bool LoadFileAgenzia(List<Attachment> att)
        {
            try
            {
                _logger.LogInfo("LoadFileAgenzia", null, "Entra nel try");
                using (var context = new SDMEntities())
                {
                    _logger.LogInfo("LoadFileAgenzia", null, "Entra nel using");
                    foreach (var item in att)
                    {
                        _logger.LogInfo("LoadFileAgenzia", null, "Faccio add file");

                        context.AttachmentsAgenzia.Add(new AttachmentsAgenzia
                        {
                            Nome = item.Nome,
                            Blob = item.Blob,
                            Type = item.Type,
                            IdUserUpdate = item.IdUserUpdate,
                            IdPratica = item.IdPratica,
                            LastUpdate = item.LastUpdate,
                        });
                        _logger.LogInfo("LoadFileAgenzia", null, "Finisco add file");
                    }

                    _logger.LogInfo("LoadFileAgenzia", null, "Faccio save");
                    return context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInfo("LoadFileAgenzia", null, "Save non riuscito" + ex.Message + ex.InnerException.Message);
                _logger.LogWrite("LoadFileAgenzia", null, ex);
                throw;
            }
        }

        public bool DelateFileAgenzia(int id)
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
                _logger.LogWrite("DelateFileAgenzia", null, ex);
                throw;
            }
        }

        public Attachment DownloadFileAgenzia(int idFile)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    AttachmentsAgenzia att = context.AttachmentsAgenzia.FirstOrDefault(i => i.Id == idFile);

                    Attachment result = new Attachment
                    {
                        Id = att.Id,
                        Nome = att.Nome,
                        Blob = att.Blob,
                        Type = att.Type,
                        IdUserUpdate = att.IdUserUpdate,
                        IdPratica = att.IdPratica,
                        LastUpdate = att.LastUpdate,
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWrite("DownloadFileAgenzia", null, ex);
                throw;
            }
        }



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

        public bool DelateFileDownload(int id)
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
                _logger.LogWrite("DelateFileDownload", null, ex);
                throw;
            }
        }
        #endregion

    }
}