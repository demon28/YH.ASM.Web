using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YH.ASM.Entites;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class SystemController : ComControllerBase
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public SystemController(IWebHostEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
        }

        [Right(PowerName = "系统配置")]
        public IActionResult Index()
        {
            return View();
        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult LoadSupportPush()
        {
            return SuccessResult(AppConfig.IsPush);
        }


        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult SwitchSupportPush() {

          
            string contentPath = _hostingEnvironment.ContentRootPath + @"\"+ "appsettings.json"; ;

            JObject jsonObject;
            using (StreamReader file = new StreamReader(contentPath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonObject = (JObject)JToken.ReadFrom(reader);
                bool IsPush = bool.Parse(jsonObject.Root["IsPush"].ToString());

                jsonObject["IsPush"] = (!IsPush).ToString();
            }

            using (var writer = new StreamWriter(contentPath))
            using (JsonTextWriter jsonwriter = new JsonTextWriter(writer))
            {
                jsonwriter.Formatting = Formatting.Indented;
                jsonObject.WriteTo(jsonwriter);
            }

            bool l= AppConfig.IsPush;

            return SuccessResult(l);
        }

    }
}
