using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
    public class TPMS_FUNCManager : DbContext<TPMS_FUNCTION>
    {
        public bool FuncListByAll(ref List<TPMS_FUNCTION> list)
        {
            list = TPMS_FUNCTIONDb.GetList(s => s.APP_ID == 1);
            return list.Count > 0;
        }

        public bool FuncListByRole( int roleid,ref List<TPMS_FUNCTION> list)
        {
            return false;
        }
    }
}
