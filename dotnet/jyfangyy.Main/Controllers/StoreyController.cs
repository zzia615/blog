﻿using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
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
        /// 楼层维护主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 保存楼层
        /// </summary>
        /// <param name="code">楼层编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Storey storey)
        {
            var obj = new { code = "0000", msg = "" };

            if (storey.action == "editStorey")
            {
                //修改楼层信息
                dbContext.Entry(storey).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                //新增楼层信息
                dbContext.Storey.Add(storey);
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
            var data = dbContext.Storey.SingleOrDefault(a => a.code == code);
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
        /// 分页查询楼层信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetStoreyList(int pageIndex,int pageSize,string code,string name)
        {
            //查询数据
            var query = dbContext.Storey.AsQueryable();
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
    }
}