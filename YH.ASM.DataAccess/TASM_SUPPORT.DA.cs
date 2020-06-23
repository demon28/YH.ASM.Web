using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{

    /// <summary>
    ///  工单信息表
    ///</summary>
    public class TASM_SUPPORT_Da : DbContext<TASM_SUPPORT>
    {
  

       public TASM_SUPPORT_Da()
       {
      
       }

       //关键字查找,自行修改或增加关键字 字段
       public List<TASM_SUPPORT> ListByWhere(string keyword, ref PageModel p)
       {
       
           int pageCount = 0;

            var list= Db.Queryable<TASM_SUPPORT>()
                .Where(s => s.TITLE.Contains(keyword))
                .OrderBy(s => s.SID,OrderByType.Asc)
                .ToPageList(p.PageIndex, p.PageSize, ref pageCount);
           
           p.PageCount = pageCount;
           
           return list;
       }
   

       //关键字查找用于导出Excel
       public List<TASM_SUPPORT> ListByWhere(string keyword)
       {

            var list= Db.Queryable<TASM_SUPPORT>()
                .Where(s => s.TITLE.Contains(keyword))
                .OrderBy(s => s.SID,OrderByType.Asc)
                .ToList();
           
           return list;
       }


    }

}

