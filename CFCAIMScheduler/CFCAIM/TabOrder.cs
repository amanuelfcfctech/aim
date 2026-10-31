using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIM.Maintenance;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CFCAIM
{
    public partial class TabOrder : Form
    {

        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleResultTemplateInfo> _template = new List<SampleResultTemplateInfo>();
        List<SampleResultTemplateDetailInfo> _templateDetail = new List<SampleResultTemplateDetailInfo>();
        List<SampleQualityControlMassEntryInfo> _qualityControlHeader = new List<SampleQualityControlMassEntryInfo>();
        List<SampleQualityControlMassEntryDetailInfo> _qualityControlDetail = new List<SampleQualityControlMassEntryDetailInfo>();
        List<SampleTestTypeInfo> _testTypeHeader = new List<SampleTestTypeInfo>();                 //8/9/23
        List<SampleTestTypeDetailInfo> _testTypeDetail = new List<SampleTestTypeDetailInfo>();     //8/9/23
        List<SampleTabControlPageInfo> _pageOrder = new List<SampleTabControlPageInfo>();          //8/18/23
        Int32 recursion = 0;
        Int32 _dataSourceTypeID = 0;
        Int32 _templateID = 0;
        Int32 _dataSourceTypeID_QualityControl = 0;
        Int32 _qualityControlID = 0;
        Int32 _labelSize = 2;               //2 = default 2-inch label (5-lines); 1 = 1-inch (3-lines)
        Int32 _saveAsCopyTemplateID = 0;
        Int32 _saveAsCopyQualityControlID = 0;
        Int32 _testTypeID = 0;                                                                   //8/9/23
        Int32 _saveAsCopyTestTypeID = 0;                                                         //8/9/23
        Boolean _saveAs = false;
        Boolean _saveQualityControlAs = false;
        Boolean _changesMade = false;
        Boolean _isLoading = true;
        Boolean _saveTestTypeAs = false;                                                        //8/9/23        
        Boolean _testTypeStatus = true;                                                         //8/17/23
        Analyzer analyzerMaintenance;                                                           //4/19/22       


        //--------------------------------------------------------------------------------------------------------
        public TabOrder()
        {
            InitializeComponent();
        }
        private void TabOrder_Load(object sender, EventArgs e)
        {
            try
            {
                //10/6/23 - these 2 lines can be commented when TestType in Aim Maintenance has been there for a couple months with no problems.
                tabControl1.TabPages.Remove(pageTestType);
                tabControl1.TabPages.Remove(pageTestTypeGroups);

                DataBindTabOrder();
                DataBindAlternateName();

                //6/29/21 - commented, changed label form
                //6/25/20
                //DataBindLabels();

                //6/29/21
                DataBindLabelFields();
                _isLoading = false;

                //5/2/23
                DataBindProdLabelFields();

                DataBindTemplate();
                DataBindQualityControlMassEntry();
                DataBindLueParameter();

                //6/18/20
                DataBindShowNameOrBoth();

                //8/10/23
                DataBindTestType();

                //8/18/23
                DataBindPageOrder();

                //10/3/23
                DataBindlueTestTypeGroup();
                DataBindlueTestType_Group();

                DataBindLue();
                DataBindlueMaterial();
                DataBindLueAnalyzerNutrient();
                SetToolTips();
                timer1.Stop();
                btnAddNutrientToList.BackColor = SystemColors.Control;

                if (AIMGlobal.massEntryTab == true)
                {
                    tabControl1.SelectedIndex = 4;
                    if (AIMGlobal.massEntryTemplateID != 0)
                    {

                        pnlExisting.Visible = true;
                        pnlExisting.Location = new Point(8, 77);

                        //4/20/21
                        //pnlExisting.Size = new Size(874, 69);
                        pnlExisting.Size = new Size(945, 69);

                        pnlNew.Visible = false;
                        pnlNew.Location = new Point(12, 66);
                        pnlNew.Size = new Size(874, 10);

                        lueTemplateName.EditValue = AIMGlobal.massEntryTemplateID;
                    }
                }

                if (AIMGlobal.qualityControlMassEntryTab == true)
                {
                    tabControl1.SelectedIndex = 4;
                    if (AIMGlobal.qualityControlMassEntryID != 0)
                    {
                        panelExistingQualityControl.Visible = true;
                        panelNewQualityControl.Visible = false;
                        lueQualityControlMassEntryName.EditValue = AIMGlobal.qualityControlMassEntryID;
                    }
                }

                //8/11/23
                if (AIMGlobal.testTypeTab == true)
                {
                    tabControl1.SelectedIndex = 6;
                    if (AIMGlobal.testTypeHeaderID != 0)
                    {
                        panelExistingTestType.Visible = true;
                        panelNewTestType.Visible = false;
                        lueTestType.EditValue = AIMGlobal.testTypeHeaderID;
                    }
                }

                //10/4/23
                else if (AIMGlobal.testTypeGroupTab == true)
                {
                    tabControl1.SelectedIndex = 7;
                    if (AIMGlobal.testTypeGroupID != 0)
                    {                        
                        lueTestTypeGroup.EditValue = AIMGlobal.testTypeGroupID;
                    }
                }


                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
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
                //--------------------------------------------------
                //6/29/21
                //Close();
                DialogResult dr = new DialogResult();
                if (_changesMade == true)
                {
                    dr = MessageBox.Show("Changes were made to the labels but not saved. Continue?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
                //--------------------------------------------------

                //TODO - call Sample Screen refresh button


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRefresh_Click(Object sender, EventArgs e)
        {
            DataBindTabOrder();
            DataBindAlternateName();

            //6/29/21 - commented, changed label form
            //DataBindLabels();

            DataBindTemplate();

            //1/4/18
            DataBindQualityControlMassEntry();
            DataBindLueQualityControlParameter();

            DataBindLue();
            DataBindlueMaterial();
            DataBindLueAnalyzerNutrient();
            timer1.Stop();
            btnAddNutrientToList.BackColor = SystemColors.Control;
            pnlExisting.Visible = true;
            pnlExisting.Location = new Point(8, 77);

            //4/20/21
            //pnlExisting.Size = new Size(874, 69);
            pnlExisting.Size = new Size(945, 69);

            pnlNew.Visible = false;
            pnlNew.Location = new Point(12, 66);
            pnlNew.Size = new Size(874, 10);


            lueTemplateName.EditValue = AIMGlobal.massEntryTemplateID;
            txtMaterial.Text = "";
            txtDataSource.Text = "";
            txtDataSource_QualityControl.Text = "";
            txtTemplateNameNew.Text = "";
            txtQualityControlMassEntryNameNew.Text = "";
            txtEditName.Text = "";
            txtEditMassEntryName.Text = "";
        }
        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
        private void TabOrder_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
        private void SetToolTips()
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnDelete, "Delete List");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

            ttip.SetToolTip(btnAddNutrientToList, "Add Nutrient to List");
            ttip.SetToolTip(btnSave, "Save Mass Entry List");
            ttip.SetToolTip(btnSaveAs, "Save As");

            ttip.SetToolTip(btnAddParameter, "AddParameter to List");
            ttip.SetToolTip(btnSaveMassEntry, "Save Mass Entry List");
            ttip.SetToolTip(btnSaveMassEntryAs, "Save As");

            //4/20/21
            ttip.SetToolTip(btnSaveExisting, "Save Mass Entry List");

            //6/28/21
            ttip.SetToolTip(btnSaveLabels, "Save Label Settings");

            //8/14/23
            ttip.SetToolTip(btnTestTypeDelete, "Delete Test Type");
            ttip.SetToolTip(btnTestTypeSaveAs, "Save As");
            ttip.SetToolTip(btnTestTypeAddDatasource, "Add Data Source to Test Type");
            ttip.SetToolTip(btnSaveTestType, "Save Test Type");

            //10/3/23
            ttip.SetToolTip(btnPreviewTestTypeGroup, "Preview Grid");
            ttip.SetToolTip(btnAddNewTestTypeGroupCode, "Add New Test Type Group");
            ttip.SetToolTip(btnAddTestType, "Add Test Type to Grid");



        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (btnAddNutrientToList.BackColor == SystemColors.Control)
            {
                btnAddNutrientToList.BackColor = SystemColors.Info;
            }
            else
            {
                btnAddNutrientToList.BackColor = SystemColors.Control;
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnDelete.Visible = false;
            if (tabControl1.SelectedIndex == 4) //Mass Entry tab
            {
                btnDelete.Visible = true;
            }
            else if (tabControl1.SelectedIndex == 5) //Quality Control Mass Entry tab
            {
                btnDelete.Visible = true;
            }

            lblColorBox.Visible = false;
            txtColorBox.Visible = false;
            if (tabControl1.SelectedIndex == 0) //Customize Field Names tab
            {
                lblColorBox.Visible = true;
                txtColorBox.Visible = true;
            }

            if (tabControl1.SelectedIndex == 3) //Page Order
            {
                grdSampleTabControlPageView.GridControl.Focus();
            }

        }

        #region CUSTOMIZE FIELD NAMES ----------------------------------------------------------------------------------
        //6/19/20 - allow editing of ShowCodeNameOrBothID column only for Owner, OwnerLoc, Supplier, and SupplierLoc
        private void grdAlternateNameView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                AlternateNameInfo alternateName = new AlternateNameInfo();
                alternateName = (AlternateNameInfo)grdAlternateNameView.GetFocusedRow();
                alternateName.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveAlternateName(ref alternateName);
                //todo DataBindAlternateName();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataBindAlternateName()
        {
            List<AlternateNameInfo> alternate = new List<AlternateNameInfo>();
            alternate = AIMManager.GetAlternateName((Int32)EnumFormID.Sample);
            if (alternate.Count != 0)
            {
                grdAlternateName.DataSource = alternate;
            }



        }
        private void DataBindShowNameOrBoth()                                                    //6/18/20
        {
            List<ShowCodeNameOrBothInfo> showCodeNameBoth = new List<ShowCodeNameOrBothInfo>();
            showCodeNameBoth = AIMManager.GetCodeNameOrBothValues();
            replueShowCodeNameOrBoth.DataSource = showCodeNameBoth;
            XtraEditors.LoadGridLookUpEdit(ref replueShowCodeNameOrBoth, "ShowCodeNameOrBothID", "Option");
        }
        void grdAlternateNameViewActiveEditor_EditValueChanged(object sender, EventArgs e)
        {
            grdAlternateNameView.CloseEditor();
        }
        private void grdAlternateNameView_CustomDrawCell(Object sender, RowCellCustomDrawEventArgs e)
        {
            String requiredField = "";
            String fieldName = "";

            if (e.Column == colVisible)
            {
                requiredField = Convert.ToString(grdAlternateNameView.GetRowCellValue(e.RowHandle, UserControlName_Original));
                if (requiredField == "Material" ||
                    requiredField == "Owner" ||
                    requiredField == "Owner Location" ||
                    requiredField == "Supplier" ||
                    requiredField == "Supplier Location" ||
                    requiredField == "Sample Code" ||
                    requiredField == "Alternate Code" ||
                    requiredField == "Sample Date")
                {
                    e.Appearance.BackColor = Color.LightSteelBlue;
                }
            }

            //6/19/20
            if (e.Column == colShowCodeNameBoth)
            {
                fieldName = Convert.ToString(grdAlternateNameView.GetRowCellValue(e.RowHandle, UserControlName_Original));
                if (fieldName == "Owner Code" || fieldName == "Owner Location Code" || fieldName == "Supplier Code" || fieldName == "Supplier Location Code")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }

            }
        }
        private void grdAlternateNameView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            String fieldName = "";

            if ((sender as GridView).FocusedColumn.FieldName == "ShowCodeNameOrBothID")
            {
                fieldName = Convert.ToString(grdAlternateNameView.GetRowCellValue(((ColumnView)sender).FocusedRowHandle, UserControlName_Original));
                if (fieldName != "Owner Code" && fieldName != "Owner Location Code" && fieldName != "Supplier Code" && fieldName != "Supplier Location Code")
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region LABELS -------------------------------------------------------------------------------------------------
        //5/3/23
        private void populateLineDropdowns(DevExpress.XtraEditors.LookUpEdit lue, List<LabelsInfo> labels)
        {
            lue.Properties.DataSource = labels;
            XtraEditors.LoadLookUpEdit(ref lue, "LabelsID", "UserControlName");
            lue.Properties.Columns[13].Caption = "Field Name";
        }
        private void setYesNoValue(Boolean lineField, Int32 labelsID, Boolean lineBold, DevExpress.XtraEditors.LookUpEdit lueLine, DevExpress.XtraEditors.LookUpEdit lueYesNo)
        {
            if (lineField == true)
            {
                lueLine.EditValue = labelsID;
                if (lineBold == true)
                {
                    lueYesNo.EditValue = (Int32)EnumYesNo.Yes;
                }
                else
                {
                    lueYesNo.EditValue = 2;
                }
            }
        }
        private void populateYesNoDropdowns(DevExpress.XtraEditors.LookUpEdit lue, List<YesNoInfo> yesNo)
        {
            lue.Properties.DataSource = yesNo;
            XtraEditors.LoadLookUpEdit(ref lue, "YesNoID", "YesNoString");
            lue.Properties.Columns[1].Caption = "";
        }
        private void grdLabelsSave(Int32 labelsID, Int32 labelfield, Boolean labelValue)
        {
            LabelsInfo labels = new LabelsInfo();
            labels.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            //AIMManager.SaveLabels(labelsID, labelfield, labelValue, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveLabels(labelsID, labelfield, labelValue, false, userGlobal.CFCLoginUser.CFCUserID);
        }
        //5/2/23 - Nearly exact same code as DataBindLabelFields, except sending "P" to stp. 
        private void DataBindProdLabelFields()
        {
            List<LabelsInfo> labels = new List<LabelsInfo>();
            labels = AIMManager.GetLabels_IngrOrProd("P");      //--> "P" excludes supplier/source fields
            labels.Insert
            (
                0,
                new LabelsInfo
                {
                    LabelsID = 0,
                    UserControlName = "--Select--"
                }
            );
            populateLineDropdowns(lueProdLabelLine1, labels);
            populateLineDropdowns(lueProdLabelLine2, labels);
            populateLineDropdowns(lueProdLabelLine3, labels);
            populateLineDropdowns(lueProdLabelLine4, labels);
            populateLineDropdowns(lueProdLabelLine5, labels);

            //---------------------------------------------------------------------------           
            List<YesNoInfo> yesNo = new List<YesNoInfo>();
            yesNo = AIMManager.GetYesNo();
            yesNo.Insert
            (
                0,
                new YesNoInfo
                {
                    YesNoID = 0,
                    YesNoString = "--Select--"
                }
            );
            populateYesNoDropdowns(lueProdYesNo1, yesNo);
            populateYesNoDropdowns(lueProdYesNo2, yesNo);
            populateYesNoDropdowns(lueProdYesNo3, yesNo);
            populateYesNoDropdowns(lueProdYesNo4, yesNo);
            populateYesNoDropdowns(lueProdYesNo5, yesNo);

            lueProdYesNo1.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo2.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo3.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo4.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo5.EditValue = (Int32)EnumYesNo.No;

            //---------------------------------------------------------------------------

            CheckLabelSize();

            lblProdLine4.Visible = true;
            lueProdLabelLine4.Visible = true;
            lueProdYesNo4.Visible = true;

            lblProdLine5.Visible = true;
            lueProdLabelLine5.Visible = true;
            lueProdYesNo5.Visible = true;

            foreach (var item in labels)
            {
                setYesNoValue(item.LabelProdFieldLine1, item.LabelsID, item.ProdLine1Bold, lueProdLabelLine1, lueProdYesNo1);
                setYesNoValue(item.LabelProdFieldLine2, item.LabelsID, item.ProdLine2Bold, lueProdLabelLine2, lueProdYesNo2);
                setYesNoValue(item.LabelProdFieldLine3, item.LabelsID, item.ProdLine3Bold, lueProdLabelLine3, lueProdYesNo3);
                setYesNoValue(item.LabelProdFieldLine4, item.LabelsID, item.ProdLine4Bold, lueProdLabelLine4, lueProdYesNo4);
                setYesNoValue(item.LabelProdFieldLine5, item.LabelsID, item.ProdLine5Bold, lueProdLabelLine5, lueProdYesNo5);
            }

            if (_labelSize == 1)
            {
                lblProdLine4.Visible = false;
                lueProdLabelLine4.Visible = false;
                lueProdYesNo4.Visible = false;

                lblProdLine5.Visible = false;
                lueProdLabelLine5.Visible = false;
                lueProdYesNo5.Visible = false;
            }
        }

        //6/29/21
        private void DataBindLabelFields()
        {
            List<LabelsInfo> labels = new List<LabelsInfo>();

            //5/2/23
            //labels = AIMManager.GetLabels();
            labels = AIMManager.GetLabels_IngrOrProd("I");

            labels.Insert
            (
                0,
                new LabelsInfo
                {
                    LabelsID = 0,
                    UserControlName = "--Select--"
                }
            );

            //5/3/23 - commented and replaced with "populate" function below
            //lueLabelLine1.Properties.DataSource = labels;
            //XtraEditors.LoadLookUpEdit(ref lueLabelLine1, "LabelsID", "UserControlName");
            //lueLabelLine1.Properties.Columns[13].Caption = "Field Name";
            //lueLabelLine2.Properties.DataSource = labels;
            //XtraEditors.LoadLookUpEdit(ref lueLabelLine2, "LabelsID", "UserControlName");
            //lueLabelLine2.Properties.Columns[13].Caption = "Field Name";
            //lueLabelLine3.Properties.DataSource = labels;
            //XtraEditors.LoadLookUpEdit(ref lueLabelLine3, "LabelsID", "UserControlName");
            //lueLabelLine3.Properties.Columns[13].Caption = "Field Name";
            //lueLabelLine4.Properties.DataSource = labels;
            //XtraEditors.LoadLookUpEdit(ref lueLabelLine4, "LabelsID", "UserControlName");
            //lueLabelLine4.Properties.Columns[13].Caption = "Field Name";
            //lueLabelLine5.Properties.DataSource = labels;
            //XtraEditors.LoadLookUpEdit(ref lueLabelLine5, "LabelsID", "UserControlName");
            //lueLabelLine5.Properties.Columns[13].Caption = "Field Name";
            populateLineDropdowns(lueLabelLine1, labels);
            populateLineDropdowns(lueLabelLine2, labels);
            populateLineDropdowns(lueLabelLine3, labels);
            populateLineDropdowns(lueLabelLine4, labels);
            populateLineDropdowns(lueLabelLine5, labels);

            //---------------------------------------------------------------------------           
            List<YesNoInfo> yesNo = new List<YesNoInfo>();
            yesNo = AIMManager.GetYesNo();
            yesNo.Insert
            (
                0,
                new YesNoInfo
                {
                    YesNoID = 0,
                    YesNoString = "--Select--"
                }
            );

            //5/3/23 - commented and replaced with "populate" function below
            //lueYesNo1.Properties.DataSource = yesNo;
            //XtraEditors.LoadLookUpEdit(ref lueYesNo1, "YesNoID", "YesNoString");
            //lueYesNo1.Properties.Columns[1].Caption = "";
            //lueYesNo2.Properties.DataSource = yesNo;
            //XtraEditors.LoadLookUpEdit(ref lueYesNo2, "YesNoID", "YesNoString");
            //lueYesNo2.Properties.Columns[1].Caption = "";
            //lueYesNo3.Properties.DataSource = yesNo;
            //XtraEditors.LoadLookUpEdit(ref lueYesNo3, "YesNoID", "YesNoString");
            //lueYesNo3.Properties.Columns[1].Caption = "";
            //lueYesNo4.Properties.DataSource = yesNo;
            //XtraEditors.LoadLookUpEdit(ref lueYesNo4, "YesNoID", "YesNoString");
            //lueYesNo4.Properties.Columns[1].Caption = "";
            //lueYesNo5.Properties.DataSource = yesNo;
            //XtraEditors.LoadLookUpEdit(ref lueYesNo5, "YesNoID", "YesNoString");
            //lueYesNo5.Properties.Columns[1].Caption = "";
            populateYesNoDropdowns(lueYesNo1, yesNo);
            populateYesNoDropdowns(lueYesNo2, yesNo);
            populateYesNoDropdowns(lueYesNo3, yesNo);
            populateYesNoDropdowns(lueYesNo4, yesNo);
            populateYesNoDropdowns(lueYesNo5, yesNo);

            lueYesNo1.EditValue = (Int32)EnumYesNo.No;
            lueYesNo2.EditValue = (Int32)EnumYesNo.No;
            lueYesNo3.EditValue = (Int32)EnumYesNo.No;
            lueYesNo4.EditValue = (Int32)EnumYesNo.No;
            lueYesNo5.EditValue = (Int32)EnumYesNo.No;

            //---------------------------------------------------------------------------

            //4/21/22
            CheckLabelSize();

            //4/21/22
            lblLine4.Visible = true;
            lueLabelLine4.Visible = true;
            lueYesNo4.Visible = true;
            lblLine5.Visible = true;
            lueLabelLine5.Visible = true;
            lueYesNo5.Visible = true;

            foreach (var item in labels)
            {
                //5/3/23 - commented and replaced with "setYesNoValue" function below
                //if (item.LabelFieldFirst == true)
                //{
                //    lueLabelLine1.EditValue = item.LabelsID;
                //    if (item.Line1Bold == true)
                //    {
                //        lueYesNo1.EditValue = (Int32)EnumYesNo.Yes;
                //    }
                //    else
                //    {
                //        lueYesNo1.EditValue = 2;                            
                //    }
                //}
                //if (item.LabelFieldLine2 == true)
                //{
                //    lueLabelLine2.EditValue = item.LabelsID;
                //    if (item.Line2Bold == true)
                //    {
                //        lueYesNo2.EditValue = (Int32)EnumYesNo.Yes;
                //    }
                //    else
                //    {
                //        lueYesNo2.EditValue = 2;
                //    }
                //}
                //if (item.LabelFieldLine3 == true)
                //{
                //    lueLabelLine3.EditValue = item.LabelsID;
                //    if (item.Line3Bold == true)
                //    {
                //        lueYesNo3.EditValue = (Int32)EnumYesNo.Yes;
                //    }
                //    else
                //    {
                //        lueYesNo3.EditValue = 2;
                //    }
                //}
                //if (item.LabelFieldLine4 == true)
                //{
                //    lueLabelLine4.EditValue = item.LabelsID;
                //    if (item.Line4Bold == true)
                //    {
                //        lueYesNo4.EditValue = (Int32)EnumYesNo.Yes;
                //    }
                //    else
                //    {
                //        lueYesNo4.EditValue = 2;
                //    }
                //}
                //if (item.LabelFieldLast == true)
                //{
                //    lueLabelLine5.EditValue = item.LabelsID;
                //    if (item.Line5Bold == true)
                //    {
                //        lueYesNo5.EditValue = (Int32)EnumYesNo.Yes;
                //    }
                //    else
                //    {
                //        lueYesNo5.EditValue = 2;
                //    }
                //}
                setYesNoValue(item.LabelFieldFirst, item.LabelsID, item.Line1Bold, lueLabelLine1, lueYesNo1);
                setYesNoValue(item.LabelFieldLine2, item.LabelsID, item.Line2Bold, lueLabelLine2, lueYesNo2);
                setYesNoValue(item.LabelFieldLine3, item.LabelsID, item.Line3Bold, lueLabelLine3, lueYesNo3);
                setYesNoValue(item.LabelFieldLine4, item.LabelsID, item.Line4Bold, lueLabelLine4, lueYesNo4);
                setYesNoValue(item.LabelFieldLast, item.LabelsID, item.Line5Bold, lueLabelLine5, lueYesNo5);

            }

            //4/21/22
            if (_labelSize == 1)
            {
                lblLine4.Visible = false;
                lueLabelLine4.Visible = false;
                lueYesNo4.Visible = false;
                lblLine5.Visible = false;
                lueLabelLine5.Visible = false;
                lueYesNo5.Visible = false;
            }

        }
        private void btnSaveLabels_Click(object sender, EventArgs e)
        {

            //4/21/22
            if (_labelSize == 1)
            {
                if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0)
                {
                    MessageBox.Show("All three label fields must be selected.");
                    return;
                }
                if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0)
                {
                    MessageBox.Show("Please select Yes or No to bolding.");
                    return;
                }
            }
            else
            {
                if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0 || (Int32)lueLabelLine4.EditValue == 0 || (Int32)lueLabelLine5.EditValue == 0)
                {
                    MessageBox.Show("All five label fields must be selected.");
                    return;
                }
                if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0 || (Int32)lueYesNo4.EditValue == 0 || (Int32)lueYesNo5.EditValue == 0)
                {
                    MessageBox.Show("Please select Yes or No to bolding.");
                    return;
                }
            }
            Boolean label1 = false;
            Boolean label2 = false;
            Boolean label3 = false;
            Boolean label4 = false;
            Boolean label5 = false;

            Boolean yesno1 = false;
            Boolean yesno2 = false;
            Boolean yesno3 = false;
            Boolean yesno4 = false;
            Boolean yesno5 = false;

            LabelsInfo label = new LabelsInfo();
            if ((Int32)lueLabelLine1.EditValue > 0) { label1 = true; }
            if ((Int32)lueLabelLine2.EditValue > 0) { label2 = true; }
            if ((Int32)lueLabelLine3.EditValue > 0) { label3 = true; }
            if ((Int32)lueLabelLine4.EditValue > 0) { label4 = true; }
            if ((Int32)lueLabelLine5.EditValue > 0) { label5 = true; }

            if ((Int32)lueYesNo1.EditValue == (Int32)EnumYesNo.Yes) { yesno1 = true; }
            if ((Int32)lueYesNo2.EditValue == (Int32)EnumYesNo.Yes) { yesno2 = true; }
            if ((Int32)lueYesNo3.EditValue == (Int32)EnumYesNo.Yes) { yesno3 = true; }
            if ((Int32)lueYesNo4.EditValue == (Int32)EnumYesNo.Yes) { yesno4 = true; }
            if ((Int32)lueYesNo5.EditValue == (Int32)EnumYesNo.Yes) { yesno5 = true; }

            AIMManager.SaveLabels((Int32)lueLabelLine1.EditValue, 1, label1, yesno1, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveLabels((Int32)lueLabelLine2.EditValue, 2, label2, yesno2, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveLabels((Int32)lueLabelLine3.EditValue, 3, label3, yesno3, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveLabels((Int32)lueLabelLine4.EditValue, 4, label4, yesno4, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveLabels((Int32)lueLabelLine5.EditValue, 5, label5, yesno5, userGlobal.CFCLoginUser.CFCUserID);

            _changesMade = false;
        }
        private void lueLabelLine1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ChangesMade(e);
        }
        private void lueLabelLine2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ChangesMade(e);
        }
        private void lueLabelLine3_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ChangesMade(e);
        }
        private void lueLabelLine4_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ChangesMade(e);
        }
        private void lueLabelLine5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ChangesMade(e);
        }
        private void lueYesNo1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_isLoading == false)
            {
                ChangesMade(e);
            }

        }
        private void lueYesNo2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_isLoading == false)
            {
                ChangesMade(e);
            }
        }
        private void lueYesNo3_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_isLoading == false)
            {
                ChangesMade(e);
            }
        }
        private void lueYesNo4_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_isLoading == false)
            {
                ChangesMade(e);
            }
        }
        private void lueYesNo5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_isLoading == false)
            {
                ChangesMade(e);
            }
        }
        private Boolean ChangesMade(DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            Boolean ChangesMade = false;

            if (e.OldValue == null)
            {
            }
            else
            {
                if ((Int32)e.OldValue > 0)
                {
                    _changesMade = true;
                }
            }
            ChangesMade = _changesMade;
            return ChangesMade;

        }
        //4/19/22
        private void CheckLabelSize()
        {
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                _labelSize = systemSettings.LabelSize;
            }
        }

        //5/2/23
        private void btnSaveProdLabels_Click(object sender, EventArgs e)
        {

            Boolean label1 = false;
            Boolean label2 = false;
            Boolean label3 = false;
            Boolean label4 = false;
            Boolean label5 = false;

            Boolean yesno1 = false;
            Boolean yesno2 = false;
            Boolean yesno3 = false;
            Boolean yesno4 = false;
            Boolean yesno5 = false;

            LabelsInfo label = new LabelsInfo();
            if ((Int32)lueProdLabelLine1.EditValue > 0) { label1 = true; }
            if ((Int32)lueProdLabelLine2.EditValue > 0) { label2 = true; }
            if ((Int32)lueProdLabelLine3.EditValue > 0) { label3 = true; }
            if ((Int32)lueProdLabelLine4.EditValue > 0) { label4 = true; }
            if ((Int32)lueProdLabelLine5.EditValue > 0) { label5 = true; }

            if ((Int32)lueProdYesNo1.EditValue == (Int32)EnumYesNo.Yes) { yesno1 = true; }
            if ((Int32)lueProdYesNo2.EditValue == (Int32)EnumYesNo.Yes) { yesno2 = true; }
            if ((Int32)lueProdYesNo3.EditValue == (Int32)EnumYesNo.Yes) { yesno3 = true; }
            if ((Int32)lueProdYesNo4.EditValue == (Int32)EnumYesNo.Yes) { yesno4 = true; }
            if ((Int32)lueProdYesNo5.EditValue == (Int32)EnumYesNo.Yes) { yesno5 = true; }

            AIMManager.SaveProdLabels((Int32)lueProdLabelLine1.EditValue, 1, label1, yesno1, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveProdLabels((Int32)lueProdLabelLine2.EditValue, 2, label2, yesno2, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveProdLabels((Int32)lueProdLabelLine3.EditValue, 3, label3, yesno3, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveProdLabels((Int32)lueProdLabelLine4.EditValue, 4, label4, yesno4, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveProdLabels((Int32)lueProdLabelLine5.EditValue, 5, label5, yesno5, userGlobal.CFCLoginUser.CFCUserID);

            _changesMade = false;
        }
        #endregion

        #region TAB ORDER ----------------------------------------------------------------------------------------------
        private void DataBindTabOrder()
        {
            if (recursion == 3)
            {
                recursion = 0;
                MessageBox.Show("Tab order is not set up for " + userGlobal.CFCLoginUser.FirstName + ". Please contact IT.", "warning...");
                return;
            }
            List<TabOrderInfo> tabOrder = new List<TabOrderInfo>();
            tabOrder = AIMManager.GetTabOrder(1, userGlobal.CFCLoginUser.CFCUserID);
            if (tabOrder.Count != 0)
            {
                grdTabOrder.DataSource = tabOrder;
            }
            else
            {
                AIMManager.InsertTabOrder(1, userGlobal.CFCLoginUser.CFCUserID);
                recursion += 1;
                DataBindTabOrder();
            }
        }
        private void grdTabOrderView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                TabOrderInfo tabOrderData = new TabOrderInfo();
                tabOrderData = (TabOrderInfo)grdTabOrderView.GetFocusedRow();
                tabOrderData.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveTabOrder(ref tabOrderData);
                DataBindTabOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region MASS ENTRY ---------------------------------------------------------------------------------------------
        private void DataBindLue()
        {
            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            dataSourceType = AIMManager.GetDataSourceType(true);
            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );
            lueDataSourceTypeNew.Properties.DataSource = dataSourceType;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeNew, "DataSourceTypeID", "Name", "TypeDisplay");

            lueDataSourceTypeNew_QualityControl.Properties.DataSource = dataSourceType;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeNew_QualityControl, "DataSourceTypeID", "Name", "TypeDisplay");

        }
        private void DataBindTemplate()
        {
            _template = AIMManager.GetSampleResultTemplate();
            _template.Insert
                (
                    0,
                    new SampleResultTemplateInfo
                    {
                        SampleResultTemplateID = 0,
                        SampleResultTemplateName = "--Select--"
                    }
                );
            lueTemplateName.Properties.DataSource = _template;
            XtraEditors.LoadLookUpEdit(ref lueTemplateName, "SampleResultTemplateID", "SampleResultTemplateName");
            lueTemplateName.Properties.Columns[1].Caption = "Mass Entry List Name";

            SampleResultTemplateDetailInfo clearResults = new SampleResultTemplateDetailInfo();
            grdSampleResultDetailTemplate.DataSource = clearResults;
            grbAddNutrient.Visible = false;
            grdSampleResultDetailTemplate.Visible = false;

        }
        private void DataBindlueNutrient()
        {
            _dataSourceTypeID = (Int32)lueDataSourceTypeNew.EditValue;
            List<AnalyzerNutrientInfo> nutrient = new List<AnalyzerNutrientInfo>();
            nutrient = AIMManager.GetAnalyzerNutrient(_dataSourceTypeID, true);
            nutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--"
                }
            );
            lueAnalyzerNutrient.Properties.DataSource = nutrient;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode", "UnitType");
        }
        private void DataBindlueMaterial()
        {
            List<AIMMaterialRefAnalyzerMaterialInfo> material = new List<AIMMaterialRefAnalyzerMaterialInfo>();
            material = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID((Int32)lueDataSourceTypeNew.EditValue);
            material.Insert
            (
                0,
                new AIMMaterialRefAnalyzerMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "All Materials",
                    AIMMaterialName = "--All Materials--"
                }
            );
            lueMaterialNew.Properties.DataSource = material;
            XtraEditors.LoadLookUpEdit(ref lueMaterialNew, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
            lueMaterialNew.EditValue = 0;

        }
        private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindlueMaterial();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTemplateNameNew.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Mass Entry List Name.");
                    txtTemplateNameNew.Focus();
                    return;
                }
                if ((Int32)lueDataSourceTypeNew.EditValue == 0)
                {
                    MessageBox.Show("Please select a Data Source.");
                    lueDataSourceTypeNew.Focus();
                    lueDataSourceTypeNew.ShowPopup();
                    return;
                }


                SampleResultTemplateInfo result = new SampleResultTemplateInfo();
                result.SampleResultTemplateID = 0;
                result.SampleResultTemplateName = txtTemplateNameNew.Text;
                result.DataSourceTypeID = (Int32)lueDataSourceTypeNew.EditValue;
                result.AIMMaterialID = (Int32)lueMaterialNew.EditValue;
                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveSampleResultTemplate(ref result);

                if (_saveAs == true)
                {
                    SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                    List<SampleResultTemplateDetailInfo> copyResult = new List<SampleResultTemplateDetailInfo>();
                    copyResult = AIMManager.GetSampleResultTemplateDetail(_saveAsCopyTemplateID);
                    foreach (var item in copyResult)
                    {
                        templateDetail.SampleResultTemplateDetailID = 0;
                        templateDetail.SampleResultTemplateID = outPut.RecordID;
                        templateDetail.AnalyzerNutrientID = item.AnalyzerNutrientID;
                        templateDetail.SortOrder = item.SortOrder;
                        templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        templateDetail.UpdateStamp = DateTime.Now;
                        ErrorInfo outPutResult = AIMManager.SaveSampleResultTemplateDetail(ref templateDetail);
                        DataBindTemplateDetail();
                    }
                    _saveAs = false;

                    //4/20/21 - moved ending paren up to this point from below
                }

                pnlExisting.Visible = true;
                pnlExisting.Location = new Point(8, 77);

                //4/20/21
                //pnlExisting.Size = new Size(874, 69);
                pnlExisting.Size = new Size(945, 69);

                pnlNew.Visible = false;
                pnlNew.Location = new Point(12, 66);
                pnlNew.Size = new Size(874, 10);

                //4/20/21 - commented and moved up to above
                //}

                SampleResultTemplateDetailInfo clearResults = new SampleResultTemplateDetailInfo();
                grdSampleResultDetailTemplate.DataSource = clearResults;

                DataBindTemplate();
                _templateID = outPut.RecordID;
                DataBindLueAnalyzerNutrient();

                //go to "Existing" tab and pull up newly-added template
                //tabControl2.SelectedIndex = 1;
                lueTemplateName.EditValue = _templateID;
                lueAnalyzerNutrient.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtEditName_Validated(Object sender, EventArgs e)
        {
            Int32 sampleResultTemplateID = 0;
            try
            {
                //to keep edited name from being blank
                if (String.IsNullOrWhiteSpace(txtEditName.Text) == false)
                {
                    if ((txtEditName.Text != lueTemplateName.Text))
                    {

                        //get current templateID id							
                        sampleResultTemplateID = (Int32)lueTemplateName.EditValue;

                        //check to see if name already exists (use new name to search template table)
                        List<SampleResultTemplateInfo> template = new List<SampleResultTemplateInfo>();
                        template = AIMManager.GetSampleResultTemplate(txtEditName.Text.ToString());
                        if (template == null)
                        {
                        }
                        else if (template.Count > 0)
                        {
                            MessageBox.Show("This list name already exists. ", "Warning...", MessageBoxButtons.OK);
                            txtEditName.Text = lueTemplateName.Text;
                        }
                        else
                        {
                            SampleResultTemplateInfo result = new SampleResultTemplateInfo();
                            result.SampleResultTemplateID = sampleResultTemplateID;
                            result.SampleResultTemplateName = txtEditName.Text;
                            result.DataSourceTypeID = (Int32)lueDataSourceTypeNew.EditValue;
                            result.AIMMaterialID = (Int32)lueMaterialNew.EditValue;
                            result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            ErrorInfo outPut = AIMManager.SaveSampleResultTemplate(ref result);

                            btnRefresh.PerformClick();
                            DataBindTemplate();
                            lueTemplateName.EditValue = sampleResultTemplateID;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSaveAs_Click(Object sender, EventArgs e)
        {
            _saveAs = true;
            _saveAsCopyTemplateID = _templateID;

            pnlExisting.Visible = false;
            pnlExisting.Location = new Point(12, 55);

            //pnlExisting.Size = new Size(874, 10);
            pnlExisting.Size = new Size(945, 10);

            pnlNew.Visible = true;
            pnlNew.Location = new Point(8, 77);
            pnlNew.Size = new Size(874, 69);
        }
        private void btnCancel_Click(Object sender, EventArgs e)
        {
            txtTemplateNameNew.Text = "";
            lueDataSourceTypeNew.EditValue = 0;
            lueMaterialNew.EditValue = 0;

            pnlNew.Visible = false;
            pnlNew.Location = new Point(12, 66);
            pnlNew.Size = new Size(874, 10);

            if (_saveAs == true)
            {
                pnlExisting.Visible = true;
                pnlExisting.Location = new Point(8, 77);

                //pnlExisting.Size = new Size(874, 69);
                pnlExisting.Size = new Size(945, 69);

                pnlNew.Visible = false;
                pnlNew.Location = new Point(12, 66);
                pnlNew.Size = new Size(874, 10);

            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                String name = "";

                if (this.pnlExisting.Visible == true)
                {
                    if ((Int32)lueTemplateName.EditValue == 0)
                    {
                        MessageBox.Show("Please select Mass Entry List to delete.");
                        lueTemplateName.Focus();
                        return;
                    }
                    name = lueTemplateName.Text;
                }
                else
                {
                    name = txtTemplateNameNew.Text;
                }
                DialogResult dialogResult = MessageBox.Show("Delete Mass Entry List '" + name + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    Int32 resultTemplateID = 0;
                    resultTemplateID = (Int32)lueTemplateName.EditValue;
                    AIMManager.DeleteSampleResultTemplate(resultTemplateID);
                    DataBindTemplate();
                    DataBindTemplateDetail();
                    txtEditName.Text = String.Empty;
                    txtDataSource.Text = String.Empty;
                    txtMaterial.Text = String.Empty;
                    lueDataSourceTypeNew.EditValue = 0;
                    lueMaterialNew.EditValue = 0;
                    lueAnalyzerNutrient.EditValue = 0;

                    grbAddNutrient.Visible = true;
                    grdSampleResultDetailTemplate.Visible = true;
                    if (_qualityControlHeader.Count < 2)
                    {
                        grbAddNutrient.Visible = false;
                        grdSampleResultDetailTemplate.Visible = false;

                    }

                    grbAddNutrient.Visible = false;
                    grdSampleResultDetailTemplate.Visible = false;

                    pnlExisting.Visible = false;
                    pnlExisting.Location = new Point(8, 77);

                    //4/20/21
                    //pnlExisting.Size = new Size(874, 10);
                    pnlExisting.Size = new Size(945, 10);

                    pnlNew.Visible = false;
                    pnlNew.Location = new Point(12, 66);
                    pnlNew.Size = new Size(874, 10);

                    panelNewQualityControl.Visible = false;

                    //MessageBox.Show("Mass Entry List Deleted.", "Information...");
                    btnSelectExisting.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void lueDataSourceTypeNew_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindlueMaterial();
        }
        private void lueDataSourceTypeNew_Enter(Object sender, EventArgs e)
        {
            lueDataSourceTypeNew.SelectAll();
        }
        private void lueDataSourceTypeNew_Click(Object sender, EventArgs e)
        {
            lueDataSourceTypeNew.SelectAll();
        }
        private void lueMaterialNew_Click(Object sender, EventArgs e)
        {
            lueMaterialNew.SelectAll();
        }
        private void lueMaterialNew_Enter(Object sender, EventArgs e)
        {
            lueMaterialNew.SelectAll();
        }
        private void btnAddSampleResult_Click(object sender, EventArgs e)
        {

            try
            {
                timer1.Stop();
                btnAddNutrientToList.BackColor = SystemColors.Control;

                if ((Int32)lueTemplateName.EditValue == 0)
                {
                    MessageBox.Show("Please enter Template Name.");
                    lueTemplateName.Focus();
                    lueTemplateName.ShowPopup();
                    return;
                }
                if ((Int32)lueAnalyzerNutrient.EditValue == 0)
                {
                    MessageBox.Show("Please select Nutrient.");
                    lueAnalyzerNutrient.Focus();
                    lueAnalyzerNutrient.ShowPopup();
                    return;
                }

                //get max SortOrder# from _templateDetail	
                ErrorInfo maxSortNumber = AIMManager.GetSampleResultTemplateDetailMaxSortNo(_templateID);


                SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                templateDetail.SampleResultTemplateDetailID = 0;
                templateDetail.SampleResultTemplateID = _templateID;
                templateDetail.AnalyzerNutrientID = (Int32)lueAnalyzerNutrient.EditValue;
                templateDetail.SortOrder = maxSortNumber.RecordID + 1;
                templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                templateDetail.UpdateStamp = DateTime.Now;
                ErrorInfo outPut = AIMManager.SaveSampleResultTemplateDetail(ref templateDetail);
                DataBindTemplateDetail();
                DataBindLueAnalyzerNutrient();

                lueAnalyzerNutrient.Focus();
                lueAnalyzerNutrient.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void lueAnalyzerNutrient_Click(Object sender, EventArgs e)
        {
            lueAnalyzerNutrient.SelectAll();
        }
        private void lueAnalyzerNutrient_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerNutrient.SelectAll();
        }
        private void repbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                templateDetail = (SampleResultTemplateDetailInfo)grdSampleResultDetailTemplateView.GetFocusedRow();
                templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //its a soft delete
                AIMManager.DeleteSampleResultTemplateDetail(templateDetail.SampleResultTemplateDetailID);
                DataBindTemplateDetail();
                DataBindLueAnalyzerNutrient();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueAnalyzerNutrient_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueAnalyzerNutrient.EditValue != 0)
            {
                timer1.Start();
            }

        }
        private void lueTemplateName_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueTemplateName.EditValue != 0)
            {

                txtMaterial.Text = String.Empty;
                txtDataSource.Text = String.Empty;
                txtEditName.Text = String.Empty;

                grbAddNutrient.Visible = true;
                grdSampleResultDetailTemplate.Visible = true;

                _templateID = (Int32)lueTemplateName.EditValue;

                if (lueTemplateName.EditValue != null)
                {
                    Int32.TryParse(lueTemplateName.EditValue.ToString(), out _templateID);
                    _template = AIMManager.GetSampleResultTemplateTemplateID(_templateID);
                }

                //1/24/19
                _dataSourceTypeID = (Int32)lueDataSourceTypeNew.EditValue;

                if (_template.Count != 0)
                {
                    _dataSourceTypeID = _template[0].DataSourceTypeID;
                    lueMaterialNew.EditValue = _template[0].AIMMaterialID;
                    txtMaterial.Text = _template[0].AIMMaterialName;
                    txtDataSource.Text = _template[0].DataSourceTypeName;

                }


                DataBindLueAnalyzerNutrient();
                DataBindTemplateDetail();
            }
        }
        private void lueMaterialNew_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindlueNutrient();
        }
        private void btnSelectExisting_Click(Object sender, EventArgs e)
        {

            pnlExisting.Visible = true;
            pnlExisting.Location = new Point(8, 77);

            //4/20/21
            //pnlExisting.Size = new Size(874, 69);
            pnlExisting.Size = new Size(945, 69);

            pnlNew.Visible = false;
            pnlNew.Location = new Point(12, 66);
            pnlNew.Size = new Size(874, 10);


        }
        private void btnAddNew_Click(Object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlNew.Location = new Point(8, 77);
            pnlNew.Size = new Size(874, 69);

            pnlExisting.Visible = false;
            pnlExisting.Location = new Point(12, 55);

            //4/20/21
            //pnlExisting.Size = new Size(874, 10);
            pnlExisting.Size = new Size(945, 10);

            grbAddNutrient.Visible = false;
            grdSampleResultDetailTemplate.Visible = false;
            lueDataSourceTypeNew.EditValue = 0;
            lueMaterialNew.EditValue = 0;
            _saveAs = false;
            txtTemplateNameNew.Text = "";

        }
        private void DataBindTemplateDetail()
        {
            _templateDetail = AIMManager.GetSampleResultTemplateDetail(_templateID);
            grdSampleResultDetailTemplate.DataSource = _templateDetail;
        }
        private void grdSampleResultDetailTemplateView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                //4/24/17 - commented, do not auto-save the results; user must click on Save button.
                //SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                //templateDetail = (SampleResultTemplateDetailInfo)grdSampleResultDetailTemplateView.GetFocusedRow();
                //templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.UpdateTemplateDetailSortOrder(ref templateDetail);
                //DataBindTemplateDetail();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //4/20/21
        private void btnSaveExisting_Click(object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue != null)
            {
                Int32.TryParse(lueTemplateName.EditValue.ToString(), out _templateID);

                List<SampleResultTemplateDetailInfo> det = new List<SampleResultTemplateDetailInfo>();
                det = (List<SampleResultTemplateDetailInfo>)grdSampleResultDetailTemplate.DataSource;
                if (det != null)
                {
                    foreach (var item in det)
                    {

                        SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                        templateDetail.SampleResultTemplateID = item.SampleResultTemplateID;
                        templateDetail.SampleResultTemplateDetailID = item.SampleResultTemplateDetailID;
                        templateDetail.SortOrder = item.SortOrder;
                        templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        AIMManager.UpdateTemplateDetailSortOrder(ref templateDetail);
                    }
                }
            }
        }
        #endregion

        #region QUALITY CONTROL MASS ENTRY -----------------------------------------------------------------------------
        private void DataBindLueParameter()
        {
            List<SampleQualityParameterInfo> param = new List<SampleQualityParameterInfo>();
            param = AIMManager.GetAllQualityParameters();
            param.Insert
            (
                0,
                new SampleQualityParameterInfo
                {
                    SampleQualityParameterID = 0,
                    SampleQualityParameterName = "--Select--"
                }
            );
            lueParameter.Properties.DataSource = param;
            XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName");
        }
        private void DataBindLueAnalyzerNutrient()
        {

            List<AnalyzerNutrientInfo> nutrient = new List<AnalyzerNutrientInfo>();
            nutrient = AIMManager.GetAnalyzerNutrientTemplateDetail(_dataSourceTypeID, _templateID, true);
            nutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--"
                }
            );
            lueAnalyzerNutrient.Properties.DataSource = nutrient;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode", "UnitType");
        }
        private void DataBindParameter()
        {
            List<SampleQualityParameterInfo> param = new List<SampleQualityParameterInfo>();
            param = AIMManager.GetSampleQualityParameterDetail(_dataSourceTypeID_QualityControl, _qualityControlID, true);
            param.Insert
            (
                0,
                new SampleQualityParameterInfo
                {
                    SampleQualityParameterID = 0,
                    SampleQualityParameterName = "--Select--"
                }
            );
            lueParameter.Properties.DataSource = param;
            XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName");
        }
        private void DataBindLueQualityControlParameter()
        {
            List<SampleQualityParameterInfo> param = new List<SampleQualityParameterInfo>();
            param = AIMManager.GetSampleQualityParameterDetail(_dataSourceTypeID_QualityControl, _qualityControlID, true);
            param.Insert
            (
                0,
                new SampleQualityParameterInfo
                {
                    SampleQualityParameterID = 0,
                    SampleQualityParameterName = "--Select--"
                }
            );
            lueParameter.Properties.DataSource = param;
            XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName");
        }
        private void DataBindQualityControlMassEntryDetail()
        {
            _qualityControlDetail = AIMManager.GetSampleQualityControlMassEntryDetail(_qualityControlID);
            grdQualityControl.DataSource = _qualityControlDetail;
        }
        private void btnAddParameter_Click(Object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                btnAddParameter.BackColor = SystemColors.Control;

                if ((Int32)lueParameter.EditValue == 0)
                {
                    MessageBox.Show("Please select Parameter.");
                    lueParameter.Focus();
                    lueParameter.ShowPopup();
                    return;
                }

                //get max SortOrder# from _qualityControlDetail	
                ErrorInfo maxSortNumber = AIMManager.GetSampleQualityControlDetailMaxSortNo(_qualityControlID);


                SampleQualityControlMassEntryDetailInfo mass = new SampleQualityControlMassEntryDetailInfo();
                mass.SampleQualityControlMassEntryDetailID = 0;
                mass.SampleQualityControlMassEntryID = _qualityControlID;
                mass.SampleQualityParameterID = (Int32)lueParameter.EditValue;
                mass.SortOrder = maxSortNumber.RecordID + 1;
                mass.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                mass.UpdateStamp = DateTime.Now;
                ErrorInfo outPut = AIMManager.SaveSampleQualityControlMassEntryDetail(ref mass);
                DataBindQualityControlMassEntryDetail();
                DataBindParameter();

                lueParameter.Focus();
                lueParameter.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSaveMassEntryAs_Click(Object sender, EventArgs e)
        {
            _saveQualityControlAs = true;
            _saveAsCopyQualityControlID = _qualityControlID;
            panelExistingQualityControl.Visible = false;
            panelNewQualityControl.Visible = true;
        }
        private void repbtnDeleteMassEntry_Click(Object sender, EventArgs e)
        {
            try
            {
                SampleQualityControlMassEntryDetailInfo mass = new SampleQualityControlMassEntryDetailInfo();
                mass = (SampleQualityControlMassEntryDetailInfo)grdQualityControlView.GetFocusedRow();
                mass.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //its a soft delete
                AIMManager.DeleteSampleQualityControlMassEntryDetail(mass.SampleQualityControlMassEntryDetailID);
                DataBindQualityControlMassEntryDetail();
                DataBindParameter();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnQualityControlExisting_Click(Object sender, EventArgs e)

        {

            panelNewQualityControl.Visible = false;
            panelNewQualityControl.Location = new Point(479, 135);
            panelNewQualityControl.Size = new Size(407, 8);

            panelExistingQualityControl.Visible = true;
            panelExistingQualityControl.Location = new Point(9, 57);
            panelExistingQualityControl.Size = new Size(877, 69);


            if ((Int32)lueQualityControlMassEntryName.EditValue != 0)
            {
                grbAddParameter.Visible = true;
                grdQualityControl.Visible = true;
                _qualityControlID = (Int32)lueQualityControlMassEntryName.EditValue;
                _dataSourceTypeID_QualityControl = _qualityControlHeader[lueQualityControlMassEntryName.ItemIndex].DataSourceTypeID;
                DataBindLueQualityControlParameter();
            }
        }
        private void btnDelete_QualityControl_Click(Object sender, EventArgs e)
        {
            String name = "";
            try
            {
                if (panelExistingQualityControl.Visible == true)
                {
                    if ((Int32)lueQualityControlMassEntryName.EditValue == 0)
                    {
                        MessageBox.Show("Please select Mass Entry List to delete.");
                        lueQualityControlMassEntryName.Focus();
                        return;
                    }
                    name = lueQualityControlMassEntryName.Text;
                }
                else
                {
                    name = txtQualityControlMassEntryNameNew.Text;
                }
                DialogResult dialogResult = MessageBox.Show("Delete Mass Entry List '" + name + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    Int32 ID = 0;
                    ID = (Int32)lueQualityControlMassEntryName.EditValue;
                    AIMManager.DeleteSampleQualityControlMassEntry(ID);

                    DataBindQualityControlMassEntry();
                    DataBindQualityControlMassEntryDetail();
                    txtEditMassEntryName.Text = String.Empty;
                    txtQualityControlMassEntryNameNew.Text = String.Empty;
                    lueDataSourceTypeNew_QualityControl.EditValue = 0;
                    lueParameter.EditValue = 0;

                    grbAddParameter.Visible = false;
                    grdQualityControl.Visible = false;
                    panelNewQualityControl.Visible = false;
                    panelNewQualityControl.Location = new Point(479, 135);
                    panelNewQualityControl.Size = new Size(407, 8);
                    panelExistingQualityControl.Visible = false;
                    panelExistingQualityControl.Location = new Point(479, 157);
                    panelExistingQualityControl.Size = new Size(407, 8);

                    MessageBox.Show("Mass Entry List Deleted.", "Information...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void DataBindQualityControlMassEntry()
        {
            _qualityControlHeader = AIMManager.GetSampleControlMassEntry();
            _qualityControlHeader.Insert
                (
                    0,
                    new SampleQualityControlMassEntryInfo
                    {
                        SampleQualityControlMassEntryID = 0,
                        SampleQualityControlMassEntryName = "--Select--"
                    }
                );
            lueQualityControlMassEntryName.Properties.DataSource = _qualityControlHeader;
            XtraEditors.LoadLookUpEdit(ref lueQualityControlMassEntryName, "SampleQualityControlMassEntryID", "SampleQualityControlMassEntryName");
            lueQualityControlMassEntryName.Properties.Columns[1].Caption = "Mass Entry List Name";

            SampleQualityControlMassEntryDetailInfo clearResults = new SampleQualityControlMassEntryDetailInfo();
            grdQualityControl.DataSource = clearResults;
            grbAddParameter.Visible = false;
            grdQualityControl.Visible = false;

        }
        private void lueQualityControlMassEntryName_EditValueChanged(Object sender, EventArgs e)
        {
            if ((Int32)lueQualityControlMassEntryName.EditValue != 0)
            {
                grbAddParameter.Visible = true;
                grdQualityControl.Visible = true;

                _qualityControlID = (Int32)lueQualityControlMassEntryName.EditValue;
                _dataSourceTypeID_QualityControl = _qualityControlHeader[this.lueQualityControlMassEntryName.ItemIndex].DataSourceTypeID;

                lueDataSourceTypeNew_QualityControl.EditValue = _dataSourceTypeID_QualityControl;


                txtDataSource_QualityControl.Text = lueDataSourceTypeNew_QualityControl.GetColumnValue("Name").ToString();



                DataBindLueQualityControlParameter();
                DataBindQualityControlMassEntryDetail();

            }
        }
        private void btnAddNewQualityControl_Click(Object sender, EventArgs e)
        {

            panelExistingQualityControl.Visible = false;
            panelExistingQualityControl.Location = new Point(479, 157);
            panelExistingQualityControl.Size = new Size(407, 8);

            panelNewQualityControl.Visible = true;
            panelNewQualityControl.Location = new Point(9, 57);
            panelNewQualityControl.Size = new Size(877, 69);


            grbAddParameter.Visible = false;
            grdQualityControl.Visible = false;
            _saveQualityControlAs = false;
            txtQualityControlMassEntryNameNew.Text = "";
            txtQualityControlMassEntryNameNew.ReadOnly = false;

            lueDataSourceTypeNew_QualityControl.ReadOnly = false;
            lueDataSourceTypeNew_QualityControl.EditValue = 0;

        }
        private void txtEditMassEntryName_Validated(Object sender, EventArgs e)
        {
            Int32 qualityControlID = 0;
            try
            {
                if ((txtEditMassEntryName.Text != lueQualityControlMassEntryName.Text))
                {

                    //get current quality control id							
                    qualityControlID = (Int32)lueQualityControlMassEntryName.EditValue;

                    //check to see if name already exists (use new name to search template table)
                    List<SampleQualityControlMassEntryInfo> mass = new List<SampleQualityControlMassEntryInfo>();
                    mass = AIMManager.GetSampleQualityControlMassEntryName(txtEditMassEntryName.Text.ToString());
                    if (mass == null)
                    {
                    }
                    else if (mass.Count > 0)
                    {
                        MessageBox.Show("This mass entry name already exists. ", "Warning...", MessageBoxButtons.OK);
                        txtEditMassEntryName.Text = lueQualityControlMassEntryName.Text;
                    }
                    else
                    {
                        SampleQualityControlMassEntryInfo result = new SampleQualityControlMassEntryInfo();
                        result.SampleQualityControlMassEntryID = qualityControlID;
                        result.SampleQualityControlMassEntryName = txtEditMassEntryName.Text;
                        result.DataSourceTypeID = (Int32)lueDataSourceTypeNew_QualityControl.EditValue;
                        result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        ErrorInfo outPut = AIMManager.SaveSampleQualityControlMassEntry(ref result);

                        btnRefresh.PerformClick();
                        DataBindQualityControlMassEntry();
                        lueQualityControlMassEntryName.EditValue = qualityControlID;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSaveMassEntry_Click(Object sender, EventArgs e)
        {
            try
            {
                if (txtQualityControlMassEntryNameNew.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Mass Entry List Name.");
                    txtQualityControlMassEntryNameNew.Focus();
                    return;
                }

                if ((Int32)lueDataSourceTypeNew_QualityControl.EditValue == 0)
                {
                    MessageBox.Show("Please select a Data Source.");
                    lueDataSourceTypeNew_QualityControl.Focus();
                    lueDataSourceTypeNew_QualityControl.ShowPopup();
                    return;
                }

                SampleQualityControlMassEntryInfo mass = new SampleQualityControlMassEntryInfo();
                mass.SampleQualityControlMassEntryID = 0;
                mass.SampleQualityControlMassEntryName = txtQualityControlMassEntryNameNew.Text;
                mass.DataSourceTypeID = (Int32)lueDataSourceTypeNew_QualityControl.EditValue;
                mass.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveSampleQualityControlMassEntry(ref mass);

                if (_saveQualityControlAs == true)
                {
                    SampleQualityControlMassEntryDetailInfo detail = new SampleQualityControlMassEntryDetailInfo();
                    List<SampleQualityControlMassEntryDetailInfo> copyResult = new List<SampleQualityControlMassEntryDetailInfo>();
                    copyResult = AIMManager.GetSampleQualityControlMassEntryDetail(_saveAsCopyQualityControlID);
                    foreach (var item in copyResult)
                    {
                        detail.SampleQualityControlMassEntryDetailID = 0;
                        detail.SampleQualityControlMassEntryID = outPut.RecordID;
                        detail.SampleQualityParameterID = item.SampleQualityParameterID;
                        detail.SortOrder = item.SortOrder;
                        detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        detail.UpdateStamp = DateTime.Now;
                        ErrorInfo outPutResult = AIMManager.SaveSampleQualityControlMassEntryDetail(ref detail);
                        DataBindQualityControlMassEntryDetail();
                    }
                    _saveQualityControlAs = false;
                    panelExistingQualityControl.Visible = true;
                    panelNewQualityControl.Visible = false;

                }

                SampleQualityControlMassEntryDetailInfo clearResults = new SampleQualityControlMassEntryDetailInfo();
                grdQualityControl.DataSource = clearResults;

                DataBindQualityControlMassEntry();
                _qualityControlID = outPut.RecordID;
                DataBindLueParameter();

                lueDataSourceTypeNew_QualityControl.EditValue = 0;

                txtQualityControlMassEntryNameNew.ReadOnly = true;
                lueDataSourceTypeNew_QualityControl.ReadOnly = true;

                lueQualityControlMassEntryName.EditValue = _qualityControlID;
                lueParameter.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelSave_Click(Object sender, EventArgs e)
        {
            txtQualityControlMassEntryNameNew.Text = "";
            panelNewQualityControl.Visible = false;
            if (_saveQualityControlAs == true)
            {
                panelExistingQualityControl.Visible = true;
            }
        }

        //4/19/22
        private void grdQualityControlView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                ErrorInfo rtn = new ErrorInfo();
                SampleQualityControlMassEntryDetailInfo mass = new SampleQualityControlMassEntryDetailInfo();
                mass = (SampleQualityControlMassEntryDetailInfo)grdQualityControlView.GetFocusedRow();

                //Check to make sure sort order# isn't already assigned                
                rtn = AIMManager.GetQualityControlMassEntryDetailSortOrder(mass.SampleQualityControlMassEntryID, mass.SortOrder);
                if (rtn.ErrorNumber == 0)
                {
                    mass.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    mass.UpdateStamp = DateTime.Now;
                    AIMManager.SaveSampleQualityControlMassEntryDetail(ref mass);
                }
                else
                {
                    MessageBox.Show("This sort number is already assigned.");
                }
                DataBindQualityControlMassEntryDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/19/22        
        private void hyperlinkLabelParam_Click(object sender, EventArgs e)
        {
            //there is a problem with the TabOrder form staying on "top" of the AnalyzerMaintenance form.
            //made this hyperlink invisible for now
            try
            {
                AIMGlobal.open_form(analyzerMaintenance = new Analyzer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region TEST TYPE -----------------------------------------------------------------------------------------------
        //8/10/23
        private void DataBindTestType()
        {
            _testTypeHeader = AIMManager.GetSampleTestType();
            _testTypeHeader.Insert
                (
                    0,
                    new SampleTestTypeInfo
                    {
                        SampleTestTypeID = 0,
                        SampleTestTypeName = "--Select--"
                    }
                );
            lueTestType.Properties.DataSource = _testTypeHeader;
            XtraEditors.LoadLookUpEdit(ref lueTestType, "SampleTestTypeID", "SampleTestTypeName");
            lueTestType.Properties.Columns[1].Caption = "Test Type Name";

            SampleTestTypeDetailInfo clearResults = new SampleTestTypeDetailInfo();
            grdTestType.DataSource = clearResults;
            gbAddDataSource.Visible = false;
            grdTestType.Visible = false;
        }
        private void DataBindTestTypeDetail()
        {
            _testTypeDetail = AIMManager.GetSampleTestTypeDetail(_testTypeID, _testTypeStatus);
            grdTestType.DataSource = _testTypeDetail;
        }
        private void DataBindLueTestTypeDatasource()
        {
            List<DataSourceTypeInfo> datasource = new List<DataSourceTypeInfo>();
            datasource = AIMManager.GetDataSourceType(true);
            datasource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );
            lueTestTypeDatasource.Properties.DataSource = datasource;
            XtraEditors.LoadLookUpEdit(ref lueTestTypeDatasource, "DataSourceTypeID", "Name", "TypeDisplay");
        }
        private void btnTestTypeAddNew_Click(object sender, EventArgs e)
        {
            panelExistingTestType.Visible = false;
            panelExistingTestType.Location = new Point(479, 157);
            panelExistingTestType.Size = new Size(407, 8);

            panelNewTestType.Visible = true;
            panelNewTestType.Location = new Point(9, 57);
            panelNewTestType.Size = new Size(877, 69);

            gbAddDataSource.Visible = false;
            grdTestType.Visible = false;
            _saveTestTypeAs = false;
            txtSampleTestTypeNameNew.Text = "";
            txtSampleTestTypeNameNew.ReadOnly = false;
            txtSampleTestTypeNameNew.Focus();
        }
        private void btnCancelSaveTestType_Click(object sender, EventArgs e)
        {
            txtSampleTestTypeNameNew.Text = "";
            panelNewTestType.Visible = false;
            if (_saveTestTypeAs == true)
            {
                panelExistingTestType.Visible = true;
            }
        }
        private void btnSaveTestType_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSampleTestTypeNameNew.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Test Type Name.");
                    txtSampleTestTypeNameNew.Focus();
                    return;
                }

                //check to see if name already exists (use new name to search template table)
                List<SampleTestTypeInfo> testType = new List<SampleTestTypeInfo>();
                testType = AIMManager.GetSampleTestType(txtSampleTestTypeNameNew.Text.ToString());
                if (testType == null)
                {
                }
                else if (testType.Count > 0)
                {
                    MessageBox.Show("This test type name already exists. ", "Warning...", MessageBoxButtons.OK);
                    txtEditTestTypeName.Text = lueTestType.Text;
                    return;
                }

                SampleTestTypeInfo type = new SampleTestTypeInfo();
                type.SampleTestTypeID = 0;
                type.SampleTestTypeName = txtSampleTestTypeNameNew.Text;
                type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveSampleTestType(ref type);

                if (_saveTestTypeAs == true)
                {
                    SampleTestTypeDetailInfo detail = new SampleTestTypeDetailInfo();
                    List<SampleTestTypeDetailInfo> copyResult = new List<SampleTestTypeDetailInfo>();
                    AIMManager.GetSampleTestTypeDetail(_saveAsCopyTestTypeID, true);
                    foreach (var item in copyResult)
                    {
                        detail.SampleTestTypeDetailID = 0;
                        detail.SampleTestTypeID = outPut.RecordID;
                        detail.DataSourceTypeID = item.DataSourceTypeID;
                        detail.SortOrder = item.SortOrder;
                        detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        detail.UpdateStamp = DateTime.Now;
                        ErrorInfo outPutResult = AIMManager.SaveSampleTestTypeDetail(ref detail);
                        DataBindTestTypeDetail();
                    }
                    _saveTestTypeAs = false;
                    panelExistingTestType.Visible = true;
                    panelNewTestType.Visible = false;
                }

                SampleTestTypeDetailInfo clearResults = new SampleTestTypeDetailInfo();
                grdTestType.DataSource = clearResults;

                DataBindTestType();
                _testTypeID = outPut.RecordID;
                txtSampleTestTypeNameNew.Text = "";

                DataBindLueTestTypeDatasource();

                //10/3/23
                DataBindlueTestType_Group();

                panelNewTestType.Visible = false;

                panelExistingTestType.Visible = true;
                panelExistingTestType.Location = new Point(9, 57);
                panelExistingTestType.Size = new Size(877, 69);


                txtSampleTestTypeNameNew.ReadOnly = true;
                lueTestType.EditValue = _testTypeID;
                lueTestTypeDatasource.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_TestType_Click(object sender, EventArgs e)
        {
            String name = "";
            try
            {
                if (panelExistingTestType.Visible == true)
                {
                    if ((Int32)lueTestType.EditValue == 0)
                    {
                        MessageBox.Show("Please select Test Type to delete.");
                        lueTestType.Focus();
                        return;
                    }
                    name = lueTestType.Text;
                }
                else
                {
                    name = txtSampleTestTypeNameNew.Text;
                }
                DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    Int32 ID = 0;
                    ID = (Int32)lueTestType.EditValue;
                    AIMManager.DeleteSampleTestType(ID);

                    DataBindTestType();
                    DataBindTestTypeDetail();
                    txtEditTestTypeName.Text = String.Empty;
                    txtSampleTestTypeNameNew.Text = String.Empty;
                    lueTestTypeDatasource.EditValue = 0;

                    gbAddDataSource.Visible = false;
                    grdTestType.Visible = false;

                    panelNewTestType.Visible = false;
                    panelNewTestType.Location = new Point(479, 135);
                    panelNewTestType.Size = new Size(407, 8);

                    panelExistingTestType.Visible = true;
                    panelExistingTestType.Location = new Point(9, 57);
                    panelExistingTestType.Size = new Size(877, 69);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTestTypeSaveAs_Click(object sender, EventArgs e)
        {
            _saveTestTypeAs = true;
            _saveAsCopyTestTypeID = _testTypeID;
            panelExistingTestType.Visible = false;
            panelNewTestType.Visible = true;
        }
        private void btnTestTypeExisting_Click(object sender, EventArgs e)
        {
            panelNewTestType.Visible = false;
            panelNewTestType.Location = new Point(479, 135);
            panelNewTestType.Size = new Size(407, 8);

            panelExistingTestType.Visible = true;
            panelExistingTestType.Location = new Point(9, 57);
            panelExistingTestType.Size = new Size(877, 69);

            lueTestType.ShowPopup();

            if ((Int32)lueTestType.EditValue != 0)
            {
                gbAddDataSource.Visible = true;
                grdTestType.Visible = true;
                _testTypeID = (Int32)lueTestType.EditValue;
                DataBindLueTestTypeDatasource();
            }
        }
        private void btnTestTypeDelete_Click(object sender, EventArgs e)
        {
            String name = "";
            try
            {
                if (panelExistingTestType.Visible == true)
                {
                    if ((Int32)lueTestType.EditValue == 0)
                    {
                        MessageBox.Show("Please select Test Type to delete.");
                        lueTestType.Focus();
                        lueTestType.ShowPopup();
                        return;
                    }
                    name = lueTestType.Text;
                }
                else
                {
                    name = txtSampleTestTypeNameNew.Text;
                }

                //If TestType is tied to a sample, do not allow delete
                ////List<SampleTestTypeInfo> type = new List<SampleTestTypeInfo>();
                ////type = AIMManager.GetSampleTestType((Int32)lueTestType.EditValue);
                ////if (type.Count > 0)
                ////{
                ////    MessageBox.Show("Test Type cannot be deleted. There are samples connected with this test type.","Cannot Delete!");
                ////    return;
                ////}
                String msg;
                SampleInfo sample = new SampleInfo();
                sample = AIMManager.GetSampleUsingTestType((Int32)lueTestType.EditValue);
                if (sample != null)
                {
                    msg = "This Test Type cannot be deleted because it is linked to a Sample.";
                    msg = msg + Environment.NewLine + Environment.NewLine;
                    msg = msg + "Instead, you may inactivate all the datasourcs for this Test Type by clicking the Active icon for each datasource.";
                    MessageBox.Show(msg, "Information...", MessageBoxButtons.OK);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    Int32 ID = 0;
                    ID = (Int32)lueTestType.EditValue;
                    AIMManager.DeleteSampleTestType(ID);

                    DataBindTestType();
                    DataBindTestTypeDetail();
                    txtEditTestTypeName.Text = String.Empty;
                    txtSampleTestTypeNameNew.Text = String.Empty;
                    lueTestTypeDatasource.EditValue = 0;

                    gbAddDataSource.Visible = false;
                    grdTestType.Visible = false;

                    panelNewTestType.Visible = false;
                    panelNewTestType.Location = new Point(479, 135);
                    panelNewTestType.Size = new Size(407, 8);

                    panelExistingTestType.Visible = true;
                    panelExistingTestType.Location = new Point(9, 57);
                    panelExistingTestType.Size = new Size(877, 69);

                    //10/3/23 - delete from group detail, if test-type exists there
                    AIMManager.DeleteTestTypeGroupDetail_TestTypeID(ID);
                    DataBindlueTestType_Group();
                    DataBindTestTypeGroupDetail();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueTestType_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueTestType.EditValue != 0)
            {
                gbAddDataSource.Visible = true;
                grdTestType.Visible = true;
                _testTypeID = (Int32)lueTestType.EditValue;

                DataBindLueTestTypeDatasource();
                DataBindTestTypeDetail();

            }
        }
        private void txtEditTestTypeName_Validated(object sender, EventArgs e)
        {
            Int32 testTypeID = 0;
            try
            {
                if ((txtEditTestTypeName.Text != lueTestType.Text))
                {

                    //get current test type id							
                    testTypeID = (Int32)lueTestType.EditValue;

                    //check to see if name already exists (use new name to search template table)
                    List<SampleTestTypeInfo> type = new List<SampleTestTypeInfo>();
                    type = AIMManager.GetSampleTestType(txtEditTestTypeName.Text.ToString());
                    if (type == null)
                    {
                    }
                    else if (type.Count > 0)
                    {
                        MessageBox.Show("This Test Type name already exists. ", "Warning...", MessageBoxButtons.OK);
                        txtEditTestTypeName.Text = lueTestType.Text;
                    }
                    else
                    {
                        SampleTestTypeInfo result = new SampleTestTypeInfo();
                        result.SampleTestTypeID = testTypeID;
                        result.SampleTestTypeName = txtEditTestTypeName.Text;
                        result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        ErrorInfo outPut = AIMManager.SaveSampleTestType(ref result);

                        btnRefresh.PerformClick();
                        DataBindTestType();
                        lueTestType.EditValue = testTypeID;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTestTypeAddDatasource_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                btnTestTypeAddDatasource.BackColor = SystemColors.Control;

                if ((Int32)lueTestTypeDatasource.EditValue == 0)
                {
                    MessageBox.Show("Please select Data Source.");
                    lueTestTypeDatasource.Focus();
                    lueTestTypeDatasource.ShowPopup();
                    return;
                }

                //get max SortOrder# from _testTypeDetail	                
                ErrorInfo maxSortNumber = AIMManager.GetSampleTestTypeDetailMaxSortNo(_testTypeID);
                SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
                type.SampleTestTypeDetailID = 0;
                type.SampleTestTypeID = _testTypeID;
                type.DataSourceTypeID = (Int32)lueTestTypeDatasource.EditValue;
                type.SortOrder = maxSortNumber.RecordID + 1;
                type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                type.UpdateStamp = DateTime.Now;
                type.IsActive = true;
                ErrorInfo outPut = AIMManager.SaveSampleTestTypeDetail(ref type);
                DataBindTestTypeDetail();

                lueTestTypeDatasource.Focus();
                lueTestTypeDatasource.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdTestTypeView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                ErrorInfo rtn = new ErrorInfo();
                SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
                type = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();

                //Check to make sure sort order# isn't already assigned                
                rtn = AIMManager.GetSampleTestTypeDetailSortOrder(type.SampleTestTypeID, type.SortOrder);
                if (rtn.ErrorNumber == 0)
                {
                    type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    type.UpdateStamp = DateTime.Now;
                    AIMManager.SaveSampleTestTypeDetail(ref type);
                }
                else
                {
                    MessageBox.Show("This sort number is already assigned.");
                }
                DataBindTestTypeDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repItemDeleteTestTypeDetail_Click(object sender, EventArgs e)
        {
            try
            {
                String msg;
                DialogResult dialogResult;
                SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
                type = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();
                type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                //make sure this test-type/datasource is not assiged to a sample.
                SampleInfo sample = new SampleInfo();
                sample = AIMManager.GetSampleUsingTestType(type.SampleTestTypeID);
                if (sample != null)
                {
                    msg = "This datasource cannot be deleted because the Test Type is linked to a Sample.";
                    msg = msg + Environment.NewLine + Environment.NewLine;
                    msg = msg + "Instead, you may inactivate the datasource by clicking the Active icon.";
                    MessageBox.Show(msg, "Information...", MessageBoxButtons.OK);
                    return;
                }
                dialogResult = MessageBox.Show("Are you sure you want to delete this datasource from the Test Type?", "Warning...", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    AIMManager.DeleteSampleTestTypeDetail(type.SampleTestTypeDetailID);
                    DataBindTestTypeDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdTestTypeView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == colImageTestType)
            {
                //get value in IsActive column for this row
                if (((SampleTestTypeDetailInfo)(e.Row)).IsActive)
                {
                    e.Value = Properties.Resources.delete;
                }
                else
                {
                    e.Value = Properties.Resources.Undo_icon;
                }
            }
        }
        private void repItemPictureEditTestType_Click(object sender, EventArgs e)
        {
            try
            {
                SampleTestTypeDetailInfo testType = new SampleTestTypeDetailInfo();
                testType = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();
                testType.IsActive = !testType.IsActive;
                AIMManager.SaveTestTypeDetailIsActive(testType.SampleTestTypeDetailID, testType.IsActive, userGlobal.CFCLoginUser.CFCUserID);
                DataBindTestTypeDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetTestTypeStatus()
        {
            if (rbActiveTestType.Checked)
                _testTypeStatus = true;
            if (rbInActiveTestType.Checked)
                _testTypeStatus = false;
        }
        private void rbActiveTestType_CheckedChanged(object sender, EventArgs e)
        {
            SetTestTypeStatus();
            if (rbActiveTestType.Checked == true)
            {
                DataBindTestTypeDetail();
            }
        }
        private void rbInActiveTestType_CheckedChanged(object sender, EventArgs e)
        {
            SetTestTypeStatus();
            if (rbInActiveTestType.Checked == true)
            {
                DataBindTestTypeDetail();
            }
        }
        #endregion

        #region PAGE ORDER --------------------------------------------------------------------------------------
        //8/18/23
        private void DataBindPageOrder()
        {
            _pageOrder = AIMManager.GetSampleTabControlPage();
            grdSampleTabControlPage.DataSource = _pageOrder;
        }

        private void btnPageOrderUp_Click(object sender, EventArgs e)
        {
            UpdateSortOrder("UP");
        }

        private void btnPageOrderDown_Click(object sender, EventArgs e)
        {
            UpdateSortOrder("DOWN");
        }

        private void UpdateSortOrder(String direction)
        {
            Int32 index;
            Int32 pageID;
            Int32 orderID;
            Int32 nextpageID;
            Int32 nextorderID;
            Int32 cfcUserID = userGlobal.CFCLoginUser.CFCUserID;
            SampleTabControlPageInfo row = new SampleTabControlPageInfo();
            List<SampleTabControlPageInfo> grid = new List<SampleTabControlPageInfo>();


            //get current row's ID and sort order                 
            row = (SampleTabControlPageInfo)grdSampleTabControlPageView.GetFocusedRow();
            pageID = row.SampleTabControlPageID;
            orderID = row.SortOrder;

            //move cursor down
            if (direction == "UP")
            {
                grdSampleTabControlPageView.MovePrev();
            }
            else
            {
                grdSampleTabControlPageView.MoveNext();
            }

            grdSampleTabControlPageView.GridControl.Focus();

            //get next row's ID and sort order                 
            row = (SampleTabControlPageInfo)grdSampleTabControlPageView.GetFocusedRow();
            nextpageID = row.SampleTabControlPageID;
            nextorderID = row.SortOrder;

            //update sort order
            AIMManager.SaveSampleTabControlPage(pageID, nextorderID, cfcUserID);
            AIMManager.SaveSampleTabControlPage(nextpageID, orderID, cfcUserID);

            //get current row handle
            index = grdSampleTabControlPageView.FocusedRowHandle;

            DataBindPageOrder();

            //set row focus
            grdSampleTabControlPageView.FocusedRowHandle = index;

        }




        #endregion

        #region TEST TYPE GROUPS --------------------------------------------------------------------------------------
        private void btnAddNewTestTypeGroup_Click(object sender, EventArgs e)
        {
            gbAddTestTypeGroup.Location = new Point(7, 9);
            gbTestTypeGroupCode.Visible = false;
            gbAddTestTypeGroup.Visible = true;
            lueTestTypeGroup.EditValue = 0;
            txtTestTypeGroupCode.Focus();
        }
        #endregion

        private void btnDeleteTestTypeGroup_Click(object sender, EventArgs e)
        {
            String name = string.Empty;
            Int32 groupID = 0;

            if ((Int32)lueTestTypeGroup.EditValue == 0)
            {
                MessageBox.Show("Please select a Group to delete.");
                lueTestTypeGroup.ShowPopup();
                return;
            }
            name = lueTestTypeGroup.Text;
            DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {

                groupID = (Int32)lueTestTypeGroup.EditValue;

                //Delete from AIMTestTypeGroup table
                AIMManager.DeleteTestTypeGroup(groupID);               

                //delete from cfcuser (set GroupID = 0)
                AIMManager.UpdateCFCUsersSampleTestTypeGroup(groupID, 0);    

                DataBindlueTestTypeGroup();

            }
        }

        private void DataBindlueTestTypeGroup()
        {
            List<SampleTestTypeGroupInfo> group = new List<SampleTestTypeGroupInfo>();
            group = AIMManager.GetTestTypeGroup(true);                  
            group.Insert
                (
                0,
                new SampleTestTypeGroupInfo
                {
                    SampleTestTypeGroupID = 0,
                    SampleTestTypeGroupCode = "--Select--"

                }
                );
            lueTestTypeGroup.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueTestTypeGroup, "SampleTestTypeGroupID", "SampleTestTypeGroupCode");
            if (group.Count == 2)
            {
                //selecting value if there is only one
                lueTestTypeGroup.EditValue = group[1].SampleTestTypeGroupID;
            }
            else
            {
                lueTestTypeGroup.EditValue = 0;
            }


        }

        private void btnAddTestType_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)lueTestTypeGroup.EditValue == 0)
                {
                    MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
                    lueTestTypeGroup.ShowPopup();
                    return;
                }

                if ((Int32)lueTestType_Group.EditValue == 0)
                {
                    MessageBox.Show("Please select a Test Type. ", "Warning...", MessageBoxButtons.OK);
                    lueTestType_Group.ShowPopup();
                    return;
                }

                SampleTestTypeGroupDetailInfo group = new SampleTestTypeGroupDetailInfo();
                group.SampleTestTypeGroupDetailID = 0;
                group.SampleTestTypeGroupID = (Int32)lueTestTypeGroup.EditValue;
                group.SampleTestTypeID = (Int32)lueTestType_Group.EditValue;
                group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                group.IsActive = true;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveTestTypeGroupDetail(ref group);                         
                if (error.ErrorNumber > 0)
                {
                    MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
                    return;
                }

                //filter grid for group code		
                DataBindTestTypeGroupDetail();

                lueTestType_Group.EditValue = 0;
                lueTestType_Group.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNewTestTypeGroupCode_Click(object sender, EventArgs e)
        {
            if (txtTestTypeGroupCode.Text == "")
            {
                MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
                txtTestTypeGroupCode.Focus();
                return;
            }

            //save group code
            SampleTestTypeGroupInfo group = new SampleTestTypeGroupInfo();
            group.SampleTestTypeGroupID = 0;
            group.SampleTestTypeGroupCode = txtTestTypeGroupCode.Text;
            group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveTestTypeGroup(ref group);                               
            if (error.ErrorNumber > 0)
            {
                MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
                txtTestTypeGroupCode.Text = String.Empty;
                return;
            }

            DataBindlueTestTypeGroup();                                                   
            lueTestTypeGroup.EditValue = error.RecordID;
            txtTestTypeGroupCode.Text = String.Empty;
            gbAddTestTypeGroup.Visible = false;
            gbTestTypeGroupCode.Visible = true;
            gbTestType.Visible = true;
        }

        private void btnCancelTestTypeGroup_Click(object sender, EventArgs e)
        {
            gbAddTestTypeGroup.Visible = false;
            gbTestTypeGroupCode.Visible = true;
            gbTestType.Visible = true;
        }
                
        private void lueTestTypeGroup_EditValueChanged(object sender, EventArgs e)
        {
            DataBindTestTypeGroupDetail();
        }

        private void DataBindTestTypeGroupDetail()
        {
            List<SampleTestTypeGroupDetailInfo> group = new List<SampleTestTypeGroupDetailInfo>();

            if (lueTestTypeGroup.EditValue != null)
            {
                group = AIMManager.GetTestTypeGroupDetail_GroupID((Int32)lueTestTypeGroup.EditValue);          
                Int32 currentRowNum = 0;
                if (grdTestTypeGroupView != null)
                {
                    Int32.TryParse(grdTestTypeGroupView.FocusedRowHandle.ToString(), out currentRowNum);
                }
                grdTestTypeGroup.DataSource = group;
                grdTestTypeGroupView.FocusedRowHandle = currentRowNum;
            }
        }

        private void lueTestTypeGroup_Enter(object sender, EventArgs e)
        {
            lueTestTypeGroup.SelectAll();
        }

        private void lueTestTypeGroup_Click(object sender, EventArgs e)
        {
            lueTestTypeGroup.SelectAll();
        }

        private void DataBindlueTestType_Group()
        {
            List<SampleTestTypeInfo> testType = new List<SampleTestTypeInfo>();
            testType = AIMManager.GetSampleTestType();
            testType.Insert
            (
                0,
                new SampleTestTypeInfo
                {
                    SampleTestTypeID = 0,
                    SampleTestTypeName = "--Select--"
                    
                }
            );
            lueTestType_Group.Properties.DataSource = testType;
            XtraEditors.LoadLookUpEdit(ref lueTestType_Group, "SampleTestTypeID", "SampleTestTypeName");

            if (testType.Count == 2)
            {
                //selecting value if there is only one
                lueTestType_Group.EditValue = testType[1].SampleTestTypeID;
            }
            else
            {
                lueTestType_Group.EditValue = testType;
            }

        }

        private void btnPreviewTestTypeGroup_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdTestTypeGroup, "Test Type Groups");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repDeleteTestTypeGroupDetail_Click(object sender, EventArgs e)
        {
            try
            {
                SampleTestTypeGroupDetailInfo group = new SampleTestTypeGroupDetailInfo();
                group = (SampleTestTypeGroupDetailInfo)grdTestTypeGroupView.GetFocusedRow();
                group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteTestTypeGroupDetail_DetailID(group.SampleTestTypeGroupDetailID);            
                DataBindTestTypeGroupDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdTestTypeGroupView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            Boolean active = true;

            active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
            if (active == false)
            {
                e.Appearance.BackColor = Color.PaleGoldenrod;
                e.HighPriority = true;
            }
        }

        private void grdTestTypeGroupView_ShownEditor(object sender, EventArgs e)
        {
            //if test type is inactive, make row readonly and change background color
            List<SampleTestTypeGroupDetailInfo> list = new List<SampleTestTypeGroupDetailInfo>();
            GridView view = (GridView)sender;
        }

        private void grdTestTypeGroup_Click(object sender, EventArgs e)
        {

        }
    }
}