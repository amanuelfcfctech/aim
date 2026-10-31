using System;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class SendUsage : Form
    {
        public SendUsage()
        {
            InitializeComponent();
        }

        private void SendUsage_Load(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //bool connected = false;
                //try
                //{
                //    var authClient = new LiveAuthClient("000000004812F3C0");
                //    String result = authClient.GetLoginUrl(new string[] { "wl.signin", "wl.skydrive" });

                //    if (result.Status == LiveConnectSessionStatus.Connected)
                //    {
                //        connected = true;
                //        var connectClient = new LiveConnectClient(result.Session);
                //        var meResult = connectClient.GetAsync("me");
                //        dynamic meData = meResult.Result;
                //        MessageBox.Show(meData.ToString());
                //        //updateUI(meData);
                //    }
                //}
                //catch (LiveAuthException ex)
                //{
                //    // Display an error message.
                //}
                //catch (LiveConnectException ex)
                //{
                //    // Display an error message.
                //}

                //// Turn off the display of the connection button in the UI.
                //connectButton.Visibility = connected ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}