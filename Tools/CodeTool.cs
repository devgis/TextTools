using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
