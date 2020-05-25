using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.CodeGenerator
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TASM_USER")]
    public partial class TASM_USER
    {
        public TASM_USER()
        {


        }
        /// <summary>
        /// Desc:主键id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long USER_ID { get; set; }

        /// <summary>
        /// Desc:用户名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USER_NAME { get; set; }

        /// <summary>
        /// Desc:用户密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USER_PWD { get; set; }

        /// <summary>
        /// Desc:用户状态{'正常','锁定','注销'}
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short USER_STATUS { get; set; }

        /// <summary>
        /// Desc:工号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string WORK_ID { get; set; }

        /// <summary>
        /// Desc:数据来源
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DATA_SOURCE { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CREATETIME { get; set; }

        /// <summary>
        /// Desc:逻辑删除
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short ISDELETED { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REMARKS { get; set; }

    }
}
