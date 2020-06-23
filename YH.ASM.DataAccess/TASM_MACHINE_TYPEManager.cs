using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
    public class TASM_MACHINE_TYPEManager : DbContext<TASM_MACHINE_TYPE>
    {

        public SimpleClient<TASM_MACHINE_TYPE> TASM_MACHINETYPERDb { get { return new SimpleClient<TASM_MACHINE_TYPE>(Db); } }


        public bool ListByWhere(string keyword, ref PageModel p, ref List<TASM_MACHINE_TYPE> list)
        {

            list = TASM_MACHINETYPERDb.GetPageList(s => s.NAME.Contains(keyword), p, s => s.ID, OrderByType.Asc);
            return list.Count > 0;
        }


        public bool ListByWhere(string keyword, ref List<TASM_MACHINE_TYPE> list)
        {

            list = TASM_MACHINETYPERDb.GetList(s => s.NAME.Contains(keyword) );

            return list.Count > 0;
        }


    }
}
