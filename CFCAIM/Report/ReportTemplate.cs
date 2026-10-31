using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using System.Drawing;
using System.IO;

namespace CFCAIM.Report
{
    public partial class ReportTemplate : Form
    {        
        CFCUser.Global _userGlobal = new CFCUser.Global();
        public ReportSimple reportView;
        

        public ReportTemplate()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void ReportTemplate_Load(Object sender, EventArgs e)
        {
            Int32 saveVal = 0;
            saveVal = AIMGlobal.templateID;  //global gets wiped out when lueViewTemplateInTab is databound in next statement, so added saveVal

            DataBindTemplate();
            SetToolTips();
            if (AIMGlobal.saveNewViewTemplate == true)
            {
                btnNewViewTemplate.PerformClick();
            }
            else
            {
                lueViewTemplateInTab.EditValue = saveVal;
            }
            
        }

       
        private void DataBindTemplate()
        {
            List<AnalysisViewTemplateInfo> viewTemplate = new List<AnalysisViewTemplateInfo>();

            //4/12/18
            //viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, (Int32)EnumGridNumber.ReportSampleCustom);
            viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, (Int32)EnumGridNumber.UserDefinedSampleReportData, AIMGlobal.templateType);

            viewTemplate.Insert
            (
                0,
                new AnalysisViewTemplateInfo
                {
                    AnalysisViewTemplateID = 0,
                    AnalysisViewTemplateName = "--Select--"
                }
            );
            lueViewTemplateInTab.Properties.DataSource = viewTemplate;
            XtraEditors.LoadLookUpEdit(ref lueViewTemplateInTab, "AnalysisViewTemplateID", "AnalysisViewTemplateName");
        }

