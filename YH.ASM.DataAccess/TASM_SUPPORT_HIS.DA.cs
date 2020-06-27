using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace  YH.ASM.DataAccess
{

    /// <summary>
    ///  工单处理人变更历史
    ///</summary>
    public class TASM_SUPPORT_HIS_Da : DbContext<TASM_SUPPORT_HIS>
    {
        

       public TASM_SUPPORT_HIS_Da()
       {
      
       }

       //关键字查找,自行修改或增加关键字 字段
       public List<TASM_SUPPORT_HIS> ListByWhere(string keyword, ref PageModel p)
       {


            List<TASM_SUPPORT_HIS> list = new List<TASM_SUPPORT_HIS>();
           return list;
       }


    }

}

