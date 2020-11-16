using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 实验室
    /// </summary>
    [Table("Laboratory")]
    public class Laboratory
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
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
        /// 最大设备数量
        /// </summary>
        public int max_num { get; set; }
        /// <summary>
        /// 实验室状态 1占用 0正常
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 占用时间
        /// </summary>
        public DateTime? occupy_date { get; set; }
        /// <summary>
        /// 楼层唯一码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string storey_code { get; set; }
        /// <summary>
        /// 楼层名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string storey_name { get; set; }
        [NotMapped]
        public string action { get; set; }
        [NotMapped]
        public string full_name { get { return storey_name + "-" + name; } }
    }
}