using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM.Report
{
    //Task1176 FG 7/3/2026 - single reusable borderless popup that shows help text for any panel
    public partial class ReportPanelHelp : Form
    {
        public ReportPanelHelp()
        {
            InitializeComponent();
        }

        //Opens the popup for a panel, anchored next to the "?" button that was clicked.
        public static void ShowNextTo(Control anchorButton, string title, string body)
        {
            ReportPanelHelp popup = new ReportPanelHelp();
            popup.lblTitle.Text = title;
            popup.txtBody.Text = body;
            popup.txtBody.SelectionLength = 0;
            popup.PositionNextTo(anchorButton);
            popup.Show(anchorButton.FindForm());
        }

        //Places the popup at the bottom-right of the anchor, keeping it inside the working area.
        private void PositionNextTo(Control anchorButton)
        {
            Point anchorScreenPoint = anchorButton.PointToScreen(new Point(anchorButton.Width, anchorButton.Height));
            Rectangle workingArea = Screen.FromControl(anchorButton).WorkingArea;

            Int32 left = anchorScreenPoint.X;
            Int32 top = anchorScreenPoint.Y;

            if (left + this.Width > workingArea.Right)
            {
                left = workingArea.Right - this.Width;
            }
            if (top + this.Height > workingArea.Bottom)
            {
                top = workingArea.Bottom - this.Height;
            }
            if (left < workingArea.Left)
            {
                left = workingArea.Left;
            }
            if (top < workingArea.Top)
            {
                top = workingArea.Top;
            }

            this.Location = new Point(left, top);
        }

        //Close when the user clicks away from the popup.
        private void ReportPanelHelp_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        //Close on Esc (KeyPreview is enabled so the form sees the key first).
        private void ReportPanelHelp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
