using System;
using System.Collections.Generic;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Windows.Forms;
using System.Drawing;
using Core.Library.DevExpress;
using System.Linq;

namespace CFCAIM.SampleQualityControl
{
    public partial class SampleQualityControlEquation : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleQualityEquationInfo> _equation = new List<SampleQualityEquationInfo>();
        Int32 _cursorPos = 0;
        Int32 _cursorPosActualEquation = 0;
        Int32 _paramCount = 0;
        
        List<SampleQualityParameterInfo> _parameter = new List<SampleQualityParameterInfo>();
        List<SampleQualityEquationBuildInfo> _build = new List<SampleQualityEquationBuildInfo>();
        DataSourceUser dataSourceUser;
        Maintenance.Analyzer analyzer;

        public SampleQualityControlEquation()
        {
            InitializeComponent();
        }

        private void SampleQualityControlEquation_Load(Object sender, EventArgs e)
        {
            SampleQualityEquationInfo def = new SampleQualityEquationInfo();
            
            DataBindLueParameter();
            DataBindLueDataSource();
            DataBindLueAnalyzer();
            SetToolTips();

            txtName.ReadOnly = true;
            if (AIMGlobal.equationID == 0)
            {

                //Have to add rec first to get EquationID for EquationBuild table                
                def.SampleQualityEquationID = 0;
                def.SampleQualityEquationName = "";
                def.AnalyzerNutrientID = 0;
                def.Equation = "";                
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                def.IsActive = false;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveSampleQualityEquation(ref def);
                AIMGlobal.equationID = error.RecordID;

                txtName.Text = "";
                txtBuild.Text = "";
                txtName.BackColor = Color.White;
                txtName.ReadOnly = false;

                gbEquation.Visible = false;
                gbParameters.Visible = false;
                gbTest.Visible = false;
                gbNutrient.Visible = false;
                gbSave.Visible = false;

            }
            else
            {               
                _equation = AIMManager.GetEquationID(AIMGlobal.equationID);
                if (_equation != null)
                {
                    txtName.Text = _equation[0].SampleQualityEquationName;
                    lueDataSource.EditValue = _equation[0].DataSourceTypeID;
                    lueAnalyzerNutrient.EditValue = _equation[0].AnalyzerNutrientID;
                    txtEquation.Text = _equation[0].Equation;                                                           
                    GetEquationFromEntityAndParse();
                }
                txtName.BackColor = SystemColors.Control;
                txtName.ReadOnly = true;
            }


            //5/20/19
            gbNutrient.Visible = false;
            gbSave.Visible = false;
            if (AIMGlobal.newEquation == false) 
            {
                gbNutrient.Visible = true;
                gbSave.Visible = true;
            }

           
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

        }

        //This function is for the user to easily see the equation
        private void GetEquationFromEntityAndParse()
        {                      
            String param = "";
            Int32 paramCount = 0;
            _build = AIMManager.GetSampleEquationBuild_SampleEquationBuildID(AIMGlobal.equationID);
            var itemsInEquation = from g in _build
                                  select g;           
            foreach (var item in itemsInEquation)
            {               
                if (item.ValueString.Length > 2)
                {
                    if (item.ValueString.Substring(0, 2) == "ID")
                    {
                        //get number part of ID and, using number, get parameter text for label
                        Int32 valID = 0;
                        Int32.TryParse(item.ValueString.Substring(2), out valID);
                        SampleQualityParameterInfo samp = new SampleQualityParameterInfo();
                        samp = AIMManager.GetQualityParameterID(valID);
                        if (samp != null)
                        {
                            paramCount++;
                            param = samp.SampleQualityParameterName;
                            txtBuild.Text = txtBuild.Text + param;

                            if (paramCount == 1)
                            {
                                txtParam1.Visible = true;
                                lblParam1.Visible = true;
                                lblParam1.Text = param;
                            }
                            else if (paramCount == 2)
                            {
                                txtParam2.Visible = true;
                                lblParam2.Visible = true;
                                lblParam2.Text = param;
                            }
                            else if (paramCount == 3)
                            {
                                txtParam3.Visible = true;
                                lblParam3.Visible = true;
                                lblParam3.Text = param;
                            }
                            else if (paramCount == 4)
                            {
                                txtParam4.Visible = true;
                                lblParam4.Visible = true;
                                lblParam4.Text = param;
                            }
                        }
                    }

                    //3/16/18 - added to include numeric values
                    else
                    {
                        txtBuild.Text = txtBuild.Text + item.ValueString;
                    }

                }
                else
                {
                    txtBuild.Text = txtBuild.Text + item.ValueString;
                }



            }
            _cursorPos = txtBuild.Text.Length;
            _cursorPosActualEquation = _equation[0].Equation.Length;
            _paramCount = paramCount;
            
        }

