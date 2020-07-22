using System;
using System.Collections.Generic;
using System.Text;

namespace YH.ASM.Entites.Model
{
    public class PersonalSupportListModel: SupportModel
    {

        /// <summary>
        /// 主键id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 个人处理状态
        /// </summary>
        public int PSTATUS { get; set; }

        /// <summary>
        /// 单节点创建人
        /// </summary>
        public  int CID { get; set; }

        /// <summary>
        /// 单节点处理人
        /// </summary>
        public int DID { get; set; }

        /// <summary>
        /// 流程节点
        /// </summary>
        public int TID { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime PCREATETIME { get; set; }


    }
}
