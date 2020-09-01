using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace jyfangyy.Main.Models
{
    /// <summary>
    /// 楼层信息
    /// </summary>
    [Table("Storey")]
    public class Storey
    {
        /// <summary>
        /// 楼层唯一码
        /// </summary>
        [Key]
        [Required]
        [StringLength(50)]
        public string code { get; set; }
        /// <summary>
        /// 楼层名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [NotMapped]
        public string action { get; set; }
    }
}