using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class LaboratoryController : Controller
    {
        private readonly SqlDbContext dbContext;

        public LaboratoryController()
        {
            this.dbContext = new SqlDbContext();
        }
        /// <summary>
        /// 实验室主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存实验室
        /// </summary>
        /// <param name="code">实验室编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Laboratory laboratory)
        {
            var obj = new { code = "0000", msg = "" };

            if (laboratory.action == "editLaboratory")
            {
                //修改实验室信息
                dbContext.Entry(laboratory).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                //新增实验室信息
                dbContext.Laboratory.Add(laboratory);
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 删除实验室
        /// </summary>
        /// <param name="code">实验室编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string code)
        {
            var obj = new { code = "0000", msg = "" };
            //查询实验室信息
            var data = dbContext.Laboratory.SingleOrDefault(a => a.code == code);
            if (data == null)
            {
                obj = new { code = "0001", msg = "实验室信息不存在" };
            }
            else
            {
                //删除实验室信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 分页查询实验室信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetLaboratoryList(int pageIndex, int pageSize, string code, string name,string storey_code)
        {
            //查询数据
            var query = dbContext.Laboratory.AsQueryable();
            //如果code有值则设置为条件
            if (!string.IsNullOrEmpty(code))
                query = query.Where(a => a.code.Contains(code));
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(storey_code))
                query = query.Where(a => a.storey_code.Contains(storey_code));
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
    }
}