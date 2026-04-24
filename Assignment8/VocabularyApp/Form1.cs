using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Microsoft.Data.Sqlite; // 引入 SQLite 库
using System.Data.SQLite;

namespace VocabularyApp
{
    public partial class Form1 : Form
    {
        // 定义一个内部类来存储单词数据
        public class WordItem
        {
            public string English { get; set; }
            public string Chinese { get; set; }
        }

        private List<WordItem> wordList = new List<WordItem>(); // 存放从数据库取出的单词
        private int currentIndex = 0; // 当前背到了第几个单词
        private string connectionString = "Data Source=vocabulary.db"; // SQLite 数据库文件路径

        public Form1()
        {
            InitializeComponent();
        }

        // 窗体加载时触发
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDatabase(); // 1. 自动建库并插入测试数据（方便演示）
            LoadWordsFromDatabase(); // 2. 从数据库读取数据到内存
            ShowCurrentWord(); // 3. 在界面上显示第一个单词
        }

        // 初始化数据库并写入一些测试单词
        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // 创建表
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Words (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        English TEXT NOT NULL,
                        Chinese TEXT NOT NULL
                    )";
                command.ExecuteNonQuery();

                // 检查是否已经有数据，没有则插入测试数据
                command.CommandText = "SELECT COUNT(*) FROM Words";
                long count = (long)command.ExecuteScalar();
                if (count == 0)
                {
                    command.CommandText = @"
                        INSERT INTO Words (English, Chinese) VALUES ('apple', '苹果');
                        INSERT INTO Words (English, Chinese) VALUES ('computer', '电脑');
                        INSERT INTO Words (English, Chinese) VALUES ('database', '数据库');
                        INSERT INTO Words (English, Chinese) VALUES ('program', '程序');";
                    command.ExecuteNonQuery();
                }
            }
        }

        // 从数据库中读取所有单词并存入 List
        private void LoadWordsFromDatabase()
        {
            wordList.Clear();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT English, Chinese FROM Words";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wordList.Add(new WordItem
                        {
                            English = reader.GetString(0),
                            Chinese = reader.GetString(1)
                        });
                    }
                }
            }
        }

        // 显示当前索引对应的中文
        private void ShowCurrentWord()
        {
            if (currentIndex < wordList.Count)
            {
                lblChinese.Text = wordList[currentIndex].Chinese;
                txtEnglish.Clear(); // 清空输入框准备输入
                txtEnglish.Focus();
                lblResult.Text = ""; // 清空提示
            }
            else
            {
                lblChinese.Text = "练习结束！";
                txtEnglish.Enabled = false;
                lblResult.Text = "全部完成";
            }
        }

        // 监听文本框的按键事件（题目要求：回车比较）
        private async void txtEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            // 判断是否按下了回车键
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 阻止回车键产生的“叮”提示音

                if (currentIndex >= wordList.Count) return;

                string expectedEnglish = wordList[currentIndex].English;
                string userInput = txtEnglish.Text.Trim();

                // 比较时忽略大小写
                if (string.Equals(expectedEnglish, userInput, StringComparison.OrdinalIgnoreCase))
                {
                    lblResult.Text = "正确";
                    lblResult.ForeColor = System.Drawing.Color.Green;

                    // 临时禁用输入框，防止在等待期间乱按
                    txtEnglish.Enabled = false;

                    // 修改2：让程序在这里稍微等 1 秒钟 (1000毫秒)
                    await System.Threading.Tasks.Task.Delay(1000);

                    // 恢复输入框并进入下一个单词
                    txtEnglish.Enabled = true;

                    // 回答正确，稍微延迟或直接进入下一个（这里选择直接进入下一个）
                    currentIndex++;
                    ShowCurrentWord();
                }
                else
                {
                    lblResult.Text = "错误";
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    // 选做：答错了可以清空文本框让用户重试，或者显示正确答案
                    txtEnglish.SelectAll();
                }
            }
        }
    }
}