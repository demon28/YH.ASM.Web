using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YH.ASM.DataAccess;
using YH.ASM.Entites.CodeGenerator;

namespace YH.ASM.ImportApp
{
    public partial class Form1 : Form
    {


        public const string default_userName = "曾丽蓉";
        public const int default_userId = 1933;
        public const string default_userWorkId = "11000200";
      

        public const int default_projectId = 1;

        public const int default_machineId = 1;




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

   



        private void btn_star_ClickAsync(object sender, EventArgs e)
        {


            //若有缺失用户直接指定 曾丽蓉


            IImporter Importer = new ExcelImporter();

            string filepath = this.tb_filePath.Text;

            var result = Importer.Import<ExcelModel>(filepath);

            List<ExcelModel> list = result.Result.Data.ToList();



            //TODO：  将excel 拆成5张表


            try
            {

                foreach (var item in list)
                {

                    //step1 添加工单主表，

                    Entites.CodeGenerator.TASM_SUPPORT supportModel = new Entites.CodeGenerator.TASM_SUPPORT();

                    supportModel.CREATOR = GetUserIdByName(item.CreateName);     //工单初始创建人
                    supportModel.CONTENT = item.Content;  //问题描述
                    supportModel.TYPE = item.SupportType;
                    supportModel.SEVERITY = item.Severity;
                    supportModel.FINDATE =DateTime.Parse( item.CreateName);
                    supportModel.CONDUCTOR = GetUserIdByName(item.ConductorName);
                    supportModel.STATUS = item.EndPiont;
                    supportModel.STATE = item.ConductorStatus;
                    supportModel.MID = GetsMachineByName(item.MachineCode);
                    supportModel.CODE = GetSupportCode(Project.CODE);



                    DataAccess.TASM_SUPPORT_Da daSupport = new TASM_SUPPORT_Da();
                    daSupport.CurrentDb.Insert(supportModel);


                    //step2 添加历史表
                    






                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        public int GetUserIdByName(string name) {

            if (string.IsNullOrEmpty(name))
            {
                return default_userId;
            }

            DataAccess.TASM_USERManager _USERManager = new DataAccess.TASM_USERManager();

            var user= _USERManager.SelectByUserName(name);

            if (user==null)
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
        public int GetsProjectByName(string name) {

            if (string.IsNullOrEmpty(name))
            {
                return default_projectId;
            }

            DataAccess.TASM_PROJECTManager _PROJECTManager = new DataAccess.TASM_PROJECTManager();

            Project= _PROJECTManager.SelectByName(name);
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
        public string GetSupportCode(string projectcode) {

            TASM_SUPPORT_Da da = new TASM_SUPPORT_Da();
            string code = projectcode + "-" + da.SelectSupprotCodeIndex();

            return code;
        }

    }
}
