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




        #region  时间轴暂时作废

        public HisDisposerModel SelectHisDisposer(int sid)
        {


            string sql = @"select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks reamrks,
t.next_status next_status,
td.id tid, 
tuse.user_name  analyzeuser,
td.analyze analyze,  
td.solution solution,
td.responsible responsible,
td.duty  duty
from tasm_support_his t 
left join tasm_support_disposer td  on t.tid=td.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 
left join tasm_user tuse on tuse.user_id=td.analyzeuser

where t.type=1 and t.sid=:sid

";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            return Db.SqlQueryable<HisDisposerModel>(sql).AddParameters(configParms).First();
        }


        public HisPmcModel SelectHisPmc(int sid)
        {


            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 
tp.id tid,
tp.bom bom,
decode(tp.isbook,0,'不需要下单',1,'需要下单' ) isbook,
tp.bookdate bookdate,
tp.bookuser bookuser,
tp.bookno bookno,
tp.delivery delivery,
tp.senddate senddate,
tp.sendno sendno,
tp.consignee consignee,
tp.remarks remarks
from tasm_support_his t 

left join tasm_support_pmc tp on t.tid=tp.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=2 and  t.sid=:sid

";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            return Db.SqlQueryable<HisPmcModel>(sql).AddParameters(configParms).First();
        }


        public HisSiteModel SelectHisSite(int sid)
        {

            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 

ts.id tid,
ts.enddate enddate,
ts.description description,
ts.reamrks remarks

from tasm_support_his t 
left join tasm_support_site ts on t.tid=ts.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=3 and t.sid=:sid
";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            return Db.SqlQueryable<HisSiteModel>(sql).AddParameters(configParms).First();


        }



        public HisPrincipalModel SelectHisPrincipal(int sid)
        {

            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 
tp.id tid,

tp.enddate enddate,
tp.checkuser checkuser,
tp.result result,
tp.remarks remarks

from tasm_support_his t 
left join tasm_support_principal tp on t.tid=tp.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=4 and t.sid=:sid
";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            return Db.SqlQueryable<HisPrincipalModel>(sql).AddParameters(configParms).First();


        }

        #endregion



        public HisDisposerModel SelectHisDisposer(int sid,int tid)
        {


            string sql = @"select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks reamrks,
t.next_status next_status,
td.id tid, 
tuse.user_name  analyzeuser,
td.analyze analyze,  
td.solution solution,
td.responsible responsible,
td.duty  duty
from tasm_support_his t 
left join tasm_support_disposer td  on t.tid=td.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 
left join tasm_user tuse on tuse.user_id=td.analyzeuser

where t.type=1 and t.sid=:sid and tid=:tid

";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));
            configParms.Add(new SugarParameter("tid", tid));

            return Db.SqlQueryable<HisDisposerModel>(sql).AddParameters(configParms).First();
        }


        public HisPmcModel SelectHisPmc(int sid, int tid)
        {


            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 
tp.id tid,
tp.bom bom,
decode(tp.isbook,0,'不需要下单',1,'需要下单' ) isbook,
tp.bookdate bookdate,
tp.bookuser bookuser,
tp.bookno bookno,
tp.delivery delivery,
tp.senddate senddate,
tp.sendno sendno,
tp.consignee consignee,
tp.remarks remarks
from tasm_support_his t 

left join tasm_support_pmc tp on t.tid=tp.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=2 and  t.sid=:sid and tid=:tid

";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));
            configParms.Add(new SugarParameter("tid", tid));

            return Db.SqlQueryable<HisPmcModel>(sql).AddParameters(configParms).First();
        }


        public HisSiteModel SelectHisSite(int sid,int tid)
        {

            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 

ts.id tid,
ts.enddate enddate,
ts.description description,
ts.reamrks remarks

from tasm_support_his t 
left join tasm_support_site ts on t.tid=ts.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=3 and t.sid=:sid and tid=:tid
";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));
            configParms.Add(new SugarParameter("tid", tid));

            return Db.SqlQueryable<HisSiteModel>(sql).AddParameters(configParms).First();


        }



        public HisPrincipalModel SelectHisPrincipal(int sid,int tid)
        {

            string sql = @"
select 
t.sid sid,
tu.user_name pre_user,
tus.user_name next_user,
t.createtime createtime,
t.remarks hisreamrks,
t.next_status next_status, 
tp.id tid,

tp.enddate enddate,
tp.checkuser checkuser,
tp.result result,
tp.remarks remarks

from tasm_support_his t 
left join tasm_support_principal tp on t.tid=tp.id
left join tasm_user tu on tu.user_id=t.pre_user 
left join tasm_user tus on tus.user_id=t.next_user 

where t.type=4 and t.sid=:sid and tid=:tid
";

            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));
            configParms.Add(new SugarParameter("tid", tid));

            return Db.SqlQueryable<HisPrincipalModel>(sql).AddParameters(configParms).First();


        }



    }

}

