using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Web.Controllers
{
    public class UserRightController : ControllerBase.ControllerBase
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Role()
        {
            return View();
        }
        [Authorize]
        public IActionResult Func()
        {
            return View();
        }




        [HttpPost]
        public IActionResult GetAllRole()
        {
            TRIGHT_ROLE_Da rolemanger = new TRIGHT_ROLE_Da();
            var list= rolemanger.CurrentDb.AsQueryable().OrderBy(s=>s.ID).ToList();

            return SuccessResultList(list);

        }

        [HttpPost]
        public IActionResult GetUserRoleMebmer(int userid)
        {
            TRIGHT_USER_ROLE_Da userroleManage = new TRIGHT_USER_ROLE_Da();
            var list = userroleManage.Db.Queryable<TRIGHT_USER_ROLE>().Where(s => s.USERID == userid).ToList();

            return SuccessResultList(list);


        }

        [HttpPost]
        public IActionResult AddUserRoleMebmer(int userid,int roleid)
        {
            TRIGHT_USER_ROLE_Da userroleManage = new TRIGHT_USER_ROLE_Da();

            if (userroleManage.CurrentDb.AsQueryable().Where(s => s.ROLEID == roleid && s.USERID == userid).Count()>0)
            {
                return SuccessMessage("请不要反复添加！");
            }  



            TRIGHT_USER_ROLE model = new TRIGHT_USER_ROLE
            {
                ROLEID = roleid,
                USERID = userid
            };
            userroleManage.Insert(model);

            return SuccessMessage("已添加！");
        }

        [HttpPost]
        public IActionResult DeleteUserRoleMebmer(int id)
        {
            TRIGHT_USER_ROLE_Da userroleManage = new TRIGHT_USER_ROLE_Da();
            var model= userroleManage.GetById(id);

            if (model==null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }

            userroleManage.Delete(model);

            return SuccessMessage("已取消！");
        }

        #region 角色操作

        [HttpPost]
        public IActionResult AddRole(TRIGHT_ROLE model)
        {
            if (string.IsNullOrEmpty(model.ROLENAME))
            {
                return FailMessage("角色名不能为空！");
            }

            if (string.IsNullOrEmpty(model.REMARKS))
            {
                model.REMARKS = string.Empty;
            }

            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();
            if (da.Insert(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }

        [HttpPost]
        public IActionResult UpdateRole(TRIGHT_ROLE model)
        {

            if (string.IsNullOrEmpty(model.ROLENAME))
            {
                return FailMessage("角色名不能为空！");
            }

         


            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();
            if (da.Update(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }

        [HttpPost]
        public IActionResult ListRole()
        {
            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();

            return SuccessResultList(da.GetList());
        }
        [HttpPost]
        public IActionResult DelRole(int id)
        {
            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();
            if (da.CurrentDb.DeleteById(id))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }
        #endregion


        #region 权限操作

        [HttpPost]
        public IActionResult ListFunc()
        {
            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();

            return SuccessResultList(da.GetList());
        }

        [HttpPost]
        public IActionResult AddFunc(TRIGHT_POWER model)
        {
            if (string.IsNullOrEmpty(model.POWERNAME))
            {
                return FailMessage("权限名不能为空！");
            }

            if (string.IsNullOrEmpty(model.REMARKS))
            {
                model.REMARKS = string.Empty;
            }

            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.Insert(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }


        [HttpPost]
        public IActionResult UpdateFunc(TRIGHT_POWER model)
        {

            if (string.IsNullOrEmpty(model.POWERNAME))
            {
                return FailMessage("权限名不能为空！");
            }




            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.Update(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }



        [HttpPost]
        public IActionResult DelFunc(int id)
        {
            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.CurrentDb.DeleteById(id))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }
        #endregion

       

    }
}
