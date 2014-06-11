using System;
using System.Collections.Generic;
using System.Text;

namespace SoftRegApp
{
	/// <summary>
	/// 作者：xxh 
	/// 时间：2014-04-26 13:15:19
	/// 版本：V1.0.0 	 
	/// </summary>
	public class SoftReg
	{
		public int[] intCode = new int[127];//存储密钥
		public int[] intNumber = new int[25];//存机器码的Ascii值
		public char[] Charcode = new char[25];//存储机器码字
		public void setIntCode()//给数组赋值小于10的数
		{
			for (int i = 1; i < intCode.Length; i++)
			{
				intCode[i] = i % 9;
			}
		}

		/// <summary>
		/// 生成注册码
		/// </summary>
		/// <returns></returns>
		public string GetRNum(string machineNum)
		{
			setIntCode();//初始化127位数组
			//string MNum = this.GetMNum();//获取注册码
			string MNum = machineNum;//获取注册码
			for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
			{
				Charcode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
			}
			for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
			{
				intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
			}
			string strAsciiName = "";//用于存储注册码
			for (int j = 1; j < intNumber.Length; j++)
			{
				if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
				{
					strAsciiName += Convert.ToChar(intNumber[j]).ToString();
				}
				else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
				{
					strAsciiName += Convert.ToChar(intNumber[j]).ToString();
				}
				else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
				{
					strAsciiName += Convert.ToChar(intNumber[j]).ToString();
				}
				else//判断字符ASCII值不在以上范围内
				{
					if (intNumber[j] > 122)//判断字符ASCII值是否大于z
					{
						strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
					}
					else
					{
						strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
					}
				}
			}
			return strAsciiName;//返回注册码
		}
	}
}
