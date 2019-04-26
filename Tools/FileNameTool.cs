using DEVGIS.Tools.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DEVGIS.Tools
{
    public partial class FileNameTool : Form
    {
        public FileNameTool()
        {
            InitializeComponent();
        }

        private void btChangeExtName_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var list = new System.IO.DirectoryInfo(fbd.SelectedPath).GetFiles("*", System.IO.SearchOption.AllDirectories);
                    foreach (FileInfo f in list)
                    {
                        string name = f.Name;
                        string ext = f.Extension.Trim('.');

                        string newname = string.Empty;
                        if (string.IsNullOrEmpty(ext))
                        {
                            newname = name+"."+cbExt.Text.Trim('.');
                        }
                        else
                        {
                            newname = name.Replace(ext, cbExt.Text.Trim('.'));
                        }
                        try
                        {
                            Directory.Move(f.FullName, Path.Combine(f.FullName.Replace(name,newname)));
                        }
                        catch(Exception ex)
                        {
                            Loger.WriteLog(ex);
                        }
                    }

                }
                catch(Exception ex)
                {
                    Loger.WriteLog(ex);
                    MessageHelper.ShowError("发生错误:" + ex.Message);
                }
            }
        }
    }
}
