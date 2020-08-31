using jyfangyy.Main.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserController : Controller
    {
        private readonly SqlDbContext dbContext;

        public UserController()
        {
            this.dbContext = new SqlDbContext();
        }
        /// <summary>
        /// 教师页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Teacher()
        {
            return View();
        }
        /// <summary>
        /// 学生页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Student()
        {
            return View();
        }
        /// <summary>
        /// 管理员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Admin()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginSys(string code,string pwd,string type)
        {
            var obj = new { code = "0000", msg = "" };

            var user = dbContext.User.SingleOrDefault(a => a.code == code&&a.type==type);
            if (user == null)
            {
                obj = new { code = "0001", msg = "用户信息不存在" };
            }
            else
            {
                if (user.pwd != pwd)
                {
                    obj = new { code = "0001", msg = "账号密码不匹配" };
                }
                else
                {
                    Session["user_code"] = code;
                    Session["user_name"] = user.name;
                    Session["user_type"] = type;
                }
            }

            return Json(obj);
        }
        /// <summary>
        /// 用户登出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult LogoutSys()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePwd(string pwd,string pwd_n)
        {
            var obj = new { code = "0000", msg = "" };
            return Json(obj);
        }
        [HttpPost]

        public ActionResult GetUserList(int pageIndex,int pageSize,string code,string name)
        {
            var data = dbContext.User.OrderBy(a=>a.name).Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();

            var obj = new { code = "0000", msg = "", data = data, count = data.Count };


            return Json(obj);
        }
    }
}