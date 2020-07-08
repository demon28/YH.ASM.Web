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
    ///  工单个人处理表
    ///</summary>
    public class TASM_SUPPORT_PERSONAL_Da : DbContext<TASM_SUPPORT_PERSONAL>
    {
  

       public TASM_SUPPORT_PERSONAL_Da()
       {
      
       }

        public List<PersonalSupportListModel> ListByWhere(string keyword, ref PageModel p, SupprotWatchType watchType, SupprotWatchState state, int Uuid)
        {

            string sql = @"   

SELECT T.STATUS    PSTATUS,
     T.ID          ID,
     T.CID         CID,
     T.DID         DID,
     T.CREATETIME  PCREATETIME,
     T.TID         TID,
     TS.SID,
     TS.TITLE,
     TS.CONTENT,
     TS.CREATOR,
     TS.TYPE,
     TS.SEVERITY,
     TS.PRIORITY,
     TS.FINDATE,
     TS.CONDUCTOR,
     TS.PROJECT,
     TS.STATUS     STATUS,
     TS.CREATETIME,
     TS.MEMBERID,
     TS.STATE,
     TS.MID,
     TS.CODE,
     TR.USER_ID    CREATORID,
     TR.USER_NAME  CREATORNAME,
     TU.USER_ID    CONDUCTORID,
     TU.USER_NAME  CONDUCTORNAME,
     TP.PID        PROJECTID,
     TP.NAME       PROJECTNAME,
     TM.NAME       MACHINENAME,
     TM.SERIAL       MACHINESERIAL

FROM TASM_SUPPORT_PERSONAL T

LEFT JOIN TASM_SUPPORT TS
  ON T.SID = TS.SID
LEFT JOIN TASM_USER TR
  ON TR.USER_ID = TS.CREATOR
LEFT JOIN TASM_USER TU
  ON TU.USER_ID = TS.CONDUCTOR
LEFT JOIN TASM_PROJECT TP
  ON TP.PID = TS.PROJECT
LEFT JOIN TASM_MACHINE TM
  ON TM.MID = TS.MID

where 1=1

";


            var configParms = new List<SugarParameter>();

            if (watchType == SupprotWatchType.创建人)
            {
                sql += " and T.CID =:watchId ";

                configParms.Add(new SugarParameter("watchId", Uuid));
            }
            if (watchType == SupprotWatchType.处理人)
            {
                sql += " and T.DID =:watchId ";
                configParms.Add(new SugarParameter("watchId", Uuid));
            }
            if (state != SupprotWatchState.全部)
            {
                sql += " and  T.STATUS= " + (int)state;

            }

            int totalCount = 0;
            int totalPage = 0;
            List<PersonalSupportListModel> list = Db.SqlQueryable<PersonalSupportListModel>(sql)
                .OrderBy(s => s.SID, OrderByType.Desc)
                .AddParameters(configParms)
                .ToPageList(p.PageIndex, p.PageSize, ref totalCount, ref totalPage);
            p.PageCount = totalCount;

            return list;

        }

    }

}

