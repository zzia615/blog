using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    [Table("User")]
    public class User
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Key]
        [Required]
        [StringLength(50)]
        public string code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string pwd { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(10)]
        public string sex { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        [StringLength(50)]
        public string grade { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [StringLength(100)]
        public string major { get; set; }
        /// <summary>
        /// 用户类别
        /// 管理员、教师、学生
        /// </summary>
        [Required]
        [StringLength(10)]
        public string type { get; set; }
        [NotMapped]
        public string sex_n
        {
            get
            {
                if (sex == "1")
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }

        [NotMapped]
        public string type_n
        {
            get
            {
                if (type == "1")
                {
                    return "管理员";
                }
                else if (type == "2")
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
        public string action { get; set; }

    }
}