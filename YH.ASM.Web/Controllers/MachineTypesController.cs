using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Web.ControllerBase;

namespace YH.ASM.Web.Controllers
{
    [Authorize]
    public class MachineTypesController : ControllerBase.ComControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }


        [Right(PowerName = "设备分类")]
        public IActionResult Types()
        {
            return View();
        }




        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult ListTypes()
        {
            DataAccess.TASM_MACHINE_TYPEManager manager = new DataAccess.TASM_MACHINE_TYPEManager();


            List<TASM_MACHINE_TYPE> list = new List<TASM_MACHINE_TYPE>();
            manager.ListByWhere(string.Empty, ref list);

            return SuccessResultList(list);
        }


        [Right(PowerName = "添加设备类型")]
        [HttpPost]
        public IActionResult AddTypes(TASM_MACHINE_TYPE model)
        {

            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;

            DataAccess.TASM_MACHINE_TYPEManager manager = new DataAccess.TASM_MACHINE_TYPEManager();
            if (!manager.CurrentDb.Insert(model))
            {
                return FailMessage();
            }
            return SuccessMessage("添加成功");

        }

        [Right(PowerName = "删除设备类型")]
        [HttpPost]
        public IActionResult DeleteTypes(int id)
        {

            TASM_MACHINE_TYPEManager dbContext = new TASM_MACHINE_TYPEManager();
            if (!dbContext.CurrentDb.Delete(S => S.ID == id))
            {
                return FailMessage();
            }
            return SuccessMessage();


        }

        [Right(PowerName = "修改设备类型")]
        [HttpPost]
        public IActionResult UpdateTypes(TASM_MACHINE_TYPE model)
        {
            DataAccess.TASM_MACHINE_TYPEManager manager = new DataAccess.TASM_MACHINE_TYPEManager();
            if (!manager.CurrentDb.Update(model))
            {
                return FailMessage();
            }
            return SuccessMessage("修改成功");

        }

        [Right(Ignore = true)]
        [HttpPost]
        public IActionResult GetTypesInfo(int id)
        {
            TASM_MACHINE_TYPEManager manager = new TASM_MACHINE_TYPEManager();
            TASM_MACHINE_TYPE model = manager.CurrentDb.GetById(id);

            return SuccessResult(model);

        }



    }
}
