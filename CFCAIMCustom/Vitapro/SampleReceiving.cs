using CFCAIMCustom.Manager;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using CFCAIMCustom.Entity.Vitapro;
using CFCAIMCore.Entity;
using DevExpress.XtraGrid.Views.Grid;
using CFCAIMCore.Manager;
using System.Linq;
using CFCAIM;
using Core.Library.DevExpress;
using System.Drawing;

namespace CFCAIMCustom.Vitapro
{
    public partial class SampleReceiving : Form
    {
        tpProdSpec sample;
        Int32 _sampleStatus = (Int32)EnumReceivingStatus.Pending;
        List<LotInfo> _header = new List<LotInfo>();
        List<SampleDetailInfo> _sampleDetail = new List<SampleDetailInfo>();
        Int32 _cfcUserID = CFCAIMCore.AIMCoreGlobal.CFCUserID;
        Int32 _days = 0;

        public SampleReceiving()
        {
            InitializeComponent();
        }


        private void SampleReceiving_Load(Object sender, EventArgs e)
        {
            InsertNewSamples();            
            DataBind();
            CheckSystemSettings();
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void InsertNewSamples()
        {
            ErrorInfo error = new ErrorInfo();
            error = CustomManager.InsertLot(_cfcUserID);
            if (error.ErrorNumber > 0)
            {
                return;
            }



        }
        private void DataBind()
        {
            List<LotInfo> sample = new List<LotInfo>();
            List<SampleDetailInfo> detail = new List<SampleDetailInfo>();

            if (_sampleStatus == (Int32)EnumReceivingStatus.All)
            {
                sample = CustomManager.GetLot(_days);
            }
            else
            {
                sample = CustomManager.GetLotStatus(_days, _sampleStatus);
            }


            Int32 currentRowNum = 0;
            if (grdSampleReceivingView != null)
            {
                Int32.TryParse(grdSampleReceivingView.FocusedRowHandle.ToString(), out currentRowNum);
            }
            grdSampleReceiving.DataSource = sample;
            grdSampleReceivingView.FocusedRowHandle = currentRowNum;

            //Get Sample Detail
            _sampleDetail = CustomManager.GetSampleDetail();

        }

        private void SetToolTips()
        {
            try
            {

                //Globals._AIMIcon = Properties.Resources.CFCAIM;               
                //System.Drawing.Icon ico = CFCAIMCustom.Properties.Resources.CFCAIM;

                //Globals global = new Globals();
                //System.Drawing.Icon AimIcon = global.AIMIcon;
                //this.Icon = Globals._AIMIcon;

                //Icon ico = CFCAIMCustom.Properties.Resources.CFCAIM;
                //Icon ico = CFCAIMCustom.Properties.Resources.AIM_icon;
                //this.Icon = ico;
                
                ToolTip ttip = new ToolTip();                                               
                ttip.SetToolTip(btnExit, "Salida");
                ttip.SetToolTip(btnRefresh, "Refrescar");
                ttip.SetToolTip(btnResize, "Volver al tamaño normal de la pantalla");
                ttip.SetToolTip(btnPreviewGrid, "Vista previa de cuadricula");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SampleReceiving_SizeChanged(Object sender, EventArgs e)
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
                AIMGlobal.previewGridResults(grdSampleReceiving, "Recepcion de la Muestra");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdSampleReceivingView_CustomUnboundColumnData(Object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column == colImage)
            {
                //get value in SampleStatus column for this row
                if (((LotInfo)(e.Row)).LotStatusID == 1)           //pending
                {
                    e.Value = Properties.Resources.status_16x16;
                }
                else if (((LotInfo)(e.Row)).LotStatusID == 2)      //accept
                {
                    e.Value = Properties.Resources.apply_16x16;
                }
                else if (((LotInfo)(e.Row)).LotStatusID == 3)      //reject
                {
                    e.Value = Properties.Resources.delete;
                }
                else                                               //ignore                               
                {
                    e.Value = Properties.Resources.handtool_16x16;
                }
            }
        }


        private void SaveStatus(Int32 lotStatusID)
        {
            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleReceivingView.GetFocusedRow();
            CustomManager.SaveLotStatus(lot.SampleID, lotStatusID, _cfcUserID);            
        }

        private void SaveProductionStatus(Int32 status)
        {
            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleReceivingView.GetFocusedRow();
            CustomManager.SaveProductionLineStatus(lot.SampleID, 1, status, _cfcUserID);
            CustomManager.SaveProductionLineStatus(lot.SampleID, 2, status, _cfcUserID);
            CustomManager.SaveProductionLineStatus(lot.SampleID, 3, status, _cfcUserID);
            CustomManager.SaveProductionLineStatus(lot.SampleID, 4, status, _cfcUserID);
            
        }

        private void repIgnore_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {           
            SaveStatus((Int32)EnumReceivingStatus.Ignore);
            SaveProductionStatus((Int32)EnumProductionStatus.Unavailable);
            DataBind();
        }

        private void repReject_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {            
            SaveStatus((Int32)EnumReceivingStatus.Reject);
            SaveProductionStatus((Int32)EnumProductionStatus.Unavailable);
            DataBind();
        }

        private void repPending_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SaveStatus((Int32)EnumReceivingStatus.Pending);
            SaveProductionStatus((Int32)EnumProductionStatus.Unavailable);
            DataBind();
        }


        private void repAccept_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                
                SaveStatus((Int32)EnumReceivingStatus.Accept);
                SaveProductionStatus((Int32)EnumProductionStatus.Available);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void SetStatus()
        {
            if (rbPending.Checked)
                _sampleStatus = (Int32)EnumReceivingStatus.Pending;
            if (rbAccepted.Checked)
                _sampleStatus = (Int32)EnumReceivingStatus.Accept;
            if (rbRejected.Checked)
                _sampleStatus = (Int32)EnumReceivingStatus.Reject;
            if (rbIgnored.Checked)
                _sampleStatus = (Int32)EnumReceivingStatus.Ignore;
            if (rbAll.Checked)
                _sampleStatus = (Int32)EnumReceivingStatus.All;
        }

        private void rbPending_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbPending.Checked == true)
            {
                _days = 0;
                pnlHistory.Visible = false;
                DataBind();
            }
        }

