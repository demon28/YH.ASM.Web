using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.Formula.Functions;
using SqlSugar;
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator.DBCore;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.ControllerBase;

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
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_ROLE> list = new List<TPMS_ROLE>();
            manager.RoleListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }


        [AuthRight]
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
            rolemodel.CREATE_TIME = DateTime.Now;

            DbContext<TPMS_ROLE> dbContext = new DbContext<TPMS_ROLE>();
            if (!dbContext.Insert(rolemodel))
            {
                return FailMessage("新建角色失败！");
            }
            return SuccessMessage();
        }

        [AuthRight]
        [HttpPost]
        public IActionResult DelRole(int roleid) {

            TPMS_ROLEManager dbContext = new TPMS_ROLEManager();
            TPMS_ROLE rolemodel = new TPMS_ROLE();
            rolemodel = dbContext.GetById(roleid);

            if (rolemodel == null)
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

        public IActionResult GetFuncAll(){

         
            TPMS_FUNCManager manager = new TPMS_FUNCManager();

            List<TPMS_FUNCTION> list = new List<TPMS_FUNCTION>();
            
            manager.FuncListByAll(ref list);

            return SuccessResultList(list);

        }
        
        public IActionResult ListByRoleid(int roleid)
        {
            TPMS_ROLE_RIGHTManager manager = new TPMS_ROLE_RIGHTManager();

            List<TPMS_ROLE_RIGHT> list = new List<TPMS_ROLE_RIGHT>();

            manager.ListByRoleid(roleid, ref list);

            return SuccessResultList(list);

        }

        [AuthRight]
        public IActionResult SaveFuncRole(int[] funclist,int roleid)
        {
            TPMS_ROLE_RIGHTManager manager = new TPMS_ROLE_RIGHTManager();

            List<TPMS_ROLE_RIGHT> list = new List<TPMS_ROLE_RIGHT>();

            if (funclist.Length <= 0)
            {
                return FailMessage("若要去除该角色所以权限，请删除该角色即可！");
            }

            try
            {

                manager.Db.Ado.BeginTran();


                if (manager.CurrentDb.GetList(s => s.ROLE_ID == roleid).Count()>0)
                {
                    //删除该用户所有角色
                    if (!manager.CurrentDb.Delete(s => s.ROLE_ID == roleid))
                    {
                        return FailMessage("更新失败！");
                    }
                }

             


                for (int i = 0; i < funclist.Length; i++)
                {
                    TPMS_ROLE_RIGHT umodel = new TPMS_ROLE_RIGHT();
                    umodel.ROLE_ID = roleid;
                    umodel.MEMBER_TYPE = 3;
                    umodel.MEMBER_ID = funclist[i];
                    umodel.CRETAE_TIME = DateTime.Now;
                    umodel.HAVE_RIGHT = 1;
                    list.Add(umodel);
                }

                if (!manager.Insert(list))
                {
                    return FailMessage("更新失败！");
                }


                manager.Db.Ado.CommitTran();


                return SuccessMessage();
            }
            catch (Exception ex)
            {
                manager.Db.Ado.RollbackTran();
                return FailMessage("更新失败！");
                throw ex;
            }


        }



     
        #endregion



        #region 功能模块


        [HttpPost]
        public IActionResult GetFuncList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_USER_RIGHTManager tASM_USERManager = new TPMS_USER_RIGHTManager();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_FUNCTION> list = new List<TPMS_FUNCTION>();
            tASM_USERManager.FuncListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [AuthRight]
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
            funcmodel.CREATETIME = DateTime.Now;

            DbContext<TPMS_FUNCTION> dbContext = new DbContext<TPMS_FUNCTION>();
            if (!dbContext.Insert(funcmodel))
            {
                return FailMessage("新建角色失败！");
            }
            return SuccessMessage();
        }


        [HttpPost]
        public IActionResult DelFunc(int funcid)
        {
            DbContext<TPMS_FUNCTION> dbContext = new DbContext<TPMS_FUNCTION>();
            if (!dbContext.CurrentDb.Delete(s=>s.FUNC_ID== funcid))
            {
                return FailMessage();
            }
            return SuccessMessage();
        }


        public IActionResult FuncOperation()
        {
            return View();
        }


        public IActionResult GetPageAll()
        {


            TPMS_PAGEManager manager = new TPMS_PAGEManager();

            List<TPMS_PAGE> list = new List<TPMS_PAGE>();

            manager.GetPageAll(ref list);

            return SuccessResultList(list);

        }
        public IActionResult PageListByFunc(int funcid)
        {


            TPMS_FUNC_MEMBERManager manager = new TPMS_FUNC_MEMBERManager();

            List<TPMS_FUNC_MEMBER> list = new List<TPMS_FUNC_MEMBER>();

            manager.ListByFuncid(funcid,ref  list);

            return SuccessResultList(list);

        }


        [AuthRight]
        public IActionResult SaveFuncPage(int[] pagelist, int funcid)
        {
            TPMS_FUNC_MEMBERManager manager = new TPMS_FUNC_MEMBERManager();

            List<TPMS_FUNC_MEMBER> list = new List<TPMS_FUNC_MEMBER>();

            if (pagelist.Length <= 0)
            {
                return FailMessage("若要去除该角色所以权限，请删除该角色即可！");
            }

            try
            {

                manager.Db.Ado.BeginTran();


                if (manager.CurrentDb.GetList(s => s.FUNC_ID == funcid).Count() > 0)
                {
                    //删除该用户所有角色
                    if (!manager.CurrentDb.Delete(s => s.FUNC_ID == funcid))
                    {
                        return FailMessage("更新失败！");
                    }
                }


                for (int i = 0; i < pagelist.Length; i++)
                {
                    TPMS_FUNC_MEMBER umodel = new TPMS_FUNC_MEMBER();
                    umodel.FUNC_ID = funcid;
                    umodel.MEMBER_TYPE = 1;
                    umodel.MEMBER_ID = pagelist[i];
                    umodel.CREATETIME = DateTime.Now;
                  
                    list.Add(umodel);
                }

                if (!manager.Insert(list))
                {
                    return FailMessage("更新失败！");
                }


                manager.Db.Ado.CommitTran();


                return SuccessMessage();
            }
            catch (Exception ex)
            {
                manager.Db.Ado.RollbackTran();
                return FailMessage("更新失败！");
                throw ex;
            }


        }



        #endregion


        #region 页面模块


        [HttpPost]
        public IActionResult GetPageList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_PAGEManager tASM_USERManager = new TPMS_PAGEManager();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = pageIndex;
            p.PageSize = pageSize;

            List<TPMS_PAGE> list = new List<TPMS_PAGE>();
            tASM_USERManager.PageListByWhere(keyword, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [AuthRight]
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


        [AuthRight]
        [HttpPost]
        public IActionResult AddPageList(string value)
        {

            string[] Array = value.Split(',');

            DbContext<TPMS_PAGE> dbContext = new DbContext<TPMS_PAGE>();

            try
            {
                dbContext.Db.Ado.BeginTran();

                foreach (var item in Array)
                {
                    TPMS_PAGE pagemodel = new TPMS_PAGE();
                    pagemodel.APP_ID = 1;
                    pagemodel.GUID = Guid.NewGuid().ToString("N");
                    pagemodel.PAGE_NAME = item.Split(':')[0];
                    pagemodel.PAGE_URL = item.Split(':')[1];
                    pagemodel.RESOUCE_TYPE = 3;
                    pagemodel.SORT_INDEX = 0;
                    pagemodel.CREATE_TIME = DateTime.Now;

                    dbContext.Insert(pagemodel); 
              
                 }
                dbContext.Db.Ado.CommitTran();
                return SuccessMessage();

            } catch (Exception e) {

                dbContext.Db.Ado.RollbackTran();

                return FailMessage();
            }

        }



        [AuthRight]
        [HttpPost]
        public IActionResult DelPage(int pageid)
        {
            DbContext<TPMS_PAGE> dbContext = new DbContext<TPMS_PAGE>();


            if (!dbContext.CurrentDb.Delete(S => S.PAGE_ID == pageid))
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

        public IActionResult NoPermission() {
            return View();

        }

        #region 管理员模块

        [HttpPost]
        public IActionResult GetAccountList(string keyword, int pageIndex, int pageSize)
        {

            TPMS_USER_RIGHTManager tASM_USERManager = new TPMS_USER_RIGHTManager();
            SqlSugar.PageModel p = new SqlSugar.PageModel();
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

        [AuthRight]
        public IActionResult  InsertAccount(int userid,int roleid)
        {



            TPMS_USER_RIGHTManager manager = new TPMS_USER_RIGHTManager();


            if (manager.SelectByUseridRoleid(userid, roleid))
            {
                return FailMessage(" 已存在该用户管理权限，请勿重复添加！");
            } 


            TPMS_USER_RIGHT model = new TPMS_USER_RIGHT();
            model.ROLE_ID = roleid;
            model.USER_ID = userid;
            model.CREATE_TIME = DateTime.Now;

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

        [AuthRight]
        public IActionResult DelAccount(int userid) {


            TPMS_USER_RIGHTManager dbContext = new TPMS_USER_RIGHTManager();
            if (!dbContext.CurrentDb.Delete(S => S.USER_ID == userid))
            {
                return FailMessage();
            }
            return SuccessMessage();

        }

        public IActionResult AccountGetRoleAll()
        {
            TPMS_ROLEManager manager = new TPMS_ROLEManager();

            List<TPMS_ROLE> rolemodel = new List<TPMS_ROLE>();

            manager.RoleListByAll(ref rolemodel);

            return SuccessResultList(rolemodel);

        }
       
        public IActionResult AccountGetRoleByUserid(int userid)
        {
            TPMS_USER_RIGHTManager manager = new TPMS_USER_RIGHTManager();

            List<TPMS_USER_RIGHT> list = new List<TPMS_USER_RIGHT>();

            manager.ListByUserid(userid, ref list);



            return SuccessResultList(list);

        }


        [AuthRight]
        public IActionResult AccountUpdateRole(int[] rolelist, int userid)
        {
            if (rolelist.Length <= 0)
            {
                return FailMessage("若要去除该用户所以权限，请删除该管理员即可！");
            }

            TPMS_USER_RIGHTManager manager = new TPMS_USER_RIGHTManager();
            List<TPMS_USER_RIGHT> addlist = new List<TPMS_USER_RIGHT>();

            //开事务关联操作
            try
            {

                manager.Db.Ado.BeginTran();

                if (manager.CurrentDb.GetList(s => s.USER_ID == userid).Count() > 0)
                {    //删除该用户所有角色
                    if (!manager.CurrentDb.Delete(s => s.USER_ID == userid))
                    {
                        return FailMessage("更新失败！");
                    }
                }

                for (int i = 0; i < rolelist.Length; i++)
                {
                    TPMS_USER_RIGHT umodel = new TPMS_USER_RIGHT();
                    umodel.USER_ID = userid;
                    umodel.ROLE_ID = rolelist[i];
                    umodel.CREATE_TIME = DateTime.Now;
                    addlist.Add(umodel);
                }

                if (!manager.Insert(addlist))
                {
                    return FailMessage("更新失败！");
                }


                manager.Db.Ado.CommitTran();


                return SuccessMessage();
            }
            catch (Exception ex)
            {
                manager.Db.Ado.RollbackTran();
                return FailMessage("更新失败！");
                throw ex;
            }

           


        }




        public IActionResult AccountOperation() {

            return View();
        
        }

        #endregion


    }



}