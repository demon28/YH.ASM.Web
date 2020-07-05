using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Facade
{
    public class PmcOrderFacade : FacadeBase.FacadeBase
    {

        public bool Create(TASM_SUPPORT_PMC model, int supportStatus, int nextUser)
        {
            TASM_SUPPORT_PMC_Da manager = new TASM_SUPPORT_PMC_Da();

            try
            {
                manager.Db.BeginTran();

                int pmcId = 0;
                //1，添加 PMC处理表数据
                if (!InsertPmc(model, manager, ref pmcId))
                {
                    this.Msg = "创建PMC处理信息失败！";
                     manager.Db.RollbackTran();
                    return false;
                }


              
                TASM_SUPPORT_Da support_manager = new TASM_SUPPORT_Da();
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息

                //2,当前处理人员发生修改，新增一条 修改记录 history
                if (!InsertHistory( model,  manager,  supportModel,  pmcId,  supportStatus,  nextUser))
                {
                    this.Msg = "创建操作历史失败！";
                    manager.Db.RollbackTran();
                    return false;
                }



                
                //3,修改工单表的状态
                if (!UpdateSupport( supportModel,  support_manager,  nextUser,  supportStatus,  pmcId))
                {
                    this.Msg = "修改工单信息失败！";
                    manager.Db.RollbackTran();
                    return false;
                }

                manager.Db.CommitTran();

                return true;

             
            }
            catch (Exception e)
            {
                manager.Db.RollbackTran();
                return false;
            }


        }


        private bool InsertPmc(TASM_SUPPORT_PMC model, TASM_SUPPORT_PMC_Da manager, ref int pmcId)
        {

            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;

            pmcId = manager.Db.Insertable(model).ExecuteReturnIdentity(); ;
            return pmcId > 0;
        }
        private bool InsertHistory(TASM_SUPPORT_PMC model, TASM_SUPPORT_PMC_Da manager, TASM_SUPPORT supportModel, int pmcId, int supportStatus, int nextUser)
        {
            TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();
            TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();

            hisModel.CREATETIME = DateTime.Now;
            hisModel.PRE_USER = supportModel.CONDUCTOR;
            hisModel.NEXT_USER = nextUser;

            hisModel.SID = model.SID;
            hisModel.REMARKS = "PMC已处理,等待现场处理";
            hisModel.PRE_STATUS = supportModel.STATUS;

            hisModel.NEXT_STATUS = supportStatus;
            hisModel.TYPE = (int)SupportHisType.PMC跟进; ;   //tasm_disposer表
            hisModel.TID = pmcId;

           return  his_manager.CurrentDb.Insert(hisModel);

        }

        private bool UpdateSupport(TASM_SUPPORT supportModel, TASM_SUPPORT_Da support_manager, int nextUser, int supportStatus, int pmcId)
        {
            supportModel.MEMBERID = pmcId;
            supportModel.STATUS = supportStatus;  //修改工单状态
            supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段
            supportModel.STATE = 1;  //工单处理中
            return support_manager.CurrentDb.Update(supportModel);  //修改工单表

        }
    }
}
