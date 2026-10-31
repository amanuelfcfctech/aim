using CFCAIMCustom.Manager;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using CFCAIMCustom.Entity.Vitapro;
using CFCAIMCore.Entity;
using Core.Library.DevExpress;
using System.Drawing;
using CFCAIM;
using CFCAIMCore;
using CFCAIMCore.Manager;

namespace CFCAIMCustom.Vitapro
{

    //-----------------------------------------------------------------------------
    //* * * the LineID on the LineRefCFCPlant table MUST MATCH the CFCPlantID * * *
    //-----------------------------------------------------------------------------


    public partial class LineMaintenance : Form
    {
        Int32 _cfcUserID = AIMCoreGlobal.CFCUserID;
        public LineMaintenance()
        {
            InitializeComponent();
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void Line_Load(Object sender, EventArgs e)
        {
            DataBind();            
            SetToolTips();            
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        
        private void DataBind()
        {
            List<LineRefCFCPlantInfo> line = new List<LineRefCFCPlantInfo>();
            line = CustomManager.GetLineRefCFCPlant();
            
            Int32 currentRowNum = 0;
            if (grdLineMaintenanceView != null)
            {
                Int32.TryParse(grdLineMaintenanceView.FocusedRowHandle.ToString(), out currentRowNum);
            }
            grdLineMaintenance.DataSource = line;
            grdLineMaintenanceView.FocusedRowHandle = currentRowNum;

            
            List<CFCPlantInfo> cfcPlant = new List<CFCPlantInfo>();
            cfcPlant = AIMManager.GetCFCPlant(true);
            cfcPlant.Insert
            (
                0,
                new CFCPlantInfo
                {
                    CFCPlantID = 0,
                    CFCPlantCode = "--Select--",
                    CFCPlantName = "--Select--"
                }
            );
            replueCFCPlantName.DataSource = cfcPlant;            
            XtraEditors.LoadGridLookUpEdit(ref replueCFCPlantName, "CFCPlantID", "CFCPlantCode", "CFCPlantName");
            
        }

        

        private void SetToolTips()
        {
            try
            {                
                ToolTip ttip = new ToolTip();               
                ttip.SetToolTip(btnResize, "Volver al tamaño normal de la pantalla");
                ttip.SetToolTip(btnPreviewGrid, "Vista previa de cuadricula");                                
                ttip.SetToolTip(btnExit, "Salida");
                ttip.SetToolTip(btnRefresh, "Refrescar");               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Line_Resize(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnExit_Click(Object sender, EventArgs e)
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

        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.previewGridResults(grdLineMaintenance, "Linea Mantenimiento");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdLineMaintenanceView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                               
                LineRefCFCPlantInfo xref = new LineRefCFCPlantInfo();
                xref = (LineRefCFCPlantInfo)grdLineMaintenanceView.GetFocusedRow();                
                xref.CFCUserID = _cfcUserID;

                
                if (xref.CFCPlantID == 0 && xref.IsActive == true)
                {                
                    xref.IsActive = false;
                }
                
                 
                CustomManager.SaveLineRefCFCPlant(ref xref);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdLineMaintenanceView_CustomDrawCell(Object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == colCFCPlantName)
            {
                if ((Int32)grdLineMaintenanceView.GetRowCellValue(e.RowHandle, "CFCPlantID") == 0)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }
            }
        }
    }
}
