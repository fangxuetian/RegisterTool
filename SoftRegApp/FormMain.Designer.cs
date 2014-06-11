namespace SoftRegApp
{
	partial class FormMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.btnReg = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tbDiskNumber = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbRegNum = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btnReg
			// 
			this.btnReg.Enabled = false;
			this.btnReg.Location = new System.Drawing.Point(128, 165);
			this.btnReg.Name = "btnReg";
			this.btnReg.Size = new System.Drawing.Size(145, 27);
			this.btnReg.TabIndex = 19;
			this.btnReg.Text = "生成注册码";
			this.btnReg.UseVisualStyleBackColor = true;
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 113);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 17;
			this.label2.Text = "注册码：";
			// 
			// tbDiskNumber
			// 
			this.tbDiskNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tbDiskNumber.Location = new System.Drawing.Point(74, 57);
			this.tbDiskNumber.MaxLength = 24;
			this.tbDiskNumber.Name = "tbDiskNumber";
			this.tbDiskNumber.Size = new System.Drawing.Size(291, 21);
			this.tbDiskNumber.TabIndex = 16;
			this.toolTip1.SetToolTip(this.tbDiskNumber, "请输入24位序列号");
			this.tbDiskNumber.TextChanged += new System.EventHandler(this.tbDiskNumber_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 15;
			this.label1.Text = "序列号：";
			// 
			// tbRegNum
			// 
			this.tbRegNum.Location = new System.Drawing.Point(74, 109);
			this.tbRegNum.Name = "tbRegNum";
			this.tbRegNum.ReadOnly = true;
			this.tbRegNum.Size = new System.Drawing.Size(291, 21);
			this.tbRegNum.TabIndex = 18;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 247);
			this.Controls.Add(this.btnReg);
			this.Controls.Add(this.tbRegNum);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbDiskNumber);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "128上传软件注册机";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnReg;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbDiskNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbRegNum;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}