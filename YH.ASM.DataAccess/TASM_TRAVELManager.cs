using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.DataAccess
{
   public class TASM_TRAVELManager : DbContext<TASM_TRAVEL>
    {


        public bool ListBaseUser(DateTime month, string keyword,ref PageModel p, ref List<DirectionModel> list)
        {
            string mounthstr = month.ToString("yyyy-MM");

            string sql = @"select tu.user_id,tu.user_name,tu.work_id,tu.department,tu.dtname ,tu.mobile, b.mounthcount from  
(
select userid ,count( to_char(t.createtime,'yyyy-mm-dd')) mounthcount from tasm_travel t  
where  t.type=0 and   to_char( t.createtime,'yyyy-mm') =:mounth  group by userid ) b  left join tasm_user tu on tu.user_id=b.userid";


            int pagecount = 0;

            if (string.IsNullOrEmpty(keyword))
            {
                keyword = string.Empty;
            }

            list = Db.SqlQueryable<DirectionModel>(sql)
                .AddParameters(new {
                    mounth= mounthstr
                })
                .Where(s => s.USER_NAME.Contains(keyword) || s.DEPARTMENT.Contains(keyword) || s.WORK_ID.Contains(keyword))
                  .ToPageList(p.PageIndex, p.PageSize, ref pagecount);


            p.PageCount = pagecount;

            return list.Count>0;
        
        }

        public bool ListByMonth(int userid ,DateTime month, ref List<DirectionCanderModel> list) {

            //去掉月份条件查询
            string mounthstr = month.ToString("yyyy-MM");
            string sql = @"select t.user_id,t.user_name,tra.* from tasm_travel tra
            left join   tasm_user t  on t.user_id=tra.userid
            where tra.userid=:userid and  to_char( tra.createtime,'yyyy-mm') =:mounth  order by tra.createtime,tra.type desc ";

            list = Db.SqlQueryable<DirectionCanderModel>(sql)
                .AddParameters(new { userid = userid, mounth = mounthstr })
                .ToList();

            return list.Count > 0;



            //string sql = @"select t.user_id,t.user_name,tra.* from tasm_travel tra
            //left join   tasm_user t  on t.user_id=tra.userid
            //where tra.userid=:userid order by tra.createtime,tra.type desc ";

            //list = Db.SqlQueryable<DirectionCanderModel>(sql)
            //    .AddParameters(new { userid = userid})
            //    .ToList();

            //return list.Count > 0;

        }

    }
}
