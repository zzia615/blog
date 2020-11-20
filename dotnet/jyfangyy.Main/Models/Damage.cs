using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 破损信息表
    /// </summary>
    [Table("Damage")]
    public class Damage
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        [Required]
        [StringLength(100)]
        public string title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string msg { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        /// <summary>
        /// 状态
        /// 1.待审核
        /// 2.待检修
        /// 3.已检修
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime publishDate { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        [NotMapped]
        public string publishDate_n
        {
            get
            {
                return publishDate.ToString("yyyy-MM-dd HH:mm");
            }
        }
        [NotMapped]
        public string status_n
        {
            get
            {
                switch (status)
                {
                    case 1:
                        return "待审核";
                    case 2:
                        return "待检修";
                    case 3:
                        return "已检修";
                    default:
                        return "";
                }

            }
        }
        [NotMapped]
        public string action { get; set; }
    }
}