using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcProcPage.Service;
using MvcProcPage.Models;
using PagedList.Mvc;
using PagedList;

namespace MvcProcPage.Controllers
{
    public class HomeController : Controller
    {
        //UserService ss = new UserService();
        IUserService service = new UserService();
        // GET: Home
        public ActionResult Index(int page=1)
        {      
            #region 插入10条数据

            //for (int i = 1; i <= 100000; i++)
            //{
            //    list.Add(
            //        new UserInfo
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            UserName = "xiaoming" + i,
            //            Birthday = Convert.ToDateTime("1987-12-11"),
            //            Gender = 1,
            //            LocalAddress = "河南省",
            //            TrueName = "小明" + i,
            //            Nation = "汉族"
            //        });
            //}
            //ss.InsertAll(list); 
            #endregion
            var pagelist = service.GetAllList().ToPagedList(page,10);
            return View(pagelist);
        }
        public ActionResult ProcPageIndex(int page=1)
        {
            var list = service.GetPageByProcList(page,5);
            return View();
        }
        public JsonResult GetProList(int page=1,int pagesize=10)
        {
            var model = service.GetPageByProcList(page, pagesize);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TestLog()
        {
            int result = 0;
            int x = 1, y = 0;
            result = x / y;
            return View();
        }
    }
}