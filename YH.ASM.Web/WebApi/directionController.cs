using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Web.Models;

namespace YH.ASM.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase.ControllerBase
    {


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


            Entites.CodeGenerator.TASM_TRAVEL _travelModel = new Entites.CodeGenerator.TASM_TRAVEL()
            {

                 ADDRESS=model.address,
                  CONTENT=model.content,
                   CREATETIME=model.date,
                    CUSTOMERID=model.customerId,
                     LATITUDE= Convert.ToDouble( model.latitude),
                      LONGITUDE = Convert.ToDouble(model.longitude),
                       SUPPORTID=model.supportId,
                        USERID=model.userId,
                         STATUS=0,
                          TYPE=model.type,
                           ISDEL=0,
                            PROJECTID=model.projecId,
            };

           

            if (!manager.Insert(_travelModel))
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
    }
}