using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class UserRightController : ControllerBase.ComControllerBase
    {

        [Right(PowerName ="主页面")]
        public IActionResult Index()
        {
            return View();
        }


        [Right(PowerName = "角色管理")]
        public IActionResult Role()
        {
            return View();
        }

        [Right(PowerName = "功能配置")]
        public IActionResult Func()
        {
            return View();
        }
        
        [Right(Ignore =true)]
         public IActionResult NoPermission()
        {
            return View();
        }


        [Right(PowerName = "页面权限")]
        [HttpPost]
        public IActionResult GetAllRole()
        {
            TRIGHT_ROLE_Da rolemanger = new TRIGHT_ROLE_Da();
            var list= rolemanger.CurrentDb.AsQueryable().OrderBy(s=>s.ID).ToList();

            return SuccessResultList(list);

        }



        /// <summary>
        /// 获取用户与角色的中间 表 信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// 
        [Right(PowerName = "分配权限")]
        [HttpPost]
        public IActionResult GetUserRoleMebmer(int userid)
        {
            TRIGHT_USER_ROLE_Da userroleManage = new TRIGHT_USER_ROLE_Da();
            var list = userroleManage.Db.Queryable<TRIGHT_USER_ROLE>().Where(s => s.USERID == userid).ToList();

            return SuccessResultList(list);


        }



        /// <summary>
        /// 获取角色 与 权限 的中间表信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [Right(PowerName = "角色功能配置")]
        [HttpPost]
        public IActionResult GetRolePowerMebmer(int roleid)
        {
            TRIGHT_ROLE_POWER_Da userroleManage = new TRIGHT_ROLE_POWER_Da();
            var list = userroleManage.Db.Queryable<TRIGHT_ROLE_POWER>().Where(s => s.ROLEID == roleid).ToList();

            return SuccessResultList(list);


        }


        [Right(PowerName = "用户关联角色")]
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
            userroleManage.CurrentDb.Insert(model);

            return SuccessMessage("已添加！");
        }
        [Right(PowerName = "用户退出角色")]
        [HttpPost]
        public IActionResult DeleteUserRoleMebmer(int id)
        {
            TRIGHT_USER_ROLE_Da userroleManage = new TRIGHT_USER_ROLE_Da();
            var model= userroleManage.CurrentDb.GetById(id);

            if (model==null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }

            userroleManage.CurrentDb.Delete(model);

            return SuccessMessage("已取消！");
        }



        [Right(PowerName = "角色关联功能")]
        [HttpPost]
        public IActionResult AddRolePowerMebmer( int roleid, int powerid)
        {
            TRIGHT_ROLE_POWER_Da Manage = new TRIGHT_ROLE_POWER_Da();

            if (Manage.CurrentDb.AsQueryable().Where(s => s.ROLEID == roleid && s.POWERID == powerid).Count() > 0)
            {
                return SuccessMessage("请不要反复添加！");
            }



            TRIGHT_ROLE_POWER model = new TRIGHT_ROLE_POWER
            {
                ROLEID = roleid,
                POWERID = powerid
            };
            Manage.CurrentDb.Insert(model);

            return SuccessMessage("已添加！");
        }

        [Right(PowerName = "角色取消功能")]
        [HttpPost]
        public IActionResult DeletedRolePowerMebmer(int id)
        {
            TRIGHT_ROLE_POWER_Da userroleManage = new TRIGHT_ROLE_POWER_Da();
            var model = userroleManage.CurrentDb.GetById(id);

            if (model == null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }

            userroleManage.CurrentDb.Delete(model);

            return SuccessMessage("已取消！");
        }







        #region 角色操作
        [Right(PowerName = "添加角色")]
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
            if (da.CurrentDb.Insert(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }


        [Right(PowerName = "修改角色")]
        [HttpPost]
        public IActionResult UpdateRole(TRIGHT_ROLE model)
        {

            if (string.IsNullOrEmpty(model.ROLENAME))
            {
                return FailMessage("角色名不能为空！");
            }

         


            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();
            if (da.CurrentDb.Update(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }


        [Right(PowerName = "角色查询")]
        [HttpPost]
        public IActionResult ListRole()
        {
            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();

            return SuccessResultList(da.CurrentDb.GetList());
        }

        [Right(PowerName = "删除角色")]
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
        [Right(PowerName = "查询功能")]
        [HttpPost]
        public IActionResult ListFunc()
        {
            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();

            return SuccessResultList(da.ListOderBy());
        }
        [Right(PowerName = "添加功能")]
        [HttpPost]
        public IActionResult AddFunc(TRIGHT_POWER model)
        {
            if (string.IsNullOrEmpty(model.POWERNAME))
            {
                return FailMessage("权限名不能为空！");
            }



            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.CurrentDb.Insert(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }

        [Right(PowerName = "修改功能")]
        [HttpPost]
        public IActionResult UpdateFunc(TRIGHT_POWER model)
        {

            if (string.IsNullOrEmpty(model.POWERNAME))
            {
                return FailMessage("权限名不能为空！");
            }


            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.CurrentDb.Update(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }


        [Right(PowerName = "删除功能")]
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
