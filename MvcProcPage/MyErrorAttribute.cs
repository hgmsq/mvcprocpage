using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MvcProcPage
{
    public class MyErrorAttribute:HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionQueue.Enqueue(filterContext.Exception);//加入异常队列
            //出现异常的时候可以跳转到异常处理的页面
            base.OnException(filterContext);
        }
    }
}