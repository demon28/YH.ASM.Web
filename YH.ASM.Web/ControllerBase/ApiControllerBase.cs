using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.ControllerBase
{
 
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApiControllerBase:TopControllerBase
    {

        public ApiControllerBase() {


        

        }

        /*
         * GET：查询                [HttpGet]
         * POST：创建单个资源。     [HttpPost]      post 集合只能接收一个 json 参数 
         * PUT：更新单个资源        [HttpPut]
         * DELETE：删除             [HttpDelete]
         *
         */



    }
}
