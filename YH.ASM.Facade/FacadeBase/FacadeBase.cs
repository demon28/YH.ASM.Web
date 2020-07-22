
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using YH.ASM.Entites;
using Microsoft.Extensions.DependencyInjection;

namespace YH.ASM.Facade.FacadeBase
{
   public class FacadeBase
    {
      
        public string Msg { get; set; }

        public ILogger Logger { get; set; }

        public FacadeBase() {

            Logger = LoggerHelper.ServiceProvider.GetService<ILogger<FacadeBase>>();
        }
     
    }
}
