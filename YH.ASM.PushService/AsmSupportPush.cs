using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YH.ASM.PushService
{
    public partial class AsmSupportPush : ServiceBase
    {

     
        private System.Timers.Timer timer1;//初始化一个定时器   

        public AsmSupportPush()
        {
         
            InitializeComponent();


            Tool.LogHelper.Info("=================工单推送服务！=========================");

            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000;
            timer1.Elapsed += Timer1_Elapsed;
        }

        protected override void OnStart(string[] args)
        {
            this.timer1.Enabled = true;//服务启动时开启定时器
            
            Tool.LogHelper.Info("服务启动");









        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnStop()
        {
            this.timer1.Enabled = false;
            Tool.LogHelper.Info("服务停止");
        }
    }
}
