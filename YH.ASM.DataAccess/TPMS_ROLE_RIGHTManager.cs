using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
   public  class TPMS_ROLE_RIGHTManager : DbContext<TPMS_ROLE_RIGHT>
    {

        public bool ListByRoleid(int roleid ,ref List<TPMS_ROLE_RIGHT> list)
        {

            list= CurrentDb.GetList(s=>s.ROLE_ID==roleid);

            return list.Count > 0;
        }



    }
}
