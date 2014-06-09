using System;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace RegisterTool
{
    public static class FileConfig
    {
        public static string dataFilePath;

        #region 获得配置文件中的值
        public static string GetConfigValue(string appKey)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
				string configPath = System.Windows.Forms.Application.ExecutablePath + @".config";
				xDoc.Load(configPath);

                XmlNode xNode;
                XmlElement xElem;
				xNode = xDoc.SelectSingleNode("//connectionStrings");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@name='" + appKey + "']");
                if (xElem != null)
					return xElem.GetAttribute("connectionString");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 修改配置文件中的值
        public static void SetValue(string AppKey, string AppValue)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                //获取可执行文件的路径和名称
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + @".config");

                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;
				xNode = xDoc.SelectSingleNode("//connectionStrings");

				xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@name='" + AppKey + "']");
				if (xElem1 != null) xElem1.SetAttribute("connectionString", AppValue);
                else
                {
                    xElem2 = xDoc.CreateElement("add");
					xElem2.SetAttribute("name", AppKey);
					xElem2.SetAttribute("connectionString", AppValue);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
            }
            catch (Exception e)
            {
                MessageBox.Show("客户端报错" + e.ToString());
            }
        }
        #endregion

        #region 文件操作
        /// <summary>
        /// 保存所有字符串到文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="stringContent"></param>
        public static void SaveAllText(string FileName, string stringContent)
        {
            if (!File.Exists(dataFilePath + FileName))
            {
                File.WriteAllText(dataFilePath + FileName, stringContent, Encoding.GetEncoding("gb2312"));
            }
        }

        /// <summary>
        /// 追加所有字符串到文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="stringContent"></param>
        public static void SaveAppendText(string FileName, string stringContent)
        {
            File.AppendAllText(dataFilePath + FileName, stringContent, Encoding.GetEncoding("gb2312"));
        }
        #endregion
    }
}
