using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
   public  class TASM_PROJECTManager : DbContext<TASM_PROJECT>
    {


        public bool ListByWhere(string keywords, ref PageModel p, ref List<TASM_PROJECT> list) {

            int pagecount = 0;
            list= Db.Queryable<TASM_PROJECT>().Where(s => s.NAME.Contains(keywords)).ToPageList(p.PageIndex, p.PageSize, ref pagecount);

            p.PageCount = pagecount;
            return list.Count > 0;
        
        }


        public bool ListByWhere(string keywords,  ref List<TASM_PROJECT> list)
        {

            list = Db.Queryable<TASM_PROJECT>().Where(s => s.NAME.Contains(keywords)).ToList();

            return list.Count > 0;




        }








    }
}
