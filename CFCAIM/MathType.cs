using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CFCAIM
{
	public partial class MathType : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        List<MathTypeInfo> _mathType = new List<MathTypeInfo>();        
		MathTypeEquation mathTypeEquation;
		SystemSettings systemSettings;
		
		
		public MathType()
        {
            InitializeComponent();
        }

        private void MathType_Load(Object sender, EventArgs e)
        {
            try
            {
				DataBind();
				SetToolTips();

				if (AIMGlobal.testNewMathTypeEquation == true)
				{
					txtName.Text = AIMGlobal.txtMathTypeName;			
					txtEquation.Text = AIMGlobal.txtMathTypeEquation;
					AIMGlobal.testNewMathTypeEquation = false;

					if (txtEquation.Text != "Error")
					{
						btnAddMathType.Enabled = true;
						timer1.Stop();
					}		  									
				}

                //6/14/17
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
				

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
			
        private void DataBind()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdMathTypeView.FocusedRowHandle.ToString(), out rowNum);
            grdMathType.DataSource = AIMManager.GetMathTypeSorted(true);
            grdMathTypeView.FocusedRowHandle = rowNum;
            _mathType = AIMManager.GetMathTypeSorted(true);	   
        }													   	   
       
		private void repSave_Click(Object sender, EventArgs e)
		{
			AIMGlobal.mathTypeID = (Int32)grdMathTypeView.GetFocusedRowCellValue(colMathTypeID);
			Close();						
			AIMGlobal.open_form(mathTypeEquation = new MathTypeEquation());
			
        }

		private void btnSave_Click(Object sender, EventArgs e)
		{
			//not used
			//MathTypeInfo def = new MathTypeInfo();
			//def.MathTypeID = (Int32)grdMathTypeView.GetFocusedRowCellValue(colMathTypeID);
			//def.MathTypeName = (String)grdMathTypeView.GetFocusedRowCellValue(colMathTypeName);
			////def.IsActive = (Boolean)grdMathTypeView.GetFocusedRowCellValue(colDelete1);
			//def.CFCUserID = (Int32)grdMathTypeView.GetFocusedRowCellValue(colCFCUserID);
			//def.IsSystem = false;			
			//ErrorInfo error = new ErrorInfo();
			//error = AIMManager.SaveMathType(ref def);
		}

		private void btnExit_Click(Object sender, EventArgs e)
		{
			try
			{
				Close();
				AIMGlobal.open_form(systemSettings = new SystemSettings());

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnRefresh_Click(Object sender, EventArgs e)
		{
			DataBind();
			txtName.Text = "";
			txtEquation.Text = "";
			timer1.Stop();
			txtEquation.BackColor = SystemColors.Control;

		}

		private void btnAddMathType_Click(Object sender, EventArgs e)
		{
			//not used
			//try
			//{					   
			//	if (txtName.Text.Trim().Length == 0)
			//	{
			//		MessageBox.Show("Please enter a Name. ", "Warning...", MessageBoxButtons.OK);
			//		txtName.Focus();
			//		return;
			//	}

			//	if (txtEquation.Text.Trim().Length == 0)
			//	{
			//		MessageBox.Show("Please enter an Equation. ", "Warning...", MessageBoxButtons.OK);
			//		txtEquation.Focus();
			//		return;
			//	}									
					
			//	MathTypeInfo mathType = new MathTypeInfo();
			//	mathType.MathTypeID = 0;
			//	mathType.MathTypeName = txtName.Text;
			//	mathType.Equation = txtEquation.Text;
			//	mathType.IsActive = true;
			//	mathType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	mathType.IsSystem = false;
					
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveMathType(ref mathType);
			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	txtName.Text = String.Empty;
			//	txtEquation.Text = String.Empty;
			//	btnAddMathType.Enabled = false;
			//	DataBind();
					
			//	}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}

		private void btnPreviewGridMathType_Click(Object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdMathType, "User Defined Proposed Value Equations");
				CFCAIMCore.Global.previewGridResults(grdMathType, "User Defined Proposed Value Equations");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDelete_Click(Object sender, EventArgs e)
		{
			try
			{	  
				DialogResult dr;
				dr = MessageBox.Show("Delete Equation?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.No)
				{	 
					MathTypeInfo mathType = new MathTypeInfo();
					mathType = (MathTypeInfo)grdMathTypeView.GetFocusedRow();
					mathType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					AIMManager.DeleteMathType(mathType.MathTypeID);
					DataBind();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repEdit1_Click(Object sender, EventArgs e)
		{
			AIMGlobal.mathTypeID = (Int32)grdMathTypeView.GetFocusedRowCellValue(colMathTypeID);
			Close();
			AIMGlobal.open_form(mathTypeEquation = new MathTypeEquation());

        }

		private void btnTextEquation_Click(Object sender, EventArgs e)
		{
			//not used
			//AIMGlobal.mathTypeID = 0;
			//AIMGlobal.txtMathTypeName = txtName.Text;
			//AIMGlobal.txtMathTypeEquation = txtEquation.Text;
			//Close();
			//AIMGlobal.open_form(mathTypeEquation = new MathTypeEquation());
		}

		private void timer1_Tick(Object sender, EventArgs e)
		{
			if (btnTextEquation.BackColor == SystemColors.Control)
			{
				btnTextEquation.BackColor = SystemColors.Info;
			}
			else
			{
				btnTextEquation.BackColor = SystemColors.Control;
			}
		}

		private void txtEquation_Validated(Object sender, EventArgs e)
		{
			//not used
			//timer1.Start();
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
				ttip.SetToolTip(btnPreviewGridMathType, "Preview Equations");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdMathTypeView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				MathTypeInfo mathType = new MathTypeInfo();
				mathType = (MathTypeInfo)grdMathTypeView.GetFocusedRow();
				mathType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;				
				AIMManager.SaveMathType(ref mathType);							
				DataBind();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/14/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void MathType_SizeChanged(Object sender, EventArgs e)
        {
            //6/14/17
            btnResize.Visible = true;
        }
    }
}

