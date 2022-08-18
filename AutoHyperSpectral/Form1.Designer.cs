
using System.Windows.Forms;

namespace AutoHyperSpectral
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.detectLeafButton = new System.Windows.Forms.Button();
            this.selectLeafButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.saveCsvButton = new System.Windows.Forms.Button();
            this.saveJsonButton = new System.Windows.Forms.Button();
            this.judgeDiseaseButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenAvi);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(227, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // detectLeafButton
            // 
            this.detectLeafButton.Enabled = false;
            this.detectLeafButton.Location = new System.Drawing.Point(29, 61);
            this.detectLeafButton.Name = "detectLeafButton";
            this.detectLeafButton.Size = new System.Drawing.Size(75, 23);
            this.detectLeafButton.TabIndex = 2;
            this.detectLeafButton.Text = "detect leaf";
            this.detectLeafButton.UseVisualStyleBackColor = true;
            this.detectLeafButton.Click += new System.EventHandler(this.PostImage);
            // 
            // selectLeafButton
            // 
            this.selectLeafButton.Enabled = false;
            this.selectLeafButton.Location = new System.Drawing.Point(29, 90);
            this.selectLeafButton.Name = "selectLeafButton";
            this.selectLeafButton.Size = new System.Drawing.Size(75, 23);
            this.selectLeafButton.TabIndex = 4;
            this.selectLeafButton.Text = "select leaf";
            this.selectLeafButton.UseVisualStyleBackColor = true;
            this.selectLeafButton.Click += new System.EventHandler(this.SelectLeaf);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 19);
            this.textBox1.TabIndex = 5;
            // 
            // saveCsvButton
            // 
            this.saveCsvButton.Enabled = false;
            this.saveCsvButton.Location = new System.Drawing.Point(29, 119);
            this.saveCsvButton.Name = "saveCsvButton";
            this.saveCsvButton.Size = new System.Drawing.Size(75, 23);
            this.saveCsvButton.TabIndex = 6;
            this.saveCsvButton.Text = "save csv";
            this.saveCsvButton.UseVisualStyleBackColor = true;
            this.saveCsvButton.Click += new System.EventHandler(this.SaveCSV);
            // 
            // saveJsonButton
            // 
            this.saveJsonButton.Enabled = false;
            this.saveJsonButton.Location = new System.Drawing.Point(29, 148);
            this.saveJsonButton.Name = "saveJsonButton";
            this.saveJsonButton.Size = new System.Drawing.Size(75, 23);
            this.saveJsonButton.TabIndex = 7;
            this.saveJsonButton.Text = "save json";
            this.saveJsonButton.UseVisualStyleBackColor = true;
            this.saveJsonButton.Click += new System.EventHandler(this.SaveJson);
            // 
            // judgeDiseaseButton
            // 
            this.judgeDiseaseButton.Enabled = false;
            this.judgeDiseaseButton.Location = new System.Drawing.Point(29, 177);
            this.judgeDiseaseButton.Name = "judgeDiseaseButton";
            this.judgeDiseaseButton.Size = new System.Drawing.Size(75, 23);
            this.judgeDiseaseButton.TabIndex = 8;
            this.judgeDiseaseButton.Text = "judge disease";
            this.judgeDiseaseButton.UseVisualStyleBackColor = true;
            this.judgeDiseaseButton.Click += new System.EventHandler(this.judgeDisease);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 427);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(800, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 405);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Click += new System.EventHandler(this.toolStripProgressBar1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.judgeDiseaseButton);
            this.Controls.Add(this.saveJsonButton);
            this.Controls.Add(this.saveCsvButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.selectLeafButton);
            this.Controls.Add(this.detectLeafButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button detectLeafButton;
        private System.Windows.Forms.Button selectLeafButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button saveCsvButton;
        private System.Windows.Forms.Button saveJsonButton;
        private System.Windows.Forms.Button judgeDiseaseButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private StatusStrip statusStrip1;
        public ToolStripProgressBar toolStripProgressBar1;
    }
}

