﻿
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
namespace YH.ASM.DataAccess.CodeGenerator
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

    }

}