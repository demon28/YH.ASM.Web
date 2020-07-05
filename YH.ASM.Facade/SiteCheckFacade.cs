using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.Facade
{
    public class SiteCheckFacade : FacadeBase.FacadeBase
    {

        public bool Create(TASM_SUPPORT_SITE model, int supportStatus, int nextUser)
        {
            DataAccess.TASM_SUPPORT_SITE_Da manager = new DataAccess.TASM_SUPPORT_SITE_Da();

            try
            {
                manager.Db.BeginTran();


                //1，添加 PMC处理表数据，
                int siteId = 0;

                if (!InserSite( model,  manager, ref  siteId))
                {
                    this.Msg = "创建现场处理信息失败！";
                    manager.Db.RollbackTran();
                    return false;

                }


                //2,当前处理人员发生修改，新增一条 修改记录 history
                DataAccess.TASM_SUPPORT_Da support_manager = new DataAccess.TASM_SUPPORT_Da();
                var supportModel = support_manager.CurrentDb.GetById(model.SID);  //工单id 查询工单信息

                if (!InsertHistory( model,  supportModel,  siteId,  supportStatus,  nextUser))
                {
                    this.Msg = "创建操作历史失败！";
                    manager.Db.RollbackTran();
                    return false;
                }


                //3,修改工单表的状态
                if (!UpdateSupport( supportModel,  support_manager,  nextUser,  supportStatus,  siteId))
                {
                    this.Msg = "修改工单状态失败！";
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

        private bool InserSite(TASM_SUPPORT_SITE model, TASM_SUPPORT_SITE_Da manager, ref int siteId)
        {

            model.CREATETIME = DateTime.Now;

            siteId = manager.Db.Insertable(model).ExecuteReturnIdentity();

            return siteId > 0;
        }
        private bool InsertHistory(TASM_SUPPORT_SITE model,  TASM_SUPPORT supportModel, int siteId, int supportStatus, int nextUser) {

            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();

            TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
            hisModel.CREATETIME = DateTime.Now;
            hisModel.PRE_USER = supportModel.CONDUCTOR;
            hisModel.NEXT_USER = nextUser;

            hisModel.SID = model.SID;
            hisModel.REMARKS = "现场已处理，等待审核";
            hisModel.PRE_STATUS = supportModel.STATUS;

            hisModel.NEXT_STATUS = supportStatus;
            hisModel.TYPE = (int)Entites.SupportHisType.现场处理;  
            hisModel.TID = siteId;

           return  his_manager.CurrentDb.Insert(hisModel);

        }

        private bool UpdateSupport(TASM_SUPPORT supportModel, TASM_SUPPORT_Da support_manager , int nextUser, int supportStatus, int siteId)
        {

            supportModel.MEMBERID = siteId;
            supportModel.STATUS = supportStatus;  //修改  新状态工单状态
            supportModel.CONDUCTOR = nextUser;  //工单流转到下一处理人员， 修改处理人员，此处 PMC处理人员 和 下一处理人员共用一个字段
            supportModel.STATE = 1;  //工单处理中

            return  support_manager.CurrentDb.Update(supportModel);  //修改工单表

        }
    }
}
