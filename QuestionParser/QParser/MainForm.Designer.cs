using System.Windows.Forms;

namespace QParser.Admin
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            groupBox1 = new GroupBox();
            btnOpen = new Button();
            lblMsg = new Label();
            tbxFile = new TextBox();
            label1 = new Label();
            btnStart = new Button();
            label2 = new Label();
            btnShowText = new Button();
            btnShowErr = new Button();
            btnOpenDir = new Button();
            webBrowser1 = new WebBrowser();
            chProcessAll = new CheckBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            progressBar1 = new ProgressBar();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnOpen);
            groupBox1.Controls.Add(lblMsg);
            groupBox1.Controls.Add(tbxFile);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            groupBox1.Location = new System.Drawing.Point(22, 25);
            groupBox1.Margin = new Padding(4, 6, 4, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 6, 4, 6);
            groupBox1.Size = new System.Drawing.Size(1356, 162);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Microsoft Word Docx File";
            // 
            // btnOpen
            // 
            btnOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOpen.Location = new System.Drawing.Point(1278, 71);
            btnOpen.Margin = new Padding(3, 4, 3, 4);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new System.Drawing.Size(51, 45);
            btnOpen.TabIndex = 2;
            btnOpen.Text = "...";
            btnOpen.Click += BtnOpen_Click;
            // 
            // lblMsg
            // 
            lblMsg.AutoSize = true;
            lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Location = new System.Drawing.Point(249, 130);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new System.Drawing.Size(146, 25);
            lblMsg.TabIndex = 4;
            lblMsg.Text = "Still working...";
            lblMsg.Visible = false;
            // 
            // tbxFile
            // 
            tbxFile.AllowDrop = true;
            tbxFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxFile.Location = new System.Drawing.Point(256, 72);
            tbxFile.Margin = new Padding(4, 6, 4, 6);
            tbxFile.Multiline = true;
            tbxFile.Name = "tbxFile";
            tbxFile.Size = new System.Drawing.Size(1022, 39);
            tbxFile.TabIndex = 0;
            tbxFile.DragDrop += Form1_DragDrop;
            tbxFile.DragEnter += Form1_DragEnter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(18, 81);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(191, 25);
            label1.TabIndex = 0;
            label1.Text = "Select File or Folder:";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.BackColor = System.Drawing.SystemColors.Control;
            btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            btnStart.Location = new System.Drawing.Point(1167, 1000);
            btnStart.Margin = new Padding(4, 6, 4, 6);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(222, 62);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += BtnStart_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(33, 225);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(236, 25);
            label2.TabIndex = 2;
            label2.Text = "Extracted text from Word：";
            // 
            // btnShowText
            // 
            btnShowText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnShowText.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnShowText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnShowText.Location = new System.Drawing.Point(44, 1000);
            btnShowText.Margin = new Padding(4, 6, 4, 6);
            btnShowText.Name = "btnShowText";
            btnShowText.Size = new System.Drawing.Size(222, 62);
            btnShowText.TabIndex = 5;
            btnShowText.Text = "View Content";
            btnShowText.UseVisualStyleBackColor = true;
            btnShowText.Click += LoadTextFile;
            // 
            // btnShowErr
            // 
            btnShowErr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnShowErr.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnShowErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnShowErr.ForeColor = System.Drawing.Color.Red;
            btnShowErr.Location = new System.Drawing.Point(278, 1000);
            btnShowErr.Margin = new Padding(4, 6, 4, 6);
            btnShowErr.Name = "btnShowErr";
            btnShowErr.Size = new System.Drawing.Size(222, 62);
            btnShowErr.TabIndex = 7;
            btnShowErr.Text = "View Error";
            btnShowErr.UseVisualStyleBackColor = true;
            btnShowErr.Click += LoadError;
            // 
            // btnOpenDir
            // 
            btnOpenDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOpenDir.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnOpenDir.BackColor = System.Drawing.SystemColors.Control;
            btnOpenDir.Enabled = false;
            btnOpenDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOpenDir.ForeColor = System.Drawing.Color.Black;
            btnOpenDir.Location = new System.Drawing.Point(511, 1000);
            btnOpenDir.Margin = new Padding(4, 6, 4, 6);
            btnOpenDir.Name = "btnOpenDir";
            btnOpenDir.Size = new System.Drawing.Size(222, 62);
            btnOpenDir.TabIndex = 8;
            btnOpenDir.Text = "View Output";
            btnOpenDir.UseVisualStyleBackColor = false;
            btnOpenDir.Click += BtnOpenDir_Click;
            // 
            // webBrowser1
            // 
            webBrowser1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webBrowser1.Location = new System.Drawing.Point(33, 278);
            webBrowser1.Margin = new Padding(3, 4, 3, 4);
            webBrowser1.MinimumSize = new System.Drawing.Size(1111, 400);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new System.Drawing.Size(1356, 650);
            webBrowser1.TabIndex = 3;
            // 
            // chProcessAll
            // 
            chProcessAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chProcessAll.AutoSize = true;
            chProcessAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            chProcessAll.Location = new System.Drawing.Point(774, 1019);
            chProcessAll.Margin = new Padding(3, 4, 3, 4);
            chProcessAll.Name = "chProcessAll";
            chProcessAll.Size = new System.Drawing.Size(136, 29);
            chProcessAll.TabIndex = 4;
            chProcessAll.Text = "Process All";
            chProcessAll.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new System.Drawing.Point(33, 938);
            progressBar1.Margin = new Padding(3, 4, 3, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(1356, 40);
            progressBar1.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = System.Drawing.SystemColors.Control;
            btnCancel.Enabled = false;
            btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            btnCancel.Location = new System.Drawing.Point(933, 1000);
            btnCancel.Margin = new Padding(4, 6, 4, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(222, 62);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += StopButton_Click;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1398, 1105);
            Controls.Add(btnCancel);
            Controls.Add(progressBar1);
            Controls.Add(chProcessAll);
            Controls.Add(webBrowser1);
            Controls.Add(btnOpenDir);
            Controls.Add(btnShowErr);
            Controls.Add(btnStart);
            Controls.Add(btnShowText);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 6, 4, 6);
            MinimumSize = new System.Drawing.Size(1420, 1161);
            Name = "MainForm";
            Text = "QParser";
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbxFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnShowText;
        private System.Windows.Forms.Button btnShowErr;
        private System.Windows.Forms.Button btnOpenDir;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox chProcessAll;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;

  
    }
}

