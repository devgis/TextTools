using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Tools
{
    public partial class CodeTool : Form
    {
        public CodeTool()
        {
            InitializeComponent();
        }

        private void btGenerate_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in textBox1.Lines)
            {
                string temp = s.Replace("  ", " ").Replace('\t',' ').TrimStart(' ');
                string[] arr = temp.Split(' ');
                sb.AppendLine("///<summary>");
                sb.AppendLine("///"+s);
                sb.AppendLine("///</summary>");
                sb.AppendLine(string.Format("public string {0}",arr[0]));
                sb.AppendLine("{");
                sb.AppendLine("  get;");
                sb.AppendLine("  set;");
                sb.AppendLine("}");
                sb.AppendLine("");
            }

            textBox2.Text = sb.ToString();
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            textBox2.Copy();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btGenFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbExtend.Text.Trim()))
            {
                MessageBox.Show("请填写文件后缀！");
                tbExtend.Focus();
                return;
            }
            FolderBrowserDialog sfd = new FolderBrowserDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                foreach (string s in textBox1.Lines)
                {
                    if(!string.IsNullOrEmpty(s))
                    {
                        try
                        {
                            File.Create(Path.Combine(sfd.SelectedPath, s + "." + tbExtend.Text));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("发生异常:" + ex.Message);
                        }
                    }
                }
            }
            MessageBox.Show("执行成功！ ");
           
        }

        #region 抓取页面所有url
        private void btGetURLS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbExtend.Text.Trim()))
            {
                MessageBox.Show("请输入url！");
                tbExtend.Focus();
                return;
            }

            string strURL = tbExtend.Text;
            if (strURL.StartsWith ("http://"))
            {
                strURL = @"http://" + strURL;
            }
            string strCode = GetPageSource(strURL);
            List<string> alLinks = GetHyperLinks(strCode);
            textBox1.Text = string.Empty;

            if (alLinks != null && alLinks.Count > 0)
            {
                foreach (string s in alLinks)
                {
                    textBox1.Text += s + "\r\n";
                }
                MessageBox.Show("完毕！");
            }
            else
            {
                MessageBox.Show("没有抓取到结果！");
            }
        }

        // 获取指定网页的HTML代码 
        public static string GetPageSource(string URL)
        {
            Uri uri = new Uri(URL);

            HttpWebRequest hwReq = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse hwRes = (HttpWebResponse)hwReq.GetResponse();

            hwReq.Method = "Get";

            hwReq.KeepAlive = false;

            StreamReader reader = new StreamReader(hwRes.GetResponseStream(), System.Text.Encoding.UTF8);

            return reader.ReadToEnd();
        }
        // 提取HTML代码中的网址 
        public static List<string> GetHyperLinks(string htmlCode)
        {
            List<string> al = new List<string>();

            string strRegex = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            MatchCollection m = r.Matches(htmlCode);

            for (int i = 0; i <= m.Count - 1; i++)
            {
                bool rep = false;
                string strNew = m[i].ToString();

                // 过滤重复的URL 
                foreach (string str in al)
                {
                    if (strNew == str)
                    {
                        rep = true;
                        break;
                    }
                }

                if (!rep) al.Add(strNew);
            }

            al.Sort();

            return al;
        }
        // 把网址写入xml文件 
        public static void WriteToXml(string strURL, List<string> alHyperLinks)
        {
            XmlTextWriter writer = new XmlTextWriter("HyperLinks.xml", Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(false);
            writer.WriteDocType("HyperLinks", null, "urls.dtd", null);
            writer.WriteComment("提取自" + strURL + "的超链接");
            writer.WriteStartElement("HyperLinks");
            writer.WriteStartElement("HyperLinks", null);
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());


            foreach (string str in alHyperLinks)
            {
                string title = GetDomain(str);
                string body = str;
                writer.WriteElementString(title, null, body);
            }

            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
        }

        // 获取网址的域名后缀 
        static string GetDomain(string strURL)
        {
            string retVal;

            string strRegex = @"(\.com/|\.net/|\.cn/|\.org/|\.gov/)";

            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
            Match m = r.Match(strURL);
            retVal = m.ToString();

            strRegex = @"\.|/$";
            retVal = Regex.Replace(retVal, strRegex, "").ToString();

            if (retVal == "")
                retVal = "other";

            return retVal;
        }
        #endregion
    }
}
