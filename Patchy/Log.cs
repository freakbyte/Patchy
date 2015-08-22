using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Patchy
{
    
    public static class Log
    {
        public static RichTextBox RTB;
        public static ProgressBar ProgressBar;
        public static Button AddButton;
        public static ListBox PatchList;

        public static void W(string line)
        {
            if(RTB!=null)
            {
                if(RTB.InvokeRequired)
                {
                    RTB.Invoke((MethodInvoker)delegate { RTB.Text += line + "\n"; });
                }
                else
                {
                    RTB.Text += line + "\n";
                }
                
            }
        }

        public static void P(int percent)
        {
            if (ProgressBar != null)
            {
                if (ProgressBar.InvokeRequired)
                {
                    ProgressBar.Invoke((MethodInvoker)delegate { ProgressBar.Value = Maths.Clamp<int>(percent, 0, 100); });
                }
                else
                {
                    ProgressBar.Value = Maths.Clamp<int>(percent, 0, 100);
                }

            }
        }

        public static void A(bool boolean)
        {
            if (AddButton != null)
            {
                if (AddButton.InvokeRequired)
                {
                    AddButton.Invoke((MethodInvoker)delegate { AddButton.Enabled = boolean; if (boolean) { AddButton.Text = "Add Patch"; } else { AddButton.Text = "Working.."; } });
                }
                else
                {
                    AddButton.Enabled = boolean;
                    if (boolean) { AddButton.Text = "Add Patch"; } else { AddButton.Text = "Working.."; }
                }

            }
        }

        public static void RefreshPatchList()
        {
            if(PatchList != null)
            {
                if (PatchList.InvokeRequired)
                {
                    PatchList.Invoke((MethodInvoker)delegate {
                        var ds = PatchList.DataSource;
                        PatchList.DataSource = null;
                        PatchList.DataSource = ds;
                    });
                }
                else
                {
                    var ds = PatchList.DataSource;
                    PatchList.DataSource = null;
                    PatchList.DataSource = ds;
                }
            }
        }

    }
}
