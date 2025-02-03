using System.Windows.Forms;

namespace Xengine.Admin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.tbxFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowText = new System.Windows.Forms.Button();
            this.btnShowErr = new System.Windows.Forms.Button();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.chProcessAll = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.lblMsg);
            this.groupBox1.Controls.Add(this.tbxFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1220, 130);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Microsoft Word Docx File";
            // 
            // btnOpen
            // 
            this.label1.AutoSize = false;
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(1150, 57);
            this.btnOpen.Name = "btnOpen";
           // this.btnOpen.Size = new System.Drawing.Size(46, 36);
            this.btnOpen.ClientSize = new System.Drawing.Size(46, 36);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "...";
           // this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);

            // 
            // tbxFile
            // 
            this.tbxFile.AllowDrop = true;
            this.tbxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFile.Location = new System.Drawing.Point(230, 58);
            this.tbxFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxFile.Multiline = true;
            this.tbxFile.Name = "tbxFile";
            this.tbxFile.Size = new System.Drawing.Size(920, 32);
            this.tbxFile.TabIndex = 0;
            this.tbxFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.tbxFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);

            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Green;
            this.lblMsg.Location = new System.Drawing.Point(224, 104);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(137, 22);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "Still working...";
            this.lblMsg.Visible = false;
           
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select File or Folder:";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(1050, 800);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(200, 50);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 180);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Extracted text from Word：";
            // 
            // btnShowText
            // 
            this.btnShowText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowText.Location = new System.Drawing.Point(40, 800); 
            this.btnShowText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowText.Name = "btnShowText";
            this.btnShowText.Size = new System.Drawing.Size(200, 50);
            this.btnShowText.TabIndex = 5;
            this.btnShowText.Text = "View Content";
            this.btnShowText.UseVisualStyleBackColor = true;
            this.btnShowText.Click += new System.EventHandler(this.LoadTextFile);
            // 
            // btnShowErr
            // 
            this.btnShowErr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowErr.ForeColor = System.Drawing.Color.Red;
            this.btnShowErr.Location = new System.Drawing.Point(250, 800);
            this.btnShowErr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShowErr.Name = "btnShowErr";
            this.btnShowErr.Size = new System.Drawing.Size(200, 50);
            this.btnShowErr.TabIndex = 7;
            this.btnShowErr.Text = "View Error";
            this.btnShowErr.UseVisualStyleBackColor = true;
            this.btnShowErr.Click += new System.EventHandler(this.LoadError);
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenDir.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenDir.Enabled = false;
            this.btnOpenDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDir.ForeColor = System.Drawing.Color.Black;
            this.btnOpenDir.Location = new System.Drawing.Point(460, 800);
            this.btnOpenDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(200, 50);
            this.btnOpenDir.TabIndex = 8;
            this.btnOpenDir.Text = "View Output";
            this.btnOpenDir.UseVisualStyleBackColor = false;
            this.btnOpenDir.Click += new System.EventHandler(this.BtnOpenDir_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(30, 222);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(1000, 320);
            this.webBrowser1.ClientSize = new System.Drawing.Size(1220, 520);
            this.webBrowser1.Name = "webBrowser1";
            
            this.webBrowser1.TabIndex = 3;
           
            // 
            // chProcessAll
            // 

            this.chProcessAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chProcessAll.AutoSize = true;
            this.chProcessAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chProcessAll.Location = new System.Drawing.Point(650, 810);
            this.chProcessAll.Name = "chProcessAll";
            this.chProcessAll.Size = new System.Drawing.Size(169, 29);
            this.chProcessAll.TabIndex = 4;
            this.chProcessAll.Text = "Process All";
            this.chProcessAll.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 750);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1220, 32);
            this.progressBar1.TabIndex = 5;
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           

            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(840, 800);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 50);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

           var w =Screen.PrimaryScreen.Bounds.Width;
           var h= Screen.PrimaryScreen.Bounds.Height;
           //var size= Screen.PrimaryScreen.Bounds.Size;
            if (Screen.PrimaryScreen.Bounds.Height > 900)
            {
                this.ClientSize = new System.Drawing.Size(1280, 880);
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(1280, 980);

            }

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chProcessAll);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.btnShowErr);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnShowText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1280, 940);
            //this.WindowState = FormWindowState.Maximized;
            this.Name = "MainForm";
            this.Text = "Xengine Admin";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
           // this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