        private void DataBindLueParameter()
        {

            Int32 dataType = 0;

            if (lueDataSource_ForParams.EditValue != null && lueDataSource_ForParams.Text != String.Empty)
            {
                dataType = (Int32)lueDataSource_ForParams.EditValue;
            }

            if (dataType == 0)
            {
                _parameter = AIMManager.GetAllQualityParameters();
            }
            else
            {
                _parameter = AIMManager.GetSampleQualityParameter((Int32)lueDataSource_ForParams.EditValue);
            }
                        
            _parameter.Insert
            (
                0,
                new SampleQualityParameterInfo
                {
                    SampleQualityParameterID = 0,
                    SampleQualityParameterName = "--Select--",
                    IsActive = false
                }
            );

            lueParameter.Properties.DataSource = _parameter;	
            XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName", "DataSourceUserName");
        }

        private void DataBindLueDataSource()
        {
            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            dataSourceType = AIMManager.GetDataSourceTypeHavingAnalyzerNutrients(true);

            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );
            lueDataSource.Properties.DataSource = dataSourceType;
            XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
                        
            //-----------------------------------------------------------------------------
            //separate datasourcetype datasource to show --All-- instead of --Select--
            List<DataSourceTypeInfo> dataSourceTypeParams = new List<DataSourceTypeInfo>();
            dataSourceTypeParams = AIMManager.GetDataSourceTypeHavingQualityControlParameters(true);

