using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.Facade
{
     public class SupportFacade:FacadeBase.FacadeBase
    {
        public bool Create(SupportCreateModel model)
        {
            DataAccess.TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            try
            {
                da.Db.BeginTran();

                int sid = 0;
                TASM_SUPPORT supportModel = new TASM_SUPPORT();

                //插入工单表
                if (!InsertSupport(model,da,ref sid, ref supportModel))
                {
                    da.Db.RollbackTran();
                    return false;
                }

                //插入操作历史表
                if (!InsertHistory(supportModel,sid))
                {
                    da.Db.RollbackTran();
                    return false;
                }

                //修改附件信息
                if (!UpdateAttachment(model,sid))
                {
                    da.Db.RollbackTran();
                    return false;
                }


                da.Db.CommitTran();

                this.Msg = "创建成功！";
                return true;
            }
            catch (Exception e)
            {
            
                da.Db.RollbackTran();

                this.Msg = "创建工单失败！";
                return false;
            }
        }

        private bool InsertSupport(SupportCreateModel model, TASM_SUPPORT_Da da, ref int  sid,ref TASM_SUPPORT supportModel)
        {

            supportModel = new TASM_SUPPORT()
            {
                CREATOR = model.CreatorId,
                CONDUCTOR = model.ConductorId,
                CONTENT = model.Content,
                CREATETIME = DateTime.Now,
                FINDATE = model.FindDate,
                ISDEL = 0,
                MEMBERID = 0,
                PRIORITY = model.Priority,
                PROJECT = model.ProjectId,
                SEVERITY = model.Severity,
                STATUS = 0,
                TITLE = model.Title,
                TYPE = model.Type,
                STATE = 0

            };

            sid = da.CurrentDb.InsertReturnIdentity(supportModel);

            if (sid <= 0)
            {
                Msg = "添加操作历史失败！";
                return false;
            }
            return true;
        }
        private bool InsertHistory(TASM_SUPPORT supportModel,int sid) {


            DataAccess.TASM_SUPPORT_HIS_Da his_manager = new TASM_SUPPORT_HIS_Da();
            TASM_SUPPORT_HIS hisModel = new TASM_SUPPORT_HIS();
            hisModel.CREATETIME = DateTime.Now;
            hisModel.PRE_USER = supportModel.CREATOR;
            hisModel.NEXT_USER = supportModel.CONDUCTOR;
            hisModel.SID = sid;
            hisModel.REMARKS = "工单创建，等待技术处理";

            hisModel.PRE_STATUS = supportModel.STATUS;
            hisModel.NEXT_STATUS = supportModel.STATUS; //初始状态
            hisModel.TYPE = 0;   //tasm_disposer表
            hisModel.TID = sid;


            if (!his_manager.CurrentDb.Insert(hisModel))
            {
                Msg = "添加操作历史失败！";
                return false;

            }
            return true;

        }
        private bool UpdateAttachment(SupportCreateModel model, int sid)
        {

            if (model.Filelist == null && model.Filelist.Count <=0)
            {
                return true;
            }

            foreach (var item in model.Filelist)
            {
                TASM_ATTACHMENTManager manager = new TASM_ATTACHMENTManager();
                TASM_ATTACHMENT attModel = manager.CurrentDb.GetById(item.ID);
                attModel.TYPE = 1;
                attModel.PID = sid;

                if (!manager.CurrentDb.Update(attModel))
                {
                    Msg = "修改附近信息失败！";
                    return false;
                }
            }

            return true;

        }
    }
}
