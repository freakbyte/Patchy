namespace Patchy
{
    partial class Form1
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lstPatches = new System.Windows.Forms.ListBox();
            this.pnlProBar = new System.Windows.Forms.Panel();
            this.proBar = new Patchy.NewProgressBar();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.pnlApi = new System.Windows.Forms.Panel();
            this.lblApi = new System.Windows.Forms.Label();
            this.pnlFiles = new System.Windows.Forms.Panel();
            this.lblFiles = new System.Windows.Forms.Label();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.lblNotes = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabNotes = new System.Windows.Forms.TabPage();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.tabFiles = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabApi = new System.Windows.Forms.TabPage();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblInfoLabel = new System.Windows.Forms.Label();
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.pnlProBar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.pnlApi.SuspendLayout();
            this.pnlFiles.SuspendLayout();
            this.pnlNotes.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabNotes.SuspendLayout();
            this.tabFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(-1, 508);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(124, 29);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add Patch";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "FirefallInstallerComplete.7z";
            this.openFileDialog.Filter = "SevenZip Archive (*.7z)|*.7z";
            this.openFileDialog.Title = "Select a 7z archive containing a complete firefall installer";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // lstPatches
            // 
            this.lstPatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPatches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lstPatches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPatches.ForeColor = System.Drawing.SystemColors.Control;
            this.lstPatches.FormattingEnabled = true;
            this.lstPatches.ItemHeight = 18;
            this.lstPatches.Location = new System.Drawing.Point(0, 0);
            this.lstPatches.Name = "lstPatches";
            this.lstPatches.Size = new System.Drawing.Size(120, 504);
            this.lstPatches.TabIndex = 3;
            this.lstPatches.SelectedIndexChanged += new System.EventHandler(this.lstPatches_SelectedIndexChanged_1);
            // 
            // pnlProBar
            // 
            this.pnlProBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.pnlProBar.Controls.Add(this.proBar);
            this.pnlProBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlProBar.Location = new System.Drawing.Point(0, 508);
            this.pnlProBar.Name = "pnlProBar";
            this.pnlProBar.Size = new System.Drawing.Size(800, 100);
            this.pnlProBar.TabIndex = 1;
            // 
            // proBar
            // 
            this.proBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.proBar.BackColor = System.Drawing.Color.SteelBlue;
            this.proBar.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.proBar.Location = new System.Drawing.Point(0, 1);
            this.proBar.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.proBar.Name = "proBar";
            this.proBar.Size = new System.Drawing.Size(803, 27);
            this.proBar.Step = 5;
            this.proBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.proBar.TabIndex = 0;
            this.proBar.Value = 100;
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.splitContainer1);
            this.pnlContent.Controls.Add(this.pnlProBar);
            this.pnlContent.Location = new System.Drawing.Point(120, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(804, 539);
            this.pnlContent.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(-1, -1);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Panel1.Controls.Add(this.btnUpload);
            this.splitContainer1.Panel1.Controls.Add(this.pnlTabs);
            this.splitContainer1.Panel1.Controls.Add(this.tabs);
            this.splitContainer1.Panel1.Controls.Add(this.lblInfo);
            this.splitContainer1.Panel1.Controls.Add(this.lblInfoLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Panel2.Controls.Add(this.rtbLogs);
            this.splitContainer1.Size = new System.Drawing.Size(801, 510);
            this.splitContainer1.SplitterDistance = 435;
            this.splitContainer1.TabIndex = 3;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(0, 414);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(179, 23);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Upload data";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pnlTabs
            // 
            this.pnlTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTabs.BackColor = System.Drawing.Color.Transparent;
            this.pnlTabs.Controls.Add(this.pnlApi);
            this.pnlTabs.Controls.Add(this.pnlFiles);
            this.pnlTabs.Controls.Add(this.pnlNotes);
            this.pnlTabs.Location = new System.Drawing.Point(706, -1);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(98, 20);
            this.pnlTabs.TabIndex = 3;
            // 
            // pnlApi
            // 
            this.pnlApi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlApi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlApi.Controls.Add(this.lblApi);
            this.pnlApi.Location = new System.Drawing.Point(0, 1);
            this.pnlApi.Name = "pnlApi";
            this.pnlApi.Size = new System.Drawing.Size(25, 20);
            this.pnlApi.TabIndex = 6;
            // 
            // lblApi
            // 
            this.lblApi.AutoSize = true;
            this.lblApi.ForeColor = System.Drawing.SystemColors.Control;
            this.lblApi.Location = new System.Drawing.Point(2, 4);
            this.lblApi.Name = "lblApi";
            this.lblApi.Size = new System.Drawing.Size(24, 13);
            this.lblApi.TabIndex = 0;
            this.lblApi.Text = "API";
            this.lblApi.Click += new System.EventHandler(this.lblApi_Click);
            // 
            // pnlFiles
            // 
            this.pnlFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.pnlFiles.Controls.Add(this.lblFiles);
            this.pnlFiles.Location = new System.Drawing.Point(26, 1);
            this.pnlFiles.Name = "pnlFiles";
            this.pnlFiles.Size = new System.Drawing.Size(31, 20);
            this.pnlFiles.TabIndex = 5;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.ForeColor = System.Drawing.SystemColors.Control;
            this.lblFiles.Location = new System.Drawing.Point(2, 4);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(28, 13);
            this.lblFiles.TabIndex = 0;
            this.lblFiles.Text = "Files";
            this.lblFiles.Click += new System.EventHandler(this.lblFiles_Click);
            // 
            // pnlNotes
            // 
            this.pnlNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlNotes.Controls.Add(this.lblNotes);
            this.pnlNotes.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlNotes.Location = new System.Drawing.Point(58, 1);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(36, 20);
            this.pnlNotes.TabIndex = 4;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNotes.Location = new System.Drawing.Point(2, 4);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(35, 13);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "Notes";
            this.lblNotes.Click += new System.EventHandler(this.lblNotes_Click);
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabs.Controls.Add(this.tabNotes);
            this.tabs.Controls.Add(this.tabFiles);
            this.tabs.Controls.Add(this.tabApi);
            this.tabs.Location = new System.Drawing.Point(179, -25);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(626, 464);
            this.tabs.TabIndex = 3;
            // 
            // tabNotes
            // 
            this.tabNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.tabNotes.Controls.Add(this.rtbNote);
            this.tabNotes.Location = new System.Drawing.Point(4, 25);
            this.tabNotes.Name = "tabNotes";
            this.tabNotes.Padding = new System.Windows.Forms.Padding(3);
            this.tabNotes.Size = new System.Drawing.Size(618, 435);
            this.tabNotes.TabIndex = 0;
            this.tabNotes.Text = "tabNotes";
            // 
            // rtbNote
            // 
            this.rtbNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNote.ForeColor = System.Drawing.SystemColors.Control;
            this.rtbNote.Location = new System.Drawing.Point(3, 3);
            this.rtbNote.Margin = new System.Windows.Forms.Padding(0);
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ReadOnly = true;
            this.rtbNote.Size = new System.Drawing.Size(612, 429);
            this.rtbNote.TabIndex = 0;
            this.rtbNote.Text = "";
            // 
            // tabFiles
            // 
            this.tabFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.tabFiles.Controls.Add(this.label1);
            this.tabFiles.Location = new System.Drawing.Point(4, 25);
            this.tabFiles.Name = "tabFiles";
            this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiles.Size = new System.Drawing.Size(618, 435);
            this.tabFiles.TabIndex = 1;
            this.tabFiles.Text = "tabNotes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Files";
            // 
            // tabApi
            // 
            this.tabApi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.tabApi.Location = new System.Drawing.Point(4, 25);
            this.tabApi.Name = "tabApi";
            this.tabApi.Size = new System.Drawing.Size(618, 435);
            this.tabApi.TabIndex = 2;
            this.tabApi.Text = "tabPage1";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblInfo.Location = new System.Drawing.Point(87, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(55, 130);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "\r\nbata-1640\r\nnone\r\n\r\n\r\n30\r\n11\r\n14\r\n34783\r\n34775";
            // 
            // lblInfoLabel
            // 
            this.lblInfoLabel.AutoSize = true;
            this.lblInfoLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblInfoLabel.Location = new System.Drawing.Point(4, 8);
            this.lblInfoLabel.Name = "lblInfoLabel";
            this.lblInfoLabel.Size = new System.Drawing.Size(72, 143);
            this.lblInfoLabel.TabIndex = 0;
            this.lblInfoLabel.Text = "Patch\r\n-> Version\r\n-> Created\r\n-> Prevous\r\n\r\nFiles\r\n-> New\r\n-> Changed\r\n-> Delete" +
    "d:\r\n-> Unaffected\r\n-> Total";
            // 
            // rtbLogs
            // 
            this.rtbLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.rtbLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogs.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.rtbLogs.Location = new System.Drawing.Point(0, 0);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rtbLogs.Size = new System.Drawing.Size(801, 71);
            this.rtbLogs.TabIndex = 4;
            this.rtbLogs.Text = "";
            this.rtbLogs.TextChanged += new System.EventHandler(this.rtbLogs_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(921, 536);
            this.Controls.Add(this.lstPatches);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.btnAdd);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "Form1";
            this.Text = "Patchy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlProBar.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.pnlApi.ResumeLayout(false);
            this.pnlApi.PerformLayout();
            this.pnlFiles.ResumeLayout(false);
            this.pnlFiles.PerformLayout();
            this.pnlNotes.ResumeLayout(false);
            this.pnlNotes.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabNotes.ResumeLayout(false);
            this.tabFiles.ResumeLayout(false);
            this.tabFiles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListBox lstPatches;
        private System.Windows.Forms.Panel pnlProBar;
        private NewProgressBar proBar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.Label lblInfoLabel;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabNotes;
        private System.Windows.Forms.TabPage tabFiles;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Panel pnlNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Panel pnlFiles;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.Panel pnlApi;
        private System.Windows.Forms.Label lblApi;
        private System.Windows.Forms.TabPage tabApi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpload;



    }
}

