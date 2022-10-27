
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveHyperImageButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenAvi);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(167, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // detectLeafButton
            // 
            this.detectLeafButton.Enabled = false;
            this.detectLeafButton.Location = new System.Drawing.Point(13, 42);
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
            this.selectLeafButton.Location = new System.Drawing.Point(13, 71);
            this.selectLeafButton.Name = "selectLeafButton";
            this.selectLeafButton.Size = new System.Drawing.Size(75, 23);
            this.selectLeafButton.TabIndex = 4;
            this.selectLeafButton.Text = "select leaf";
            this.selectLeafButton.UseVisualStyleBackColor = true;
            this.selectLeafButton.Click += new System.EventHandler(this.SelectLeaf);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 19);
            this.textBox1.TabIndex = 5;
            // 
            // saveCsvButton
            // 
            this.saveCsvButton.Enabled = false;
            this.saveCsvButton.Location = new System.Drawing.Point(13, 100);
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
            this.saveJsonButton.Location = new System.Drawing.Point(13, 129);
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
            this.judgeDiseaseButton.Location = new System.Drawing.Point(13, 158);
            this.judgeDiseaseButton.Name = "judgeDiseaseButton";
            this.judgeDiseaseButton.Size = new System.Drawing.Size(75, 23);
            this.judgeDiseaseButton.TabIndex = 8;
            this.judgeDiseaseButton.Text = "judge disease";
            this.judgeDiseaseButton.UseVisualStyleBackColor = true;
            this.judgeDiseaseButton.Click += new System.EventHandler(this.judgeDisease);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1003, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Click += new System.EventHandler(this.toolStripProgressBar1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.saveHyperImageButton);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.detectLeafButton);
            this.panel1.Controls.Add(this.judgeDiseaseButton);
            this.panel1.Controls.Add(this.selectLeafButton);
            this.panel1.Controls.Add(this.saveJsonButton);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.saveCsvButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 450);
            this.panel1.TabIndex = 11;
            // 
            // saveHyperImageButton
            // 
            this.saveHyperImageButton.Location = new System.Drawing.Point(13, 207);
            this.saveHyperImageButton.Name = "saveHyperImageButton";
            this.saveHyperImageButton.Size = new System.Drawing.Size(75, 23);
            this.saveHyperImageButton.TabIndex = 10;
            this.saveHyperImageButton.Text = "save h-img";
            this.saveHyperImageButton.UseVisualStyleBackColor = true;
            this.saveHyperImageButton.Click += new System.EventHandler(this.saveHyperImage);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(824, 15);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "read json";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1003, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private StatusStrip statusStrip1;
        public ToolStripProgressBar toolStripProgressBar1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Button saveHyperImageButton;
        private Button button2;
    }
}

