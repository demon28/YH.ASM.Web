using NPOI.OpenXml4Net.OPC.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.Models
{
    public class DirectionListModel:ApiModelBase
    {
        public int userid { get; set; }
        public int pageindex { get; set; }
        public int pagesize{ get; set; }


    }
}
