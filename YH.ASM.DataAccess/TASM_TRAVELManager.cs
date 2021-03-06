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
    public class TASM_TRAVELManager : DbContext<TASM_TRAVEL>
    {


        public bool ListBaseUser(DateTime month, string keyword, ref PageModel p, ref List<DirectionModel> list)
        {
            string mounthstr = month.ToString("yyyy-MM");

            string sql = @"SELECT TU.USER_ID,
       TU.USER_NAME,
       TU.WORK_ID,
       TU.DEPARTMENT,
       TU.DTNAME,
       TU.MOBILE,
TU.DEPT1,
TU.DEPT2,
TU.DEPT3,
TU.DEPT4,
TU.DEPT5,
       
       B.MOUNTHCOUNT
  
  FROM (
  
  select userid,to_char(trunc(mdate,'mm'),'yyyy-mm') date_month,count(0) MOUNTHCOUNT from (
    
    select userid,trunc(createtime) mdate,count(0) dailycount from TASM_TRAVEL group by userid,trunc(createTime )
    
    ) tab WHERE to_char(trunc(mdate,'mm'),'yyyy-mm')=:mounth
    
     group by trunc(tab.mdate,'mm'),userid
         
         
         ) B
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = B.USERID ";


            int pagecount = 0;

            if (string.IsNullOrEmpty(keyword))
            {
                keyword = string.Empty;
            }

            list = Db.SqlQueryable<DirectionModel>(sql)
                .AddParameters(new
                {
                    mounth = mounthstr
                })
                .Where(s => s.USER_NAME.Contains(keyword)
                || s.DEPARTMENT.Contains(keyword)
                || s.WORK_ID.Contains(keyword)
                || s.DEPT2.Contains(keyword)
                || s.DEPT3.Contains(keyword)
                || s.DEPT4.Contains(keyword)
                || s.DEPT5.Contains(keyword))
                  .ToPageList(p.PageIndex, p.PageSize, ref pagecount);


            p.PageCount = pagecount;

            return list.Count > 0;

        }

        public bool ListByMonth(int userid, DateTime month, ref List<DirectionCanderModel> list)
        {

            //去掉月份条件查询
            string mounthstr = month.ToString("yyyy-MM");
            string sql = @"select t.user_id,t.user_name,tra.* from tasm_travel tra
            left join   tasm_user t  on t.user_id=tra.userid
            where tra.userid=:userid and  to_char( tra.createtime,'yyyy-mm') =:mounth  order by tra.createtime,tra.type desc ";

            list = Db.SqlQueryable<DirectionCanderModel>(sql)
                .AddParameters(new { userid = userid, mounth = mounthstr })
                .ToList();

            return list.Count > 0;


        }


        /// <summary>
        /// 用于导出Excel
        /// </summary>
        /// <param name="month"></param>
        /// <param name="keyword"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ListBaseUser(DateTime month, string keyword, ref List<DirectionModel> list)
        {
            string mounthstr = month.ToString("yyyy-MM");

            string sql = @"select tu.user_id,tu.user_name,tu.work_id,tu.department,tu.dtname ,tu.mobile, b.mounthcount from  
(
select userid ,count( to_char(t.createtime,'yyyy-mm-dd')) mounthcount from tasm_travel t  
where  t.type=0 and   to_char( t.createtime,'yyyy-mm') =:mounth  group by userid ) b  left join tasm_user tu on tu.user_id=b.userid";




            if (string.IsNullOrEmpty(keyword))
            {
                keyword = string.Empty;
            }

            list = Db.SqlQueryable<DirectionModel>(sql)
                .AddParameters(new
                {
                    mounth = mounthstr
                })
                .Where(s => s.USER_NAME.Contains(keyword) || s.DEPARTMENT.Contains(keyword) || s.WORK_ID.Contains(keyword))
                .ToList();



            return list.Count > 0;

        }





        public bool ListAllByDate(DateTime? month, string keyword, ref List<DirectionCanderModel> list)
        {
            string sql = @"SELECT t.*,
       tu.user_name  user_name,
       tu.work_id    work_id,
       tu.department department,
       tp.name       PROJECTNAME,
       tp.code       PROJECTCODE,
       tm.name       machinename,
       tm.serial     machineserial
  from tasm_travel t
  LEFT JOIN tasm_user tu
    ON t.userid = tu.user_id
  left join tasm_project tp
    on t.projectid = tp.pid
  LEFT JOIN tasm_machine tm
    ON t.machineid = tm.mid
 where 1 = 1  ";

            if (month != null)
            {
                string mounthstr = month.Value.ToString("yyyy-MM");

                sql += "  and  to_char( t.createtime,'yyyy-mm') ='" + mounthstr + "' ";
            }

            list = Db.SqlQueryable<DirectionCanderModel>(sql).ToList();

            return list.Count > 0;


        }


        public bool ListByUserId(int userid, ref SqlSugar.PageModel page, ref List<TASM_TRAVEL> list)
        {

            int pagecount = 0;
            list = Db.Queryable<TASM_TRAVEL>().Where(s => s.USERID == userid)
                .OrderBy(s => s.CREATETIME, OrderByType.Desc)
                .ToPageList(page.PageIndex, page.PageSize, ref pagecount);

            page.PageCount = pagecount;
            return list.Count > 0;

        }

        public bool SelectByDate(int userid, DateTime date, int type)
        {

            string day = date.ToString("yyyy-MM-dd");

            int i = Db.Queryable<TASM_TRAVEL>().Count(s => s.TYPE.Value == type && s.USERID == userid && s.CREATETIME.Date.Equals(date.Date));

            return i > 0;
        }

        /*  玛特 总又一天要改回来


        public DirectionDetailModel SelectByTraid(int traid) {


            string sql = @"SELECT t.traid, t.userid,t.type,t.supportid,t.longitude,t.latitude,t.content,t.status,t.createtime,t.address,
 
                            tu.user_name  user_name,
                            tp.name project_name , 
                            tc.name customer_name,
                            ta.area_name provincen_name,
                            tar.area_name city_name,
                            ts.title  support_title
                            from tasm_travel t
                            LEFT JOIN tasm_user tu ON t.userid =tu.user_id
                            LEFT JOIN tasm_project tp ON t.projectid=tp.pid
                            LEFT JOIN tasm_customer tc ON t.customerid=tc.cid
                            LEFT JOIN tnet_area ta ON t.provinceid=ta.area_id
                            LEFT JOIN tnet_area tar ON t.cityid=tar.area_id
                            LEFT JOIN tasm_support ts ON t.supportid=ts.sid


                        WHERE t.traid=:traid";


            DirectionDetailModel model = Db.SqlQueryable<DirectionDetailModel>(sql)
                .AddParameters(new{
                                     traid = traid
                                 })
                .First();

            return model;

        }


        */



        public DirectionCanderModel SelectByTraid(int traid)
        {
          
            string sql = @"
SELECT T.*,
       tp.name projectname,
       tp.code projectcode,
       tm.name machinename,
       tm.serial machineserial,
       TU.USER_NAME USER_NAME,
       tsup.code supportCode
  FROM TASM_TRAVEL T
  LEFT JOIN TASM_USER TU
    ON T.USERID = TU.USER_ID
  LEFT JOIN tasm_project tp
    ON t.projectid=tp.pid
  LEFT JOIN tasm_machine tm
    ON t.machineid=tm.mid
  LEFT JOIN tasm_support tsup
   ON t.supportid=tsup.sid  

 WHERE T.TRAID = :TRAID

";



            /*
                      string sql = @"
          SELECT T.*,
                 TU.USER_NAME USER_NAME,
                 tsup.code supportCode
            FROM TASM_TRAVEL T
            LEFT JOIN TASM_USER TU
              ON T.USERID = TU.USER_ID
            LEFT JOIN tasm_project tp
              ON t.projectid=tp.pid
            LEFT JOIN tasm_machine tm
              ON t.machineid=tm.mid
            LEFT JOIN tasm_support tsup
             ON t.supportid=tsup.sid  

           WHERE T.TRAID = :TRAID
                     
            ";
              */

            DirectionCanderModel model = Db.SqlQueryable<DirectionCanderModel>(sql)
                          .AddParameters(new
                          {
                              traid = traid
                          })
                          .First();
            return model;

        }
    }
}
