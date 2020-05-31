using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
   public class TPMS_ROLEManager :  DbContext<TPMS_ROLE>
    {

        /// <summary>
        /// 查询角色表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="p"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool RoleListByWhere(string keyword, ref PageModel p, ref List<TPMS_ROLE> list)
        {

            list = TPMS_ROLEDb.GetPageList(it => it.ROLE_NAME.Contains(keyword) && it.APP_ID == 1, p, it => it.ROLE_ID, OrderByType.Asc);

            return list.Count > 0;
        }

        public bool RoleListByAll(ref List<TPMS_ROLE> list) {

            list=TPMS_ROLEDb.GetList(s => s.APP_ID == 1);
            return list.Count > 0;
        }
    }
}
