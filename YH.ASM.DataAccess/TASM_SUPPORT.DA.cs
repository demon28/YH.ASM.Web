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
                .Where(s => s.TITLE.Contains(keyword))
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

        public List<SupportListModel> ListByWhere(string keyword,ref PageModel p, SupprotWatchType watchType, SupprotWatchState state,  int? watchId,string order = "SID")
        {

            string sql = @"SELECT T.*,
       TR.USER_ID   CREATORID,
       TR.USER_NAME CREATORNAME,
       TU.USER_ID   CONDUCTORID,
       TU.USER_NAME CONDUCTORNAME,
       TP.PID PROJECTID,
       TP.NAME PROJECTNAME
       
  FROM TASM_SUPPORT T

  LEFT JOIN TASM_USER TR
    ON TR.USER_ID = T.CREATOR
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = T.CONDUCTOR
   LEFT JOIN TASM_PROJECT TP ON TP.PID=T.PROJECT

WHERE 1=1 

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

            int totalCount = 0;
            int totalPage = 0;
            List<SupportListModel> list = Db.SqlQueryable<SupportListModel>(sql)
                .Where(s => s.TITLE.Contains(keyword))
                .OrderBy(s=>s.SID,OrderByType.Desc)
                .AddParameters(configParms)
                .ToPageList(p.PageIndex, p.PageSize, ref totalCount, ref totalPage);
            p.PageCount = totalCount;


            return list;

        }




        public SupportListModel SelectById(int sid)
        {

            string sql = @"SELECT T.*,
       TR.USER_ID   CREATORID,
       TR.USER_NAME CREATORNAME,
       TU.USER_ID   CONDUCTORID,
       TU.USER_NAME CONDUCTORNAME,
       TP.PID PROJECTID,
       TP.NAME PROJECTNAME
       
  FROM TASM_SUPPORT T

  LEFT JOIN TASM_USER TR
    ON TR.USER_ID = T.CREATOR
  LEFT JOIN TASM_USER TU
    ON TU.USER_ID = T.CONDUCTOR
   LEFT JOIN TASM_PROJECT TP ON TP.PID=T.PROJECT

WHERE 1=1  and t.SID=:sid

";
            var configParms = new List<SugarParameter>();

            configParms.Add(new SugarParameter("sid", sid));


            SupportListModel model = Db.SqlQueryable<SupportListModel>(sql)  
                .AddParameters(configParms)
                .First();

            return model;


        }
    }

}

