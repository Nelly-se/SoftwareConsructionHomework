using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

// 这里的命名空间必须和你的项目名完全一致！
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 选择第一个文件按钮
        private void btnSelect1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                openDlg.Title = "请选择第一个文本文件";
                openDlg.RestoreDirectory = true;

                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    txtFile1.Text = openDlg.FileName;
                }
            }
        }

        // 选择第二个文件按钮
        private void btnSelect2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                openDlg.Title = "请选择第二个文本文件";
                openDlg.RestoreDirectory = true;

                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    txtFile2.Text = openDlg.FileName;
                }
            }
        }

        // 合并文件核心逻辑
        private void btnMerge_Click(object sender, EventArgs e)
        {
            // 1. 校验输入：是否选中两个文件
            string file1Path = txtFile1.Text.Trim();
            string file2Path = txtFile2.Text.Trim();

            if (string.IsNullOrWhiteSpace(file1Path) || string.IsNullOrWhiteSpace(file2Path))
            {
                MessageBox.Show("请先选择两个文本文件！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 校验文件是否真实存在
            if (!File.Exists(file1Path) || !File.Exists(file2Path))
            {
                MessageBox.Show("选中的文件不存在，请重新选择！", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 3. 获取程序exe目录，自动创建Data子目录
                string exeDir = Application.StartupPath; // .NET Framework专用
                string dataDir = Path.Combine(exeDir, "Data");
                Directory.CreateDirectory(dataDir); // 不存在则新建，存在不报错

                // 4. 生成带时间戳的新文件名（避免覆盖历史文件）
                string newFileName = $"merged_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string newFilePath = Path.Combine(dataDir, newFileName);

                // 5. 读取文件内容（UTF8编码，解决中文乱码）
                // 若文件为GBK编码，替换为 Encoding.GetEncoding("GB2312")
                string content1 = File.ReadAllText(file1Path, Encoding.UTF8);
                string content2 = File.ReadAllText(file2Path, Encoding.UTF8);

                // 6. 合并内容（可自定义分隔符）
                string mergedContent =
                    $"{content1}{Environment.NewLine}{Environment.NewLine}" +
                    $"========== 第二个文件内容 =========={Environment.NewLine}{Environment.NewLine}" +
                    $"{content2}";

                // 7. 写入合并后的新文件
                File.WriteAllText(newFilePath, mergedContent, Encoding.UTF8);

                // 8. 成功提示
                MessageBox.Show($"✅ 合并成功！\n新文件路径：\n{newFilePath}",
                    "操作完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // 异常捕获，友好提示
                MessageBox.Show($"❌ 合并失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}