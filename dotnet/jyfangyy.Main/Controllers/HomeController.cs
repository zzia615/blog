using jyfangyy.Main.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDbContext dbContext;

        public HomeController()
        {
            this.dbContext = new SqlDbContext();
        }
        public ActionResult Index()
        {
            string code = Session["user_code"].AsString();
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Login", "User");
            }
            //查询用户信息
            var user = dbContext.User.SingleOrDefault(a => a.code == code);
            return View(user);
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