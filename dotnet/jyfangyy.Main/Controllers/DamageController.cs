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
                //修改破损信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                damage.publishDate = DateTime.Now;
                dbContext.Damage.Add(damage);
                dbContext.SaveChanges();
            }
            return Json(obj);
        }

        [HttpPost]
        public ActionResult GetDamageList(int pageIndex,int pageSize,string title)
        {
            //查询数据
            var query = dbContext.Damage.AsQueryable();
            //如果title有值则设置为条件
            if (!string.IsNullOrEmpty(title))
                query = query.Where(a => a.title.Contains(title));
           
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

       
    }
}