            dataSourceTypeParams.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--All--"
                }
            );            
            lueDataSource_ForParams.Properties.DataSource = dataSourceTypeParams;
            XtraEditors.LoadLookUpEdit(ref lueDataSource_ForParams, "DataSourceTypeID", "Name");

            //-----------------------------------------------------------------------------
            if (dataSourceType.Count == 2)
            {
                //selecting value if there is only one
                lueDataSource.EditValue = dataSourceType[1].DataSourceTypeID;
                lueDataSource_ForParams.EditValue = dataSourceType[1].DataSourceTypeID;
            }
            else
            {
                lueDataSource.EditValue = dataSourceType;
                lueDataSource_ForParams.EditValue = dataSourceType;
            }
            
        }

        private void DataBindLueAnalyzer()
        {
            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            Int32 dataType = 0;

            if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
            {
                dataType = (Int32)lueDataSource.EditValue;
            }

            if (dataType == 0)
            {
                analyzerNutrient = AIMManager.GetAllAnalyzerNutrient(true);
            }
            else
            {
                analyzerNutrient = AIMManager.GetAnalyzerNutrient(dataType, true);
            }

            analyzerNutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--",
                    AnalyzerNutrientName = "--Select--"
                }
            );
            lueAnalyzerNutrient.Properties.DataSource = analyzerNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrient, "AnalyzerNutrientID", "AnalyzerNutrientName");
            lueAnalyzerNutrient.Properties.Columns["AnalyzerNutrientCode"].Visible = true;
            lueAnalyzerNutrient.Properties.Columns["AnalyzerNutrientName"].Visible = true;

            //checkfilter();

        }

        private void checkfilter()
        {
            if (lueDataSource.EditValue != null)
            {                
            }
        }

        private void SetToolTips()
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                ToolTip ttip = new ToolTip();
                ttip.SetToolTip(btnTest, "Test Equation");
                ttip.SetToolTip(btnSave, "Save Equation");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                List<SampleQualityEquationInfo> type = new List<SampleQualityEquationInfo>();
                type = AIMManager.GetEquationID(AIMGlobal.equationID);
                if (type.Count > 0)
                {
                    if (type[0].Equation != txtEquation.Text)
                    {
                        DialogResult dr;
                        dr = MessageBox.Show("Changes have not been saved. Close form without saving?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                //delete any records in the SampleQualityEquationBuild table where IsActive = false
                AIMManager.DeleteSampleQualityEquationInactive();


                //delete any records in the SampleQualityEquationBuild table where status = false -- for the EquationID
                AIMManager.DeleteSampleQualityEquationBuild_Inactive(AIMGlobal.equationID);


                Close();
                SampleQuality sampleQuality;
                AIMGlobal.open_form(sampleQuality = new SampleQuality());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(Object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Please enter an Equation Name. ", "Warning...", MessageBoxButtons.OK);
                txtName.Focus();
                return;
            }

            if ((Int32)lueDataSource.EditValue == 0)
            {
                MessageBox.Show("Please select a DataSource. ", "Warning...", MessageBoxButtons.OK);
                lueDataSource.Focus();
                return;
            }

            if ((Int32)lueAnalyzerNutrient.EditValue == 0)
            {
                MessageBox.Show("Please select an Analyzer Nutrient. ", "Warning...", MessageBoxButtons.OK);
                lueAnalyzerNutrient.Focus();
                return;
            }


            //5/20/19
            if (txtBuild.Text.Length == 0)
            {
                MessageBox.Show("Please enter an equation. ", "Warning...", MessageBoxButtons.OK);
                txtBuild.Focus();                
                return;
            }
            if (lblResult.Text == "Error")
            {
                MessageBox.Show("There is something wrong with the expression.. ", "Warning...", MessageBoxButtons.OK);                
                return;
            }


            SampleQualityEquationInfo def = new SampleQualityEquationInfo();
            def.SampleQualityEquationID = AIMGlobal.equationID;
            def.SampleQualityEquationName = txtName.Text;
            def.AnalyzerNutrientID = (Int32)lueAnalyzerNutrient.EditValue;
            def.DataSourceTypeID = (Int32)lueDataSource.EditValue;
            def.Equation = txtEquation.Text;            
            def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;                
            def.IsActive = true;
            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveSampleQualityEquation(ref def);
            AIMGlobal.equationID = error.RecordID;

            //update status on equation-build table        
            ErrorInfo errorBuild = new ErrorInfo();
            errorBuild = AIMManager.UpdateSampleQualityEquationBuildStatus(def.SampleQualityEquationID, true, userGlobal.CFCLoginUser.CFCUserID);

            if (txtName.ReadOnly == true)
            {
                MessageBox.Show("Equation updated.");
            }
            else
            {
                MessageBox.Show("Equation added.");
            }
            
            //delete any records in the SampleQualityEquationBuild table where IsActive = false
            AIMManager.DeleteSampleQualityEquationInactive();

            Close();
            SampleQuality sampleQuality;
            AIMGlobal.open_form(sampleQuality = new SampleQuality());

        }
               

        private void AddSymbol(String symbol)
        {
            String leftChars = "";
            String rightChars = "";

            String leftCharsActual = "";
            String rightCharsActual = "";

            if (txtBuild.Text != "")
            {
                leftChars = txtBuild.Text.Substring(0, _cursorPos);
                rightChars = txtBuild.Text.Substring(_cursorPos, txtBuild.Text.Length - leftChars.Length);

                leftCharsActual = txtEquation.Text.Substring(0, _cursorPosActualEquation);
                rightCharsActual = txtEquation.Text.Substring(_cursorPosActualEquation, txtEquation.Text.Length - leftCharsActual.Length);
            }
            
            txtBuild.Text = leftChars + symbol + rightChars;
            txtEquation.Text = leftCharsActual + symbol + rightCharsActual;

            _cursorPos = _cursorPos + symbol.Length;
            _cursorPosActualEquation = _cursorPosActualEquation + symbol.Length;

            AddParamToEquationBuild(symbol, 0);
        }

        private void AddParameter(String param, Int32 ID)
        {
            String leftChars = "";
            String rightChars = "";

            String leftCharsActual = "";
            String rightCharsActual = "";

            if (txtBuild.Text != "")
            {
                leftChars = txtBuild.Text.Substring(0, _cursorPos);
                rightChars = txtBuild.Text.Substring(_cursorPos, txtBuild.Text.Length - leftChars.Length);

                leftCharsActual = txtEquation.Text.Substring(0, _cursorPosActualEquation);
                rightCharsActual = txtEquation.Text.Substring(_cursorPosActualEquation, txtEquation.Text.Length - leftCharsActual.Length);
            }
            txtBuild.Text = leftChars + param + rightChars;
            txtEquation.Text = leftCharsActual + "ID" + ID + rightCharsActual;                       

            _cursorPos = _cursorPos + param.Length;
            _cursorPosActualEquation = _cursorPosActualEquation + Convert.ToString(ID).Length + 2;  // + 2 is for "ID"

            AddParamToEquationBuild("ID"+ID, ID);


        }

        private void AddParamToEquationBuild(String param, Int32 ID)
        {
            SampleQualityEquationBuildInfo def = new SampleQualityEquationBuildInfo();
            def.SampleQualityEquationBuildID = 0;
            def.SampleQualityEquationID = AIMGlobal.equationID;
            def.SampleQualityParameterID = ID;
            def.ValueString = param;
            def.Status = false;
            def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveSampleQualityEquationBuild(ref def);

            //5/20/18
            gbNutrient.Visible = false;
            gbSave.Visible = false;
            lblResult.Text = "";
            txtEval.Text = "";            
            txtParam1.Text = "";
            txtParam2.Text = "";
            txtParam3.Text = "";
            txtParam4.Text = "";


        }


        private void btnParenFront_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnParenFront.Text);
        }

        private void btnParenBack_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnParenBack.Text);
        }

        private void btnMinus_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnMinus.Text);
        }

        private void btnPlus_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnPlus.Text);
        }

        private void btnMultiply_Click(Object sender, EventArgs e)
        {
            AddSymbol("*");
        }

        private void btnDivide_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnDivide.Text);
        }
                
        private void btnRemove_Click(Object sender, EventArgs e)
        {
            if (txtBuild.Text.Length > 0)
            {
                txtBuild.Text = txtBuild.Text.Substring(0, txtBuild.Text.Length - 1);                
                _paramCount--;
            }
        }

        private void BuildEval()
        {
            String str = txtBuild.Text;
            Int32 operPos = 0;
           

            if (lblParam1.Visible == true)
            {
                str = str.Replace(lblParam1.Text, txtParam1.Text);
            }
            if (lblParam2.Visible == true)
            {                
                str = str.Replace(lblParam2.Text, txtParam2.Text);
            }
            if (lblParam3.Visible == true)
            {
                str = str.Replace(lblParam3.Text, txtParam3.Text);
            }
            if (lblParam4.Visible == true)
            {
                str = str.Replace(lblParam4.Text, txtParam4.Text);
            }
                        
            
            //5/20/19
            if (_paramCount > 1)
            {
                if (_paramCount >= 2)
                {
                    operPos = lblParam1.Text.Length;
                    if (CheckParams(txtBuild.Text, operPos) == false)
                    {
                        str = "Error";
                    }
                }

                if (_paramCount >= 3)
                {
                    operPos = lblParam1.Text.Length + 1 + lblParam2.Text.Length;
                    if (CheckParams(txtBuild.Text, operPos) == false)
                    {
                        str = "Error";
                    }
                }

                if (_paramCount >= 4)
                {
                    operPos = lblParam1.Text.Length + 1 + lblParam2.Text.Length + 1 + lblParam3.Text.Length;
                    if (CheckParams(txtBuild.Text, operPos) == false)
                    {
                        str = "Error";
                    }
                }                
                
                
            }
            

            txtEval.Text = str;
        }

        private static Boolean CheckParams(String text, Int32 operPos) 
        {
            Boolean rtn = false;
            if (text.Substring(operPos, 1) == "*" || text.Substring(operPos, 1) == "/" || text.Substring(operPos, 1) == "+" || text.Substring(operPos, 1) == "-" || text.Substring(operPos, 1) == "(" || text.Substring(operPos, 1) == ")")
            {
                rtn = true;
            }
            return rtn;
        }
        

        public static double Evaluate(string expression)
        {
            Double rtn = 0;

            try
            {
                rtn = (double)new System.Xml.XPath.XPathDocument
                (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
                (string.Format("number({0})", new
                System.Text.RegularExpressions.Regex(@"([\+\-\*])")
                .Replace(expression, " ${1} ")
                //.Replace("^", " ^ ")
                .Replace("/", " div ")
                .Replace("%", " mod ")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - There is something wrong with the expression.");
                rtn = -9999;

            }
            return rtn;
        }

        private void btnTest_Click(Object sender, EventArgs e)
        {
            
            Double dbl = 0;

            //5/20/19
            gbNutrient.Visible = false;
            gbSave.Visible = false;


            BuildEval();

            //5/20/19
            if (txtParam1.Text == "" && txtParam2.Text == "" && txtParam3.Text == "" && txtParam4.Text == "")
            {
                txtEval.Text = "Test values are empty";
                lblResult.Text = "Empty";
                lblResult.ForeColor = Color.Red;
                return;
            }

            else if (txtEval.Text == "")
            {
                lblResult.Text = "Empty";
                lblResult.ForeColor = Color.Red;

                //5/20/19 - commented
                //btnSave.Enabled = true;               
                return;
            }

            //5/20/19
            else if (txtEval.Text == "Error")
            {                
                txtEval.Text = "Equation is invalid.  Clear and re-add parameters.";
                lblResult.Text = "Error";
                lblResult.ForeColor = Color.Red;

                return;
            }
            

            dbl = Evaluate(txtEval.Text);
            lblResult.ForeColor = Color.Black;
            if (dbl.ToString() == "NaN" || dbl == -9999)
            {
                lblResult.Text = "Error";
                lblResult.ForeColor = Color.Red;
            }
            else
            {
                lblResult.Text = dbl.ToString();

                //5/20/19
                //btnSave.Enabled = true;
                gbNutrient.Visible = true;
                if ((Int32)lueDataSource.EditValue != 0 && (Int32)lueAnalyzerNutrient.EditValue != 0)
                {
                    gbSave.Visible = true;
                }

            }
        }

        private void txtBuild_TextChanged(Object sender, EventArgs e)
        {            
            if (txtBuild.Text.Length > 0)
            {
                gbTest.Visible = true;

                //5/20/19 - commented
                //gbNutrient.Visible = true;
                //gbSave.Visible = true;
            }

            //5/20/19
            //btnSave.Enabled = false;
            else
            {
                gbTest.Visible = false;
            }
        }

        private void txtBuild_Leave(Object sender, EventArgs e)
        {
            _cursorPos = txtBuild.SelectionStart;
        }

        private void txtBuild_KeyDown(Object sender, KeyEventArgs e)
        {
            _cursorPos = txtBuild.SelectionStart;
        }

        private void txtBuild_MouseDown(Object sender, MouseEventArgs e)
        {
            _cursorPos = txtBuild.SelectionStart;
        }

        private void btnClear_Click(Object sender, EventArgs e)
        {
            
            DialogResult dr;
            dr = MessageBox.Show("Clear Equation?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }

            AIMManager.DeleteSampleQualityEquationBuild(AIMGlobal.equationID);
                        
            //Clear Equation on SampleQualityEquation table for this equationID
            SampleQualityEquationInfo def = new SampleQualityEquationInfo();
            def.SampleQualityEquationID = AIMGlobal.equationID;                       
            def.Equation = "";            
            def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;           
            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveSampleQualityEquation(ref def);

            txtBuild.Text = "";
            txtEquation.Text = "";
            _paramCount = 0;
            txtEval.Text = "";
            lblResult.Text = "";
            lueParameter.EditValue = 0;
            _cursorPos = 0;
            _cursorPosActualEquation = 0;

            txtNumericValue.Text = String.Empty;

            lblParam1.Text = "";
            lblParam2.Text = "";
            lblParam3.Text = "";
            lblParam4.Text = "";
            txtParam1.Text = "";
            txtParam2.Text = "";
            txtParam3.Text = "";
            txtParam4.Text = "";
            lblParam1.Visible = false;
            lblParam2.Visible = false;
            lblParam3.Visible = false;
            lblParam4.Visible = false;
            txtParam1.Visible = false;
            txtParam2.Visible = false;
            txtParam3.Visible = false;
            txtParam4.Visible = false;

            //5/20/19
            gbTest.Visible = false;
            gbNutrient.Visible = false;
            gbSave.Visible = false;

        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SampleQualityControlEquation_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void lueParameter_EditValueChanged(Object sender, EventArgs e)
        {
            if ((Int32)lueParameter.EditValue != 0)
            {
                AddParameter(lueParameter.Text, (Int32)lueParameter.EditValue);
                AddParamLabel(lueParameter.Text);
                _paramCount++;               
            }
            
        }

       
        private void AddParamLabel(String param)
        {
            if (_paramCount == 0)
            {
                txtParam1.Visible = true;
                lblParam1.Visible = true;
                lblParam1.Text = param;
            }
            if (_paramCount == 1)
            {
                txtParam2.Visible = true;
                lblParam2.Visible = true;
                lblParam2.Text = param;
            }
            if (_paramCount == 2)
            {
                txtParam3.Visible = true;
                lblParam3.Visible = true;
                lblParam3.Text = param;
            }
            if (_paramCount == 3)
            {
                txtParam4.Visible = true;
                lblParam4.Visible = true;
                lblParam4.Text = param;
            }
        }

        private void RemoveParamLabel(String param)
        {
            if (_paramCount == 0)
            {
                txtParam1.Visible = false;
                lblParam1.Visible = false;
                lblParam1.Text = "";
            }
            if (_paramCount == 1)
            {
                txtParam2.Visible = false;
                lblParam2.Visible = false;
                lblParam2.Text = "";
            }
            if (_paramCount == 2)
            {
                txtParam3.Visible = false;
                lblParam3.Visible = false;
                lblParam3.Text = "";
            }
            if (_paramCount == 3)
            {
                txtParam4.Visible = false;
                lblParam4.Visible = false;
                lblParam4.Text = "";
            }
        }

        private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindLueAnalyzer();

            //5/20/19
            // btnSave.Enabled = true;
            gbSave.Visible = false;
            if ((Int32)lueDataSource.EditValue != 0 && (Int32)lueAnalyzerNutrient.EditValue != 0)
            {
                gbSave.Visible = true;
            }
           
        }

        private void lueDataSource_ForParams_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindLueParameter();
            lueParameter.ShowPopup();      
        }

        private void lueAnalyzerNutrient_EditValueChanged(Object sender, EventArgs e)
        {
            //5/20/19
            // btnSave.Enabled = true;
            gbSave.Visible = false;
            if ((Int32)lueDataSource.EditValue != 0 && (Int32)lueAnalyzerNutrient.EditValue != 0)
            {
                if (gbNutrient.Visible == true)
                {
                    gbSave.Visible = true;
                }
                
            }
        }

        private void hyperDataSource_Click(Object sender, EventArgs e)
        {
            
            try
            {
                AIMGlobal.open_form(dataSourceUser = new DataSourceUser());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }

            
        }

        private void hyperDataSourceFilter_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataSourceUser = new DataSourceUser());                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void hyperAnalyzerNutrient_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.analyzerRequiredField = "NUTRIENT";
                AIMGlobal.open_form(analyzer = new Maintenance.Analyzer());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void hypereEquationParameter_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.analyzerRequiredField = "EQUATIONPARAMETER";
                AIMGlobal.open_form(analyzer = new Maintenance.Analyzer());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void btnAddNumeric_Click(Object sender, EventArgs e)
        {
            Decimal i = 0;
            Boolean result = Decimal.TryParse(txtNumericValue.Text, out i);
            if (result == false)
            {
                MessageBox.Show("Please enter numeric value");
                txtNumericValue.Text = String.Empty;
                return;
            }
            AddSymbol(txtNumericValue.Text);
        }

        private void txtName_TextChanged(Object sender, EventArgs e)
        {
            if (txtName.Text.Length != 0)
            {
                gbEquation.Visible = true;
                gbParameters.Visible = true;
            }
        }

       
    }
}

