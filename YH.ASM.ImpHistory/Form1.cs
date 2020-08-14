using log4net;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;
using YH.ASM.ImportApp;

namespace YH.ASM.ImpHistory
{
    public partial class Form1 : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Form1));

        public const string default_userName = "曾丽蓉";
        public const int default_userId = 1933;
        public const string default_userWorkId = "11000200";


        public const int default_projectId = 1;

        public const int default_machineId = 1;



        private void Log(string text)
        {
            if (tb_log.InvokeRequired)
            {
                Action<string> action = new Action<string>(Log);
                Invoke(action, new object[] { text });
            }
            else
            {
                tb_log.Text += "\r\n";
                tb_log.AppendText(text);
                //定位到最后一行
            }
        }



        public TASM_PROJECT Project { get; set; }
        public TASM_MACHINE Machine { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_chose_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tb_filePath.Text = dialog.FileName;
            }
        }

        private void btn_star_Click(object sender, EventArgs e)
        {
            string filepath = this.tb_filePath.Text;

            IExcelImporter Importer = new ExcelImporter();

            var result = Importer.Import<ExcelModel>(filepath);


            if (result.Result.Data == null)
            {
                MessageBox.Show("读取excel失败！");
                return;
            }


            List<ExcelModel> list = result.Result.Data.ToList();



            //TODO：  将excel 拆成5张表

            TASM_SUPPORT_Da daSupport = new TASM_SUPPORT_Da();
            daSupport.Db.BeginTran();   //事务开

            try
            {
                int i = 0;

                foreach (var item in list)
                {
                    i++;
                    logger.Info("===============导入开始==============" + i);
                    Log("===============导入开始==============" + i);


                    int sid = 0;   //工单id
                    TASM_SUPPORT supportModel = new TASM_SUPPORT();

                    Log("No：" + i + "创建工单-开始");
                    if (!CreateSupport(daSupport, item, ref sid, ref supportModel))
                    {
                        daSupport.Db.RollbackTran();
                        logger.Info("===============导入失败====创建工单失败==========" + item.Content);
                        Log("No：" + i + "创建工单-失败");
                        Log("===============导入失败====创建工单失败==========" + item.Content);
                        continue;
                    }
                    Log("No：" + i + "创建工单-结束");



                    //判断是否有现场处理节点，
                    if (item.IsDisposerPoint.Trim() == "否" || string.IsNullOrWhiteSpace(item.IsDisposerPoint))
                    {
                        //没有的话，继续下一条
                        Log("===============导入结束=========没有Disposer节点=====" + i);
                        continue;
                    }



                    Log("No：" + i + "现场处理-开始");
                    if (!CreateDisposer(supportModel, item, sid))
                    {
                        daSupport.Db.RollbackTran();
                        logger.Info("===============导入失败====创建现场处理失败==========" + item.Content);
                        Log("No：" + i + "现场处理-失败");
                        Log("===============导入失败====创建现场处理失败==========" + item.Content);
                        continue;
                    }
                    Log("No：" + i + "现场处理-结束");




                    if (item.IsPmcPoint.Trim() == "否" || string.IsNullOrWhiteSpace(item.IsPmcPoint))
                    {
                        //没有的话，继续下一条
                        Log("===============导入结束=========没有Pmc节点=====" + i);
                        continue;
                    }

                    Log("No：" + i + "PMC处理-开始");

                    if (!CreatePmcOrder(supportModel, item, sid))
                    {
                        daSupport.Db.RollbackTran();
                        logger.Info("===============导入失败====PMC处理失败==========" + item.Content);
                        Log("No：" + i + "PMC处理失败-失败");
                        Log("===============导入失败====PMC处理失败==========" + item.Content);
                        continue;
                    }

                    Log("No：" + i + "PMC处理-结束");


                    if (item.IsSitePoint == "否" || string.IsNullOrWhiteSpace(item.IsSitePoint))
                    {
                        //没有的话，继续下一条
                        Log("===============导入结束=========没有site节点=====" + i);
                        continue;
                    }


                    Log("No：" + i + "现场处理-开始");
                    if (!CreateSiteCheck(supportModel, item, sid))
                    {
                        daSupport.Db.RollbackTran();
                        logger.Info("===============导入失败====现场处理失败==========" + item.Content);
                     
                        Log("No：" + i + "现场处理失败-失败");
                        Log("===============导入失败====现场处理失败==========" + item.Content);
                        continue;
                    }
                    Log("No：" + i + "现场处理-结束");



                    if (item.IsPrincipalPoint == "否" || string.IsNullOrWhiteSpace(item.IsPrincipalPoint))
                    {
                        Log("===============导入结束=========没有principal节点=====" + i);
                        continue;
                    }


                    if (!CreatePrincipal(supportModel, item, sid))
                    {
                        daSupport.Db.RollbackTran();
                        logger.Info("===============导入失败====审核失败==========" + item.Content);
                        Log("No：" + i + "审核处理失败-失败");
                        Log("===============导入失败====审核失败==========" + item.Content);
                        continue;
                    }



                    Log("===============导入结束==============" + i);
                    logger.Info("===============导入结束==============" + i);

                }
            }
            catch (Exception ex)
            {
                daSupport.Db.RollbackTran();
                logger.Info("程序异常，外层捕捉：" + ex);

            }
        }


        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public int GetUserIdByName(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                return default_userId;
            }

            DataAccess.TASM_USERManager _USERManager = new DataAccess.TASM_USERManager();

            var user = _USERManager.SelectByUserName(name);

            if (user == null)
            {
                return default_userId;
            }

            return (int)user.USER_ID;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetsProjectByName(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                return default_projectId;
            }

            DataAccess.TASM_PROJECTManager _PROJECTManager = new DataAccess.TASM_PROJECTManager();

            Project = _PROJECTManager.SelectByName(name);
            if (Project == null)
            {
                return default_projectId;
            }

            return Project.PID;
        }


        /// <summary>
        /// 获取设备
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetsMachineByName(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                return default_machineId;
            }


            DataAccess.TASM_MACHINEManager manager = new DataAccess.TASM_MACHINEManager();

            Machine = manager.SelectByName(name);

            if (Machine == null)
            {
                return default_machineId;
            }

            return Machine.MID;
        }

        /// <summary>
        /// 生成工单编号
        /// </summary>
        /// <param name="projectcode"></param>
        /// <returns></returns>
        public string GetSupportCode(string projectcode)
        {

            TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            string code = projectcode + "-" + da.SelectSupprotCodeIndex();

            return code;
        }

        /// <summary>
        /// 转换时间类型
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ConventDateTime(string date)
        {

            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }


        }


        /// <summary>
        /// 创建工单
        /// </summary>
        /// <param name="daSupport"></param>
        /// <param name="item"></param>
        /// <returns></returns>

        private bool CreateSupport(TASM_SUPPORT_Da daSupport, ExcelModel item, ref int sid, ref TASM_SUPPORT supportModel)
        {

            try
            {
                logger.Info("===创建工单开始===！");

                //step1 添加工单主表，
                supportModel = new Entites.CodeGenerator.TASM_SUPPORT();

                supportModel.CREATOR = GetUserIdByName(item.CreateName);     //工单初始创建人
                supportModel.CONTENT = item.Content;  //问题描述
                supportModel.TYPE = item.SupportType;
                supportModel.SEVERITY = item.Severity;
                supportModel.FINDATE = ConventDateTime(item.Createtime);
                supportModel.CONDUCTOR = GetUserIdByName(item.ConductorName);
                supportModel.PROJECT = GetsProjectByName(item.ProjectName);
                supportModel.STATUS = item.EndPiont;
                supportModel.STATE = item.ConductorStatus;
                supportModel.MID = GetsMachineByName(item.MachineCode);
                supportModel.CODE = GetSupportCode(Project.CODE);
                supportModel.TITLE = "导入数据";
                supportModel.CREATETIME = DateTime.Now;

                sid = daSupport.Db.Insertable(supportModel).ExecuteReturnIdentity();

                logger.Info("插入工单表");

                //step2 添加历史表


                TASM_SUPPORT_HIS_Da supportHis = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS supportHisModel = new TASM_SUPPORT_HIS();

                supportHisModel.SID = sid;
                supportHisModel.PRE_USER = supportModel.CREATOR; //上一处理人
                supportHisModel.NEXT_USER = supportModel.CONDUCTOR;  //下一处理人
                supportHisModel.PRE_STATUS = 0;   //上一状态，订单初始创建，为0
                supportHisModel.NEXT_STATUS = 0;  //下一状态，订单初始创建 下一状态为 现场处理，状态0，此处创建订单不做为状态，默认创建 和 创建后未处理状态都是0
                supportHisModel.TYPE = 0;   //类型，也代表着是哪个表的数据 初始为0
                supportHisModel.TID = sid;  //数据id 根据type 那张表，看是哪一条数据。
                supportHisModel.REMARKS = "工单创建，等待技术处理,导入数据";
                supportHisModel.CREATETIME = DateTime.Now;
                supportHis.Db.Insertable(supportHisModel);

                logger.Info("插入历史表：创建工单");



                logger.Info("===创建工单结束===！");
                return true;

            }
            catch (Exception ex)
            {
                logger.Error("创建工单失败" + ex);
                return false;
            }

        }

        /// <summary>
        /// 技术处理
        /// </summary>
        /// <param name="supportModel"></param>
        /// <param name="item"></param>
        /// <param name="sid"></param>
        /// <param name="disid"></param>
        /// <returns></returns>
        private bool CreateDisposer(TASM_SUPPORT supportModel, ExcelModel item, int sid)
        {

            try
            {



                logger.Info("===创建现场处理开始===！");

                //step3 现场处理表

                TASM_SUPPORT_DISPOSER_Da supportDisposer = new TASM_SUPPORT_DISPOSER_Da();
                TASM_SUPPORT_DISPOSER disposerModel = new TASM_SUPPORT_DISPOSER();


                disposerModel.SID = sid;
                disposerModel.ANALYZEUSER = GetUserIdByName(item.AnalyzeUserName);
                disposerModel.ANALYZE = item.Analyze;
                disposerModel.SOLUTION = item.Solution;
                disposerModel.STATUS = 0;
                disposerModel.REMARKS = "导入数据";
                disposerModel.RESPONSIBLE = item.Responsible;
                disposerModel.DUTY = item.Duty;
                disposerModel.BOM = item.Bom;
                disposerModel.ORDERMAN = item.OrderMan;
                disposerModel.ORDERTIME = ConventDateTime(item.OrderTime);
                disposerModel.ISORDER = item.IsOrder == "是" ? 1 : 0;

                int disid = supportDisposer.Db.Insertable(disposerModel).ExecuteReturnIdentity();
                logger.Info("插入技术处理表完成");

                //step4 现场处理

                TASM_SUPPORT_HIS_Da supportHis = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS supportHisModel = new TASM_SUPPORT_HIS();

                supportHisModel.SID = sid;
                supportHisModel.PRE_USER = supportModel.CONDUCTOR; //上一处理人
                supportHisModel.NEXT_USER = supportModel.CONDUCTOR;  //下一处理人
                supportHisModel.PRE_STATUS = 0;   //上一状态，订单初始创建，为0
                supportHisModel.NEXT_STATUS = item.IsPmcPoint == "是" ? 2 : 1;  //下一状态，订单初始创建 下一状态为 现场处理，状态0，此处创建订单不做为状态，默认创建 和 创建后未处理状态都是0
                supportHisModel.TYPE = 1;   //类型，也代表着是哪个表的数据 初始为0
                supportHisModel.TID = disid;  //数据id 根据type 那张表，看是哪一条数据。
                supportHisModel.REMARKS = "技术已处理，等待现场处理,导入数据";
                supportHisModel.CREATETIME = DateTime.Now;
                supportHis.Db.Insertable(supportHisModel);
                logger.Info("插入历史表，技术处理完成");


                TASM_SUPPORT_PERSONAL_Da supportPersonal = new TASM_SUPPORT_PERSONAL_Da();
                TASM_SUPPORT_PERSONAL personalModel = new TASM_SUPPORT_PERSONAL();

                personalModel.CID = supportModel.CONDUCTOR;
                personalModel.DID = supportModel.CONDUCTOR;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.SID = sid;
                personalModel.STATUS = 2;
                personalModel.TID = 0;   //2代表现场完成，PMC 介入， 1则是 现场处理完成，无需PMC，直接跳到现场处理。
                personalModel.CREATETIME = DateTime.Now;
                personalModel.REMARKS = "数据导入";

                supportPersonal.Db.Insertable(personalModel);

                logger.Info("插入个人处理表，技术处理完成");


                logger.Info("===创建现场处理结束===！");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("创建Disposer失败" + ex);
                return false;
            }



        }

        /// <summary>
        /// PMC处理
        /// </summary>
        /// <param name="supportModel"></param>
        /// <param name="item"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private bool CreatePmcOrder(TASM_SUPPORT supportModel, ExcelModel item, int sid)
        {
            try
            {
                logger.Info("===创建PMC处理开始===！");
                DataAccess.TASM_SUPPORT_PMC_Da supportPmcOrder = new TASM_SUPPORT_PMC_Da();

                TASM_SUPPORT_PMC pmcModel = new TASM_SUPPORT_PMC();
                pmcModel.BOOKNO = item.BookNo;
                pmcModel.DELIVERY = ConventDateTime(item.Delivery);
                pmcModel.SENDDATE = ConventDateTime(item.SendDate);
                pmcModel.SENDNO = item.SendNo;
                pmcModel.CONSIGNEE = item.Consignee;
                pmcModel.STATUS = 0;
                pmcModel.CREATETIME = DateTime.Now;
                pmcModel.REMARKS = "导入数据";

                int pmcid = supportPmcOrder.Db.Insertable(pmcModel).ExecuteReturnIdentity();

                logger.Info("插入PMC处理表，处理完成");


                TASM_SUPPORT_HIS_Da supportHis = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS supportHisModel = new TASM_SUPPORT_HIS();

                supportHisModel.SID = sid;
                supportHisModel.PRE_USER = supportModel.CONDUCTOR; //上一处理人
                supportHisModel.NEXT_USER = supportModel.CONDUCTOR;  //下一处理人
                supportHisModel.PRE_STATUS = 1;   //上一状态，订单初始创建，为0
                supportHisModel.NEXT_STATUS = 2;  //下一状态，订单初始创建 下一状态为 现场处理，状态0，此处创建订单不做为状态，默认创建 和 创建后未处理状态都是0
                supportHisModel.TYPE = 2;   //类型，也代表着是哪个表的数据 初始为0
                supportHisModel.TID = pmcid;  //数据id 根据type 那张表，看是哪一条数据。
                supportHisModel.REMARKS = "PMC已处理,等待现场处理,导入数据";
                supportHisModel.CREATETIME = DateTime.Now;
                supportHis.Db.Insertable(supportHisModel);
                logger.Info("插入历史表，PMC处理完成");



                TASM_SUPPORT_PERSONAL_Da supportPersonal = new TASM_SUPPORT_PERSONAL_Da();
                TASM_SUPPORT_PERSONAL personalModel = new TASM_SUPPORT_PERSONAL();

                personalModel.CID = supportModel.CONDUCTOR;
                personalModel.DID = supportModel.CONDUCTOR;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.SID = sid;
                personalModel.STATUS = 2;
                personalModel.TID = 2;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.REMARKS = "数据导入";

                supportPersonal.Db.Insertable(personalModel);

                logger.Info("插入个人处理表，PMC处理完成");



                logger.Info("===创建PMC处理结束===！");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("创建Pmc失败" + ex);
                return false;
            }



        }

        /// <summary>
        /// 现场处理
        /// </summary>
        /// <returns></returns>
        private bool CreateSiteCheck(TASM_SUPPORT supportModel, ExcelModel item, int sid)
        {
            try
            {
                logger.Info("===创建现场处理开始===！");
                TASM_SUPPORT_SITE_Da supportSite = new TASM_SUPPORT_SITE_Da();

                TASM_SUPPORT_SITE siteMode = new TASM_SUPPORT_SITE();

                siteMode.CREATETIME = DateTime.Now;
                siteMode.DESCRIPTION = item.Description;
                siteMode.ENDDATE = ConventDateTime(item.EndDate);
                siteMode.REAMRKS = "导入数据";

                int siteId = supportSite.Db.Insertable(siteMode).ExecuteReturnIdentity();



                TASM_SUPPORT_HIS_Da supportHis = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS supportHisModel = new TASM_SUPPORT_HIS();

                supportHisModel.SID = sid;
                supportHisModel.PRE_USER = supportModel.CONDUCTOR; //上一处理人
                supportHisModel.NEXT_USER = supportModel.CONDUCTOR;  //下一处理人
                supportHisModel.PRE_STATUS = item.IsPmcPoint == "是" ? 2 : 1;   //上一状态，订单初始创建，为0
                supportHisModel.NEXT_STATUS = 3;  //下一状态，订单初始创建 下一状态为 现场处理，状态0，此处创建订单不做为状态，默认创建 和 创建后未处理状态都是0
                supportHisModel.TYPE = 3;   //类型，也代表着是哪个表的数据 初始为0
                supportHisModel.TID = siteId;  //数据id 根据type 那张表，看是哪一条数据。
                supportHisModel.REMARKS = "现场已处理，等待审核导入数据";
                supportHisModel.CREATETIME = DateTime.Now;
                supportHis.Db.Insertable(supportHisModel);
                logger.Info("插入历史表，现场处理完成");



                TASM_SUPPORT_PERSONAL_Da supportPersonal = new TASM_SUPPORT_PERSONAL_Da();
                TASM_SUPPORT_PERSONAL personalModel = new TASM_SUPPORT_PERSONAL();

                personalModel.CID = supportModel.CONDUCTOR;
                personalModel.DID = supportModel.CONDUCTOR;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.SID = sid;
                personalModel.STATUS = 2;
                personalModel.TID = 3;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.REMARKS = "数据导入";

                supportPersonal.Db.Insertable(personalModel);

                logger.Info("插入个人处理表，现场处理完成");







                logger.Info("===创建现场处理结束===！");

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("创建现场处理失败" + ex);
                return false;
            }

        }

        /// <summary>
        /// 创建负责人审核
        /// </summary>
        /// <param name="supportModel"></param>
        /// <param name="item"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        private bool CreatePrincipal(TASM_SUPPORT supportModel, ExcelModel item, int sid)
        {

            try
            {
                logger.Info("===创建审核开始===！");

                DataAccess.TASM_SUPPORT_PRINCIPAL_Da supportPrincipal = new TASM_SUPPORT_PRINCIPAL_Da();
                TASM_SUPPORT_PRINCIPAL princalModel = new TASM_SUPPORT_PRINCIPAL();

                princalModel.CHECKUSER = item.CheckName;
                princalModel.CREATETIME = DateTime.Now;
                princalModel.ENDDATE = ConventDateTime(item.FinishDate);
                princalModel.RESULT = item.CheckResult;
                princalModel.SID = sid;
                princalModel.STATUS = 0;
                princalModel.REMARKS = "导入数据";


                int prId = supportPrincipal.Db.Insertable(princalModel).ExecuteReturnIdentity();



                TASM_SUPPORT_HIS_Da supportHis = new TASM_SUPPORT_HIS_Da();
                TASM_SUPPORT_HIS supportHisModel = new TASM_SUPPORT_HIS();

                supportHisModel.SID = sid;
                supportHisModel.PRE_USER = supportModel.CONDUCTOR; //上一处理人
                supportHisModel.NEXT_USER = supportModel.CONDUCTOR;  //下一处理人
                supportHisModel.PRE_STATUS = item.IsPmcPoint == "是" ? 2 : 1;   //上一状态，订单初始创建，为0
                supportHisModel.NEXT_STATUS = 4;  //下一状态，订单初始创建 下一状态为 现场处理，状态0，此处创建订单不做为状态，默认创建 和 创建后未处理状态都是0
                supportHisModel.TYPE = 4;   //类型，也代表着是哪个表的数据 初始为0
                supportHisModel.TID = prId;  //数据id 根据type 那张表，看是哪一条数据。
                supportHisModel.REMARKS = "现场已处理，等待审核导入数据";
                supportHisModel.CREATETIME = DateTime.Now;
                supportHis.Db.Insertable(supportHisModel);
                logger.Info("插入历史表，审核处理完成");



                TASM_SUPPORT_PERSONAL_Da supportPersonal = new TASM_SUPPORT_PERSONAL_Da();
                TASM_SUPPORT_PERSONAL personalModel = new TASM_SUPPORT_PERSONAL();

                personalModel.CID = supportModel.CONDUCTOR;
                personalModel.DID = supportModel.CONDUCTOR;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.SID = sid;
                personalModel.STATUS = 2;
                personalModel.TID = 4;
                personalModel.CREATETIME = DateTime.Now;
                personalModel.REMARKS = "数据导入";

                supportPersonal.Db.Insertable(personalModel);

                logger.Info("插入个人处理表，审核处理完成");




                logger.Info("===创建审核结束===！");

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("创建审核失败" + ex);
                return false;
            }

        }


    }
}
