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
    public class UserRightController : ControllerBase.ControllerBase
    {

        [Right]
        public IActionResult Index()
        {
            return View();
        }


        [Right]
        public IActionResult Role()
        {
            return View();
        }

        [Right]
        public IActionResult Func()
        {
            return View();
        }
        
        [Right(Ignore =true)]
         public IActionResult NoPermission()
        {
            return View();
        }


        [Right]
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
        [Right]
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
        [Right]
        [HttpPost]
        public IActionResult GetRolePowerMebmer(int roleid)
        {
            TRIGHT_ROLE_POWER_Da userroleManage = new TRIGHT_ROLE_POWER_Da();
            var list = userroleManage.Db.Queryable<TRIGHT_ROLE_POWER>().Where(s => s.ROLEID == roleid).ToList();

            return SuccessResultList(list);


        }


        [Right]
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
        [Right]
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



        [Right]
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
            Manage.Insert(model);

            return SuccessMessage("已添加！");
        }

        [Right]
        [HttpPost]
        public IActionResult DeletedRolePowerMebmer(int id)
        {
            TRIGHT_ROLE_POWER_Da userroleManage = new TRIGHT_ROLE_POWER_Da();
            var model = userroleManage.GetById(id);

            if (model == null)
            {
                return SuccessMessage("请不要反复取消！"); ;
            }

            userroleManage.Delete(model);

            return SuccessMessage("已取消！");
        }







        #region 角色操作
        [Right]
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
        [Right]
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

        [Right]
        [HttpPost]
        public IActionResult ListRole()
        {
            DataAccess.TRIGHT_ROLE_Da da = new TRIGHT_ROLE_Da();

            return SuccessResultList(da.GetList());
        }
        
        [Right]
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
        [Right]
        [HttpPost]
        public IActionResult ListFunc()
        {
            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();

            return SuccessResultList(da.ListOderBy());
        }
        [Right]
        [HttpPost]
        public IActionResult AddFunc(TRIGHT_POWER model)
        {
            if (string.IsNullOrEmpty(model.POWERNAME))
            {
                return FailMessage("权限名不能为空！");
            }

        

            


            DataAccess.TRIGHT_POWER_Da da = new TRIGHT_POWER_Da();
            if (da.Insert(model))
            {
                return SuccessMessage("成功！");
            }
            return FailMessage("失败！");
        }

        [Right]
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


        [Right]
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
