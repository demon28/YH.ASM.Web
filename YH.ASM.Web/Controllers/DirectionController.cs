﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{

    //动向记录
    public class DirectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}