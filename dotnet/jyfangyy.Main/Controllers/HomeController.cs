using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Session["user_code"].AsString()))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        /// <summary>
        /// 校验是否登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckLogin()
        {
            var obj = new { code = "0000", msg = "" };
            if (string.IsNullOrEmpty(Session["user_code"].AsString()))
            {
                obj = new { code = "0001", msg = "用户未登录" };
            }

            return Json(obj);
        }
    }
}