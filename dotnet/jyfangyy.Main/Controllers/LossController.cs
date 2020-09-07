using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class LossController : Controller
    {
        private readonly SqlDbContext dbContext;

        public LossController()
        {
            this.dbContext = new SqlDbContext();
        }
        // GET: Loss
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 删除失物
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var obj = new { code = "0000", msg = "" };

            var apply = dbContext.Loss.SingleOrDefault(a => a.id == id);
            if (apply == null)
            {
                obj = new { code = "0001", msg = "失物信息不存在，无法删除" };
            }
            else
            {
                //删除失物信息
                dbContext.Entry(apply).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 失物领用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Finish(string id,string user_code)
        {
            var obj = new { code = "0000", msg = "" };
            var user = dbContext.User.SingleOrDefault(a => a.code == user_code);

            if (user == null)
            {
                obj = new { code = "0001", msg = "领用人信息不存在，无法领用" };
            }
            else
            {
                var loss = dbContext.Loss.SingleOrDefault(a => a.id == id);
                if (loss == null)
                {
                    obj = new { code = "0002", msg = "失物信息不存在，无法领用" };
                }
                else
                {
                    loss.user_code = user.code;
                    loss.user_name = user.name;
                    loss.user_type = user.type;
                    loss.status = 2;
                    //修改失物信息状态为已领用
                    dbContext.Entry(loss).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            //返回结果
            return Json(obj);
        }

        [HttpPost]
        public ActionResult GetLossList(int pageIndex, int pageSize, string lab_code)
        {
            //查询数据
            var query = dbContext.Loss.AsQueryable();
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(lab_code))
                query = query.Where(a => a.lab_code.Contains(lab_code));
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderByDescending(a => a.date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }

        /// <summary>
        /// 保存申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Loss loss)
        {
            var obj = new { code = "0000", msg = "" };
            if (loss.action == "editLoss")
            {
                //修改申请信息
                dbContext.Entry(loss).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else if(loss.action== "finishLoss")
            {
                return Finish(loss.id, loss.user_code);
            }
            else
            {
                loss.status = 1;
                //新增申请信息
                dbContext.Loss.Add(loss);
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }
    }
}