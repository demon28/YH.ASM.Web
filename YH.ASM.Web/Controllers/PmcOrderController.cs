﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YH.ASM.Web.Controllers
{
    public class PmcOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}