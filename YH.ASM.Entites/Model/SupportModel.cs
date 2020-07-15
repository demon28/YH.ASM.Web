using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Entites.Model
{
    public  class SupportModel:TASM_SUPPORT
    {

        public int PROJECTID { get; set; }

        public int CREATORID { get; set; }
        public int CONDUCTORID { get; set; }

        public string CREATORNAME { get; set; }
        public string CONDUCTORNAME { get; set; }
        public string PROJECTNAME { get; set; }
        public string PROJECTCODE { get; set; }
        

        public string MACHINENAME { get; set; }
        public string MACHINESERIAL { get; set; }
    }
}
