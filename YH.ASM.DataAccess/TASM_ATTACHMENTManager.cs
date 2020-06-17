﻿using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.DataAccess
{
     public  class TASM_ATTACHMENTManager : DbContext<TASM_ATTACHMENT>
    {

        public bool ListByPid(int pid,ref List<TASM_ATTACHMENT> list)
        {

            list = Db.Queryable<TASM_ATTACHMENT>().Where(s => s.PID == pid).ToList();

            return list.Count > 0;


        }




    }
}
