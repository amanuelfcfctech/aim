using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIM.Maintenance;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Group;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.CodeParser;
using DevExpress.Utils.About;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors;
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
        Boolean _IsGroupTemplate = false;                                                       //Task1061 DB251022
        Analyzer analyzerMaintenance;                                                           //4/19/22       

        //--------------------------------------------------------------------------------------------------------
        public TabOrder()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }
        private void TabOrder_Load(object sender, EventArgs e)
        {
            try
            {


                //10/6/23 - these 2 lines can be commented when TestType in Aim Maintenance has been there for a couple months with no problems.
                tabControl1.TabPages.Remove(pageTestType);
                tabControl1.TabPages.Remove(pageTestTypeGroups);

                tcSampleTemplateResultGroup.Location = new Point(4, 150);            //Task1061 DB251104

                DataBindTabOrder();
                DataBindAlternateName();
                //Task1054 MW250505 _isLoading = false;

                //6/29/21 - commented, changed label form
                //6/25/20
                //DataBindLabels();

                //3/24/25 - commented, changed label tables
                //6/29/21
                //DataBindLabelFields();
                //DataBindProdLabelFields();                      //5/2/23
                DataBindlueLabelDropdowns();
                PopulateLabelFormat((Int32)MaterialTypeEnum.Ingredient);
                PopulateLabelFormat((Int32)MaterialTypeEnum.Product);

                _IsGroupTemplate = false;           //Have to discuss how we want to handle this, is it a system setting? We default to single for now

                DataBindTemplate();
                DataBindQualityControlMassEntry();
                DataBindLueParameter();
                DataBindShowNameOrBoth();                       //6/18/20                
                DataBindTestType();                             //8/10/23                
                DataBindPageOrder();                            //8/18/23                
                DataBindlueTestTypeGroup();                     //10/3/23
                DataBindlueTestType_Group();                    //10/3/23
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

                        gbGroupOptions.Visible = false;                 //Task1061 DB251024
                        if (_IsGroupTemplate == true)                   //Task1061 DB251024
                        {
                            gbGroupOptions.Visible = true;              //Task1061 DB251024
                        }
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

                _isLoading = false;                             //Task1054 MW250505
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
                    dr = MessageBox.Show("Changes were made to the labels but not saved. Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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

            //3/24/25
            //6/29/21 - commented, changed label form
            //DataBindLabels();
            PopulateLabelFormat((Int32)MaterialTypeEnum.Ingredient);
            PopulateLabelFormat((Int32)MaterialTypeEnum.Product);

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

            gbGroupOptions.Visible = false;                 //Task1061 DB251024
            if (_IsGroupTemplate == true)                   //Task1061 DB251024
            {
                gbGroupOptions.Visible = true;              //Task1061 DB251024
            }

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

        //3/24/25
        private void DataBindlueLabelDropdowns()
        {
            List<LabelInfo> labels = new List<LabelInfo>();
            //Task1141 DB251216 labels = AIMManager.GetLabel();
            labels = AIMManager.GetLabelIsVisibleToPrinterLabel();  //Task1141 DB251216 
            labels.Insert
            (
                0,
                new LabelInfo
                {
                    LabelID = 0,
                    UserControlName = "--Select--"
                }
            );

            populateLineDropdowns(lueLabelLine1, labels);
            populateLineDropdowns(lueLabelLine2, labels);
            populateLineDropdowns(lueLabelLine3, labels);
            populateLineDropdowns(lueLabelLine4, labels);
            populateLineDropdowns(lueLabelLine5, labels);

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

            populateYesNoDropdown(lueYesNo1, yesNo);
            populateYesNoDropdown(lueYesNo2, yesNo);
            populateYesNoDropdown(lueYesNo3, yesNo);
            populateYesNoDropdown(lueYesNo4, yesNo);
            populateYesNoDropdown(lueYesNo5, yesNo);

            lueYesNo1.EditValue = (Int32)EnumYesNo.No;
            lueYesNo2.EditValue = (Int32)EnumYesNo.No;
            lueYesNo3.EditValue = (Int32)EnumYesNo.No;
            lueYesNo4.EditValue = (Int32)EnumYesNo.No;
            lueYesNo5.EditValue = (Int32)EnumYesNo.No;

            populateYesNoDropdown(lueProdYesNo1, yesNo);
            populateYesNoDropdown(lueProdYesNo2, yesNo);
            populateYesNoDropdown(lueProdYesNo3, yesNo);
            populateYesNoDropdown(lueProdYesNo4, yesNo);
            populateYesNoDropdown(lueProdYesNo5, yesNo);

            lueProdYesNo1.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo2.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo3.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo4.EditValue = (Int32)EnumYesNo.No;
            lueProdYesNo5.EditValue = (Int32)EnumYesNo.No;

            //---------------------------------------------------------------------------           
            //Task1058 MW250516
            List<FontSizeInfo> fontSize = new List<FontSizeInfo>();

            for (Int32 i = 0; i < 6; i++)
            {
                fontSize.Add(new FontSizeInfo
                {
                    FontSizeID = i,
                    FontSize = i + 7
                });
            }

            populateFontSizeDropdown(lueFontSize1, fontSize);
            populateFontSizeDropdown(lueFontSize2, fontSize);
            populateFontSizeDropdown(lueFontSize3, fontSize);
            populateFontSizeDropdown(lueFontSize4, fontSize);
            populateFontSizeDropdown(lueFontSize5, fontSize);
            populateFontSizeDropdown(lueProductFontSize1, fontSize);
            populateFontSizeDropdown(lueProductFontSize2, fontSize);
            populateFontSizeDropdown(lueProductFontSize3, fontSize);
            populateFontSizeDropdown(lueProductFontSize4, fontSize);
            populateFontSizeDropdown(lueProductFontSize5, fontSize);

            //Task1058 MW250516
            //---------------------------------------------------------------------------           

            CheckLabelSize();

            lblProdLine4.Visible = true;
            lueProdLabelLine4.Visible = true;
            lueProdYesNo4.Visible = true;

            lblProdLine5.Visible = true;
            lueProdLabelLine5.Visible = true;
            lueProdYesNo5.Visible = true;

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

        //3/24/25
        //5/3/23
        //private void populateLineDropdowns(DevExpress.XtraEditors.LookUpEdit lue, List<LabelsInfo> labels)
        private void populateLineDropdowns(DevExpress.XtraEditors.LookUpEdit lue, List<LabelInfo> labels)
        {
            lue.Properties.DataSource = labels;

            //3/24/25
            //XtraEditors.LoadLookUpEdit(ref lue, "LabelsID", "UserControlName");
            //lue.Properties.Columns[13].Caption = "Field Name";
            //cursor change 08/25/2026 XtraEditors.LoadLookUpEdit(ref lue, "LabelID", "UserControlName");
            XtraEditors.LoadLookUpEdit(ref lue, "LabelID", "FieldNameDisplay");                 //cursor change 08/25/2026

        }

        //3/24/25
        private void PopulateLabelFormat(Int32 materialTypeID)
        {
            Int32 intNo = 2;
            //Task1110 DB251014 Int32 fontSizeID = 4;
            Int32 fontSizeID = 0;       //Task1110 DB251014         default this to 0 ==> 7 font size instead of 12
            Int32 boldID = 0;
            //---------------------------------------------------------------------------           
            //Task1058 MW250520
            List<FontSizeInfo> fontSize = new List<FontSizeInfo>();

            for (Int32 i = 0; i < 6; i++)
            {
                fontSize.Add(new FontSizeInfo
                {
                    FontSizeID = i,
                    FontSize = i + 7
                });
            }
            //Task1058 MW250520
            //---------------------------------------------------------------------------           

            if (materialTypeID == (Int32)MaterialTypeEnum.Ingredient)
            {
                txtLineID1.Text = "0";
                txtLineID2.Text = "0";
                txtLineID3.Text = "0";
                txtLineID4.Text = "0";
                txtLineID5.Text = "0";
                lueLabelLine1.EditValue = 0;
                lueLabelLine2.EditValue = 0;
                lueLabelLine3.EditValue = 0;
                lueLabelLine4.EditValue = 0;
                lueLabelLine5.EditValue = 0;
                lueYesNo1.EditValue = intNo;
                lueYesNo2.EditValue = intNo;
                lueYesNo3.EditValue = intNo;
                lueYesNo4.EditValue = intNo;
                lueYesNo5.EditValue = intNo;
                lueFontSize1.EditValue = fontSizeID;                    //Task1056 MW250516
                lueFontSize2.EditValue = fontSizeID;                    //Task1056 MW250516
                lueFontSize3.EditValue = fontSizeID;                    //Task1056 MW250516
                lueFontSize4.EditValue = fontSizeID;                    //Task1056 MW250516
                lueFontSize5.EditValue = fontSizeID;                    //Task1056 MW250516
            }
            else
            {
                txtProdID1.Text = "0";
                txtProdID2.Text = "0";
                txtProdID3.Text = "0";
                txtProdID4.Text = "0";
                txtProdID5.Text = "0";

                lueProdLabelLine1.EditValue = 0;
                lueProdLabelLine2.EditValue = 0;
                lueProdLabelLine3.EditValue = 0;
                lueProdLabelLine4.EditValue = 0;
                lueProdLabelLine5.EditValue = 0;

                lueProdYesNo1.EditValue = intNo;
                lueProdYesNo2.EditValue = intNo;
                lueProdYesNo3.EditValue = intNo;
                lueProdYesNo4.EditValue = intNo;
                lueProdYesNo5.EditValue = intNo;
                lueProductFontSize1.EditValue = fontSizeID;                    //Task1056 MW250516
                lueProductFontSize2.EditValue = fontSizeID;                    //Task1056 MW250516
                lueProductFontSize3.EditValue = fontSizeID;                    //Task1056 MW250516
                lueProductFontSize4.EditValue = fontSizeID;                    //Task1056 MW250516
                lueProductFontSize5.EditValue = fontSizeID;                    //Task1056 MW250516
            }

            List<LabelFormatInfo> labels = new List<LabelFormatInfo>();
            labels = AIMManager.GetLabelFormat();
            if (labels != null)
            {
                foreach (var item in labels)
                {

                    if (materialTypeID == (Int32)MaterialTypeEnum.Ingredient)
                    {

                        if (item.MaterialTypeID == 1)
                        {
                            if (item.OrderNumber == 1)
                            {
                                setDropdown(fontSize, txtLineID1, lueLabelLine1, lueYesNo1, lueFontSize1, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 2)
                            {
                                setDropdown(fontSize, txtLineID2, lueLabelLine2, lueYesNo2, lueFontSize2, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 3)
                            {
                                setDropdown(fontSize, txtLineID3, lueLabelLine3, lueYesNo3, lueFontSize3, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 4)
                            {
                                setDropdown(fontSize, txtLineID4, lueLabelLine4, lueYesNo4, lueFontSize4, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 5)
                            {
                                setDropdown(fontSize, txtLineID5, lueLabelLine5, lueYesNo5, lueFontSize5, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }

                        }
                    }
                    else
                    {
                        if (item.MaterialTypeID == 2)
                        {
                            if (item.OrderNumber == 1)
                            {
                                setDropdown(fontSize, txtProdID1, lueProdLabelLine1, lueProdYesNo1, lueProductFontSize1, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 2)
                            {
                                setDropdown(fontSize, txtProdID2, lueProdLabelLine2, lueProdYesNo2, lueProductFontSize2, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 3)
                            {
                                setDropdown(fontSize, txtProdID3, lueProdLabelLine3, lueProdYesNo3, lueProductFontSize3, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 4)
                            {
                                setDropdown(fontSize, txtProdID4, lueProdLabelLine4, lueProdYesNo4, lueProductFontSize4, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                            else if (item.OrderNumber == 5)
                            {
                                setDropdown(fontSize, txtProdID5, lueProdLabelLine5, lueProdYesNo5, lueProductFontSize5, item.LabelFormatID, item.LabelID, item.IsBold, item.FontSize);
                            }
                        }
                    }
                }
            }
        }

        private void setDropdown(List<FontSizeInfo> fontSizes, TextBox txtLineID, LookUpEdit lueLabelLine, LookUpEdit lueYesNo, LookUpEdit lueFontSize, Int32 labelFormatID, Int32 labelID, Boolean isBold, Int32 fontSize)
        {
            Int32 fontSizeID = 4;
            Int32 boldID = 2;

            for (Int32 i = 0; i < fontSizes.Count; i++)
            {
                if (fontSizes[i].FontSize == fontSize)
                {
                    fontSizeID = i;
                    break;
                }
            }

            if (isBold == true)
            {
                boldID = 1;
            }

            txtLineID.Text = labelFormatID.ToString();
            lueLabelLine.EditValue = labelID;
            lueYesNo.EditValue = boldID;
            lueFontSize.EditValue = fontSizeID;
        }

        private void populateYesNoDropdown(DevExpress.XtraEditors.LookUpEdit lue, List<YesNoInfo> yesNo)
        {
            lue.Properties.DataSource = yesNo;
            XtraEditors.LoadLookUpEdit(ref lue, "YesNoID", "YesNoString");
            lue.Properties.Columns[1].Caption = "";
        }
        private void populateFontSizeDropdown(DevExpress.XtraEditors.LookUpEdit lue, List<FontSizeInfo> fontSizes)
        {
            lue.Properties.DataSource = fontSizes;
            XtraEditors.LoadLookUpEdit(ref lue, "FontSizeID", "FontSize");
            lue.Properties.Columns[1].Caption = "Font Size";
        }
        private void btnSaveLabels_Click(object sender, EventArgs e)
        {
            Int32 labelFormatID = 0;
            Int32 fontSize = 0;                                             //Task1058 MW250516
            Boolean yesno1 = false;
            Boolean yesno2 = false;
            Boolean yesno3 = false;
            Boolean yesno4 = false;
            Boolean yesno5 = false;


            //4/21/22
            if (_labelSize == 1)
            {
                //Task1085 DB250812 if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0)
                //Task1085 DB250812 {
                //Task1085 DB250812 MessageBox.Show("All three label fields must be selected.");
                //Task1085 DB250812 return;
                //Task1085 DB250812 }
                if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0)
                {
                    MessageBox.Show("Please select Yes or No to bolding.");
                    return;
                }
            }
            else
            {
                //Task1085 DB250812 if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0 || (Int32)lueLabelLine4.EditValue == 0 || (Int32)lueLabelLine5.EditValue == 0)
                //Task1085 DB250812 {
                //Task1085 DB250812 MessageBox.Show("All five label fields must be selected.");
                //Task1085 DB250812 return;
                //Task1085 DB250812 }
                if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0 || (Int32)lueYesNo4.EditValue == 0 || (Int32)lueYesNo5.EditValue == 0)
                {
                    MessageBox.Show("Please select Yes or No to bolding.");
                    return;
                }
            }

            if ((Int32)lueYesNo1.EditValue == (Int32)EnumYesNo.Yes)
            {
                yesno1 = true;
            }
            if ((Int32)lueYesNo2.EditValue == (Int32)EnumYesNo.Yes)
            {
                yesno2 = true;
            }
            if ((Int32)lueYesNo3.EditValue == (Int32)EnumYesNo.Yes)
            {
                yesno3 = true;
            }
            if ((Int32)lueYesNo4.EditValue == (Int32)EnumYesNo.Yes)
            {
                yesno4 = true;
            }
            if ((Int32)lueYesNo5.EditValue == (Int32)EnumYesNo.Yes)
            {
                yesno5 = true;
            }

            if ((Int32)lueLabelLine1.EditValue > 0)
            {
                Int32.TryParse(lueFontSize1.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID1.Text), (Int32)lueLabelLine1.EditValue, (Int32)MaterialTypeEnum.Ingredient, 1, yesno1, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtLineID1.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtLineID1.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueLabelLine2.EditValue > 0)
            {
                Int32.TryParse(lueFontSize2.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID2.Text), (Int32)lueLabelLine2.EditValue, (Int32)MaterialTypeEnum.Ingredient, 2, yesno2, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtLineID2.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtLineID2.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueLabelLine3.EditValue > 0)
            {
                Int32.TryParse(lueFontSize3.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID3.Text), (Int32)lueLabelLine3.EditValue, (Int32)MaterialTypeEnum.Ingredient, 3, yesno3, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtLineID3.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtLineID3.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueLabelLine4.EditValue > 0)
            {
                Int32.TryParse(lueFontSize4.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID4.Text), (Int32)lueLabelLine4.EditValue, (Int32)MaterialTypeEnum.Ingredient, 4, yesno4, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtLineID4.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtLineID4.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueLabelLine5.EditValue > 0)
            {
                //Task1110 DB251014 Int32.TryParse(lueFontSize5.Text.ToString(), out AIMGlobal.line5FontSize);           //Task1058 MW250516
                Int32.TryParse(lueFontSize5.Text.ToString(), out fontSize);                                              //Task1110 DB251014 
                //Task1110 DB251014  AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID5.Text), (Int32)lueLabelLine5.EditValue, (Int32)MaterialTypeEnum.Ingredient, 5, yesno5, AIMGlobal.line1FontSize, userGlobal.CFCLoginUser.CFCUserID);
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtLineID5.Text), (Int32)lueLabelLine5.EditValue, (Int32)MaterialTypeEnum.Ingredient, 5, yesno5, fontSize, userGlobal.CFCLoginUser.CFCUserID);       //Task1110 DB251014 
            }
            else if (txtLineID5.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtLineID5.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            PopulateLabelFormat((Int32)MaterialTypeEnum.Ingredient);

            _changesMade = false;
        }
        private void btnSaveProdLabels_Click(object sender, EventArgs e)        //5/2/23
        {
            Int32 labelFormatID = 0;              //Task1085 DB250813
            Int32 fontSize = 0;
            Boolean yesno1 = false;
            Boolean yesno2 = false;
            Boolean yesno3 = false;
            Boolean yesno4 = false;
            Boolean yesno5 = false;

            if ((Int32)lueProdYesNo1.EditValue == (Int32)EnumYesNo.Yes) { yesno1 = true; }
            if ((Int32)lueProdYesNo2.EditValue == (Int32)EnumYesNo.Yes) { yesno2 = true; }
            if ((Int32)lueProdYesNo3.EditValue == (Int32)EnumYesNo.Yes) { yesno3 = true; }
            if ((Int32)lueProdYesNo4.EditValue == (Int32)EnumYesNo.Yes) { yesno4 = true; }
            if ((Int32)lueProdYesNo5.EditValue == (Int32)EnumYesNo.Yes) { yesno5 = true; }

            if ((Int32)lueProdLabelLine1.EditValue > 0)
            {
                Int32.TryParse(lueProductFontSize1.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtProdID1.Text), (Int32)lueProdLabelLine1.EditValue, (Int32)MaterialTypeEnum.Product, 1, yesno1, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtProdID1.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtProdID1.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }


            if ((Int32)lueProdLabelLine2.EditValue > 0)
            {
                Int32.TryParse(lueProductFontSize2.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtProdID2.Text), (Int32)lueProdLabelLine2.EditValue, (Int32)MaterialTypeEnum.Product, 2, yesno2, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtProdID2.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtProdID2.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueProdLabelLine3.EditValue > 0)
            {
                Int32.TryParse(lueProductFontSize3.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtProdID3.Text), (Int32)lueProdLabelLine3.EditValue, (Int32)MaterialTypeEnum.Product, 3, yesno3, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtProdID3.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtProdID3.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueProdLabelLine4.EditValue > 0)
            {
                Int32.TryParse(lueProductFontSize4.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtProdID4.Text), (Int32)lueProdLabelLine4.EditValue, (Int32)MaterialTypeEnum.Product, 4, yesno4, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtProdID4.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtProdID4.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }

            if ((Int32)lueProdLabelLine5.EditValue > 0)
            {
                Int32.TryParse(lueProductFontSize5.Text.ToString(), out fontSize);                        //Task1058 MW250516
                AIMManager.SaveLabelFormat(Convert.ToInt32(txtProdID5.Text), (Int32)lueProdLabelLine5.EditValue, (Int32)MaterialTypeEnum.Product, 5, yesno5, fontSize, userGlobal.CFCLoginUser.CFCUserID);
            }
            else if (txtProdID5.Text.Length > 0)                    //Task1085 DB250813
            {
                Int32.TryParse(txtProdID5.Text, out labelFormatID);
                AIMManager.DeleteLabelFormat(labelFormatID);
            }


            PopulateLabelFormat((Int32)MaterialTypeEnum.Product);

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
        private void CheckLabelSize()                                           //4/19/22                                                          
        {
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                _labelSize = systemSettings.LabelSize;
            }
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
            Boolean IsGroupTemplate = false;          //Task1061 DB251022 Start
            if (_IsGroupTemplate == true)
            {
                rbIsGroupTemplateYes.Checked = true;
            }
            else
            {
                rbIsGroupTemplateNo.Checked = true;
            }

            if (_IsGroupTemplate == true)
            {
                IsGroupTemplate = true;
            }
            else
            {
                IsGroupTemplate = false;
            }                                       //Task1061 DB251022 End

            //Task1061  _template = AIMManager.GetSampleResultTemplate();
            _template = AIMManager.GetSampleResultTemplate(IsGroupTemplate);            //Task1061 DB251022
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

            if (_IsGroupTemplate == true)                                   //Task1061 DB251022 Start
            {
                txtCurrentIteration.Text = _template[0].CurrentIteration.ToString();
                SampleResultTemplateDetailGroupInfo sampleResultTemplateDetailGroup = new SampleResultTemplateDetailGroupInfo();
                grdSampleResultTemplateDetailGroup.DataSource = sampleResultTemplateDetailGroup;
                grbAddNutrient.Visible = false;
                grdSampleResultTemplateDetailGroup.Visible = false;

            }                                                               //Task1061 DB251022 End
            else
            {
                SampleResultTemplateDetailInfo clearResults = new SampleResultTemplateDetailInfo();
                grdSampleResultDetailTemplate.DataSource = clearResults;
                grbAddNutrient.Visible = false;
                grdSampleResultDetailTemplate.Visible = false;
            }

            txtDataSource.Text = "";                    //Task1061 DB251024
            txtMaterial.Text = "";                      //Task1061 DB251024

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

        private void DataBindSampleResultTemplateDetailGroup()                 //Task1061 DB251024
        {
            Int32 sampleRestultTemplateID = 0;

            grdSampleResultTemplateDetailGroup.RefreshDataSource();
            sampleRestultTemplateID = (Int32)lueTemplateName.EditValue;
            List<SampleResultTemplateDetailGroupInfo> sampleResultTemplateDetailGroup = new List<SampleResultTemplateDetailGroupInfo>();

            sampleResultTemplateDetailGroup = AIMManager.GetSampleResultTemplateDetailGroup(sampleRestultTemplateID);
            grdSampleResultTemplateDetailGroup.DataSource = sampleResultTemplateDetailGroup;
 
        }

        private void DataBindMassEntry()                        //Task1061 DB251104
        {
            if (lueTemplateName.EditValue != null)
            {
                if ((Int32)lueTemplateName.EditValue > 0)         //Task1061 DB251031
                {               
                    List<SampleResultTemplateGridInfo> templateDetailGrid = AIMManager.GetSampleResultTemplateDetailGroupTemplateID((Int32)lueTemplateName.EditValue);                                                                                                                                            
                    grdMassEntry.DataSource = templateDetailGrid;         
                }
            }
        }
        private void DataBindNutrientGroup()
        {
            if ((Int32)lueTemplateName.EditValue > 0)
            {
                List<NutrientGroupInfo> nutrientGroup = new List<NutrientGroupInfo>();

                nutrientGroup = AIMManager.GetNutrientGroupList();
                nutrientGroup.Insert
                (
                    0,
                    new NutrientGroupInfo
                    {
                        NutrientGroupID = 0,
                        NutrientGroupName = "--Select--"
                    }
                );
                lueSampleResultNutrientGroup.Properties.DataSource = nutrientGroup;
                XtraEditors.LoadLookUpEdit(ref lueSampleResultNutrientGroup, "NutrientGroupID", "NutrientGroupName");
            }           
        }


        private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindlueMaterial();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Int32 currentIteration = 0;                 //Task1061 DB251022
            Boolean cancelSave = false;
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
                if (_IsGroupTemplate == true)                               //Task1061 DB251022 Start
                {
                    Int32.TryParse(txtCurrentIterationNew.Text, out currentIteration);

                    if (currentIteration == 0)
                    {
                        currentIteration = 1;
                    }

                    result.CurrentIteration = currentIteration;
                    result.IsGroupTemplate = true;
                }
                else
                {
                    result.CurrentIteration = 0;
                    result.IsGroupTemplate = false;
                }                                                           //Task1061 DB251022 End

                if (_IsGroupTemplate == true)                       //Task1061 DB251028 Start
                {
                    List<SampleResultTemplateInfo> template = new List<SampleResultTemplateInfo>();
                    template = AIMManager.GetSampleResultTemplateAIMMaterialIDDataSourceTypeID((Int32)lueMaterialNew.EditValue, (Int32)lueDataSourceTypeNew.EditValue);

                    if (template.Count > 0)
                    {
                        cancelSave = true;
                    }
                }                                                   //Task1061 DB251028 End

                if (cancelSave == false)                            //Task1061 DB251028
                {
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

                    gbGroupOptions.Visible = false;                 //Task1061 DB251024
                    if (_IsGroupTemplate == true)                   //Task1061 DB251024
                    {
                        gbGroupOptions.Visible = true;              //Task1061 DB251024
                    }

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
                }                                                           //Task1061 DB251028 End
                else
                {                                                                                           //Task1061 DB251028
                    MessageBox.Show("Group Template with Data Source and AIM Material already exists.");    //Task1061 DB251028
                }                                                                                           //Task1061 DB251028 


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

            gbGroupOptionsNew.Visible = false;              //Task1061 DB251024
            if (_IsGroupTemplate == true)                   //Task1061 DB251024
            {
                gbGroupOptionsNew.Visible = true;           //Task1061 DB251024
            }

        }
        private void btnCancel_Click(Object sender, EventArgs e)
        {
            txtTemplateNameNew.Text = "";
            lueDataSourceTypeNew.EditValue = 0;
            lueMaterialNew.EditValue = 0;

            pnlNew.Visible = false;
            pnlNew.Location = new Point(12, 66);
            pnlNew.Size = new Size(874, 10);
            tcSampleTemplateResultGroup.Visible = false;

            if (_saveAs == true)
            {
                pnlExisting.Visible = true;
                pnlExisting.Location = new Point(8, 77);

                gbGroupOptions.Visible = false;                 //Task1061 DB251024
                if (_IsGroupTemplate == true)                   //Task1061 DB251024
                {
                    gbGroupOptions.Visible = true;              //Task1061 DB251024
                }

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
                DialogResult dialogResult = MessageBox.Show("Delete Mass Entry List '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    Int32 resultTemplateID = 0;
                    resultTemplateID = (Int32)lueTemplateName.EditValue;
                    AIMManager.DeleteSampleResultTemplate(resultTemplateID);

                    if (_IsGroupTemplate == true)           //Task1061 DB251028
                    {
                        AIMManager.DeleteSampleResultTemplateDetailGroup(resultTemplateID);
                        DataBindSampleResultTemplateDetailGroup();
                        DataBindMassEntry();
                    }                    

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

                    grbAddSampleResultTemplateGroup.Visible = false;        //Task1061 DB251028
                    grdSampleResultTemplateDetailGroup.Visible  = false;    //Task1061 DB251028

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
            if ((Int32)lueTemplateName.EditValue > 0)
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
                    txtDataSource.Text = _template[0].DataSourceTypeID.ToString();             //Task1061 DB251024

                    if (_IsGroupTemplate == true)                               //Task1061 DB251024 Start
                    {
                        grbAddNutrient.Visible = false;
                        tcSampleTemplateResultGroup.Visible = true;
                        if ((Int32)lueTemplateName.EditValue > 0)
                        {
                            txtCurrentIteration.Text = _template[0].CurrentIteration.ToString();
                            grbAddSampleResultTemplateGroup.Visible = true;
                            grdSampleResultTemplateDetailGroup.Visible = true;
                            grdSampleResultDetailTemplate.Visible = false;
                            DataBindNutrientGroup();
                            DataBindSampleResultTemplateDetailGroup();
                            DataBindMassEntry();
                        }
                    }
                    else                                                        //Task1061 DB251024 End                
                    {
                        grbAddSampleResultTemplateGroup.Visible = false;            //Task1061 DB251024
                    }
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

            gbGroupOptions.Visible = false;                 //Task1061 DB251024
            if (_IsGroupTemplate == true)                   //Task1061 DB251024
            {
                gbGroupOptions.Visible = true;              //Task1061 DB251024
            }

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

            grbAddSampleResultTemplateGroup.Visible = false;        //Task1061 DB251028
            grdSampleResultTemplateDetailGroup.Visible = false;     //Task1061 DB251028
            gbGroupOptionsNew.Visible = false;                      //Task1061 DB251024
            if (_IsGroupTemplate == true)                           //Task1061 DB251024
            {
                gbGroupOptionsNew.Visible = true;                   //Task1061 DB251024
            }

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
                //6/26/25 - uncommented to align with CFC standards -- DB
                SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
                templateDetail = (SampleResultTemplateDetailInfo)grdSampleResultDetailTemplateView.GetFocusedRow();
                templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.UpdateTemplateDetailSortOrder(ref templateDetail);
                DataBindTemplateDetail();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //4/20/21
        private void btnSaveExisting_Click(object sender, EventArgs e)
        {
            // DB 250626 Modified function of the save button to save the header information on form
            //                    Save functionality of the detail info placed on grid moved to validate event of row

            //if (lueTemplateName.EditValue != null)
            //{
            //    Int32.TryParse(lueTemplateName.EditValue.ToString(), out _templateID);

            //    List<SampleResultTemplateDetailInfo> det = new List<SampleResultTemplateDetailInfo>();
            //    det = (List<SampleResultTemplateDetailInfo>)grdSampleResultDetailTemplate.DataSource;
            //    if (det != null)
            //    {
            //        foreach (var item in det)
            //        {
            //            SampleResultTemplateDetailInfo templateDetail = new SampleResultTemplateDetailInfo();
            //            templateDetail.SampleResultTemplateID = item.SampleResultTemplateID;
            //            templateDetail.SampleResultTemplateDetailID = item.SampleResultTemplateDetailID;
            //            templateDetail.SortOrder = item.SortOrder;
            //            templateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            //            AIMManager.UpdateTemplateDetailSortOrder(ref templateDetail);
            //        }
            //    }
            //}

            try
            {
                SampleResultTemplateInfo sampleResultTemplate = new SampleResultTemplateInfo();
                Int32 templateID = 0;
                Int32.TryParse(lueTemplateName.EditValue.ToString(), out templateID);
                if (templateID == 0)
                {
                    MessageBox.Show("Please select a valid Mass Entry List before saving");
                }
                else
                {
                    Int32 currentIteration = 0;
                    sampleResultTemplate = (SampleResultTemplateInfo)lueTemplateName.GetSelectedDataRow();

                    if(_IsGroupTemplate == true)            //Task1061 DB251103 Start
                    {
                        Int32.TryParse(txtCurrentIteration.Text, out currentIteration);

                        if (currentIteration == 0)
                        {
                            currentIteration = 1;
                        }

                        sampleResultTemplate.CurrentIteration = currentIteration;
                        sampleResultTemplate.IsGroupTemplate = true;
                    }                                           //Task1061 DB251103 End
                    else
                    {                        
                        sampleResultTemplate.CurrentIteration = currentIteration;
                        sampleResultTemplate.IsGroupTemplate = false;
                    }

                    ErrorInfo outPut = AIMManager.SaveSampleResultTemplate(ref sampleResultTemplate);

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

                    gbGroupOptions.Visible = false;                 //Task1061 DB251024
                    if (_IsGroupTemplate == true)                   //Task1061 DB251024
                    {
                        gbGroupOptions.Visible = true;              //Task1061 DB251024
                    }

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

                    _templateID = (Int32)lueTemplateName.EditValue;
                    DataBindTemplate();

                    DataBindLueAnalyzerNutrient();

                    //go to "Existing" tab and pull up newly-added template
                    //tabControl2.SelectedIndex = 1;
                    lueTemplateName.EditValue = _templateID;
                    lueAnalyzerNutrient.SelectAll();
                }

            }
            catch
            {
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
                DialogResult dialogResult = MessageBox.Show("Delete Mass Entry List '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
                DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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

                DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
            DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
                lueTestType_Group.EditValue = 0; // testType;   //11/14/24
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbIsGroupTemplateNo_CheckedChanged(object sender, EventArgs e)     //Task1061 DB251022
        {
            HandleGroupTemplateRadio();
            DataBindTemplate();
            gbGroupOptions.Visible = false;
            gbGroupOptionsNew.Visible = false;
            grbAddSampleResultTemplateGroup.Visible = false;
            grdSampleResultTemplateDetailGroup.Visible = false;     //Task1061 DB251028
            tcSampleTemplateResultGroup.Visible = false;
        }

        private void rbIsGroupTemplateYes_CheckedChanged(object sender, EventArgs e)        //Task1061 DB251022
        {
            HandleGroupTemplateRadio();
            DataBindTemplate();
            gbGroupOptions.Visible = true;
            gbGroupOptionsNew.Visible = true;
            tcSampleTemplateResultGroup.Visible = false;
            tcSampleTemplateResultGroup.SelectedIndex = 0;
            grdSampleResultDetailTemplate.Visible = false;
            grbAddNutrient.Visible = false;            
        }

        private void HandleGroupTemplateRadio()         //Task1061 DB251022
        {
            if (rbIsGroupTemplateYes.Checked == true)
            {
                _IsGroupTemplate = true;
            }
            else if (rbIsGroupTemplateNo.Checked == true)
            {
                _IsGroupTemplate = false;
            }
        }

        private void btnAddNutrientGroupToList_Click(object sender, EventArgs e)                //Task1061 DB251024
        {
            try
            {
                timer1.Stop();
                btnAddNutrientGroupToList.BackColor = SystemColors.Control;

                if ((Int32)lueTemplateName.EditValue == 0)
                {
                    MessageBox.Show("Please enter Template Name.");
                    lueTemplateName.Focus();
                    lueTemplateName.ShowPopup();
                    return;
                }
                if ((Int32)lueSampleResultNutrientGroup.EditValue == 0)
                {
                    MessageBox.Show("Please select Nutrient.");
                    lueSampleResultNutrientGroup.Focus();
                    lueSampleResultNutrientGroup.ShowPopup();
                    return;
                }

                List<SampleResultTemplateDetailGroupInfo> sampleResultTemplateDetailGroupInfo = new List<SampleResultTemplateDetailGroupInfo>();
                sampleResultTemplateDetailGroupInfo = AIMManager.GetSampleResultTemplateDetailGroup((Int32)lueTemplateName.EditValue, (Int32)lueSampleResultNutrientGroup.EditValue);

                if (sampleResultTemplateDetailGroupInfo.Count > 0)
                {
                    MessageBox.Show("Nutrient Group already exists in current template");
                    return;
                }
                else
                {
                    SampleResultTemplateDetailGroupInfo templateDetailGroup = new SampleResultTemplateDetailGroupInfo();

                    templateDetailGroup.SampleResultTemplateDetailGroupID = 0;
                    templateDetailGroup.SampleResultTemplateID = _templateID;
                    templateDetailGroup.NutrientGroupID = (Int32)lueSampleResultNutrientGroup.EditValue;
                    templateDetailGroup.TestFrequency = 1;
                    templateDetailGroup.IsDateFrequency = false;
                    templateDetailGroup.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    templateDetailGroup.UpdateStamp = DateTime.Now;
                    ErrorInfo outPut = AIMManager.SaveSampleResultTemplateDetailGroup(ref templateDetailGroup);
                    DataBindNutrientGroup();
                    DataBindSampleResultTemplateDetailGroup();
                    DataBindMassEntry();

                    lueSampleResultNutrientGroup.Focus();
                    lueSampleResultNutrientGroup.SelectAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblNutrientGroup_Click(object sender, EventArgs e)                     //Task1061 DB251027
        {
            AIM aim = new AIM();
            AIMGlobal.nutrientGroupTab = true;
            //Task1103 DB251111 aim.ShowDialog();
            aim.Show();       //Task1103 DB251111
        }

        private void grdSampleResultTemplateDetailGroupView_ValidateRow(object sender, ValidateRowEventArgs e)          //Task1061 DB251028
        {
            try
            {
                Int32 templateID = 0;
                ErrorInfo error = new ErrorInfo();
                templateID = (Int32)lueTemplateName.EditValue;
                Int32 resultValue = 0;
                resultValue = (Int32)grdSampleResultTemplateDetailGroupView.GetFocusedRowCellValue(colSampleResultTemplateIDGroup);

                if (resultValue > 0)
                {
                    SampleResultTemplateDetailGroupInfo sampleResultTemplateDetailGroup = new SampleResultTemplateDetailGroupInfo();
                    sampleResultTemplateDetailGroup = (SampleResultTemplateDetailGroupInfo)grdSampleResultTemplateDetailGroupView.GetFocusedRow();
                    sampleResultTemplateDetailGroup.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                    error = AIMManager.SaveSampleResultTemplateDetailGroup(ref sampleResultTemplateDetailGroup);

                    DataBindSampleResultTemplateDetailGroup();
                    DataBindMassEntry();
                }
                else
                {
                    MessageBox.Show("Please enter a number greater than zero");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDeleteSampleResultTemplateGroup_Click(object sender, EventArgs e)                                //Task1061 DB251028
        {
            try
            {
                SampleResultTemplateDetailGroupInfo group = new SampleResultTemplateDetailGroupInfo();
                group = (SampleResultTemplateDetailGroupInfo)grdSampleResultTemplateDetailGroupView.GetFocusedRow();
                AIMManager.DeleteSampleResultTemplateDetailGroupDetailID(group.SampleResultTemplateDetailGroupID);
                DataBindSampleResultTemplateDetailGroup();
                DataBindMassEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdMassEntryView_RowCellClick(object sender, RowCellClickEventArgs e)          //Task1061 DB251104
        {
            Int32 aimNutrientID = 0;
            Int32 dataSourceTypeID = 0;
            if (e.Column.Name.ToString() == "colAnalyzerNutrientCode_Mass")
            {
                if (grdMassEntryView.GetFocusedRowCellValue(colAnalyzerNutrientCode_Mass) == null)
                {
                    Int32.TryParse(grdMassEntryView.GetFocusedRowCellValue(colAIMNutrientID_Mass).ToString(), out aimNutrientID);
                    if (aimNutrientID > 0)
                    {
                        Int32.TryParse(txtDataSource.Text, out dataSourceTypeID);
                        AIMGlobal.analyzerCrossRefenceAIMNutrientID = aimNutrientID;
                        AIMGlobal.analyzerCrossRefenceDataSourceTypeID = dataSourceTypeID;
                        AIMGlobal.analyzerCrossRefenceIsNutrientTab = true;

                        AnalyzerCrossReference analyzerCrossReference = new AnalyzerCrossReference();
                        analyzerCrossReference.ShowDialog();

                        AIMGlobal.analyzerCrossRefenceAIMNutrientID = 0;
                        AIMGlobal.analyzerCrossRefenceDataSourceTypeID = 0;
                        AIMGlobal.analyzerCrossRefenceIsNutrientTab = false;
                    }
                }
            }
        }

        private void grdMassEntryView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name.ToString() == "colAnalyzerNutrientCode_Mass")
            {
                object cellValue = grdMassEntryView.GetRowCellValue(e.RowHandle, e.Column);
                if (cellValue == null || cellValue == DBNull.Value || string.IsNullOrEmpty(cellValue.ToString()))
                {
                    e.RepositoryItem = btnGoToAnalyzerMaintenance;
                }
            }
        }

        //3/24/25 - commented
        //5/2/23 - Nearly exact same code as DataBindLabelFields, except sending "P" to stp. 
        //private void DataBindProdLabelFields()
        //{

        //    List<LabelsInfo> labels = new List<LabelsInfo>();
        //    labels = AIMManager.GetLabels_IngrOrProd("P");      //--> "P" excludes supplier/source fields
        //    labels.Insert
        //    (
        //        0,
        //        new LabelsInfo
        //        {
        //            LabelsID = 0,
        //            UserControlName = "--Select--"
        //        }
        //    );
        //    populateLineDropdowns(lueProdLabelLine1, labels);
        //    populateLineDropdowns(lueProdLabelLine2, labels);
        //    populateLineDropdowns(lueProdLabelLine3, labels);
        //    populateLineDropdowns(lueProdLabelLine4, labels);
        //    populateLineDropdowns(lueProdLabelLine5, labels);

        //    ---------------------------------------------------------------------------
        //    List < YesNoInfo > yesNo = new List<YesNoInfo>();
        //    yesNo = AIMManager.GetYesNo();
        //    yesNo.Insert
        //    (
        //        0,
        //        new YesNoInfo
        //        {
        //            YesNoID = 0,
        //            YesNoString = "--Select--"
        //        }
        //    );
        //    populateYesNoDropdowns(lueProdYesNo1, yesNo);
        //    populateYesNoDropdowns(lueProdYesNo2, yesNo);
        //    populateYesNoDropdowns(lueProdYesNo3, yesNo);
        //    populateYesNoDropdowns(lueProdYesNo4, yesNo);
        //    populateYesNoDropdowns(lueProdYesNo5, yesNo);

        //    lueProdYesNo1.EditValue = (Int32)EnumYesNo.No;
        //    lueProdYesNo2.EditValue = (Int32)EnumYesNo.No;
        //    lueProdYesNo3.EditValue = (Int32)EnumYesNo.No;
        //    lueProdYesNo4.EditValue = (Int32)EnumYesNo.No;
        //    lueProdYesNo5.EditValue = (Int32)EnumYesNo.No;

        //    //---------------------------------------------------------------------------

        //    CheckLabelSize();

        //    lblProdLine4.Visible = true;
        //    lueProdLabelLine4.Visible = true;
        //    lueProdYesNo4.Visible = true;

        //    lblProdLine5.Visible = true;
        //    lueProdLabelLine5.Visible = true;
        //    lueProdYesNo5.Visible = true;

        //    foreach (var item in labels)
        //    {
        //        setYesNoValue(item.LabelProdFieldLine1, item.LabelsID, item.ProdLine1Bold, lueProdLabelLine1, lueProdYesNo1);
        //        setYesNoValue(item.LabelProdFieldLine2, item.LabelsID, item.ProdLine2Bold, lueProdLabelLine2, lueProdYesNo2);
        //        setYesNoValue(item.LabelProdFieldLine3, item.LabelsID, item.ProdLine3Bold, lueProdLabelLine3, lueProdYesNo3);
        //        setYesNoValue(item.LabelProdFieldLine4, item.LabelsID, item.ProdLine4Bold, lueProdLabelLine4, lueProdYesNo4);
        //        setYesNoValue(item.LabelProdFieldLine5, item.LabelsID, item.ProdLine5Bold, lueProdLabelLine5, lueProdYesNo5);
        //    }

        //    if (_labelSize == 1)
        //    {
        //        lblProdLine4.Visible = false;
        //        lueProdLabelLine4.Visible = false;
        //        lueProdYesNo4.Visible = false;

        //        lblProdLine5.Visible = false;
        //        lueProdLabelLine5.Visible = false;
        //        lueProdYesNo5.Visible = false;
        //    }
        //}

        //3/24/25 - commented
        //6/29/21
        //private void DataBindLabelFields()
        //{
        //    //3/24/25
        //    //List<LabelsInfo> labels = new List<LabelsInfo>();            
        //    //labels = AIMManager.GetLabels();                  //5/2/23
        //    //labels = AIMManager.GetLabels_IngrOrProd("I");    
        //    List<LabelInfo> labels = new List<LabelInfo>();
        //    labels = AIMManager.GetLabel();
        //    labels.Insert
        //    (
        //        0,
        //        new LabelInfo
        //        {
        //            LabelID = 0,
        //            UserControlName = "--Select--"
        //        }
        //    );

        //    populateLineDropdowns(lueLabelLine1, labels);
        //    populateLineDropdowns(lueLabelLine2, labels);
        //    populateLineDropdowns(lueLabelLine3, labels);
        //    populateLineDropdowns(lueLabelLine4, labels);
        //    populateLineDropdowns(lueLabelLine5, labels);

        //    populateLineDropdowns(lueProdLabelLine1, labels);
        //    populateLineDropdowns(lueProdLabelLine2, labels);
        //    populateLineDropdowns(lueProdLabelLine3, labels);
        //    populateLineDropdowns(lueProdLabelLine4, labels);
        //    populateLineDropdowns(lueProdLabelLine5, labels);


        //    //---------------------------------------------------------------------------           
        //    List<YesNoInfo> yesNo = new List<YesNoInfo>();
        //    yesNo = AIMManager.GetYesNo();
        //    yesNo.Insert
        //    (
        //        0,
        //        new YesNoInfo
        //        {
        //            YesNoID = 0,
        //            YesNoString = "--Select--"
        //        }
        //    );

        //    populateYesNoDropdowns(lueYesNo1, yesNo);
        //    populateYesNoDropdowns(lueYesNo2, yesNo);
        //    populateYesNoDropdowns(lueYesNo3, yesNo);
        //    populateYesNoDropdowns(lueYesNo4, yesNo);
        //    populateYesNoDropdowns(lueYesNo5, yesNo);

        //    lueYesNo1.EditValue = (Int32)EnumYesNo.No;
        //    lueYesNo2.EditValue = (Int32)EnumYesNo.No;
        //    lueYesNo3.EditValue = (Int32)EnumYesNo.No;
        //    lueYesNo4.EditValue = (Int32)EnumYesNo.No;
        //    lueYesNo5.EditValue = (Int32)EnumYesNo.No;

        //    //---------------------------------------------------------------------------

        //    //4/21/22
        //    CheckLabelSize();

        //    //4/21/22
        //    lblLine4.Visible = true;
        //    lueLabelLine4.Visible = true;
        //    lueYesNo4.Visible = true;
        //    lblLine5.Visible = true;
        //    lueLabelLine5.Visible = true;
        //    lueYesNo5.Visible = true;


        //    foreach (var item in labels)
        //    {

        //        setYesNoValue(item.LabelFieldFirst, item.LabelsID, item.Line1Bold, lueLabelLine1, lueYesNo1);
        //        setYesNoValue(item.LabelFieldLine2, item.LabelsID, item.Line2Bold, lueLabelLine2, lueYesNo2);
        //        setYesNoValue(item.LabelFieldLine3, item.LabelsID, item.Line3Bold, lueLabelLine3, lueYesNo3);
        //        setYesNoValue(item.LabelFieldLine4, item.LabelsID, item.Line4Bold, lueLabelLine4, lueYesNo4);
        //        setYesNoValue(item.LabelFieldLast, item.LabelsID, item.Line5Bold, lueLabelLine5, lueYesNo5);

        //    }

        //    //4/21/22
        //    if (_labelSize == 1)
        //    {
        //        lblLine4.Visible = false;
        //        lueLabelLine4.Visible = false;
        //        lueYesNo4.Visible = false;
        //        lblLine5.Visible = false;
        //        lueLabelLine5.Visible = false;
        //        lueYesNo5.Visible = false;
        //    }
        //}

        //3/24/25 - commented
        //private void setYesNoValue(Boolean lineField, Int32 labelsID, Boolean lineBold, DevExpress.XtraEditors.LookUpEdit lueLine, DevExpress.XtraEditors.LookUpEdit lueYesNo)
        //{
        //    if (lineField == true)
        //    {
        //        lueLine.EditValue = labelsID;
        //        if (lineBold == true)
        //        {
        //            lueYesNo.EditValue = (Int32)EnumYesNo.Yes;
        //        }
        //        else
        //        {
        //            lueYesNo.EditValue = 2;
        //        }
        //    }
        //}

        //3/24/25 - commented - really old
        //private void grdLabelsSave(Int32 labelsID, Int32 labelfield, Boolean labelValue)
        //{
        //    LabelsInfo labels = new LabelsInfo();
        //    labels.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
        //    //AIMManager.SaveLabels(labelsID, labelfield, labelValue, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveLabels(labelsID, labelfield, labelValue, false, userGlobal.CFCLoginUser.CFCUserID);
        //}

        //3/24/25 - commented
        //private void btnSaveLabels_ClickOld(object sender, EventArgs e)
        //{

        //    //4/21/22
        //    if (_labelSize == 1)
        //    {
        //        if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0)
        //        {
        //            MessageBox.Show("All three label fields must be selected.");
        //            return;
        //        }
        //        if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0)
        //        {
        //            MessageBox.Show("Please select Yes or No to bolding.");
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        if ((Int32)lueLabelLine1.EditValue == 0 || (Int32)lueLabelLine2.EditValue == 0 || (Int32)lueLabelLine3.EditValue == 0 || (Int32)lueLabelLine4.EditValue == 0 || (Int32)lueLabelLine5.EditValue == 0)
        //        {
        //            MessageBox.Show("All five label fields must be selected.");
        //            return;
        //        }
        //        if ((Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo1.EditValue == 0 || (Int32)lueYesNo3.EditValue == 0 || (Int32)lueYesNo4.EditValue == 0 || (Int32)lueYesNo5.EditValue == 0)
        //        {
        //            MessageBox.Show("Please select Yes or No to bolding.");
        //            return;
        //        }
        //    }


        //    Boolean label1 = false;
        //    Boolean label2 = false;
        //    Boolean label3 = false;
        //    Boolean label4 = false;
        //    Boolean label5 = false;

        //    LabelsInfo label = new LabelsInfo();
        //    if ((Int32)lueLabelLine1.EditValue > 0) { label1 = true; }
        //    if ((Int32)lueLabelLine2.EditValue > 0) { label2 = true; }
        //    if ((Int32)lueLabelLine3.EditValue > 0) { label3 = true; }
        //    if ((Int32)lueLabelLine4.EditValue > 0) { label4 = true; }
        //    if ((Int32)lueLabelLine5.EditValue > 0) { label5 = true; }

        //    if ((Int32)lueYesNo1.EditValue == (Int32)EnumYesNo.Yes) { yesno1 = true; }
        //    if ((Int32)lueYesNo2.EditValue == (Int32)EnumYesNo.Yes) { yesno2 = true; }
        //    if ((Int32)lueYesNo3.EditValue == (Int32)EnumYesNo.Yes) { yesno3 = true; }
        //    if ((Int32)lueYesNo4.EditValue == (Int32)EnumYesNo.Yes) { yesno4 = true; }
        //    if ((Int32)lueYesNo5.EditValue == (Int32)EnumYesNo.Yes) { yesno5 = true; }

        //    AIMManager.SaveLabels((Int32)lueLabelLine1.EditValue, 1, label1, yesno1, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveLabels((Int32)lueLabelLine2.EditValue, 2, label2, yesno2, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveLabels((Int32)lueLabelLine3.EditValue, 3, label3, yesno3, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveLabels((Int32)lueLabelLine4.EditValue, 4, label4, yesno4, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveLabels((Int32)lueLabelLine5.EditValue, 5, label5, yesno5, userGlobal.CFCLoginUser.CFCUserID);


        //    _changesMade = false;
        //}


        //3/24/25 - commented
        //private void btnSaveProdLabels_Click_Old(object sender, EventArgs e)        //5/2/23
        //{

        //    Boolean label1 = false;
        //    Boolean label2 = false;
        //    Boolean label3 = false;
        //    Boolean label4 = false;
        //    Boolean label5 = false;

        //    Boolean yesno1 = false;
        //    Boolean yesno2 = false;
        //    Boolean yesno3 = false;
        //    Boolean yesno4 = false;
        //    Boolean yesno5 = false;

        //    LabelsInfo label = new LabelsInfo();
        //    if ((Int32)lueProdLabelLine1.EditValue > 0) { label1 = true; }
        //    if ((Int32)lueProdLabelLine2.EditValue > 0) { label2 = true; }
        //    if ((Int32)lueProdLabelLine3.EditValue > 0) { label3 = true; }
        //    if ((Int32)lueProdLabelLine4.EditValue > 0) { label4 = true; }
        //    if ((Int32)lueProdLabelLine5.EditValue > 0) { label5 = true; }

        //    if ((Int32)lueProdYesNo1.EditValue == (Int32)EnumYesNo.Yes) { yesno1 = true; }
        //    if ((Int32)lueProdYesNo2.EditValue == (Int32)EnumYesNo.Yes) { yesno2 = true; }
        //    if ((Int32)lueProdYesNo3.EditValue == (Int32)EnumYesNo.Yes) { yesno3 = true; }
        //    if ((Int32)lueProdYesNo4.EditValue == (Int32)EnumYesNo.Yes) { yesno4 = true; }
        //    if ((Int32)lueProdYesNo5.EditValue == (Int32)EnumYesNo.Yes) { yesno5 = true; }

        //    AIMManager.SaveProdLabels((Int32)lueProdLabelLine1.EditValue, 1, label1, yesno1, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveProdLabels((Int32)lueProdLabelLine2.EditValue, 2, label2, yesno2, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveProdLabels((Int32)lueProdLabelLine3.EditValue, 3, label3, yesno3, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveProdLabels((Int32)lueProdLabelLine4.EditValue, 4, label4, yesno4, userGlobal.CFCLoginUser.CFCUserID);
        //    AIMManager.SaveProdLabels((Int32)lueProdLabelLine5.EditValue, 5, label5, yesno5, userGlobal.CFCLoginUser.CFCUserID);

        //    _changesMade = false;
        //}

    }


}