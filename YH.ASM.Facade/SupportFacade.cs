using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.Facade
{
     public class SupportFacade:FacadeBase.FacadeBase
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

                //添加处理人表
                if (!InsertPersonal(supportModel, sid))
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

                //添加推送消息
                if (!InsertPush( model, sid))
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



        /// <summary>
        /// 创建工单主表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="da"></param>
        /// <param name="sid"></param>
        /// <param name="supportModel"></param>
        /// <returns></returns>
        private bool InsertSupport(SupportCreateModel model, TASM_SUPPORT_Da da, ref int  sid,ref TASM_SUPPORT supportModel)
        {
            TASM_PROJECTManager projectda = new TASM_PROJECTManager();
            TASM_PROJECT projectmodel = projectda.CurrentDb.GetById(model.ProjectId);

            string code = projectmodel.CODE +"-"+ da.SelectSupprotCodeIndex();

            supportModel = new TASM_SUPPORT()
            {
                CREATOR = model.CreatorId,
                CONDUCTOR = model.ConductorId,
                CONTENT = model.Content,
                CREATETIME = DateTime.Now,
                FINDATE = model.FindDate,
          
                MEMBERID = 0,
    
                PROJECT = model.ProjectId,
                SEVERITY = model.Severity,
                STATUS = 0,
                TITLE = model.Title,
                TYPE = model.Type,
                STATE = 0,
                MID=model.Mid,
                CODE=code
            };

            sid = da.CurrentDb.InsertReturnIdentity(supportModel);

            if (sid <= 0)
            {
                Msg = "添加操作历史失败！";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 增加操作历史
        /// </summary>
        /// <param name="supportModel"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
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
            hisModel.TYPE = (int)SupportHisType.创建工单;   //tasm_disposer表
            hisModel.TID = sid;


            if (!his_manager.CurrentDb.Insert(hisModel))
            {
                Msg = "添加操作历史失败！";
                return false;

            }
            return true;

        }
      
        /// <summary>
        /// 修改附件归属
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
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
       /// <summary>
       /// 消息推送表
       /// </summary>
       /// <param name="model"></param>
       /// <param name="sid"></param>
       /// <returns></returns>
        private bool InsertPush(SupportCreateModel model, int sid)
        {
            if (model.Push == null)
            {
                return true;
            }

            TASM_SUPPORT_PUSH_Da da = new TASM_SUPPORT_PUSH_Da();
            TASM_SUPPORT_PUSH pushModel = new TASM_SUPPORT_PUSH()
            {
                SID = sid,
                CC = model.Push.CC,
                CONDUCTOR = model.Push.CONDUCTOR,
                CONTENT = model.Push.CONTENT,
                CREATETIME = DateTime.Now,
                POINT = (int)Entites.SupportHisType.创建工单,
                STATUS = 0,
                TID = sid

            };
            return da.CurrentDb.Insert(pushModel);

        }
        
        /// <summary>
        /// 个人处理信息表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private bool InsertPersonal(TASM_SUPPORT model, int sid)
        {

            TASM_SUPPORT_PERSONAL_Da da= new TASM_SUPPORT_PERSONAL_Da();
            TASM_SUPPORT_PERSONAL psersonlModel = new TASM_SUPPORT_PERSONAL()
            {
                CID = model.CREATOR,
                CREATETIME = DateTime.Now,
                DID = model.CONDUCTOR,
                SID = sid,
                STATUS = 0,
                TID = model.STATUS

            };
            return da.CurrentDb.Insert(psersonlModel);

        }

    }
}
