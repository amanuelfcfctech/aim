using System;
using System.Collections.Generic;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Windows.Forms;
using System.Drawing;

namespace CFCAIM
{
	public partial class MathTypeEquation : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<MathTypeInfo> _mathType = new List<MathTypeInfo>();		
		Int32 _cursorPos = 0;

		public MathTypeEquation()
		{
			InitializeComponent();
		}

		private void MathTypeEquation_Load(Object sender, EventArgs e)
		{
			MathTypeInfo def = new MathTypeInfo();

			if (AIMGlobal.mathTypeID == 0)
			{
				txtName.Text = AIMGlobal.txtMathTypeName;
				txtBuild.Text = AIMGlobal.txtMathTypeEquation;
				
			}
			else
			{
				_mathType = AIMManager.GetMathTypeID(AIMGlobal.mathTypeID);
				if (_mathType != null)
				{
					txtName.Text = _mathType[0].MathTypeName;
					txtBuild.Text = _mathType[0].Equation;
				}
			}
			SetToolTips();

            //6/14/17
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

        }

		private void btnExit_Click(Object sender, EventArgs e)
		{
			try
			{					
				List<MathTypeInfo> type = new List<MathTypeInfo>();
				type = AIMManager.GetMathTypeID(AIMGlobal.mathTypeID);
				if (type[0].Equation != txtBuild.Text)
				{
					DialogResult dr;
					dr = MessageBox.Show("Changes have not been saved. Close form without saving?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr == DialogResult.No)
					{
						return;
					}
				}			

				Close();
				MathType mathType;
				AIMGlobal.open_form(mathType = new MathType());			


            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSave_Click(Object sender, EventArgs e)
		{
			if (AIMGlobal.mathTypeID == 0) 
			{
				if (lblResult.Text == "Error" )
				{
					AIMGlobal.txtMathTypeEquation = "Error";
				}
				else
				{
					AIMGlobal.txtMathTypeEquation = txtBuild.Text;					
				}
				AIMGlobal.testNewMathTypeEquation = true;

			}
			else
			{
				MathTypeInfo def = new MathTypeInfo();
				def.MathTypeID = AIMGlobal.mathTypeID;
				def.MathTypeName = txtName.Text;
				def.Equation = txtBuild.Text;
				def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				def.IsSystem = false;
				def.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveMathType(ref def);
			}
			
		}

		private void AddSymbol(String symbol)
		{
			String leftChars = "";
			String rightChars = "";						
			
			leftChars = txtBuild.Text.Substring(0, _cursorPos);
			rightChars = txtBuild.Text.Substring(_cursorPos, txtBuild.Text.Length - leftChars.Length);  				 				
			txtBuild.Text = leftChars + symbol + rightChars;										
			
			_cursorPos = _cursorPos + symbol.Length;														  											
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

		private void btnParenFront_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnParenFront.Text);
            
        }
        private void btnParenBack_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnParenBack.Text);
        }
        private void btnPlus_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnPlus.Text);
        }
        private void btnMinus_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnMinus.Text);
        }
        private void btnMultiply_Click(Object sender, EventArgs e)
        {
            AddSymbol("*");
        }
        private void btnDivide_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnDivide.Text);
        }

        private void btnMean_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnMean.Text);
        }

        private void btnSD_Click(Object sender, EventArgs e)
        {
            AddSymbol(btnSD.Text);
        }

		private void btnRemove_Click(Object sender, EventArgs e)
		{
			if (txtBuild.Text.Length > 0)
			{
				txtBuild.Text = txtBuild.Text.Substring(0, txtBuild.Text.Length - 1);				
			}
		 
		}

		private void btnTest_Click(Object sender, EventArgs e)
		{	   
            
			Double dbl = 0;
			BuildEval();

			if (txtEval.Text == "")
			{
				lblResult.Text = "Empty";
				btnSave.Enabled = true;
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
				btnSave.Enabled = true;
			}
			

			// TODO - CHECK THIS:  https://www.codeproject.com/Articles/18481/Dynamic-formula-calculator-evaluator-in-VB-NET		
		}

		private void BuildEval()
		{				
			String str = txtBuild.Text;
			str = str.Replace("MEAN", txtMean.Text);
			str = str.Replace("SD", txtSD.Text);
			txtEval.Text = str;
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
				MessageBox.Show(ex.Message + "- There is something wrong with the expression.");
				rtn = -9999;

			}
			return rtn;
		}

		private void txtBuild_TextChanged(Object sender, EventArgs e)
		{
			btnSave.Enabled = false;
		}
	
		private void txtBuild_Leave(Object sender, EventArgs e)
		{			
			_cursorPos = txtBuild.SelectionStart;			
		}

		private void txtBuild_MouseLeave(Object sender, EventArgs e)
		{

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
			txtBuild.Text = "";
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/14/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void MathTypeEquation_SizeChanged(Object sender, EventArgs e)
        {
            //6/14/17
            btnResize.Visible = true;
        }
    }
}
