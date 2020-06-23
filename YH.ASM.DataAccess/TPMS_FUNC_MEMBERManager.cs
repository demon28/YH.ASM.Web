using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
    public class TPMS_FUNC_MEMBERManager: DbContext<TPMS_FUNC_MEMBER>
    {
        public bool ListByFuncid(int funcid,ref List<TPMS_FUNC_MEMBER> list)
        {
           
            list= CurrentDb.GetList(it => it.FUNC_ID == funcid );

            return list.Count>0;
        }




    }
}
