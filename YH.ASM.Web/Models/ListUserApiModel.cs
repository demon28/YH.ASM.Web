﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YH.ASM.Web.Models
{
    public class ListUserApiModel:ApiModelBase
    {

        public int pageindex { get; set; }

        public int pagesize { get; set; }

        public string keywords { get; set; }


    }
}
