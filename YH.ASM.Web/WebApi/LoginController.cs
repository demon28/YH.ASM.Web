using System;
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
    public class LoginController : ControllerBase.ControllerBase
    {

        [HttpPost("Login")]
        public JsonResult Login([FromBody] Models.LoginInputModel model) {

            //第一版不验证MD5

            //if (!Entites.Tool.VerifyApiMd5.Check(model.SigningKey,model.Username,model.Password))
            //{
            //    return FailMessage("异常访问！");
            //}


            TASM_USER usermodel = new TASM_USER();
            TASM_USERManager tASM_USERManager = new TASM_USERManager();

            if (!tASM_USERManager.LoginByUser(model.Username, Entites.Tool.MD5.Encrypt(model.Password), ref usermodel))
            {
                return FailMessage();
            }
            return SuccessResult(usermodel, "成功");
        }


    }
}