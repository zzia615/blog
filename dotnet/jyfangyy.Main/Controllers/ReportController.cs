using jyfangyy.Main.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class ReportController : Controller
    {
        private readonly SqlDbContext dbContext;

        public ReportController()
        {
            this.dbContext = new SqlDbContext();
        }
        // GET: Report
        public ActionResult LabPaike()
        {
            return View();
        }
        public ActionResult LabUse()
        {
            return View();
        }
        /// <summary>
        /// 查询排课情况
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="mode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLabPaike(int pageIndex, int pageSize, string code, string mode,int status=99)
        {
            //查询数据
            var query = dbContext.LabApply.AsQueryable();
            query = query.Where(a => a.mode == mode);
            if (status != 99)
            {
                query = query.Where(a => a.status == status);
            }
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(code))
                query = query.Where(a => a.code.Contains(code));
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderByDescending(a => a.apply_date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }
        /// <summary>
        /// 查询使用情况
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="mode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLabUse(int pageIndex, int pageSize, string code, string mode, int status = 99)
        {
            //查询数据
            var query = dbContext.LabApply.AsQueryable();
            query = query.Where(a => a.mode == mode);
            if (status != 99)
            {
                query = query.Where(a => a.status == status);
            }
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(code))
            {
                query = from q in query
                        from d in dbContext.Device
                        where q.code == d.code && d.laboratory_code == code
                        select q;
            }
             //   query = query.Where(a => a.code.Contains(code));
            //查询总条数
            int count = query.Count();
            //分页查询数据库的操作
            var data = query.OrderByDescending(a => a.apply_date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //返回json
            var obj = new { code = "0000", msg = "", data = data, count = count };
            return Json(obj);
        }


    }
}