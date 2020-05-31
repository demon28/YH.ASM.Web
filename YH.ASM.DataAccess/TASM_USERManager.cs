
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
namespace YH.ASM.DataAccess
{
    public class TASM_USERManager : DbContext<TASM_USER>
    {

        public TASM_USERManager()
        {
        
        }

        public bool LoginByUser(string workid,string pwd,ref TASM_USER model)
        {
             model =Db.Queryable<TASM_USER>().First(it => it.WORK_ID == workid && it.USER_PWD ==pwd);
             return model != null;
        }

        public bool SingeByUser(string str,ref TASM_USER model)
        {
            model = Db.Queryable<TASM_USER>().First(it => it.WORK_ID.Contains(str) ||  it.USER_NAME.Contains(str));
            return model != null;
        }


        public bool GetListByWhere(string keyword,ref PageModel p, ref List<TASM_USER> list) 
        {

            list = TASM_USERDb.GetPageList(it =>it.USER_NAME.Contains(keyword) || it.WORK_ID.Contains(keyword), p, it => it.WORK_ID, OrderByType.Asc);

            return list.Count > 0;
        }

        public bool GetAll(string keyword, ref List<TASM_USER> list) {

            if (string.IsNullOrEmpty(keyword))
            {
                keyword = string.Empty;
            }

            list= TASM_USERDb.GetList().Where(it => it.USER_NAME.Contains(keyword) || it.WORK_ID.Contains(keyword)).OrderBy(s=>s.WORK_ID).ToList();
            return list.Count > 0;
        }
    }

}