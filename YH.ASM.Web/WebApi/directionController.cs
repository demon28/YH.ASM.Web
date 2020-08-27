using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class directionController : ControllerBase.ComControllerBase
    {

        [HttpGet]
        public IActionResult Get() {

            return View("123333333");
        }



        [HttpPost("Put")]
        public JsonResult Put([FromBody] Models.DirectionInputModel model)
        {
            //同一天 同一种类型，不能反复提交
            DataAccess.TASM_TRAVELManager manager = new DataAccess.TASM_TRAVELManager();

            if (manager.SelectByDate(model.userId,model.date,model.type))
            {

                string v = model.type == 0 ? "早报" : "晚报";
                return FailMessage("今天已添加过'"+ v+"'请勿重复添加！");
            }


            TASM_TRAVEL _travelModel = new TASM_TRAVEL();


            _travelModel.ADDRESS = model.address;
            _travelModel.CONTENT = model.content;
            _travelModel.CREATETIME = model.date;
            _travelModel.CUSTOMERID = model.customerId;
            _travelModel.LATITUDE = Convert.ToDouble(model.latitude);
            _travelModel.LONGITUDE = Convert.ToDouble(model.longitude);
            _travelModel.SUPPORTID = model.supportId;
            _travelModel.USERID = model.userId;
            _travelModel.STATUS = 0;
            _travelModel.TYPE = model.type;
            _travelModel.ISDEL = 0;
            _travelModel.PROJECTID = model.projecId;

            _travelModel.CUSTOMER_NAME = model.customerName;
            _travelModel.PROJECT_NAME = model.projectName;
            _travelModel.SUPPORT_NAME = model.supportName;

            _travelModel.MACHINE_NAME = model.machineName;
            _travelModel.MACHINE_COUNT = model.machineCount;
            _travelModel.REMARKS = model.remarks;
            _travelModel.MACHINEID = model.machineId;

            //此处 有建 关联人员信息 表 ，未启用

            _travelModel.MACHINEASSIST = model.machineAssist;

            if (!manager.CurrentDb.Insert(_travelModel))
            {
                return FailMessage("添加失败！");
            }



            return SuccessMessage("添加成功！");
        }


        [HttpGet("List")]
        public JsonResult List([FromQuery] DirectionListModel model)
        {

            DataAccess.TASM_TRAVELManager manager = new DataAccess.TASM_TRAVELManager();

            List<TASM_TRAVEL> list = new List<TASM_TRAVEL>();

            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            manager.ListByUserId(model.userid, ref p, ref list);

            return SuccessResultList(list,p);
        }


        [HttpGet("UserList")]
        public JsonResult UserList([FromQuery] ListUserApiModel model) {

            DataAccess.TASM_USERManager manager = new DataAccess.TASM_USERManager();



            SqlSugar.PageModel p = new SqlSugar.PageModel();
            p.PageIndex = model.pageindex;
            p.PageSize = model.pagesize;

            List<TASM_USER> list = new List<TASM_USER>();

            manager.ListByDept(model.keywords, ref p, ref list);

            return SuccessResultList(list, p);

        }

        [HttpGet("WrokReport")]
        public IActionResult WrokReport(int traid)
        {

            DataAccess.TASM_TRAVELManager mannager = new TASM_TRAVELManager();
            var model = mannager.SelectByTraid(traid);

            return SuccessResult(model);

        }

    }
}