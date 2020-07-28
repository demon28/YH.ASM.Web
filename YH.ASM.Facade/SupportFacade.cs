

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using YH.ASM.DataAccess;
using YH.ASM.DataAccess.CodeGenerator;
using YH.ASM.Entites;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.Entites.Model;
using YH.ASM.Entites.Tool;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace YH.ASM.Facade
{
     public class SupportFacade:FacadeBase.FacadeBase
    {

        /// <summary>
        /// 创建工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Create(SupportCreateModel model)
        {
            DataAccess.TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            try
            {
                da.Db.BeginTran();

                Logger.LogInformation("=====开始创建工单========");

                int sid = 0;
                TASM_SUPPORT supportModel = new TASM_SUPPORT();

                //插入工单表
                if (!InsertSupport(model,da,ref sid, ref supportModel))
                {
                    Logger.LogInformation("插入工单表失败");
                    da.Db.RollbackTran();
                    return false;
                }

                //插入操作历史表
                if (!InsertHistory(supportModel,sid))
                {
                    Logger.LogInformation("插入历史表失败");
                    da.Db.RollbackTran();
                    return false;
                }

                //添加处理人表
                if (!InsertPersonal(supportModel, sid))
                {
                    Logger.LogInformation("插入处理人表失败");
                    da.Db.RollbackTran();
                    return false;
                }


                //修改附件信息
                if (!UpdateAttachment(model,sid))
                {
                    Logger.LogInformation("修改附件信息失败");
                    da.Db.RollbackTran();
                    return false;
                }

                //添加推送消息
                if (!InsertPush( model, sid))
                {
                    Logger.LogInformation("添加推送消息失败");
                    da.Db.RollbackTran();
                    return false;
                }

                //发送通知-----非推送消息，推送消息 需要windows服务去跑
                if (!PushMessage(sid,da))
                {
                    Logger.LogInformation("推送消息失败");
                    da.Db.RollbackTran();
                    return false;
                }


                Logger.LogInformation("=====结束创建工单========");
                da.Db.CommitTran();

                this.Msg = "创建成功！";
                return true;
            }
            catch (Exception e)
            {
            
                da.Db.RollbackTran();
                Logger.LogInformation(""+ e);
                this.Msg = "创建工单失败！";
                return false;
            }
        }

        /// <summary>
        /// 删除工单
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool Delete(int sid)
        {

            TASM_SUPPORT_Da support = new TASM_SUPPORT_Da();
            support.Db.BeginTran();

            try
            {
                if (!support.CurrentDb.DeleteById(sid))
                {
                    support.Db.RollbackTran();
                    this.Msg = "删除工单主表失败";
                    Logger.LogInformation("删除工单主表失败！");
                    return false;
                }


                TASM_SUPPORT_DISPOSER_Da disposer = new TASM_SUPPORT_DISPOSER_Da();

                if (disposer.Db.Queryable<TASM_SUPPORT_DISPOSER>().Where(s => s.SID == sid).Count()>0)
                {
                    if (disposer.Db.Deleteable<TASM_SUPPORT_DISPOSER>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除责任人处理失败";
                        Logger.LogInformation("删除责任人处理失败！");
                        return false;
                    }
                }



                TASM_SUPPORT_PMC_Da pmc = new TASM_SUPPORT_PMC_Da();

                if (pmc.Db.Queryable<TASM_SUPPORT_PMC>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (pmc.Db.Deleteable<TASM_SUPPORT_PMC>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "售后内勤删除失败！";
                        Logger.LogInformation("售后内勤删除失败！");
                        return false;
                    }
                }


                TASM_SUPPORT_SITE_Da site = new TASM_SUPPORT_SITE_Da();

                if (site.Db.Queryable<TASM_SUPPORT_SITE>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (site.Db.Deleteable<TASM_SUPPORT_SITE>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除现场信息失败！";
                        Logger.LogInformation("删除现场信息失败！");
                        return false;
                    }
                }

                TASM_SUPPORT_PRINCIPAL_Da principal = new TASM_SUPPORT_PRINCIPAL_Da();

                if (principal.Db.Queryable<TASM_SUPPORT_PRINCIPAL>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (principal.Db.Deleteable<TASM_SUPPORT_PRINCIPAL>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除审核信息失败！";
                        Logger.LogInformation("删除审核信息失败！");
                        return false;
                    }
                }



                TASM_SUPPORT_HIS_Da his = new TASM_SUPPORT_HIS_Da();

                if (his.Db.Queryable<TASM_SUPPORT_HIS>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (his.Db.Deleteable<TASM_SUPPORT_HIS>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除审核信息失败！";
                        Logger.LogInformation("删除审核信息失败！");
                        return false;
                    }
                }




                TASM_SUPPORT_PERSONAL_Da personal = new TASM_SUPPORT_PERSONAL_Da();

                if (personal.Db.Queryable<TASM_SUPPORT_PERSONAL>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (personal.Db.Deleteable<TASM_SUPPORT_PERSONAL>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除审核信息失败！";
                        Logger.LogInformation("删除审核信息失败！");
                        return false;
                    }
                }




                TASM_SUPPORT_PUSH_Da push = new TASM_SUPPORT_PUSH_Da();

                if (push.Db.Queryable<TASM_SUPPORT_PUSH>().Where(s => s.SID == sid).Count() > 0)
                {
                    if (push.Db.Deleteable<TASM_SUPPORT_PUSH>().Where(s => s.SID == sid).ExecuteCommand() <= 0)
                    {
                        support.Db.RollbackTran();
                        this.Msg = "删除审核信息失败！";
                        Logger.LogInformation("删除审核信息失败！");
                        return false;
                    }
                }



                support.Db.CommitTran();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogInformation(e.ToString());
                support.Db.RollbackTran();
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

            if (model.Filelist == null || model.Filelist.Count <=0)
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


        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="da">不在同一个事务中，查询不到id</param>
        /// <returns></returns>
        private bool PushMessage(int sid, TASM_SUPPORT_Da da) {


            if (Entites.AppConfig.IsPush == false)
            {
                Logger.LogInformation("不发送消息通知，若需要请打开配置文件");
                return true;
            } 

            try
            {
                var model= da.SelectBySid4Push(sid);

                string title = $"您有一份新的任务，问题管理表编号[{model.CODE}],请登录售后管理系统查看详情。";

                StringBuilder content = new StringBuilder();

                content.AppendLine($"项目名称：{model.PROJECTNAME}[{model.PROJECTCODE}]");
                content.AppendLine($"问题机型：{model.MACHINENAME}[{model.MACHINESERIAL}]");

                content.AppendLine($"问题类型：{ Enum.GetName(typeof(SupportProblemType), model.TYPE)}");
                content.AppendLine($"当前处理人：{model.CONDUCTORNAME}");

                content.AppendLine($"流程节点：{Enum.GetName(typeof(SupportendPoint), model.STATUS).Replace('_','>')}");

                content.AppendLine($"问题描述：{model.CONTENT}");

                PushHelper.PushWeChat(model.ConductorWorkId, title, content.ToString());
                return true;
            }
            catch (Exception e) {

                Logger.LogInformation("" + e);
                return false;
            }


        }
    }
}
