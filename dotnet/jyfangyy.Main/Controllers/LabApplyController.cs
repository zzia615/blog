using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class LabApplyController : Controller
    {
        private readonly SqlDbContext dbContext;

        public LabApplyController()
        {
            this.dbContext = new SqlDbContext();
        }
        // GET: LabApply
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 保存申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(LabApply apply)
        {
            var obj = new { code = "0000", msg = "" };

            apply.mode = "机房预约";
            if (apply.action == "editLabApply")
            {
                //修改申请信息
                dbContext.Entry(apply).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                apply.status = 1;
                //新增申请信息
                dbContext.LabApply.Add(apply);
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }
        /// <summary>
        /// 删除申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var obj = new { code = "0000", msg = "" };

            var apply = dbContext.LabApply.SingleOrDefault(a => a.id == id);
            if (apply == null)
            {
                obj = new { code = "0001", msg = "申请信息不存在，无法删除" };
            }
            else
            {
                //新增申请信息
                dbContext.Entry(apply).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }
        [HttpPost]
        public ActionResult GetLabApplyList(int pageIndex,int pageSize,string laboratory_code)
        {
            //查询数据
            var query = dbContext.LabApply.AsQueryable();
            query = query.Where(a => a.mode == "机房预约");
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(laboratory_code))
                query = query.Where(a => a.code.Contains(laboratory_code));
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderBy(a => a.code).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }
    }
}