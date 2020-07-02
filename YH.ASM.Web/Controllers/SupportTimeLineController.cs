﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class SupportTimeLineController : ControllerBase.ControllerBase
    {
    
        [Right]
        public IActionResult Index()
        {
            return View();
        }


        [Right]
        public IActionResult TimeLine()
        {
            return View();

        }


        [Right]

        public IActionResult WorkFolw()
        {
            return View();

        }


        [Right]
        [HttpPost]
        public IActionResult ListByHis(int sid)
        {

            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            List<TASM_SUPPORT_HIS> list = manager.Db.Queryable<TASM_SUPPORT_HIS>().Where(s => s.SID == sid).OrderBy(s => s.CREATETIME,SqlSugar.OrderByType.Asc).ToList();
            return SuccessResultList(list);
        }


        #region  工作流

        [Right]
        [HttpPost]
        public IActionResult GetDisposerInfo(int sid,int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisDisposerModel model = manager.SelectHisDisposer(sid, tid);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }


        [Right]
        [HttpPost]
        public IActionResult GetPmcInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPmcModel model = manager.SelectHisPmc(sid, tid);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }

        [Right]
        [HttpPost]
        public IActionResult GetSiteInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisSiteModel model = manager.SelectHisSite(sid, tid);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }


        [Right]
        [HttpPost]
        public IActionResult GetPrincipalInfo(int sid, int tid)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPrincipalModel model = manager.SelectHisPrincipal(sid, tid);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }


        #endregion



        /*  单项修改

       




        [HttpPost]
        public IActionResult GetDisposerInfo(int id)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisDisposerModel model = manager.SelectHisDisposer(id);

            if (model==null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }

        [HttpPost]
        public IActionResult GetPmcInfo(int id)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPmcModel model = manager.SelectHisPmc(id);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);

        }

        [HttpPost]
        public IActionResult GetSiteInfo(int id)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisSiteModel model = manager.SelectHisSite(id);
            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }
        [HttpPost]
        public IActionResult GetPrincipalInfo(int id)
        {
            DataAccess.TASM_SUPPORT_HIS_Da manager = new DataAccess.TASM_SUPPORT_HIS_Da();
            HisPrincipalModel model = manager.SelectHisPrincipal(id);

            if (model == null)
            {
                return FailMessage("未创建");
            }
            return SuccessResult(model);
        }

       
        */
        
    }
}
