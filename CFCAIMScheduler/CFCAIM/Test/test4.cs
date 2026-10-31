using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.Drawing;
using System.Data;	  
using DevExpress.XtraGrid.Drawing;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;




namespace CFCAIM
{
    public partial class test4 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
		List<DataSourceTypeInfo> _dataUser = new List<DataSourceTypeInfo>();

		

		public test4()
		{
			InitializeComponent();
		}

		private void testcm4_Load(Object sender, EventArgs e)
		{
			/*
			string dateString, format;
			DateTime result;
			CultureInfo provider = CultureInfo.InvariantCulture;


			dateString = "01/13/2017";
			//dateString = "13/01/2017";
			format = "d";
			provider = new CultureInfo("fr-FR");
			try
			{
				result = DateTime.ParseExact(dateString, format, provider);
				//Console.WriteLine("{0} converts to {1}.", dateString, result.ToString());
			}
			catch (FormatException)
			{
				//Console.WriteLine("{0} is not in the correct format.", dateString);
			}
			*/

			//try
			//{

			//String dateString = "13/01/2015";


			//String format = "g";
			//CultureInfo provider = CultureInfo.InvariantCulture;
			//provider = new CultureInfo("fr-FR");
			//DateTime result;
			//result = DateTime.ParseExact(dateString, format, provider);

			//	//double dbt = double.Parse(dateString);
			//	//DateTime dtconv = DateTime.FromOADate(dbt);
			//	//String strDtConv = dtconv.ToString();
			//	//DateTime dt = DateTime.Parse(strDtConv);


			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}


			//rbAll.Checked = true;
			//DataBindDataSource("All", 0);
			//lueDataSource.EditValue = 0;

			//List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
			//analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue);
			//grdMaterial.DataSource = analyzerMaterial;


		}

        private void DataBindDataSource(String type, Int32 id)
		{
			if (AIMGlobal.importDataBindDataSourceType != "")
			{
				type = AIMGlobal.importDataBindDataSourceType;
			}

			//--------------------------------------------------------	
			if (type == "All")
			{
				_dataUser = AIMManager.GetDataSourceType(true);
			}
			if (type == "System")
			{
				_dataUser = AIMManager.GetDataSourceTypeByUserDefinedSwitch(false);
			}
			if (type == "UserDefined")
			{
				_dataUser = AIMManager.GetDataSourceTypeByUserDefinedSwitch(true);
			}

			//--------------------------------------------------------			
			if (type == "All")
			{
				_dataUser.Insert
			 (
				 0,
					new DataSourceTypeInfo
					{
						DataSourceTypeID = 0,
						Name = "--Select ALL--",
					}
				);
			}			
			else
			{
				_dataUser.Insert
			  (
				  0,
				  new DataSourceTypeInfo
				  {
					  DataSourceTypeID = 0,
					  Name = "--Select--"
				  }
			  );
			}

			//--------------------------------------------------------
			lueDataSource.Properties.DataSource = _dataUser;
			XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSource.Properties.Columns["TypeDisplay"].Caption = "Type";
			lueDataSource.BackColor = Color.White;

			//4/17/17
			lueDataSource.EditValue = id;


			if ((Int32)lueDataSource.EditValue == 0)
			{
				lueDataSource.BackColor = Color.LightYellow;
			}
			if (_dataUser.Count == 2)
			{
				//selecting value if there is only one
				lueDataSource.EditValue = _dataUser[1].DataSourceTypeID;
			}

			//--------------------------------------------------------
			//show or hide tab control
			if ((type == "System" || type == "UserDefined") && (Int32)lueDataSource.EditValue == 0)
			{
				tabControl1.Visible = false;
			}
			else
			{
				tabControl1.Visible = true;
			}

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

		private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
		{
			tabControl1.Visible = true;									
			DataBindGrid();									
			lueDataSource.BackColor = Color.White;

			if ((Int32)lueDataSource.EditValue != 0)
			{		   			
				if (checkUserDefinedSwitch() == true)
				{
					rbUserDefined.Checked = true;
				}	   
				else
				{
					rbSystem.Checked = true;
				}
			}
		}

		private void DataBindGrid()
		{
			DataBindGrid_Material();			
		}
		private void DataBindGrid_Material()
		{
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceTypeMat.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceTypeMat, "DataSourceTypeID", "Name", "TypeDisplay");
			List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{
					analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue, true);
				}
				else
				{  
					analyzerMaterial = AIMManager.GetAnalyzerMaterial(true);	
				}
				Int32 currentRowNum = 0;
				if (grdMaterialView != null)
				{
					Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdMaterial.DataSource = analyzerMaterial;
				grdMaterialView.FocusedRowHandle = currentRowNum;
			}
		}

		private Boolean checkUserDefinedSwitch()
		{
			Boolean rtn = false;

			DataSourceTypeInfo type = new DataSourceTypeInfo();
			type = AIMManager.GetDataSourceType_DataSourceTypeID((Int32)lueDataSource.EditValue);
			if (type != null)
			{
				if (type.IsUserDefined == true)
				{
					rtn = true;
				}
			}

			return rtn;
		}

		private void rbAll_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{

				if (rbAll.Checked == true)
				{
					lueDataSource.EditValue = 0;
					DataBindDataSource("All", 0);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void rbUserDefined_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{		 
				if (rbUserDefined.Checked == true)
				{
					//if a datasource has already been selected, see if it's user-defined or system
					if ((Int32)lueDataSource.EditValue != 0)
					{
						//currently system datasource is selected
						if (checkUserDefinedSwitch() == false)
						{ 
							lueDataSource.EditValue = 0;
							DataBindDataSource("UserDefined", (Int32)lueDataSource.EditValue);
							lueDataSource.ShowPopup();						
						}
						else
						{
							DataBindDataSource("UserDefined", (Int32)lueDataSource.EditValue);
						}
					}
					else
					{
						DataBindDataSource("UserDefined", 0);
						lueDataSource.ShowPopup();
					}
				}
				else
				{
					lueDataSource.EditValue = 0;
					DataBindDataSource("System", 0);
					lueDataSource.ShowPopup();								
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		   		

		private void rbSystem_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{
				if (rbSystem.Checked == true)
				{
					//if a datasource has already been selected, see if it's user-defined or system
					if ((Int32)lueDataSource.EditValue != 0)
					{
						//currently user-defined datasource is selected
						if (checkUserDefinedSwitch() == true)
						{							
							DataBindDataSource("System", (Int32)lueDataSource.EditValue);
							lueDataSource.ShowPopup();		 
						}
						else
						{
							DataBindDataSource("System", (Int32)lueDataSource.EditValue);	
						}
					}
					else
					{
						DataBindDataSource("System", 0);
						lueDataSource.ShowPopup();
					}
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repositoryItemPictureEdit1_Click(Object sender, EventArgs e)
		{

		}

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void toggleFirstRowItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
