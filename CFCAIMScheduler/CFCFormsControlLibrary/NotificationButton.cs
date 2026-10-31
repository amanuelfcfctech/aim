using System;
using System.Windows.Forms;
using CFCFormsControlLibrary.Properties;

namespace CFCFormsControlLibrary
{
    public partial class NotificationButton : UserControl
    {
        public NotificationButton()
        {
            InitializeComponent();
			WireAllControls(this);
        }

        private void lblNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblNo != null)
                {
                    if (Convert.ToInt32(lblNo.Text) > 0)
                    {
                        picMessage.Image = Resources.NotificationGreen;
                    }
                    else
                    {
                        picMessage.Image = Resources.NotificationGrey;
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred with the Notification Button control. Contact IT.", "warning...");
            }
        }

        public void SetNumberOfNotification(Int32 number)
        {
            try
            {
                lblNo.Text = number.ToString();
            }
            catch (Exception)
            {
            }
        }

        public Int32 GetNumberOfNotification()
        {
            try
            {
                return Convert.ToInt32(lblNo.Text);
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

		private void lblNo_Click(object sender, EventArgs e)
		{
			
		}

		private void WireAllControls(Control cont)
		{
			foreach (Control ctl in cont.Controls)
			{
				ctl.Click += ctl_Click;
				if (ctl.HasChildren)
				{
					WireAllControls(ctl);
				}
			}
		}

		private void ctl_Click(object sender, EventArgs e)
		{
			this.InvokeOnClick(this, EventArgs.Empty);
		}
    }
}
