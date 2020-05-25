using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace YH.ASM.SSO.Models
{

    [Table("tasm_user")]
    public class UserModel
    {

        /// <summary>
        /// 主键id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int User_Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(40)]
        public string User_Name { get; set; }
       
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(40)]
        public string User_Pwd { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [Required]
        [Column(TypeName = "enum('正常','锁定','注销')")]
        public int User_Status { get; set; } = 0;

        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Work_Id { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Required]
        [StringLength(80)]
        public string Data_Source { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(TypeName = "DateTime")]
        public DateTime Createtime { get; set; }



        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; } = false;



        [StringLength(200)]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }


    }
}
