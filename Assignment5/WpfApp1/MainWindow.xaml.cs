using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private string currentInput = "";      // 当前输入的数字
        private string currentOperator = "";   // 当前选择的运算符
        private double firstOperand = 0;       // 第一个操作数
        private bool isNewEntry = true;        // 是否是新的输入阶段

        public MainWindow()
        {
            InitializeComponent();
        }

        // 数字按钮点击事件 (0-9)
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string number = btn.Content.ToString();

            // 如果刚计算完，清空屏幕开始新一轮
            if (isNewEntry && currentOperator == "")
            {
                txtDisplay.Text = "";
                currentInput = "";
            }

            isNewEntry = false;
            currentInput += number;
            txtDisplay.Text += number; // 实时显示在文本框中
        }

        // 运算符按钮点击事件 (+, -, *, /)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (!string.IsNullOrEmpty(currentInput))
            {
                firstOperand = double.Parse(currentInput);
                currentOperator = btn.Content.ToString();
                txtDisplay.Text += currentOperator; // 将运算符追加到显示框
                currentInput = ""; // 清空当前输入，准备接收第二个数字
            }
        }

        // 等号按钮点击事件 (=)
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(currentOperator))
            {
                double secondOperand = double.Parse(currentInput);
                double result = 0;

                // 执行计算
                switch (currentOperator)
                {
                    case "+": result = firstOperand + secondOperand; break;
                    case "-": result = firstOperand - secondOperand; break;
                    case "*": result = firstOperand * secondOperand; break;
                    case "/":
                        if (secondOperand != 0)
                            result = firstOperand / secondOperand;
                        else
                        {
                            MessageBox.Show("除数不能为0！");
                            return;
                        }
                        break;
                }

                // 按照作业要求格式化输出： 表达式及结果如：18+5=23
                txtDisplay.Text = $"{firstOperand}{currentOperator}{secondOperand}={result}";

                // 重置状态，以便在结果的基础上继续计算（可选）
                currentInput = result.ToString();
                currentOperator = "";
                isNewEntry = true;
            }
        }

        // 清空按钮点击事件 (C)
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            currentOperator = "";
            firstOperand = 0;
            txtDisplay.Text = "";
            isNewEntry = true;
        }
    }
}