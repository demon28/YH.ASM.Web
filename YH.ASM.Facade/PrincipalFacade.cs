using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Facade
{
    public class PrincipalFacade : FacadeBase.FacadeBase
    {
        public bool Create(TASM_SUPPORT_PRINCIPAL model, int supportStatus, int nextUser)
        {


            DataAccess.TASM_SUPPORT_PRINCIPAL_Da manager = new DataAccess.TASM_SUPPORT_PRINCIPAL_Da();


            try
            {
                manager.Db.BeginTran();

                int pid = 0;
                //1，添加 PMC处理表数据，并获得新id
                if (!InsertSupport( model,  manager, ref pid))
                {
                    Msg = "创建审核信息失败！";
                    manager.Db.RollbackTran();
                    return false;
                }


                //2,创建操作历史      
                DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息

                if (!InsertHistory( model,  supportModel,  supportStatus,  nextUser,  pid))
                {
                    Msg = "创建操作历史失败！";
                    manager.Db.RollbackTran();
                    return false;

                }




                //3,修改工单表的状态
                if (!UpdateSupport( supportModel,  support_manager,  nextUser,  supportStatus,  pid))
                {
                    Msg = "修改工单状态失败！";
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

        private bool InsertSupport(TASM_SUPPORT_PRINCIPAL model, TASM_SUPPORT_PRINCIPAL_Da manager, ref int pid)
        {

            model.CREATETIME = DateTime.Now;
            model.STATUS = 0;
            pid = manager.Db.Insertable(model).ExecuteReturnIdentity();

            return pid > 0;

        }
        private bool InsertHistory(TASM_SUPPORT_PRINCIPAL model, TASM_SUPPORT supportModel, int supportStatus, int nextUser, int pid)
        {

            string remarks = supportStatus == 5 ? "未完成" : "已完成";

            //3,当前处理人员发生修改，新增一条 修改记录 history
            TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
            hisModel.CREATETIME = DateTime.Now;
            hisModel.PRE_USER = supportModel.CONDUCTOR;
            hisModel.NEXT_USER = nextUser;

            hisModel.SID = model.SID;
            hisModel.REMARKS = "负责人已审核:" + remarks;
            hisModel.PRE_STATUS = supportModel.STATUS;

            hisModel.NEXT_STATUS = supportStatus;
            hisModel.TYPE =(int) Entites.SupportHisType.负责人审核;   
            hisModel.TID = pid;

            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();
           return   his_manager.CurrentDb.Insert(hisModel);

        }


        private bool UpdateSupport(TASM_SUPPORT supportModel, TASM_SUPPORT_Da support_manager, int nextUser, int supportStatus, int pid)
        {
            supportModel.MEMBERID = pid;
            supportModel.STATUS = supportStatus;  //修改工单状态
            supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段

            if (supportModel.STATUS == 6 || supportModel.STATUS == 7)
            {
                supportModel.STATE = 2;  //工单已完成
            }

          return  support_manager.CurrentDb.Update(supportModel);  //修改工单表


        }



    }
}
