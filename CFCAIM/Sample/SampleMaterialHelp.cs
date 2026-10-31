using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CFCAIM.Sample
{
    public partial class SampleMaterialHelp : Form
    {

        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }

        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color Color3 { get; set; }

        public SampleMaterialHelp()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SampleMaterialHelp_Load(object sender, EventArgs e)

        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            
            //Text1 = "Data Source";
            //Color1 = Color.Blue;


            //base.OnPaint(e);

            //var g = e.Graphics;
                        
            //e.Graphics.DrawString("The ", this.Font, Brushes.Black, 0, 0);
            //var sz = g.MeasureString("The ", this.Font).Width;
            
            ////DataSource
            //using (SolidBrush sb = new SolidBrush(Color1))
            //    e.Graphics.DrawString(Text1, this.Font, sb, sz, 0);
            //sz += g.MeasureString(Text1, this.Font).Width;
                        
            //e.Graphics.DrawString(" is where the material is coming from.", this.Font, Brushes.Black, sz, 0);
            //sz += g.MeasureString(" is where the material is coming from.", this.Font).Width;

            ////using (SolidBrush sb = new SolidBrush(Color2))
            ////    e.Graphics.DrawString(Text2, this.Font, sb, sz, 0);
            ////sz += g.MeasureString(Text2, this.Font).Width;

            ////e.Graphics.DrawString(" at ", this.Font, Brushes.Black, sz, 0);
            ////sz += g.MeasureString(" at ", this.Font).Width;
            ////using (SolidBrush sb = new SolidBrush(Color3))
            ////    e.Graphics.DrawString(Text3, this.Font, sb, sz, 0);

            //using (SolidBrush sb = new SolidBrush(Color3))
            //    e.Graphics.DrawString(Text1, this.Font, sb, sz, 0);

        }

    }
}
