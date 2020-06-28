using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{

    /// <summary>
    ///  现场人员处理表
    ///</summary>
    public class TASM_SUPPORT_SITE_Da : DbContext<TASM_SUPPORT_SITE>
    {
  

       public TASM_SUPPORT_SITE_Da()
       {
      
       }

       //关键字查找,自行修改或增加关键字 字段
       public List<TASM_SUPPORT_SITE> ListByWhere(string keyword, ref PageModel p)
       {
            List<TASM_SUPPORT_SITE> list = new List<TASM_SUPPORT_SITE>();

            
           return list;
       }


    }

}

