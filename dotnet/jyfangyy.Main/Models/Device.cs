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
        public string storey_code { get; set; }
        [NotMapped]
        public string storey_name { get; set; }
        [NotMapped]
        public string action { get; set; }
        [NotMapped]
        public string status_n
        {
            get
            {
                if (status == 1)
                {
                    return "占用";
                }
                else if (status == 0)
                {
                    return "空闲";
                }
                else if (status == 9)
                {
                    return "损坏";
                }
                else
                {
                    return "";
                }
            }
        }
        [NotMapped]
        public string full_name { get { return storey_name+"-"+laboratory_name + "-" + name; } }

    }
}