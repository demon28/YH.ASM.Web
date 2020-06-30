
using SqlSugar;
using System;
using System.Collections.Generic;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

public class TPMS_USER_RIGHTManager : DbContext<TPMS_USER_RIGHT>
{






    /// <summary>
    /// 查询页面
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="p"></param>
    /// <param name="list"></param>
    /// <returns></returns>
   

    public bool AccountListByWhere(string keyword, ref PageModel p, ref List<UserPmsModel> list)
    {

        string sql = @"SELECT b.* FROM tasm_user b WHERE b.user_id IN  (

SELECT  tu.user_id from tasm_user tu 

LEFT  JOIN   tpms_user_right  tr  ON tu.user_id=tr.user_id  

LEFT  JOIN   tpms_role  te  ON tr.role_id=te.role_id

WHERE te.app_id=1 and  tr.role_id !=4

) ";
        int pagecount = 0;

        list= Db.SqlQueryable<UserPmsModel>(sql).Where(it => it.USER_NAME.Contains(keyword)).ToPageList(p.PageIndex, p.PageSize, ref pagecount);
        p.PageCount = pagecount;

        return list.Count>0;

    }


    public bool SelectByUseridRoleid(int uesrid, int roleid) {

        TPMS_USER_RIGHT model = new TPMS_USER_RIGHT();

        model = Db.Queryable<TPMS_USER_RIGHT>().First(it => it.USER_ID == uesrid && it.ROLE_ID == roleid);

        return model != null;
    }


    /// <summary>
    /// 根据用户id 查询该用户有哪些角色
    /// </summary>
    /// <param name="uesrid"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public bool ListByUserid(int uesrid,ref List<TPMS_USER_RIGHT> list)
    {

        list = CurrentDb.GetList(it => it.USER_ID == uesrid);

        return list.Count > 0;
    }

    public bool ListByPmsView(int userid, ref List<PMSViewModel> list) {

        string sql = "select t.* from vpms_asm t ";
        list = Db.SqlQueryable<PMSViewModel>(sql).Where(s=>s.USER_ID==userid).ToList();

       return list.Count > 0;
    }

    public bool ListByPmsPageView(int userid, string pageurl,ref List<PMSViewModel> list)
    {

        string sql = "select t.* from vpms_asm t ";
        list = Db.SqlQueryable<PMSViewModel>(sql).Where(s => s.USER_ID == userid && s.PAGE_URL== pageurl).ToList();

        return list.Count > 0;
    }

    /// <summary>
    /// 根据页面查出 该页面哪些角色有 权限访问
    /// </summary>
    /// <param name="pageurl"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public List<RoleRightModel>  ListRoleRightPageView(int userid,string pageurl )
    {
        string sql = @"SELECT T.*
  FROM TPMS_USER_RIGHT T
 WHERE T.USER_ID = :userid
   AND T.ROLE_ID IN (SELECT TR.ROLE_ID
                       FROM VPMS_ROLE_RIGHT TR
                      WHERE TR.PAGE_URL = :pageurl)";


        var configParms = new List<SugarParameter>();

        configParms.Add(new SugarParameter("userid", userid));
        configParms.Add(new SugarParameter("pageurl", pageurl));

        return Db.SqlQueryable<RoleRightModel>(sql).AddParameters(configParms).ToList();
    }

}