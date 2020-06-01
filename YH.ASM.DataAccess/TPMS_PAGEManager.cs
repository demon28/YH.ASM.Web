using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
   public class TPMS_PAGEManager : DbContext<TPMS_PAGE>
    {

        public bool PageListByWhere(string keyword, ref PageModel p, ref List<TPMS_PAGE> list)
        {

            list = TPMS_PAGEDb.GetPageList(it => it.PAGE_NAME.Contains(keyword) && it.APP_ID == 1, p, it => it.PAGE_ID, OrderByType.Asc);

            return list.Count > 0;
        }

    }
}
