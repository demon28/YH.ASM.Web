using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{

    /// <summary>
    ///  权限表
    ///</summary>
    public class TRIGHT_POWER_Da : DbContext<TRIGHT_POWER>
    {
  

       public TRIGHT_POWER_Da()
       {
      
       }

        public List<TRIGHT_POWER> ListOderBy()
        {

            string sql = @"SELECT tr.*
FROM tright_power tr 

start with tr.parentid =0 
  connect by prior tr.id = tr.parentid
  order siblings by tr.id ";

            return Db.SqlQueryable<TRIGHT_POWER>(sql).ToList();


        }


    }

}

