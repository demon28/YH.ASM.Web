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
        /// Desc:用户主键id
        /// Default:
        /// Nullable:False
        /// </summary>           
    
        [SugarColumn(OracleSequenceName = "seq_tasm_user", IsPrimaryKey = true)]
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
        /// Desc:在职状态：{0：在职，1:离职，2：锁定}
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
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REMARKS { get; set; }

        /// <summary>
        /// Desc:是否逻辑删除{0:正常,1:删除}
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short ISDELETED { get; set; }

        /// <summary>
        /// Desc:部门全称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DEPARTMENT { get; set; }

        /// <summary>
        /// Desc:入职时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime COMEDATE { get; set; }

        /// <summary>
        /// Desc:职务名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DTNAME { get; set; }

        /// <summary>
        /// Desc:手机号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MOBILE { get; set; }

        /// <summary>
        /// Desc:性别{0：男，1：女}
        /// Default:
        /// Nullable:False
        /// </summary>           
        public short USER_SEX { get; set; }

    }
}
