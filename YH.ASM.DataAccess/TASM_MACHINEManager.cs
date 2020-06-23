﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.DataAccess
{
    public class TASM_MACHINEManager :  DbContext<TASM_MACHINE>
    {

        public SimpleClient<TASM_MACHINE> TASM_MACHINERDb { get { return new SimpleClient<TASM_MACHINE>(Db); } }


        public bool ListByWhere(string keyword, ref PageModel p, ref List<MachineModel> list)
        {

            string sql = @"select t.*, tr.name typesname 
  from tasm_machine t
  left join tasm_machine_type tr
    on t.types = tr.id"
;
            int pagecount = 0;
            list = Db.SqlQueryable<MachineModel>(sql).Where(s=>s.NAME.Contains(keyword)  
            ||s.DELIVERYNUMBER.Contains(keyword)
            ||s.CONTRACT.Contains(keyword)
            || s.TYPESNAME.Contains(keyword)).OrderBy(s=>s.MID,OrderByType.Asc).ToPageList(p.PageIndex,p.PageSize, ref pagecount);


          

            p.PageCount = pagecount;

            return list.Count > 0;
        }


        public bool ListByWhere(string keyword, ref List<MachineModel> list)
        {
            string sql = @"select t.*, tr.name typesname 
  from tasm_machine t
  left join tasm_machine_type tr
    on t.types = tr.id"
           ;
      
            list = Db.SqlQueryable<MachineModel>(sql).Where(s => s.NAME.Contains(keyword)).OrderBy(s => s.MID, OrderByType.Asc).ToList();

            return list.Count > 0;
        }

        public bool  ListByType(int typeid, ref List<TASM_MACHINE> list)
        {

            list = TASM_MACHINERDb.GetList().Where(s => s.TYPES == typeid).ToList();

            return list.Count > 0;

        }



    }
}
