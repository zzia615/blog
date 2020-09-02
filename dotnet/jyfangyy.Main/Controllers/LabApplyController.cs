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
        public bool CheckLabUsable(LabApply apply)
        {
            //查询是否存在已审核的实验室/设备申请
            var data = dbContext.LabApply.Where(a => a.code == apply.code && a.plan_date == apply.plan_date && a.plan_sjd == apply.plan_sjd && a.status == 2&&a.mode==apply.mode).ToList();
            if (data.Count > 0) return false;
            if (apply.mode == "机房预约")
            {
                //机房预约需要校验预约时间和时间段内是否有人预约了单个座位
                //如果无人预约（数量0）则返回成功
                //否则返回失败
                var tmp = from a in dbContext.Laboratory
                          from b in dbContext.Device
                          from c in dbContext.LabApply
                          where a.code == b.laboratory_code & b.code == c.code & c.status == 2 & c.mode == "座位预约"
                          & c.plan_date == apply.plan_date & c.plan_sjd == apply.plan_sjd & a.code == apply.code
                          select c;
                if (tmp.Count() > 0)
                {
                    return false;
                }
            }
            if (apply.mode == "座位预约")
            {
                //座位预约需要校验预约时间和时间段内是否有人预约了整个机房
                //如果无人预约（数量0）则返回成功
                //否则返回失败
                var tmp = from a in dbContext.Laboratory
                          from b in dbContext.Device
                          from c in dbContext.LabApply
                          where a.code == b.laboratory_code & a.code == c.code & c.status == 2 & c.mode == "机房预约"
                          & c.plan_date == apply.plan_date & c.plan_sjd == apply.plan_sjd & a.code == apply.code
                          select c;
                if (tmp.Count() > 0)
                {
                    return false;
                }
            }

            return true;
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
            if (!CheckLabUsable(apply))
            {
                obj = new { code = "0001", msg = "实验室/设备已被预约，请更换预约时间或时间段" };
            }
            else
            {
                if (apply.action == "editLabApply")
                {
                    //修改申请信息
                    dbContext.Entry(apply).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                else
                {
                    apply.user_code = Session["user_code"].AsString();
                    apply.user_name = Session["user_name"].AsString();
                    apply.user_type = Session["user_type"].AsString();
                    apply.status = 1;
                    //新增申请信息
                    dbContext.LabApply.Add(apply);
                    dbContext.SaveChanges();
                }
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