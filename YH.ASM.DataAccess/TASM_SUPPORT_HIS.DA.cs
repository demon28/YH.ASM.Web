using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace  YH.ASM.DataAccess
{

    /// <summary>
    ///  工单处理人变更历史
    ///</summary>
    public class TASM_SUPPORT_HIS_Da : DbContext<TASM_SUPPORT_HIS>
    {
        

       public TASM_SUPPORT_HIS_Da()
       {
      
       }

       //关键字查找,自行修改或增加关键字 字段
       public List<SupportHisModel> List(int Sid)
       {
            
            string sql = @"select t.*, tr.user_name prename, tu.user_name nextname
  from tasm_support_his t
  left join tasm_user tr
    on tr.user_id = t.pre_user
  left join tasm_user tu
    on tu.user_id=t.next_user
    where t.sid=:sid
  order by t.createtime desc  

";
            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", Sid));


            return Db.SqlQueryable<SupportHisModel>(sql).AddParameters(configParms).ToList();
        }

       


    }

}

