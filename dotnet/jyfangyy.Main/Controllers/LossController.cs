using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class LossController : Controller
    {
        Data.SqlDbContext dbContext;
        public LossController()
        {
            dbContext = new Data.SqlDbContext();
        }
        // GET: Loss
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Apply()
        {
            return View();
        }
        public ActionResult Check()
        {
            return View();
        }
        public ActionResult ApplyForMe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage()
        {
            if(Request.Files.Count<=0)
            {
                return Json(new { code = "0001", msg = "未找到文件信息" });
            }
            var file = Request.Files[0];
            string path = Server.MapPath("/Upload");
            string name = Guid.NewGuid().ToString() + "_" + file.FileName;
            string fileName = System.IO.Path.Combine(path, name);
            file.SaveAs(fileName);
            return Json(new { code = "0000", msg = "",imageUrl= "/Upload/"+ name });
        }
        /// <summary>
        /// 保存失物信息
        /// </summary>
        /// <param name="loss"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Models.Loss loss)
        {
            var obj = new { code = "0000", msg = "" };
            if (loss.action == "editLoss")
            {
                var data = dbContext.Loss.Find(loss.id);
                data.title = loss.title;
                data.msg = loss.msg;
                data.status = loss.status;
                data.imageUrl = loss.imageUrl;
                data.place = loss.place;
                //修改失物信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {

                loss.pub_user_code = Session["user_code"].AsString();
                loss.pub_user_name = Session["user_name"].AsString();
                loss.pub_user_type = Session["user_type"].AsString();
                loss.publishDate = DateTime.Now;
                dbContext.Loss.Add(loss);
                dbContext.SaveChanges();
            }
            return Json(obj);
        }

        [HttpPost]
        public ActionResult GetLossList(int pageIndex,int pageSize,string title,int? status)
        {
            //查询数据
            string user_code = Session["user_code"].AsString();
            string user_type = Session["user_type"].AsString();
            //查询数据
            var query = dbContext.Loss.Where(a => a.pub_user_code == user_code && a.pub_user_type == user_type).AsQueryable();
            if (user_type == "1" || status != null && status.Value == 1)
            {
                //管理员查询所有，也可以删除所有
                query = dbContext.Loss.AsQueryable();
            }
            //如果title有值则设置为条件
            if (!string.IsNullOrEmpty(title))
                query = query.Where(a => a.title.Contains(title));
            if (status != null)
            {
                if (status.Value >= 0)
                {
                    query = query.Where(a => a.status == status);
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
        public ActionResult GetMyLossList(int pageIndex, int pageSize, string title, int? status)
        {
            //查询数据
            string user_code = Session["user_code"].AsString();
            string user_type = Session["user_type"].AsString();
            var query = dbContext.Loss.Where(a=>a.user_code== user_code&&a.user_type==user_type).AsQueryable();
            //如果title有值则设置为条件
            if (!string.IsNullOrEmpty(title))
                query = query.Where(a => a.title.Contains(title));
            //if (status != null)
            //{
            //    if (status.Value > 0)
            //    {
            //        query = query.Where(a => a.status == status);
            //    }
            //}
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
            var data = dbContext.Loss.Find(id);
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
        public ActionResult CheckLoss(int id)
        {
            var obj = new { code = "0000", msg = "" };
            var data = dbContext.Loss.Find(id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "未找到失物信息" };
            }
            else
            {
                data.status = 1;
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            return Json(obj);
        }
        [HttpPost]
        public ActionResult GotLoss(int id)
        {
            var obj = new { code = "0000", msg = "" };
            var data = dbContext.Loss.Find(id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "未找到失物信息" };
            }
            else
            {
                data.status = 3;
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            return Json(obj);
        }
        [HttpPost]
        public ActionResult Apply(int id)
        {
            var obj = new { code = "0000", msg = "" };
            var data = dbContext.Loss.Find(id);
            if (data == null)
            {
                obj = new { code = "0001", msg = "未找到失物信息" };
            }
            else
            {
                
                data.status = 2;
                data.user_code = Session["user_code"].AsString();
                data.user_name = Session["user_name"].AsString();
                data.user_type = Session["user_type"].AsString();
                data.gotDate = DateTime.Now;

                if (data.pub_user_code == data.user_code)
                {
                    obj = new { code = "0002", msg = "申领人不能是发布人自己" };
                }
                else
                {
                    dbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            return Json(obj);
        }
    }
}