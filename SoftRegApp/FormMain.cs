using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoftRegApp
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void btnReg_Click(object sender, EventArgs e)
		{
			SoftReg sr = new SoftReg();
			string buildNum = sr.GetRNum(tbDiskNumber.Text.Trim());
			buildNum = buildNum.Insert(4, "-");
			buildNum = buildNum.Insert(9, "-");
			buildNum = buildNum.Insert(14, "-");
			buildNum = buildNum.Insert(19, "-");
			buildNum = buildNum.Insert(24, "-");

			tbRegNum.Text = buildNum;
		}

		private void tbDiskNumber_TextChanged(object sender, EventArgs e)
		{
			if (tbDiskNumber.Text.Trim().Length < 24)
			{
				btnReg.Enabled = false;
			}
			else
			{
				btnReg.Enabled = true;
			}
		}
	}
}
