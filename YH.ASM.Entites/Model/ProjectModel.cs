using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Entites.Model
{
   public  class ProjectModel:TASM_PROJECT
    {

        public int TYPESID { get; set; }

        public int MACID { get; set; }


        public string TYPESNAME { get; set; }


        public string MACNAME { get; set; }
     

        public int DEBUGDAYS { get; set; }

        public int CHECKDAYS { get; set; }

        public int INSTALLDAYS { get; set; }



    }
}
