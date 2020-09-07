using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 失物招领
    /// </summary>

    [Table("Loss")]
    public class Loss
    {
        private string _id = Guid.NewGuid().AsString();
        /// <summary>
        /// 主键编码
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
        /// 实验室编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string lab_code { get; set; }
        /// <summary>
        /// 实验室名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string lab_name { get; set; }

        private DateTime _date = DateTime.Now;
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }
        [NotMapped]
        public string date_n
        {
            get
            {
                return date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 发布说明
        /// </summary>
        [Required]
        [StringLength(500)]
        public string description { get; set; }
        /// <summary>
        /// 状态
        /// 1.已发布 2.已领取
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
                        return "已发布";
                    case 2:
                        return "已领取";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 领用人编号
        /// </summary>
        [StringLength(50)]
        public string user_code { get; set; }
        /// <summary>
        /// 领用人姓名
        /// </summary>
        [StringLength(20)]
        public string user_name { get; set; }
        /// <summary>
        /// 领用人类别
        /// 1.管理员、2.教师、3.学生
        /// </summary>
        [StringLength(10)]
        public string user_type { get; set; }

        [NotMapped]
        public string user_type_n
        {
            get
            {
                if (user_type == "1")
                {
                    return "管理员";
                }
                else if (user_type == "2")
                {
                    return "教师";
                }
                else
                {
                    return "学生";
                }
            }
        }

        [NotMapped]
        public string action
        {
            get;set;
        }
    }
}