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
        public string action { get; set; }
    }
}