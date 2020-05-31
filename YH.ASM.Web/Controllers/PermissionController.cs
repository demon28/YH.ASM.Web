using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using NPOI.SS.Formula.Functions;
using SqlSugar;
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.Web.Controllers
{

    [Authorize]
    public class PermissionController : ControllerBase.ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddAccount()
        {
            return View();
        }
       

        #region  角色模块

        [HttpPost]
        public IActionResult GetRoleList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_ROLEManager manager = new TPMS_ROLEManager();
            PageModel p = new PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_ROLE> list = new List<TPMS_ROLE>();
            manager.RoleListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [HttpPost]
        public IActionResult AddRole(string rolename)
        {

            if (string.IsNullOrEmpty(rolename))
            {
                return FailMessage("请填写角色名！");
            }

            TPMS_ROLE rolemodel = new TPMS_ROLE();
            rolemodel.APP_ID = 1;
            rolemodel.GUID = Guid.NewGuid().ToString("N");
            rolemodel.ROLE_NAME = rolename;


            DbContext<TPMS_ROLE> dbContext = new DbContext<TPMS_ROLE>();
            if (!dbContext.Insert(rolemodel))
            {
                   return FailMessage("新建角色失败！");
            }
            return SuccessMessage();
        }


        [HttpPost]
        public IActionResult DelRole(int roleid) {

            TPMS_ROLEManager dbContext =new  TPMS_ROLEManager();
            TPMS_ROLE rolemodel = new TPMS_ROLE();
            rolemodel=dbContext.GetById(roleid);

            if (rolemodel==null)
            {
                return FailMessage("未查询到删除项！");
            }

            if (!dbContext.Delete(rolemodel))
            {
                return FailMessage();
            }
            return SuccessMessage();
        }


        public IActionResult RoleOperation()
        {
            return View();
        }
        #endregion



        #region 功能模块


        [HttpPost]
        public IActionResult GetFuncList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_USER_RIGHTManager tASM_USERManager = new TPMS_USER_RIGHTManager();
            PageModel p = new PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_FUNCTION> list = new List<TPMS_FUNCTION>();
            tASM_USERManager.FuncListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [HttpPost]
        public IActionResult AddFunc(string funcname)
        {

            if (string.IsNullOrEmpty(funcname))
            {
                return FailMessage("请填写角色名！");
            }

            TPMS_FUNCTION funcmodel = new TPMS_FUNCTION();
            funcmodel.APP_ID = 1;
            funcmodel.GUID = Guid.NewGuid().ToString("N");
            funcmodel.FUNC_NAME = funcname;


            DbContext<TPMS_FUNCTION> dbContext = new DbContext<TPMS_FUNCTION>();
            if (!dbContext.Insert(funcmodel))
            {
                return FailMessage("新建角色失败！");
            }
            return SuccessMessage();
        }


        [HttpPost]
        public IActionResult DelFunc(int roleid)
        {
            DbContext<TPMS_FUNCTION> dbContext = new DbContext<TPMS_FUNCTION>();
            if (!dbContext.Delete(roleid))
            {
                return FailMessage();
            }
            return SuccessMessage();
        }


        public IActionResult FuncOperation()
        {
            return View();
        }
        #endregion


        #region 页面模块


        [HttpPost]
        public IActionResult GetPageList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_USER_RIGHTManager tASM_USERManager = new TPMS_USER_RIGHTManager();
            PageModel p = new PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_PAGE> list = new List<TPMS_PAGE>();
            tASM_USERManager.PageListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [HttpPost]
        public IActionResult AddPage(string pagename,string pageurl,int pagetype)
        {

            if (string.IsNullOrEmpty(pagename))
            {
                return FailMessage("请填写角色名！");
            }

            TPMS_PAGE pagemodel = new TPMS_PAGE();
            pagemodel.APP_ID = 1;
            pagemodel.GUID = Guid.NewGuid().ToString("N");
            pagemodel.PAGE_NAME = pagename;
            pagemodel.PAGE_URL = pageurl;
            pagemodel.RESOUCE_TYPE = pagetype;


            DbContext<TPMS_PAGE> dbContext = new DbContext<TPMS_PAGE>();
            if (!dbContext.Insert(pagemodel))
            {
                return FailMessage("新建页面失败！");
            }
            return SuccessMessage();
        }


        [HttpPost]
        public IActionResult DelPage(int pageid)
        {
            DbContext<TPMS_PAGE> dbContext = new DbContext<TPMS_PAGE>();


            if (!dbContext.CurrentDb.DeleteById(pageid))
            {
                return FailMessage();
            }
            return SuccessMessage();
        }


        public IActionResult PageOperation()
        {
            return View();
        }
        #endregion



        #region 管理员模块

        [HttpPost]
        public IActionResult GetAccountList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_USER_RIGHTManager tASM_USERManager = new TPMS_USER_RIGHTManager();
            PageModel p = new PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<UserPmsModel> list = new List<UserPmsModel>();
            tASM_USERManager.AccountListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        public IActionResult GetRoleAll() {

            TPMS_ROLEManager manager = new TPMS_ROLEManager();

            List<TPMS_ROLE> list = new List<TPMS_ROLE>();

            manager.RoleListByAll(ref list);

            return SuccessResultList(list);

        }

        public IActionResult  InsertAccount(int userid,int roleid)
        {
            //TODO: 添加之前需要先查询是否存在！


            TPMS_USER_RIGHTManager manager = new TPMS_USER_RIGHTManager();

            TPMS_USER_RIGHT model = new TPMS_USER_RIGHT();
            model.ROLE_ID = roleid;
            model.USER_ID = userid;

            if (!manager.Insert(model))
            {
                return FailMessage(" 添加失败");
            }
            return SuccessMessage();
        }


        public IActionResult GetSingAccount(string where) {

            TASM_USERManager manager = new TASM_USERManager();

            TASM_USER model = new TASM_USER();
            if (!manager.SingeByUser(where, ref model))
            {
                return FailMessage();
            }

            return SuccessResult(model);

        }


        public IActionResult DelAccount(int rightid) {

            TPMS_USER_RIGHTManager dbContext = new TPMS_USER_RIGHTManager();
            if (!dbContext.CurrentDb.DeleteById(rightid))
            {
                return FailMessage();
            }
            return SuccessMessage();

        }

        #endregion


    }



}