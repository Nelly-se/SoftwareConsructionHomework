namespace SearchEngineApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBaidu = new System.Windows.Forms.TextBox();
            this.txtBing = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(174, 12);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(337, 25);
            this.txtKeyword.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(533, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBaidu
            // 
            this.txtBaidu.Location = new System.Drawing.Point(174, 68);
            this.txtBaidu.Multiline = true;
            this.txtBaidu.Name = "txtBaidu";
            this.txtBaidu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBaidu.Size = new System.Drawing.Size(434, 149);
            this.txtBaidu.TabIndex = 2;
            // 
            // txtBing
            // 
            this.txtBing.Location = new System.Drawing.Point(174, 240);
            this.txtBing.Multiline = true;
            this.txtBing.Name = "txtBing";
            this.txtBing.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBing.Size = new System.Drawing.Size(434, 163);
            this.txtBing.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtBing);
            this.Controls.Add(this.txtBaidu);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtKeyword);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBaidu;
        private System.Windows.Forms.TextBox txtBing;
    }
}

