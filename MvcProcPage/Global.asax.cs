using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using log4net;
namespace MvcProcPage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //log4初始化
            log4net.Config.XmlConfigurator.Configure();            
            AreaRegistration.RegisterAllAreas();      
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    if (MyErrorAttribute.ExceptionQueue.Count > 0)
                    {
                        Exception ex = MyErrorAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            ILog logger = LogManager.GetLogger("testError");
                            logger.Error(ex.ToString());//将异常信息写入log4
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            });
        }
    }
}
