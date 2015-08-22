using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Patchy
{
    public partial class Form1 : Form
    {
        public Patches patches;
        public bool inspector = false;
        public Color tabActiveColor = Color.FromArgb(255, 50, 50, 50);
        public Color tabInactiveColor = Color.FromArgb(255, 100, 100, 100);


        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.PatchList = lstPatches;
            Log.AddButton = btnAdd;
            Log.ProgressBar = proBar;
            Log.RTB = rtbLogs;
            Log.W("Duh herro...");

            patches = new Patches();
                
            lstPatches.DataSource = patches.List;

            PatchNotes.AutoUpdate = false;
            PatchNotes.OnLoaded = UpdateNotes;
            PatchNotes.Load();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void lstPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            patches.Add(openFileDialog.FileName);
        }


        private void lstPatches_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string v = lstPatches.Text;
            patches.LoadToMemory(v);

            lblInfo.Text = "\n";
            lblInfo.Text += patches.patches[v].name + "\n";
            lblInfo.Text += patches.patches[v].creationTime.ToShortDateString() + " " + patches.patches[v].creationTime.ToShortTimeString() + "\n";
            lblInfo.Text += (patches.patches[v].parentName != null ? patches.patches[v].parentName : "none") + "\n";
            lblInfo.Text += "\n\n";
            lblInfo.Text += patches.patches[v].newFiles.Count + "\n";
            lblInfo.Text += patches.patches[v].modifiedFiles.Count + "\n";
            lblInfo.Text += patches.patches[v].deletedFiles.Count + "\n";
            lblInfo.Text += patches.patches[v].files.Count - patches.patches[v].newFiles.Count - patches.patches[v].modifiedFiles.Count - patches.patches[v].deletedFiles.Count + "\n";
            lblInfo.Text += patches.patches[v].files.Count + "\n";

            UpdateNotes();


        }

        public void UpdateNotes()
        {
            PatchNotes.Note n = PatchNotes.Get(lstPatches.Text);
            StringBuilder sb = new StringBuilder();

            sb.Append("{\\rtf1\\ansi Notes for " + n.patch + ", written by " + n.writtenBy + "\\line");

            string[] lines = n.data.Split('\n');
            foreach (string line in lines)
            {
                if (line.StartsWith("-> "))
                {
                    sb.Append("-" + line + "\\line");
                }
                else
                {
                    sb.Append("\\line\\b " + line + "\\b0\\line");
                }
            }
            sb.Append("}");

            rtbNote.Rtf = sb.ToString().Replace("\\line\\line\\line", "\\line\\line");
        }

        private void rtbLogs_TextChanged(object sender, EventArgs e)
        {
            rtbLogs.SelectionStart = rtbLogs.Text.Length;
            rtbLogs.ScrollToCaret();
        }
        private void lblNotes_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tabNotes;
            pnlNotes.BackColor = tabActiveColor;
            pnlFiles.BackColor = tabInactiveColor;
            pnlApi.BackColor = tabInactiveColor;
        }

        private void lblFiles_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tabFiles;
            pnlFiles.BackColor = tabActiveColor;
            pnlNotes.BackColor = tabInactiveColor;
            pnlApi.BackColor = tabInactiveColor;
        }

        private void lblApi_Click(object sender, EventArgs e)
        {
            tabs.SelectedTab = tabApi;
            pnlApi.BackColor = tabActiveColor;
            pnlFiles.BackColor = tabInactiveColor;
            pnlNotes.BackColor = tabInactiveColor;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Upload.UploadPatch(patches.patches[lstPatches.Text]);
        }




    }
}
