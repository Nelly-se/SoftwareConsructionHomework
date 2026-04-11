using ConsoleApp1;
using System;
using System.Net.Http; // 用于网络请求
using System.Text.RegularExpressions; // 用于正则表达式
using System.Threading.Tasks; // 用于异步编程

private async void btnFetch_Click(object sender, EventArgs e)
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