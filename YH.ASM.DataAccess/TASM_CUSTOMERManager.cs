using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
   public class TASM_CUSTOMERManager : DbContext<TASM_CUSTOMER>
    {


        public SimpleClient<TASM_CUSTOMER> TASM_CUSTOMERDb { get { return new SimpleClient<TASM_CUSTOMER>(Db); } }

        public bool ListByWhere(string keyword, ref PageModel p, ref List<TASM_CUSTOMER> list)
        {

            list = TASM_CUSTOMERDb.GetPageList(s => s.NAME.Contains(keyword), p, s => s.CID, OrderByType.Asc);

            return list.Count > 0;
        }


        public bool ListByWhere(string keyword, ref List<TASM_CUSTOMER> list)
        {

            list = TASM_CUSTOMERDb.GetList(s => s.NAME.Contains(keyword));

            return list.Count > 0;
        }


    }
}
