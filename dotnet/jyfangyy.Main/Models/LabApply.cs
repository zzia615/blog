using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    [Table("LabApply")]
    public class LabApply
    {
        private string _id = Guid.NewGuid().AsString();
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Required]
        [StringLength(60)]
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 预约模式
        /// 机房预约、座位预约
        /// </summary>
        [Required]
        [StringLength(20)]
        public string mode { get; set; }
        /// <summary>
        /// 实验室/设备编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string code { get; set; }
        /// <summary>
        /// 实验室/设备名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        private DateTime _apply_date = DateTime.Now;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Required]
        public DateTime apply_date
        {
            get { return _apply_date; }
            set { _apply_date = value; }
        }
        [NotMapped]
        public string apply_date_n
        {
            get
            {
                return apply_date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 申请人账号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string user_code { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string user_name { get; set; }
        /// <summary>
        /// 申请人类别
        /// </summary>
        [Required]
        [StringLength(10)]
        public string user_type { get; set; }
        /// <summary>
        /// 计划时间
        /// </summary>
        [Required]
        public DateTime plan_date { get; set; }
        [NotMapped]
        public string plan_date_n
        {
            get
            {
                return plan_date.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 计划时间段
        /// </summary>
        [Required]
        [StringLength(20)]
        public string plan_sjd { get; set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        [Required]
        [StringLength(200)]
        public string remark { get; set; }
        /// <summary>
        /// 申请状态
        /// 0.已撤销 1.审核中 2.已审核 3.已完成 9.已打回
        /// </summary>
        public int status { get; set; }

        [NotMapped]
        public string status_n
        {
            get
            {
                switch (status)
                {
                    case 0:
                        return "已撤销";
                    case 1:
                        return "审核中";
                    case 2:
                        return "已审核";
                    case 3:
                        return "已完成";
                    case 9:
                        return "已打回";
                    default:
                        return "";
                }

            }
        }
        [NotMapped]
        public int plan_sjd1
        {
            get
            {
                if (plan_sjd.Contains("-"))
                {
                    var arr = plan_sjd.Split('-');
                    return arr[0].AsInt(); ;
                }
                else
                {
                    return 0;
                }
            }
        }
        [NotMapped]
        public int plan_sjd2
        {
            get
            {
                if (plan_sjd.Contains("-"))
                {
                    var arr = plan_sjd.Split('-');
                    return arr[1].AsInt();
                }
                else
                {
                    return 0;
                }
            }
        }
        /*<el-option label = "上午-第一节" value="1"></el-option>
          <el-option label = "上午-第二节" value="2"></el-option>
          <el-option label = "上午-第三节" value="3"></el-option>
          <el-option label = "上午-第四节" value="4"></el-option>
          <el-option label = "上午-第五节" value="5"></el-option>
          <el-option label = "下午-第六节" value="6"></el-option>
          <el-option label = "下午-第七节" value="7"></el-option>
          <el-option label = "下午-第八节" value="8"></el-option>
          <el-option label = "下午-第九节" value="9"></el-option>
          <el-option label = "晚上-第十节" value="10"></el-option>
          <el-option label = "晚上-第十一节" value="11"></el-option>
          <el-option label = "晚上-第十二节" value="12"></el-option>*/
        string GetPlanSJD(string number)
        {
            switch (number)
            {
                case "1":
                    return "上午-第一节";
                case "2":
                    return "上午-第二节";
                case "3":
                    return "上午-第三节";
                case "4":
                    return "上午-第四节";
                case "5":
                    return "上午-第五节";
                case "6":
                    return "下午-第六节";
                case "7":
                    return "下午-第七节";
                case "8":
                    return "下午-第八节";
                case "9":
                    return "下午-第九节";
                case "10":
                    return "晚上-第十节";
                case "11":
                    return "晚上-第十一节";
                case "12":
                    return "晚上-第十二节";
            }
            return number;
        }

        [NotMapped]
        public string plan_sjd_n
        {
            get
            {
                if (plan_sjd.Contains("-"))
                {
                    var arr = plan_sjd.Split('-');
                    return string.Format("{0}到{1}", GetPlanSJD(arr[0]), GetPlanSJD(arr[1]));
                }
                else
                {
                    return plan_sjd;
                }
            }
        }


        [NotMapped]
        public string action { get; set; }
    }
}