using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
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
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="pwd_n"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePwd(string pwd,string pwd_n)
        {
            var obj = new { code = "0000", msg = "" };
            string code = Session["user_code"].AsString();
            var user = dbContext.User.SingleOrDefault(a => a.code == code);
            if (user == null)
            {
                obj = new { code = "0001", msg = "请退出系统重新登录" };
            }
            else
            {
                if (user.pwd != pwd)
                {
                    obj = new { code = "0002", msg = "原密码输入不正确" };
                }
                else
                {
                    if (pwd == pwd_n)
                    {
                        obj = new { code = "0003", msg = "新老密码不允许相同" };
                    }
                    else
                    {
                        user.pwd = pwd_n;
                        dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
            return Json(obj);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页显示大小</param>
        /// <param name="code">账号</param>
        /// <param name="name">姓名</param>
        /// <param name="type">用户类别 1管理员 2教师 3学生</param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult GetUserList(int pageIndex, int pageSize, string code, string name, string type)
        {
            //查询数据
            var query = dbContext.User.AsQueryable();
            //限定查询的类别
            query = query.Where(a => a.type == type);
            //如果code有值则设置为条件
            if (!string.IsNullOrEmpty(code))
                query = query.Where(a => a.code.Contains(code));
            //如果name有值则设置为条件
            if (!string.IsNullOrEmpty(name))
                query = query.Where(a => a.name.Contains(name));
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderBy(a => a.code).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string code)
        {
            var obj = new { code = "0000", msg = "" };
            //查询用户数据
            var user = dbContext.User.SingleOrDefault(a => a.code == code);
            if (user == null)
            {
                obj = new { code = "0001", msg = "用户信息不存在" };
            }
            else
            {
                //删除数据
                dbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回数据
            return Json(obj);
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(User user)
        {
            if (user.action == "editUser")
            {
                return EditUser(user);
            }
            else
            {
                return AddUser(user);
            }

        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ActionResult AddUser(User user)
        {
            //新增用户
            dbContext.User.Add(user);
            dbContext.SaveChanges();
            //返回数据
            var obj = new { code = "0000", msg = ""};
            return Json(obj);

        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ActionResult EditUser(User user)
        {
            //修改用户
            dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
            //返回数据
            var obj = new { code = "0000", msg = "" };
            return Json(obj);
        }
    }
}