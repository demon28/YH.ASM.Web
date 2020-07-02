using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{

    /// <summary>
    ///  用户角色关联表
    ///</summary>
    public class TRIGHT_USER_ROLE_Da : DbContext<TRIGHT_USER_ROLE>
    {
  

       public TRIGHT_USER_ROLE_Da()
       {
      
       }
        
        public List<VRIGHT_ASM> ListByVm(int userid, string pageurl)
        {

            return Db.Queryable<VRIGHT_ASM>().Where(s => s.USERID == userid && s.PAGEURL.ToLower() == pageurl.ToLower()).ToList();

        }
    }

}

