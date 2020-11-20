using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class DamageController : Controller
    {
        Data.SqlDbContext dbContext;
        public DamageController()
        {
            dbContext = new Data.SqlDbContext();
        }
        // GET: Loss
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Check()
        {
            return View();
        }

        public ActionResult Query()
        {
            return View();
        }
        /// <summary>
        /// 保存失物信息
        /// </summary>
        /// <param name="loss"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Models.Damage damage)
        {
            var obj = new { code = "0000", msg = "" };
            if (damage.action == "editDamage")
            {
                var data = dbContext.Damage.Find(damage.id);
                data.title = damage.title;
                data.msg = damage.msg;
                data.code = damage.code;
                data.name = damage.name;
                data.status = damage.status;
                //修改破损信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                damage.pub_user_code = Session["user_code"].AsString();
                damage.pub_user_name = Session["user_name"].AsString();
                damage.pub_user_type = Session["user_type"].AsString();
                damage.publishDate = DateTime.Now;
                dbContext.Damage.Add(damage);
                dbContext.SaveChanges();
            }
            return Json(obj);
        }

        [HttpPost]
        public ActionResult GetDamageList(int pageIndex,int pageSize,string title,string code,int? status)
        {
            //查询数据
            string user_code = Session["user_code"].AsString();
            string user_type = Session["user_type"].AsString();
            //查询数据
            var query = dbContext.Damage.Where(a => a.pub_user_code == user_code && a.pub_user_type == user_type).AsQueryable();
            if (user_type == "1" || status != null && status.Value == 2)
            {
                //管理员查询所有，也可以删除所有
                query = dbContext.Damage.AsQueryable();
            }

            //如果title有值则设置为条件
            if (!string.IsNullOrEmpty(title))
                query = query.Where(a => a.title.Contains(title));
            //根据设备编号查询
            if (!string.IsNullOrEmpty(code))
                query = query.Where(a => a.code.Contains(code));
            if (status != null)
            {
                if (status.Value > 0)
                {
                    query = query.Where(a => a.status==status.Value);
                }
            }
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderBy(a => a.id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }

        

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var obj = new { code = "0000", msg = "" };
            var data = dbContext.Damage.Find(id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "未找到失物信息" };
            }
            else
            {
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            return Json(obj);
        }

        [HttpPost]
        public ActionResult CheckDamage(int id)
        {
            var obj = new { code = "0000", msg = "" };

            var data = dbContext.Damage.SingleOrDefault(a => a.id == id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "破损信息不存在，无法审核" };
            }
            else
            {
                var count = dbContext.Damage.Where(a => a.code == data.code &&a.status==2&& a.id != id).Count();
                if (count>0)
                {
                    obj = new { code = "0001", msg = "已经存在待检修的记录，无法审核" };
                }
                else
                {
                    data.status = 2;
                    var device = dbContext.Device.Find(data.code);
                    device.status = 9;
                    dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    dbContext.Entry(device).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            //返回结果
            return Json(obj);
        }

        [HttpPost]
        public ActionResult FinishDamage(int id)
        {
            var obj = new { code = "0000", msg = "" };

            var data = dbContext.Damage.SingleOrDefault(a => a.id == id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "破损信息不存在，无法检修完成" };
            }
            else
            {
                data.status = 3;
                var device = dbContext.Device.Find(data.code);
                device.status = 0;
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.Entry(device).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

    }
}