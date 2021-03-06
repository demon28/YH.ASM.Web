﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.WebApi
{

    

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase.ComControllerBase
    {

        [HttpPost("Login")]
        public JsonResult Login([FromBody] Models.LoginInputModel model) 
        {

            TASM_USER usermodel = new TASM_USER();
            TASM_USERManager tASM_USERManager = new TASM_USERManager();


            if (!tASM_USERManager.SelectByWorkId(model.Username))
            {
                return FailMessage("用户名不存在！");
            }



            if (!tASM_USERManager.LoginByUser(model.Username, Entites.Tool.MD5.Encrypt(model.Password), ref usermodel))
            {
                return FailMessage("用户名或密码错误！");
            }
            return SuccessResult(usermodel, "成功");
        }


    }
}