        private void rbIgnored_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbIgnored.Checked == true)
            {                
                pnlHistory.Visible = true;               
                DataBind();
            }
        }

        private void rbAccepted_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbAccepted.Checked == true)
            {
                _days = 0;
                pnlHistory.Visible = false;
                DataBind();
            }
        }

        private void rbRejected_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbRejected.Checked == true)
            {
                pnlHistory.Visible = true;
                DataBind();
            }

                
        }

        private void rbAll_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbAll.Checked == true)
            {
                pnlHistory.Visible = true;
                DataBind();
            }
        }

        #region Master Events
        private void grdSampleReceiving_ViewRegistered(Object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView view;
            view = e.View as GridView;
            
            //format visible columns
            view.Columns["ResultValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            view.Columns["ResultValue"].DisplayFormat.FormatString = "n2";
            view.Columns["ResultValue"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            view.Columns["ResultValue"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            view.Columns["AnalyzerNutrientCode"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
            view.Columns["ResultValue"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
            view.Columns["UnitType"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);            

            view.Columns["AnalyzerNutrientCode"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
            view.Columns["ResultValue"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
            view.Columns["UnitType"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);

            view.Columns["AnalyzerNutrientCode"].OptionsColumn.AllowEdit = false;
            view.Columns["ResultValue"].OptionsColumn.AllowEdit = false;
            view.Columns["UnitType"].OptionsColumn.AllowEdit = false;

            view.Columns["AnalyzerNutrientCode"].Caption = "Código de nutrientes";
            view.Columns["ResultValue"].Caption = "Valor de resultado";
            view.Columns["UnitType"].Caption = "Tipo de unidad";

            //Hide columns
            view.Columns["SampleID"].Visible = false;
            view.Columns["SampleResultID"].Visible = false;
            
        }

        private void grdSampleReceivingView_MasterRowEmpty(Object sender, DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs e)
        {
            LotInfo header = new LotInfo();
            SampleDetailInfo detail = new SampleDetailInfo();
            header = (LotInfo)grdSampleReceivingView.GetRow(e.RowHandle);
        }

        private void grdSampleReceivingView_MasterRowExpanded(Object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView masterView = (GridView)sender;
            GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;                       
            detailView.BestFitColumns();            
        }

        private void grdSampleReceivingView_MasterRowGetChildList(Object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            LotInfo header = new LotInfo();



            List<SampleDetailInfo> detail = new List<SampleDetailInfo>();
            header = (LotInfo)grdSampleReceivingView.GetRow(e.RowHandle);            
            var det = from g in _sampleDetail
                      where g.SampleID == header.SampleID
                      select g;

            foreach (var item in det)
            {
                detail.Add(new SampleDetailInfo
                {
                    AnalyzerNutrientCode = item.AnalyzerNutrientCode,
                    ResultValue = item.ResultValue,
                    UnitType = item.UnitType
                }
                );
            }
            e.ChildList = detail;
          
        }

        private void grdSampleReceivingView_MasterRowGetRelationCount(Object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void grdSampleReceivingView_MasterRowGetRelationName(Object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "";
        }
        #endregion

        private void grdSampleReceivingView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //If lot number is changed, update lot# on Sample table
            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleReceivingView.GetFocusedRow();
            ErrorInfo outPut = AIMManager.SaveSample(lot.SampleID, lot.LotNumber, _cfcUserID);
            DataBind();
        }

        private void repHyperLinkEdit1_Click(Object sender, EventArgs e)
        {
            try
            {
                try
                {

                    AIMGlobal.SplashScreenShow();

                    Int32 sampleID;
                    Int32.TryParse(grdSampleReceivingView.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);
                    AIMGlobal.sampleID = sampleID;

                    if (sample == null || sample.Visible == false)
                    {
                        sample = new tpProdSpec();
                        sample.MdiParent = this.MdiParent;
                        sample.WindowState = FormWindowState.Normal;
                        sample.Show();
                    }
                    else
                    {
                        sample.WindowState = FormWindowState.Normal;
                        sample.Activate();
                    }
                    AIMGlobal.SplashScreenHide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void btnCurrent_Click(Object sender, EventArgs e)
        {
            lblDays.Visible = true;
            txtDays.Visible = true;
            lblHistorical.Visible = false;
            btnCurrent.BackColor = SystemColors.GradientInactiveCaption;
            btnHistorical.BackColor = SystemColors.Control;
            CheckSystemSettings();
            SetStatus();
            DataBind();
        }

        private void btnHistorical_Click(Object sender, EventArgs e)
        {
            lblDays.Visible = false;
            txtDays.Visible = false;
            lblHistorical.Visible = true;
            btnCurrent.BackColor = SystemColors.Control;
            btnHistorical.BackColor = SystemColors.GradientInactiveCaption;
            _days = 0;
            SetStatus();
            DataBind();
        }

        private void CheckSystemSettings()
        {
            //Check SystemSetting to get number of days to show lots on the screen (based on date depleted)
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                _days = systemSettings.SampleNumberOfDays;
                if (_days == 0)
                {
                    _days = 60;
                }
                txtDays.Text = _days.ToString() + " días";
            }

        }
    }
}
