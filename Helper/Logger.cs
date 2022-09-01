using System;
using System.IO;
using System.Reflection;
using SDM.Models.Database;

namespace SDM.Helper
{
    public class Logger
    {
        public void LogInfo(string controllerName, string Request, string Message)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Logs log = new Logs();

                    log.ControllerName = controllerName;
                    log.Request = Request;
                    log.Message = Message;
                    log.LastUpdate = DateTime.Now;

                    context.Logs.Add(log);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void LogWrite(string controllerName, string Request, Exception ex)
        {
            try
            {
                using (var context = new SDMEntities())
                {
                    Logs log = new Logs();

                    log.ControllerName = controllerName;
                    log.Request = Request;
                    log.Message = ex.Message;
                    log.LastUpdate = DateTime.Now;

                    context.Logs.Add(log);

                    if (ex.InnerException != null)
                    {
                        Logs logInner = new Logs();

                        logInner.ControllerName = controllerName;
                        logInner.Request = Request;
                        logInner.Message = ex.InnerException.Message;
                        logInner.LastUpdate = log.LastUpdate;

                        context.Logs.Add(logInner);
                    }                    

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}