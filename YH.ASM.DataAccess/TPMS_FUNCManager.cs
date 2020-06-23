using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
    public class TPMS_FUNCManager : DbContext<TPMS_FUNCTION>
    {


        /// <summary>
        /// 查询功能
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="p"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool FuncListByWhere(string keyword, ref PageModel p, ref List<TPMS_FUNCTION> list)
        {

            list = CurrentDb.GetPageList(it => it.FUNC_NAME.Contains(keyword) && it.APP_ID == 1, p, it => it.FUNC_ID, OrderByType.Asc);

            return list.Count > 0;
        }


        public bool FuncListByAll(ref List<TPMS_FUNCTION> list)
        {
            list = CurrentDb.GetList(s => s.APP_ID == 1);
            return list.Count > 0;
        }

        public bool FuncListByRole( int roleid,ref List<TPMS_FUNCTION> list)
        {
            return false;
        }
    }
}
