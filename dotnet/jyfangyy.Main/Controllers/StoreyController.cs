using jyfangyy.Main.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class StoreyController : Controller
    {
        private readonly SqlDbContext dbContext;

        public StoreyController()
        {
            this.dbContext = new SqlDbContext();
        }

        /// <summary>
        /// 保存楼层
        /// </summary>
        /// <param name="code">楼层编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(string code)
        {
            var obj = new { code = "0000", msg = "" };
            //查询楼层信息
            var data = dbContext.Device.SingleOrDefault(a => a.code == code);
            if (data == null)
            {
                obj = new { code = "0001", msg = "楼层信息不存在" };
            }
            else
            {
                //删除楼层信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="code">楼层编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string code)
        {
            var obj = new { code = "0000", msg = "" };
            //查询楼层信息
            var data = dbContext.Device.SingleOrDefault(a => a.code == code);
            if (data == null)
            {
                obj = new { code = "0001", msg = "楼层信息不存在" };
            }
            else
            {
                //删除楼层信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }
    }
}