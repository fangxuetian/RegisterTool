namespace WinTest
{
    partial class frMainKey
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frMainKey));
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnInstall = new System.Windows.Forms.Button();
			this.lblLog = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbCheckDays = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbPreAlertDays = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnSaveConfig = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(19, 175);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "安装服务";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(108, 175);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "启动服务";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnInstall
			// 
			this.btnInstall.Location = new System.Drawing.Point(195, 175);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new System.Drawing.Size(75, 23);
			this.btnInstall.TabIndex = 2;
			this.btnInstall.Text = "卸载服务";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			// 
			// lblLog
			// 
			this.lblLog.AutoSize = true;
			this.lblLog.ForeColor = System.Drawing.Color.Blue;
			this.lblLog.Location = new System.Drawing.Point(17, 211);
			this.lblLog.Name = "lblLog";
			this.lblLog.Size = new System.Drawing.Size(41, 12);
			this.lblLog.TabIndex = 3;
			this.lblLog.Text = "123456";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(18, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(253, 70);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "说明";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(16, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(227, 36);
			this.label1.TabIndex = 0;
			this.label1.Text = "本软件是用于安装和卸载授权服务的工具\r\n请在安装服务完成以后启动服务。最后使\r\n\"Shift+Del\"快捷键将整个安装文件删除。";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "授权使用";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "提前";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(93, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "天警告";
			// 
			// tbCheckDays
			// 
			this.tbCheckDays.Location = new System.Drawing.Point(65, 22);
			this.tbCheckDays.Name = "tbCheckDays";
			this.tbCheckDays.Size = new System.Drawing.Size(49, 21);
			this.tbCheckDays.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(123, 26);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "天";
			// 
			// tbPreAlertDays
			// 
			this.tbPreAlertDays.Location = new System.Drawing.Point(44, 49);
			this.tbPreAlertDays.Name = "tbPreAlertDays";
			this.tbPreAlertDays.Size = new System.Drawing.Size(43, 21);
			this.tbPreAlertDays.TabIndex = 10;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnSaveConfig);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.tbPreAlertDays);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.tbCheckDays);
			this.groupBox2.Location = new System.Drawing.Point(19, 84);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(251, 76);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "配置";
			// 
			// btnSaveConfig
			// 
			this.btnSaveConfig.Location = new System.Drawing.Point(163, 27);
			this.btnSaveConfig.Name = "btnSaveConfig";
			this.btnSaveConfig.Size = new System.Drawing.Size(75, 35);
			this.btnSaveConfig.TabIndex = 12;
			this.btnSaveConfig.Text = "保存配置";
			this.btnSaveConfig.UseVisualStyleBackColor = true;
			this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
			// 
			// frMainKey
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 226);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblLog);
			this.Controls.Add(this.btnInstall);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frMainKey";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "授权服务";
			this.Load += new System.EventHandler(this.frMainKey_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbCheckDays;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbPreAlertDays;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnSaveConfig;
    }
}