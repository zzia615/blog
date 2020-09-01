using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 实验室
    /// </summary>
    [Table("Device")]
    public class Device
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
        /// 状态 1占用 0空闲 9损坏
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 所属实验室编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string laboratory_code { get; set; }
        /// <summary>
        /// 所属实验室名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string laboratory_name { get; set; }
        [NotMapped]
        public string action { get; set; }

    }
}