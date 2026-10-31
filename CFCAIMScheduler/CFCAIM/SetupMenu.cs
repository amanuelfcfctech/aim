using System;
using System.Windows.Forms;
using CFCAIM.Maintenance;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Drawing;

namespace CFCAIM.Maintenance
{
    public partial class SetupMenu : Form
    {
        //DataMapping dataMapping;
        //AIMPlant aimPlant;
        //AnalyzerMaterial AnalyzerMaterial;
        //CFCProduct cfcProduct;
        //CFCPlant cfcPlant;
        //AnalyzerNutrient AnalyzerNutrient;
        //AnalyzerIngredient AnalyzerIngredient;
        //SourceLocation sourceLocation;
        //SourceMaintenance source;
        //PrinterSetup printerS;
        //OwnerLocation ownerLocation;
        //Owner owner;

        public SetupMenu()
        {
            InitializeComponent();
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (AIMGlobal.dataMapping == null || AIMGlobal.dataMapping.Visible == false)
                {
                    AIMGlobal.dataMapping = new DataMapping();
                    AIMGlobal.dataMapping.MdiParent = this.MdiParent;
                    AIMGlobal.dataMapping.Show();
                }
                else
                {
                    AIMGlobal.dataMapping.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

	
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                float fontSize = (float)(this.Font.SizeInPoints - 0.75);
                this.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                float fontSize = (float)(this.Font.SizeInPoints + 0.75);
                this.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrinterSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (AIMGlobal.printerS == null || !AIMGlobal.printerS.Visible)
                {
                    AIMGlobal.printerS = new PrinterSetup();
                    AIMGlobal.printerS.MdiParent = this.MdiParent;
                    AIMGlobal.printerS.Show();
                }
                else
                {
                    AIMGlobal.printerS.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void btnAnalyzerCrossReference_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetupMenu_Load(object sender, EventArgs e)
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
		private void btnExit_MouseHover(object sender, EventArgs e)
		{
			ToolTip toolTip1 = new ToolTip();
			toolTip1.SetToolTip(btnExit, "Exit");
		}
    }
}