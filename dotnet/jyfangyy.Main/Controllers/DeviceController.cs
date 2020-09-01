using jyfangyy.Main.Data;
using jyfangyy.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jyfangyy.Main.Controllers
{
    public class DeviceController : Controller
    {
        private readonly SqlDbContext dbContext;

        public DeviceController()
        {
            this.dbContext = new SqlDbContext();
        }
        /// <summary>
        /// 设备主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存设备
        /// </summary>
        /// <param name="code">设备编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Device device)
        {
            var obj = new { code = "0000", msg = "" };

            if (device.action == "editDevice")
            {
                //修改设备信息
                dbContext.Entry(device).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                //查询实验室
                var lab = dbContext.Laboratory.SingleOrDefault(a => a.code == device.laboratory_code);
                if (lab == null)
                {
                    obj = new { code = "0001", msg = "实验室信息不存在" };
                }
                else
                {
                    //查询实验室已有设备数量
                    int count = dbContext.Device.Where(a => a.laboratory_code == device.laboratory_code).Count();
                    //超出则提醒用户
                    if (lab.max_num <= count)
                    {
                        obj = new { code = "0002", msg = "实验室能容设备的最大数量超出" };
                    }
                    else
                    {
                        //新增设备信息
                        dbContext.Device.Add(device);
                        dbContext.SaveChanges();
                    }
                }
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="code">设备编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string code)
        {
            var obj = new { code = "0000", msg = "" };
            //查询设备信息
            var data = dbContext.Device.SingleOrDefault(a => a.code == code);
            if (data == null)
            {
                obj = new { code = "0001", msg = "设备信息不存在" };
            }
            else
            {
                //删除设备信息
                dbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }
            //返回结果
            return Json(obj);
        }

        /// <summary>
        /// 分页查询设备信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDeviceList(int pageIndex, int pageSize, string code, string name,string laboratory_code)
        {
            //查询数据
            var query = dbContext.Device.AsQueryable();
            //如果code有值则设置为条件
            if (!string.IsNullOrEmpty(code))
                query = query.Where(a => a.code.Contains(code));
            //如果storey_code有值则设置为条件
            if (!string.IsNullOrEmpty(laboratory_code))
                query = query.Where(a => a.laboratory_code.Contains(laboratory_code));
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