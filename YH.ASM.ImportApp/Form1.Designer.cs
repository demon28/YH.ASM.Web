namespace YH.ASM.ImportApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_chose = new System.Windows.Forms.Button();
            this.tb_filePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.btn_star = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_chose
            // 
            this.btn_chose.Location = new System.Drawing.Point(584, 25);
            this.btn_chose.Name = "btn_chose";
            this.btn_chose.Size = new System.Drawing.Size(75, 23);
            this.btn_chose.TabIndex = 0;
            this.btn_chose.Text = "选择文件";
            this.btn_chose.UseVisualStyleBackColor = true;
            this.btn_chose.Click += new System.EventHandler(this.btn_chose_Click);
            // 
            // tb_filePath
            // 
            this.tb_filePath.Location = new System.Drawing.Point(94, 26);
            this.tb_filePath.Name = "tb_filePath";
            this.tb_filePath.Size = new System.Drawing.Size(477, 21);
            this.tb_filePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "工单文件：";
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(25, 143);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.Size = new System.Drawing.Size(658, 277);
            this.tb_log.TabIndex = 3;
            // 
            // btn_star
            // 
            this.btn_star.Location = new System.Drawing.Point(584, 59);
            this.btn_star.Name = "btn_star";
            this.btn_star.Size = new System.Drawing.Size(75, 23);
            this.btn_star.TabIndex = 4;
            this.btn_star.Text = "开始导入";
            this.btn_star.UseVisualStyleBackColor = true;
            this.btn_star.Click += new System.EventHandler(this.btn_star_ClickAsync);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 450);
            this.Controls.Add(this.btn_star);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_filePath);
            this.Controls.Add(this.btn_chose);
            this.Name = "Form1";
            this.Text = "工单导入工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_chose;
        private System.Windows.Forms.TextBox tb_filePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Button btn_star;
    }
}

