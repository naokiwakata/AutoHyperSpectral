
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
            this.stackMaskButton = new System.Windows.Forms.Button();
            this.createCsvButton = new System.Windows.Forms.Button();
            this.saveHyperImageButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 37);
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
            this.detectLeafButton.Location = new System.Drawing.Point(12, 68);
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
            this.selectLeafButton.Location = new System.Drawing.Point(13, 97);
            this.selectLeafButton.Name = "selectLeafButton";
            this.selectLeafButton.Size = new System.Drawing.Size(75, 23);
            this.selectLeafButton.TabIndex = 4;
            this.selectLeafButton.Text = "select leaf";
            this.selectLeafButton.UseVisualStyleBackColor = true;
            this.selectLeafButton.Click += new System.EventHandler(this.SelectLeaf);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 19);
            this.textBox1.TabIndex = 5;
            // 
            // saveCsvButton
            // 
            this.saveCsvButton.Enabled = false;
            this.saveCsvButton.Location = new System.Drawing.Point(13, 211);
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
            this.saveJsonButton.Location = new System.Drawing.Point(13, 182);
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
            this.judgeDiseaseButton.Location = new System.Drawing.Point(13, 126);
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
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.stackMaskButton);
            this.panel1.Controls.Add(this.createCsvButton);
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
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // stackMaskButton
            // 
            this.stackMaskButton.Location = new System.Drawing.Point(13, 293);
            this.stackMaskButton.Name = "stackMaskButton";
            this.stackMaskButton.Size = new System.Drawing.Size(75, 23);
            this.stackMaskButton.TabIndex = 12;
            this.stackMaskButton.Text = "stack mask";
            this.stackMaskButton.UseVisualStyleBackColor = true;
            this.stackMaskButton.Click += new System.EventHandler(this.stackMaskButton_Click);
            // 
            // createCsvButton
            // 
            this.createCsvButton.Enabled = false;
            this.createCsvButton.Location = new System.Drawing.Point(12, 322);
            this.createCsvButton.Name = "createCsvButton";
            this.createCsvButton.Size = new System.Drawing.Size(75, 23);
            this.createCsvButton.TabIndex = 11;
            this.createCsvButton.Text = "save csv";
            this.createCsvButton.UseVisualStyleBackColor = true;
            this.createCsvButton.Click += new System.EventHandler(this.saveCsvButtonByJson_Click);
            // 
            // saveHyperImageButton
            // 
            this.saveHyperImageButton.Location = new System.Drawing.Point(12, 264);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "マスク画像";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "保存";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "自動判別";
            this.label3.Click += new System.EventHandler(this.label3_Click);
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
        private Button createCsvButton;
        private Button stackMaskButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}

