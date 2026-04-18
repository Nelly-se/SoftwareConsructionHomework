using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchEngineApp
{
    public partial class Form1 : Form
    {
        // 推荐复用 HttpClient 实例
        private static readonly HttpClient httpClient = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        // 注意：事件处理程序必须标记为 async
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入搜索关键字！");
                return;
            }

            // 搜索开始时禁用按钮，防止重复点击，并提示用户
            btnSearch.Enabled = false;
            txtBaidu.Text = "百度搜索中，请稍候...";
            txtBing.Text = "Bing搜索中，请稍候...";

            try
            {
                string baiduUrl = $"https://www.baidu.com/s?wd={Uri.EscapeDataString(keyword)}";
                string bingUrl = $"https://cn.bing.com/search?q={Uri.EscapeDataString(keyword)}";

                // 【核心要求】：使用异步任务并行发起两个网络请求
                Task<string> baiduTask = FetchAndExtractTextAsync(baiduUrl);
                Task<string> bingTask = FetchAndExtractTextAsync(bingUrl);

                // 等待两个任务全部完成 (并行执行，总耗时取决于最慢的那个)
                await Task.WhenAll(baiduTask, bingTask);

                // 将结果显示到多行文本框中
                txtBaidu.Text = baiduTask.Result;
                txtBing.Text = bingTask.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}");
            }
            finally
            {
                // 无论成功还是失败，最后都要恢复按钮可用状态
                btnSearch.Enabled = true;
            }
        }

        /// <summary>
        /// 异步获取网页 HTML 并提取前 200 个字符的纯文本
        /// </summary>
        private async Task<string> FetchAndExtractTextAsync(string url)
        {
            try
            {
                // 构造请求，添加 User-Agent 伪装成浏览器，防止被搜索引擎拦截
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

                // 发送异步 GET 请求
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // 异步读取响应内容
                string htmlContent = await response.Content.ReadAsStringAsync();

                // 使用正则表达式去除 HTML 标签 (<...>)
                string plainText = Regex.Replace(htmlContent, "<[^>]+>", string.Empty);

                // 去除多余的换行符和空格，让文本更紧凑
                plainText = Regex.Replace(plainText, @"\s+", " ").Trim();

                // 截取前 200 个字
                if (plainText.Length > 200)
                {
                    return plainText.Substring(0, 200);
                }

                return plainText;
            }
            catch (Exception ex)
            {
                return $"获取数据失败: {ex.Message}";
            }
        }
    }
}