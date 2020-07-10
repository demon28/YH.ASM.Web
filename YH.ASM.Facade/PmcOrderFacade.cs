using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;

namespace YH.ASM.Facade
{
    public class PmcOrderFacade : FacadeBase.FacadeBase
    {

        public bool Create(AddPmcCheckModel model)
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
                if (!InsertHistory( model,  manager,  supportModel,  pmcId,  model.SUPPORTSTATUS,  model.NEXTUSER))
                {
                    this.Msg = "创建操作历史失败！";
                    manager.Db.RollbackTran();
                    return false;
                }




                //3,新的处理人员再新增一条 处理信息(顺序不能变) 取了工单处理人，为个人处理表的创建人，顺序不能变
                if (!InsertPersonal(supportModel.CONDUCTOR, model.NEXTUSER, model.SUPPORTSTATUS, model.SID))
                {
                    this.Msg = "分发工单失败！";
                    manager.Db.RollbackTran();
                    return false;
                }


                //4,修改工单表的状态
                if (!UpdateSupport( supportModel,  support_manager, model.NEXTUSER, model.SUPPORTSTATUS,  pmcId))
                {
                    this.Msg = "修改工单信息失败！";
                    manager.Db.RollbackTran();
                    return false;
                }


                //5,修改个人信息处理表
                if (!UpdatePersonal(model.PERSONALID))
                {
                    this.Msg = "修改个人处理状态失败！";
                    manager.Db.RollbackTran();
                    return false;
                }



                //6,添加推送消息
                if (!InsertPush(model, pmcId))
                {
                    this.Msg = "修改个人处理状态失败！";
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

        /// <summary>
        /// 修改个人处理状态
        /// </summary>
        /// <param name="personalId"></param>
        /// <returns></returns>
        private bool UpdatePersonal(int personalId)
        {

            TASM_SUPPORT_PERSONAL_Da da = new TASM_SUPPORT_PERSONAL_Da();

            var personalmodel = da.CurrentDb.GetById(personalId);
            personalmodel.STATUS = (int)Entites.SupportPersnalStatus.已完成;

            return da.CurrentDb.Update(personalmodel);

        }

        /// <summary>
        /// 新增个人信息处理表
        /// </summary>
        /// <param name="cid">创建人，也就是本张工单的处理人，不是整个工单的创建人</param>
        /// <param name="did">处理人，也就是 下一个处理人，</param>
        /// <param name="tid">流程节点id，走到哪个环节了</param>
        /// <param name="sid">工单id</param>
        /// <returns></returns>
        private bool InsertPersonal(int cid, int did, int tid, int sid)
        {

            TASM_SUPPORT_PERSONAL_Da da = new TASM_SUPPORT_PERSONAL_Da();
            TASM_SUPPORT_PERSONAL psersonlModel = new TASM_SUPPORT_PERSONAL()
            {
                CID = cid,
                CREATETIME = DateTime.Now,
                DID = did,
                SID = sid,
                STATUS = (int)Entites.SupportPersnalStatus.待办,
                TID = tid

            };
            return da.CurrentDb.Insert(psersonlModel);

        }


        /// <summary>
        /// 消息推送表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private bool InsertPush(AddPmcCheckModel model, int tid)
        {
            if (model.Push == null)
            {
                return true;
            }

            TASM_SUPPORT_PUSH_Da da = new TASM_SUPPORT_PUSH_Da();
            TASM_SUPPORT_PUSH pushModel = new TASM_SUPPORT_PUSH()
            {
                SID = model.SID,
                CC = model.Push.CC,
                CONDUCTOR = model.Push.CONDUCTOR,
                CONTENT = model.Push.CONTENT,
                CREATETIME = DateTime.Now,
                POINT = (int)Entites.SupportHisType.技术处理,
                STATUS = 0,
                TID = tid

            };
            return da.CurrentDb.Insert(pushModel);

        }

    }
}
