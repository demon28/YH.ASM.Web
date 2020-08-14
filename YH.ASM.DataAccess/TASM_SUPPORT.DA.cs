using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.DataAccess
{

    /// <summary>
    ///  工单信息表
    ///</summary>
    public class TASM_SUPPORT_Da : DbContext<TASM_SUPPORT>
    {


        public TASM_SUPPORT_Da()
        {

        }

        //关键字查找,自行修改或增加关键字 字段
        public List<TASM_SUPPORT> ListByWhere(string keyword, ref PageModel p)
        {

            int pageCount = 0;

            var list = Db.Queryable<TASM_SUPPORT>()
                .Where(s => s.CONTENT.Contains(keyword))
                .OrderBy(s => s.SID, OrderByType.Asc)
                .ToPageList(p.PageIndex, p.PageSize, ref pageCount);

            p.PageCount = pageCount;

            return list;
        }


        //关键字查找用于导出Excel
        public List<TASM_SUPPORT> ListByWhere(string keyword)
        {

            var list = Db.Queryable<TASM_SUPPORT>()
                .Where(s => s.TITLE.Contains(keyword))
                .OrderBy(s => s.SID, OrderByType.Asc)
                .ToList();

            return list;
        }

        public List<SupportModel> ListByWhere(string keyword,ref PageModel p, SupprotWatchType watchType, SupprotWatchState state,  int? watchId,string order = "SID")
        {

            string sql = @"SELECT T.*,
       TR.USER_ID   CREATORID,
       TR.USER_NAME CREATORNAME,
       TU.USER_ID   CONDUCTORID,
       TU.USER_NAME CONDUCTORNAME,
       TP.PID       PROJECTID,
       TP.NAME      PROJECTNAME,
       TP.CODE      PROJECTCODE,
       TM.NAME      MACHINENAME,
       TM.SERIAL    MACHINESERIAL

  FROM TASM_SUPPORT T

  LEFT JOIN TASM_USER TR
    ON TR.USER_ID = T.CREATOR
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = T.CONDUCTOR
  LEFT JOIN TASM_PROJECT TP
    ON TP.PID = T.PROJECT
  LEFT JOIN TASM_MACHINE TM
    ON TM.MID = T.MID

 WHERE 1 = 1
";
            var configParms = new List<SugarParameter>();

            if (watchType== SupprotWatchType.创建人)
            {
                sql += " and t.creator=:watchId ";
                configParms.Add( new SugarParameter("watchId", watchId.Value));
            }
            if (watchType == SupprotWatchType.处理人)
            {
                sql += " and  t.conductor=:watchId ";
                configParms.Add(new SugarParameter("watchId", watchId.Value));
            }
            if (state!= SupprotWatchState.全部)
            {
                sql += " and  t.state= "+(int)state;
            }
            if (!string.IsNullOrEmpty(keyword)) 
            {
                sql+= " and   (t.CODE like '%" + keyword + "%'  or  t.CONTENT like '%" + keyword+"%'  or  TR.USER_NAME like '%"+keyword+"%' or TU.USER_NAME like '%"+keyword+"%'  or  TP.NAME like '%"+keyword+"%') ";
            }


            int totalCount = 0;
            int totalPage = 0;
            List<SupportModel> list = Db.SqlQueryable<SupportModel>(sql)
                .OrderBy(s=>s.SID,OrderByType.Desc)
                .AddParameters(configParms)
                .ToPageList(p.PageIndex, p.PageSize, ref totalCount, ref totalPage);
            p.PageCount = totalCount;


            return list;

        }


        public SupportModel SelectById(int sid)
        {

            string sql = @"
SELECT T.*,
       TR.USER_ID   CREATORID,
       TR.USER_NAME CREATORNAME,
       TU.USER_ID   CONDUCTORID,
       TU.USER_NAME CONDUCTORNAME,
       TP.PID       PROJECTID,
       TP.NAME      PROJECTNAME,
       TP.CODE      PROJECTCODE,
       tm.name      MACHINENAME,
       tm.serial    MACHINESERIAL

  FROM TASM_SUPPORT T

  LEFT JOIN TASM_USER TR
    ON TR.USER_ID = T.CREATOR
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = T.CONDUCTOR
  LEFT JOIN TASM_PROJECT TP
    ON TP.PID = T.PROJECT
  left join tasm_machine tm
    on tm.mid = t.mid

WHERE 1=1  and t.SID=:sid

";
            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            SupportModel model = Db.SqlQueryable<SupportModel>(sql)  
                .AddParameters(configParms)
                .First();

            return model;


        }



        public SupportPushModel SelectBySid4Push(int sid) {


            string sql = @"
SELECT T.*,
       TR.USER_ID   CREATORID,
       TR.USER_NAME CREATORNAME,
       TR.WORK_ID   CreatorWorkId,
       
       TU.USER_ID   CONDUCTORID,
       TU.WORK_ID   ConductorWorkId,
       TU.USER_NAME CONDUCTORNAME,

       TP.PID       PROJECTID,
       TP.NAME      PROJECTNAME,
       TP.CODE      PROJECTCODE,
       tm.name      MACHINENAME,
       tm.serial    MACHINESERIAL

  FROM TASM_SUPPORT T

  LEFT JOIN TASM_USER TR
    ON TR.USER_ID = T.CREATOR
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = T.CONDUCTOR
  LEFT JOIN TASM_PROJECT TP
    ON TP.PID = T.PROJECT
  left join tasm_machine tm
    on tm.mid = t.mid

WHERE 1=1  and t.SID=:sid

";
            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            SupportPushModel model = Db.SqlQueryable<SupportPushModel>(sql)
                .AddParameters(configParms)
                .First();

            return model;

        }



        public int SelectSupprotCodeIndex() {

            string sql = "select SEQ_INDEX_SUPORT_CODE.Nextval from dual  ";

            var dataTable= Db.Ado.GetDataTable(sql);

            return Convert.ToInt32(dataTable.Rows[0][0]);

        }


        public SupportReportCountModel SelectCount() {

            string sql = @"select 
sum(decode(state,0,1,0)) as wait,
sum(decode(state,1,1,0)) as being ,
sum(decode(state,2,1,0)) as complete

from tasm_support";

            return Db.SqlQueryable<SupportReportCountModel>(sql).First();



        }


        public int SelectSupportCount() {

          return  Db.Queryable<TASM_SUPPORT>().Count();
        }


    }

}

