﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.Entites;
using YH.ASM.Entites.Model;
using YH.ASM.Web.Attribute;
using YH.ASM.Web.ControllerBase;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    public class SupportController : ApiControllerBase
    {
        
        [WebApi]
        [HttpPost]
        public IActionResult Create(SupportCreateModel model)
        {
            Facade.SupportFacade support = new Facade.SupportFacade();
            if (!support.Create(model))
            {
                return FailMessage(support.Msg);
            }
            return SuccessMessage("创建工单成功！");

        }


        [WebApi]
        [HttpGet]
        public IActionResult ListMyDaiBan(ListSupportInputModel model) {

            DataAccess.TASM_SUPPORT_Da manager = new DataAccess.TASM_SUPPORT_Da();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            List<SupportListModel> list = manager.ListByWhere(string.Empty, ref p, SupprotWatchType.处理人, SupprotWatchState.待办, model.Creator);

            return SuccessResultList(list, p);



        }



    }
}
