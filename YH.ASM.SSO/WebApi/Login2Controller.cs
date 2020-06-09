using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YH.ASM.SSO.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login2Controller : ControllerBase.ControllerBase
    {



        [HttpPost("{value}")]
        public JsonResult POST(string value)
        {

            return SuccessResult(value, "成功");

        }

      

        [HttpPost("Login")]
        public JsonResult Login([FromBody] Models.LoginInputModel model)
        {


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
