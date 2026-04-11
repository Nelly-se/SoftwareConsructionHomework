using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http; // 用于网络请求
using System.Text.RegularExpressions; // 用于正则表达式

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(url)) return;

            try
            {
                // 1. 获取网页源代码
                HttpClient client = new HttpClient();
                string htmlContent = await client.GetStringAsync(url);

                // 2. 定义正则表达式
                // 手机号正则（简单匹配11位）
                string phonePattern = @"1[3-9]\d{9}";
                // 邮箱正则
                string emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

                // 3. 执行匹配
                MatchCollection phoneMatches = Regex.Matches(htmlContent, phonePattern);
                MatchCollection emailMatches = Regex.Matches(htmlContent, emailPattern);

                // 4. 显示结果
                rtbResult.Clear();
                rtbResult.AppendText("--- 找到的手机号 ---\n");
                foreach (Match match in phoneMatches)
                {
                    rtbResult.AppendText(match.Value + "\n");
                }

                rtbResult.AppendText("\n--- 找到的邮箱 ---\n");
                foreach (Match match in emailMatches)
                {
                    rtbResult.AppendText(match.Value + "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取失败: " + ex.Message);
            }
        }
    }
}