        private void btnSaveViewTemplate_Click(Object sender, EventArgs e)
        {
            try
            {
                int newRecID = 0;
                if (txtNewViewTemplateName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide a template name.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                if (txtNewViewTemplateName.Text.Trim().Length > 30)
                {
                    MessageBox.Show("Template name must be 30 characters or less.", "Warning...", MessageBoxButtons.OK);
                    return;
                }

                //verify template name does not already exist 
                List<AnalysisViewTemplateNameInfo> viewTemplateName = new List<AnalysisViewTemplateNameInfo>();

                //4/12/18
                //viewTemplateName = AIMManager.GetTemplateName(txtNewViewTemplateName.Text.Trim());
                viewTemplateName = AIMManager.GetTemplateName(txtNewViewTemplateName.Text.Trim(), AIMGlobal.templateType);

                if (viewTemplateName.Count != 0)
                {
                    MessageBox.Show("This template name already exists.  Please pick another name.", "Warning...", MessageBoxButtons.OK);
                    txtNewViewTemplateName.Text = String.Empty;
                    txtNewViewTemplateName.Focus();                   
                    return;
                }
                                
                if (AIMGlobal.saveReportTemplateAs == false)
                {
                    AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
                    viewTemplate.AnalysisViewTemplateID = 0;
                    viewTemplate.AnalysisViewTemplateName = txtNewViewTemplateName.Text.Trim();
                    viewTemplate.AnalysisViewTemplateXML = "";
                    viewTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                    viewTemplate.GridNumber = (Int32)EnumGridNumber.UserDefinedSampleReportData;

                    //4/12/18  TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                    viewTemplate.TemplateType = AIMGlobal.templateType;

                    //5/21/19
                    viewTemplate.templateAIMMaterialID = AIMGlobal.templateAIMMaterialID;
                    viewTemplate.templateAIMOwnerID = AIMGlobal.templateAIMOwnerID;
                    viewTemplate.templateAIMOwnerLocationID = AIMGlobal.templateAIMOwnerLocationID;
                    viewTemplate.templateAIMSupplierID = AIMGlobal.templateAIMSupplierID;
                    viewTemplate.templateAIMSupplierLocationID = AIMGlobal.templateAIMSupplierLocationID;
                    viewTemplate.templateMaterialTypeID = AIMGlobal.templateMaterialTypeID;

                    newRecID = AIMManager.SaveAnalysisViewTemplate(ref viewTemplate).RecordID;

                    if (newRecID == 0)
                    {
                        //couldn't save the template, error message will be in the data layer
                        pnlNewViewTemplate.Visible = false;
                        pnlExistingViewTemplate.Visible = true;
                        pnlViewButtons.Visible = true;
                        return;
                    }


                    if (AIMGlobal.saveNewViewTemplate == true)
                    {
                        //getting layout in stream
                        //Stream stream = new System.IO.MemoryStream();
                        //reportView.grdReportView.SaveLayoutToStream(stream);
                        Stream stream = AIMGlobal.streamNew;

                        stream.Position = 0;
                        StreamReader streamReader = new StreamReader(stream);                       
                        viewTemplate.AnalysisViewTemplateID = newRecID;
                        viewTemplate.AnalysisViewTemplateName = txtNewViewTemplateName.Text;
                        viewTemplate.AnalysisViewTemplateXML = streamReader.ReadToEnd();
                        viewTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;                        
                        viewTemplate.GridNumber = (Int32)EnumGridNumber.UserDefinedSampleReportData;

                        //4/12/18  TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                        viewTemplate.TemplateType = AIMGlobal.templateType;

                        viewTemplate.IsActive = true;

                        //5/21/19
                        viewTemplate.templateAIMMaterialID = AIMGlobal.templateAIMMaterialID;
                        viewTemplate.templateAIMOwnerID = AIMGlobal.templateAIMOwnerID;
                        viewTemplate.templateAIMOwnerLocationID = AIMGlobal.templateAIMOwnerLocationID;
                        viewTemplate.templateAIMSupplierID = AIMGlobal.templateAIMSupplierID;
                        viewTemplate.templateAIMSupplierLocationID = AIMGlobal.templateAIMSupplierLocationID;
                        viewTemplate.templateMaterialTypeID = AIMGlobal.templateMaterialTypeID;

                        stream.Dispose();
                        AIMManager.SaveAnalysisViewTemplate(ref viewTemplate);
                    }

                }
                if (AIMGlobal.saveReportTemplateAs == true)
                {
                    AnalysisViewTemplateInfo viewTemplateForSaveAs = new AnalysisViewTemplateInfo();
                    viewTemplateForSaveAs = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                    viewTemplateForSaveAs.AnalysisViewTemplateID = 0;
                    viewTemplateForSaveAs.AnalysisViewTemplateName = txtNewViewTemplateName.Text.Trim();
                    viewTemplateForSaveAs.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;                    
                    viewTemplateForSaveAs.GridNumber = (Int32)EnumGridNumber.UserDefinedSampleReportData;

                    //4/12/18  TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                    viewTemplateForSaveAs.TemplateType = AIMGlobal.templateType;

                    //5/21/19
                    viewTemplateForSaveAs.templateAIMMaterialID = AIMGlobal.templateAIMMaterialID;
                    viewTemplateForSaveAs.templateAIMOwnerID = AIMGlobal.templateAIMOwnerID;
                    viewTemplateForSaveAs.templateAIMOwnerLocationID = AIMGlobal.templateAIMOwnerLocationID;
                    viewTemplateForSaveAs.templateAIMSupplierID = AIMGlobal.templateAIMSupplierID;
                    viewTemplateForSaveAs.templateAIMSupplierLocationID = AIMGlobal.templateAIMSupplierLocationID;
                    viewTemplateForSaveAs.templateMaterialTypeID = AIMGlobal.templateMaterialTypeID;


                    newRecID = AIMManager.SaveAnalysisViewTemplate(ref viewTemplateForSaveAs).RecordID;
                }

                AIMGlobal.templateID = newRecID;

                DataBindTemplate();
                lueViewTemplateInTab.EditValue = newRecID;
                pnlNewViewTemplate.Visible = false;
                pnlExistingViewTemplate.Visible = true;
                pnlViewButtons.Visible = true;
                txtNewViewTemplateName.Text = "";

                //5/21/19
                btnExit.PerformClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewViewTemplate_Click(Object sender, EventArgs e)
        {
            //_saveViewTemplateAs = false;
            pnlViewButtons.Visible = false;
            pnlExistingViewTemplate.Visible = false;
            pnlNewViewTemplate.Visible = true;
            txtNewViewTemplateName.Focus();
            txtNewViewTemplateName.Select();
        }

        private void btnDeleteViewTemplate_Click(Object sender, EventArgs e)
        {
            try
            {
                AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                if (viewTemplate == null || viewTemplate.AnalysisViewTemplateID == 0)
                {
                    return;
                }
                AIMManager.DeleteAnalysisViewTemplate(ref viewTemplate);
                DataBindTemplate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateCurrentViewTemplate_Click(Object sender, EventArgs e)
        {
            try
            {
                AnalysisViewTemplateInfo temp = new AnalysisViewTemplateInfo();
                temp = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                if (temp == null)
                {
                    return;
                }
                if (temp.AnalysisViewTemplateID == 0)
                {
                    MessageBox.Show("Please select the template first.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                AIMGlobal.saveReportTemplateAs = true;
                pnlExistingViewTemplate.Visible = false;
                pnlViewButtons.Visible = false;
                pnlNewViewTemplate.Visible = true;
                txtNewViewTemplateName.Focus();
                txtNewViewTemplateName.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //5/21/19
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                Close();
                ReportSimple reportView;
                AIMGlobal.open_form(reportView = new ReportSimple());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void btnCancelViewTemplateSave_Click(Object sender, EventArgs e)
        {
            pnlViewButtons.Visible = true;
            pnlExistingViewTemplate.Visible = true;
            pnlNewViewTemplate.Visible = false;
            
        }

        private void SetToolTips()
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }
                ttip.SetToolTip(btnNewViewTemplate, "Add New Template");
                ttip.SetToolTip(btnUpdateCurrentViewTemplate, "Save Current Template");
                ttip.SetToolTip(btnDeleteViewTemplate, "Delete Template");
                ttip.SetToolTip(btnSaveViewTemplate, "Save Template");
                ttip.SetToolTip(btnExit, "Preview Grid Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueViewTemplateInTab_EditValueChanged(Object sender, EventArgs e)
        {
            AIMGlobal.templateID = (Int32)lueViewTemplateInTab.EditValue;
        }
    }
}
