using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 失物信息
    /// </summary>
    [Table("Loss")]
    public class Loss
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(100)]
        public string title { get; set; }
        /// <summary>
        /// 失物内容
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string msg { get; set; }
        /// <summary>
        /// 失物地点
        /// </summary>
        [Required]
        [StringLength(100)]
        public string place { get; set; }
        /// <summary>
        /// 失物图片
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string imageUrl { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime publishDate { get; set; }
        /// <summary>
        /// 状态
        /// 0.待审核
        /// 1.待领取
        /// 2.申领中
        /// 3.已申领
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 申领日期
        /// </summary>
        public DateTime? gotDate { get; set; }
        /// <summary>
        /// 发布人账号
        /// </summary>
        [StringLength(50)]
        public string pub_user_code { get; set; }
        /// <summary>
        /// 发布人姓名
        /// </summary>
        [StringLength(20)]
        public string pub_user_name { get; set; }
        /// <summary>
        /// 发布人类别
        /// </summary>
        [StringLength(10)]
        public string pub_user_type { get; set; }
        /// <summary>
        /// 申领人账号
        /// </summary>
        [StringLength(50)]
        public string user_code { get; set; }
        /// <summary>
        /// 申领人姓名
        /// </summary>
        [StringLength(20)]
        public string user_name { get; set; }
        /// <summary>
        /// 申领人类别
        /// </summary>
        [StringLength(10)]
        public string user_type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [NotMapped]
        public string status_n
        {
            get
            {
                if (status == 0)
                {
                    return "待审核";
                }
                else if (status == 1)
                {
                    return "待领取";
                }
                else if (status == 2)
                {
                    return "申领中";
                }
                else
                {
                    return "已申领";
                }
            }
        }
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
        public string action
        {
            get;set;
        }
    }
}
