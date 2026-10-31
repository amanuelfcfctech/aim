using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class Movie : Form
    {
        public Movie()
        {
            InitializeComponent();
        }

        private void Movie_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            const string page = "<html><head><title></title></head><body><iframe width=\"560\" height=\"315\" src=\"http://www.youtube.com/embed/{0}\" frameborder=\"0\" allowfullscreen></iframe></body></html>";
            webBrowser1.DocumentText = string.Format(page, AIMGlobal.youtubeVideoKey);
        }
    }
}
