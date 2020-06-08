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

        #region
        // GET: api/<Login2Controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Login2Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Login2Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Login2Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Login2Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion




        [HttpPost]
        public JsonResult Login([FromBody] Models.LoginInputModel model)
        {


            TASM_USER usermodel = new TASM_USER();
            TASM_USERManager tASM_USERManager = new TASM_USERManager();

            if (!tASM_USERManager.LoginByUser(model.Username, Entites.Tool.MD5.Encrypt(model.Password), ref usermodel))
            {

                return FailMessage();
            
            }
            return SuccessResult(model, "成功");
        
        }


    }
}
