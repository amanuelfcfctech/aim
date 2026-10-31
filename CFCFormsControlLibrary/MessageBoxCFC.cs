using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CFCFormsControlLibrary
{
    public partial class MessageBoxCFC: UserControl
    {
        Boolean _isMessageWindowVisition = false;
        Queue<MessageCFC> messagesQueue = new Queue<MessageCFC>();

        public MessageBoxCFC()
        {
            InitializeComponent();
        }

        private void MessageBoxCFC_Load(object sender, EventArgs e)
        {
            this.Height = 0;
        }

        private void tmrAnimate_Tick(object sender, EventArgs e)
        {
            if (lblMessage.Text.Length == 0 && messagesQueue.Count > 0)
            {
                MessageCFC messageCFC = messagesQueue.Dequeue();
                lblMessage.Text = messageCFC.message;
                picMessage.Visible = true;
                if (messageCFC.picGoodBad == true)
                {
                    picMessage.Image = Properties.Resources.checkbox_32x32;
                }
                else if (messageCFC.picGoodBad == false)
                {
                    picMessage.Image = Properties.Resources.close_32x32;
                }
                else
                {
                    picMessage.Visible = false;
                }
            }
            if (this.Height < 46 && _isMessageWindowVisition == false)
            {
                this.Height += 2;
                return;
            }
            if (this.Height > 45 && _isMessageWindowVisition == false)
            {
                tmrAnimate.Interval = 3000;
                _isMessageWindowVisition = true;
                return;
            }
            if (_isMessageWindowVisition == true)
            {
                tmrAnimate.Interval = 20;
                if (this.Height > 0)
                {
                    this.Height -= 2;
                    return;
                }
                if (this.Height == 0)
                {
                    _isMessageWindowVisition = false;
                    lblMessage.Text = "";
                    if (messagesQueue.Count == 0)
                    {
                        tmrAnimate.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// Shows the message
        /// </summary>
        /// <param name="message"></param>
        public void Show(String message)
        {
            MessageCFC messageCFC = new MessageCFC();
            messageCFC.message = message;
            messagesQueue.Enqueue(messageCFC);
            if (tmrAnimate.Enabled == false)
            {
                tmrAnimate.Enabled = true;
            }
        }
        /// <summary>
        /// Shows the message with the check or cross picture on the left.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <param name="checkOrCrossPic">true: will show a green check mark and false will a red cross.</param>
        public void Show(String message, Boolean checkOrCrossPic)
        {
            MessageCFC messageCFC = new MessageCFC();
            messageCFC.message = message;
            messageCFC.picGoodBad = checkOrCrossPic;
            messagesQueue.Enqueue(messageCFC);
            if (tmrAnimate.Enabled == false)
            {
                tmrAnimate.Enabled = true;
            }
        }
        /// <summary>
        /// (This function is not yet implemented)
        /// Shows the message with the picture on the left and buttons on right.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <param name="checkOrCrossPic">true: will show a green check mark and false will a red cross.</param>
        /// <param name="buttons">what buttons to show</param>

        public void Show(String message, Boolean checkOrCrossPic, MessageCFC.Buttons buttons)
        {
            MessageCFC messageCFC = new MessageCFC();
            messageCFC.message = message;
            messageCFC.picGoodBad = checkOrCrossPic;
            /*
             * TODO add buttons panel in the design then show the
             * appropriate one 
            */
            messagesQueue.Enqueue(messageCFC);
            if (tmrAnimate.Enabled == false)
            {
                tmrAnimate.Enabled = true;
            }
        }
    }

    public class MessageCFC
    {
        public Boolean? picGoodBad { get; set; }
        public String message { get; set; }
        public enum Buttons
        {
            YesNo = 1
        }
    }
}
