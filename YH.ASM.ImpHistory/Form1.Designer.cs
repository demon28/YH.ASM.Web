namespace YH.ASM.ImpHistory
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_chose = new System.Windows.Forms.Button();
            this.btn_star = new System.Windows.Forms.Button();
            this.tb_filePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_chose
            // 
            this.btn_chose.Location = new System.Drawing.Point(606, 31);
            this.btn_chose.Name = "btn_chose";
            this.btn_chose.Size = new System.Drawing.Size(83, 41);
            this.btn_chose.TabIndex = 0;
            this.btn_chose.Text = "选择文件";
            this.btn_chose.UseVisualStyleBackColor = true;
            this.btn_chose.Click += new System.EventHandler(this.btn_chose_Click);
            // 
            // btn_star
            // 
            this.btn_star.Location = new System.Drawing.Point(606, 96);
            this.btn_star.Name = "btn_star";
            this.btn_star.Size = new System.Drawing.Size(83, 41);
            this.btn_star.TabIndex = 0;
            this.btn_star.Text = "开始导入";
            this.btn_star.UseVisualStyleBackColor = true;
            this.btn_star.Click += new System.EventHandler(this.btn_star_Click);
            // 
            // tb_filePath
            // 
            this.tb_filePath.Location = new System.Drawing.Point(114, 40);
            this.tb_filePath.Name = "tb_filePath";
            this.tb_filePath.Size = new System.Drawing.Size(486, 23);
            this.tb_filePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件地址：";
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(68, 169);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.Size = new System.Drawing.Size(613, 199);
            this.tb_log.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 398);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_filePath);
            this.Controls.Add(this.btn_star);
            this.Controls.Add(this.btn_chose);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_chose;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_filePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_star;
        private System.Windows.Forms.TextBox tb_log;
    }
}

