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
        /// 申请状态
        /// 1.审核中 2.已审核 3.已完成 9.拒绝审核
        /// </summary>
        public int status { get; set; }

        [NotMapped]
        public string status_n
        {
            get
            {
                switch (status)
                {
                    case 1:
                        return "审核中";
                    case 2:
                        return "已审核";
                    case 3:
                        return "已完成";
                    case 9:
                        return "拒绝审核";
                    default:
                        return "";
                }

            }
        }

        [NotMapped]
        public string action { get; set; }
    }
}