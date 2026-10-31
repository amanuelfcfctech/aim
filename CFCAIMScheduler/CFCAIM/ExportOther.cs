using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.Xml;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace CFCAIM
{
    public partial class ExportOther : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<SampleExportTemplateInfo> _exportTemplate = new List<SampleExportTemplateInfo>();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		Boolean _saveAs = false;		
		List<SampleExportHeaderInfo> _sample = new List<SampleExportHeaderInfo>();
		List<SampleExportDetailInfo> _detail = new List<SampleExportDetailInfo>();
		Boolean _firstTime = true;

		public ExportOther()
		{
			InitializeComponent();
		}

		private void ExportOther_Load(Object sender, EventArgs e)
		{
			try
			{
				DataBindLue();				
				DataBindCriteriaLue();
				DataBindOwnerLocationlue();
				DataBindSupplierLocationlue();
				SetToolTips();
				WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
			

		private void DataBindCriteriaLue()
		{
			//SAMPLE DATE -----------------------------------------------------------------
			List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
			queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);
			queryOperation.Insert
			(
				0,
				new QueryOperationInfo
				{
					QueryOperationID = 0,
					Operation = "--Select--"
				}
			);
			lueOperator.Properties.DataSource = queryOperation;
			XtraEditors.LoadLookUpEdit(ref lueOperator, "QueryOperationID", "Operation");
			lueOperator.EditValue = 0;


			//MATERIAL --------------------------------------------------------------------
			List<AIMMaterialInfo> AimMaterial = new List<AIMMaterialInfo>();
			AimMaterial = AIMManager.GetAIMMaterial(true);
			AimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
			);
			lueMaterialCode.Properties.DataSource = AimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueMaterialName.Properties.DataSource = AimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
						
			//NUTRIENT --------------------------------------------------------------------

			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			aimNutrient = AIMManager.GetAllAIMNutrient(true);
			aimNutrient.Insert
			(
				0,
				new AIMNutrientInfo
				{
					AIMNutrientID = 0,
					AIMNutrientCode = "--Select--",
					AIMNutrientName = "--Select--"
				}
			);
			lueNutrientCode.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			lueNutrientName.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");
		 
			//OWNER -----------------------------------------------------------------------
			List<AIMOwnerInfo> AimOwner = new List<AIMOwnerInfo>();
			AimOwner = AIMManager.GetAIMOwner(true);
			AimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--",
					OwnerName = "--Select--"
				}
			);
			lueOwnerCode.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode", "OwnerName");
			lueOwnerName.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName", "OwnerCode");

			lueOwnerCode_Loc.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode_Loc, "AIMOwnerID", "OwnerCode", "OwnerName");
			lueOwnerCode_Loc.Properties.Columns["OwnerCode"].Visible = true;

			lueOwnerCode.EditValue = 0;
			lueOwnerCode_Loc.EditValue = 0;
			if (AimOwner.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerCode.EditValue = AimOwner[1].AIMOwnerID;
				lueOwnerCode_Loc.EditValue = AimOwner[1].AIMOwnerID;
			}

			lueOwnerName_Loc.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName_Loc, "AIMOwnerID", "OwnerName", "OwnerCode");
			lueOwnerName_Loc.Properties.Columns["OwnerName"].Visible = true;

			//SUPPLIER --------------------------------------------------------------------
			List<AIMSupplierInfo> AimSupplier = new List<AIMSupplierInfo>();
			AimSupplier = AIMManager.GetSupplier(true);
			AimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);
			lueSupplierCode.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierCode, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

			lueSupplierName.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

			lueSupplierCode_Loc.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierCode_Loc, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueSupplierCode_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			lueSupplierName_Loc.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierName_Loc, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueSupplierCode_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			
		}
		private void DataBindOwnerLocationlue()
		{
			//OWNER LOCATION -------------------------------------------------------------
			Int32 owner = 0;

			//------------------------------------------------------
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			if (lueOwnerName_Loc.EditValue != null)
			{
				Int32.TryParse(lueOwnerName_Loc.EditValue.ToString(), out owner);
			}
			aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner);
			aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationCode = "--Select--",
					LocationName = "--Select--"
				}
			);

			lueOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");
			lueOwnerLocationName.Properties.Columns["LocationName"].Visible = true;


			lueOwnerLocationCode.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationCode, "AIMOwnerLocationID", "LocationCode", "LocationName");
			lueOwnerLocationCode.Properties.Columns["LocationCode"].Visible = true;


			if (aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one                
				lueOwnerLocationCode.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}
			else
			{
				lueOwnerLocationCode.EditValue = 0;
			}
		}
		private void DataBindSupplierLocationlue()
		{
			//SUPPLIER LOCATION -------------------------------------------------------------
			Int32 supplier = 0;

			//------------------------------------------------------
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			if (lueSupplierName_Loc.EditValue != null)
			{
				Int32.TryParse(lueSupplierName_Loc.EditValue.ToString(), out supplier);
			}
			aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierLocationCode = "--Select--",
					SupplierLocationName = "--Select--"
				}
			);

			lueSupplierLocationCode.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationCode, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueSupplierLocationCode.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			lueSupplierLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			if (aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueSupplierLocationCode.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueSupplierLocationCode.EditValue = aimSupplierLocation;
			}
		}

		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		
		private void SetToolTips()
		{
			try
			{
				this.Icon = (Icon)Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				
				ttip.SetToolTip(btnNewExportTemplate, "New Template");
				ttip.SetToolTip(btnCancelQueryTemplateSave, "Cancel ");
				ttip.SetToolTip(btnSaveExportTemplateAs, "Save Template As");
				ttip.SetToolTip(btnDeleteQueryTemplate, "Delete Template");
				ttip.SetToolTip(btnSave, "Save Template");
				ttip.SetToolTip(btnCancelQueryTemplateSave, "Cancel ");
				ttip.SetToolTip(btnPreview, "Get Export Data");
				ttip.SetToolTip(btnExport, "Export Data");
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

				ttip.SetToolTip(btnAddMaterial, "Add Material to Query");
				ttip.SetToolTip(btnAddNutrient, "Add Nutrient to Query");
				ttip.SetToolTip(btnAddOwner, "Add Owner to Query");
				ttip.SetToolTip(btnAddOwnerLocation, "Add Owner Location to Query");
				ttip.SetToolTip(btnAddSupplier, "Add Supplier to Query");
				ttip.SetToolTip(btnAddSupplierLocation, "Add Supplier Location to Query");
				ttip.SetToolTip(btnAddSampleDate, "Add Sample Date to Query");				
				ttip.SetToolTip(btnRemoveSampleDate, "Remove Sample Date from Query");

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
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private void ExportOther_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}

		

		private void btnPrintPreview_Click(Object sender, EventArgs e)
		{
            if ((Int32)lueExportTemplate.EditValue == 0)
            {
                MessageBox.Show("Please select a template.", "Information...");
                lueExportTemplate.Focus();
                return;
            }

            //9/5/19
            tabControl1.SelectedIndex = 1;
            AIMGlobal.SplashScreenShow();

            DataBindGrid();

            //9/5/19
            AIMGlobal.SplashScreenHide();

        }

        

        private void btnExport_Click(Object sender, EventArgs e)
		{
			try
			{

				List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
				List<SampleExportHeaderInfo> sample = new List<SampleExportHeaderInfo>();
				sample = (List<SampleExportHeaderInfo>)grdSampleView.DataSource;

				if (sample == null)
				{
					return;
				}
				List<SampleExportDetailInfo> results = new List<SampleExportDetailInfo>();


				//Make sure there are "accepted" samples to export before exporting												
				DialogResult dr = new DialogResult();
				var samples = from g in sample
							  where g.ExportStatus == 1
							  select new
							  {
								  g.ExportStatus
							  };
				if (samples.Count() == 0)
				{
					AIMGlobal.SplashScreenHide();
					dr = MessageBox.Show("No Samples have been accepted. Nothing to export.", "Information ...");
					return;
				}


                String path = txtExportPath.Text;
                String pathAndFileName = txtExportPath.Text + "\\" + txtExportFileName.Text + "." + grdSample.Text;
                				if (sample != null)
				{
					AIMGlobal.SplashScreenShow();

                    #region CSV

                    //5/15/20 - changed to use correct ENUM
                    //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
                    if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.CSV)

					{
						//----------------------------------  CSV	
						System.IO.StreamWriter file = new System.IO.StreamWriter(pathAndFileName);

						//header
						var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17} ",
									"SampleDate", "SampleID", "AimMaterialCode",
									"AimOwnerCode", "AimOwnerLocationCode", "AimSupplierCode", "AimSupplierLocationCode",
									"SampleCode", "AlternateCode", "BillOfLoad", "ContractNumber",
									"InvoiceNumber", "LoadNumber", "LotNumber", "PoNumber",
									"TicketNumber", "VehicleNumber", "MaterialTypeID"
									);
						file.WriteLine(line);
												
						foreach (var item in sample)
						{
							if (item.ExportStatus == 1)
							{			   
								var sampleDate = item.SampleDate;
								var sampleID = item.SampleID;
								var aimMaterialCode = item.AIMMaterialCode;
								var aimOwnerCode = item.AIMOwnerCode;
								var aimOwnerLocationCode = item.AIMOwnerLocationCode;
								var aimSupplierCode = item.AIMSupplierCode;
								var aimSupplierLocationCode = item.AIMSupplierLocationCode;								
								var sampleCode = item.SampleCode;
								var alternateCode = item.AlternateCode;
								var billOfLoad = item.BillOfLoad;
								var contractNumber = item.ContractNumber;
								var invoiceNumber = item.InvoiceNumber;
								var loadNumber = item.LoadNumber;
								var lotNumber = item.LotNumber;
								var poNumber = item.PoNumber;
								var ticketNumber = item.TicketNumber;
								var vehicleNumber = item.VehicleNumber;
								var materialTypeID = item.MaterialTypeID;

								line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17} ",
										sampleDate, sampleID, aimMaterialCode, aimOwnerCode, aimOwnerLocationCode,
										aimSupplierCode, aimSupplierLocationCode,
										sampleCode, alternateCode, billOfLoad, contractNumber, invoiceNumber, loadNumber,
										lotNumber, poNumber, ticketNumber, vehicleNumber, materialTypeID);

								if (item.DetailExport != null)
								{
									foreach (var result in item.DetailExport)
									{
										line = line + "," + result.AnalyzerNutrientCode;
										line = line + "," + result.ResultValue.ToString("G29");
										line = line + "," + result.Unit;
									}
								}

								if (item.ExportStatus == 1)
								{
									file.WriteLine(line);
								}
								file.Flush();
							}
						}
						file.Close();
					}
                    #endregion

                    #region XML
                    //------------------------------------------------------------------------------

                    //5/15/20 - changed to use correct ENUM
                    //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.XML)
                    if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XML)

                    {
                        XmlTextWriter writer = new XmlTextWriter(txtExportPath.Text + "\\" + txtExportFileName.Text + ".xml", System.Text.Encoding.UTF8);
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 1;

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("Samples");

                        writer.WriteElementString("ExportName", "CFCTechExportXML");
                        writer.WriteElementString("ExportDate", DateTime.Now.ToString());

                        foreach (var item in sample)
                        {
                            if (item.ExportStatus == 1)
                            {
                                
                                writer.WriteStartElement("Sample");

                                writer.WriteElementString("SampleID", item.SampleID.ToString());
                                writer.WriteElementString("SampleDate", item.SampleDate.ToString());
                                writer.WriteElementString("AIMMaterialCode", item.AIMMaterialCode.ToString());
                                writer.WriteElementString("AIMOwnerCode", item.AIMOwnerCode.ToString());
                                writer.WriteElementString("AIMOwnerLocationCode", item.AIMOwnerLocationCode.ToString());
                                writer.WriteElementString("AIMSupplierCode", item.AIMSupplierCode.ToString());
                                writer.WriteElementString("AIMSupplierLocationCode", item.AIMSupplierLocationCode.ToString());
                                writer.WriteElementString("SampleCode", item.SampleCode);
                                writer.WriteElementString("AlternateCode", item.AlternateCode);
                                writer.WriteElementString("BillOfLoad", item.BillOfLoad);
                                writer.WriteElementString("ContractNumber", item.ContractNumber);
                                writer.WriteElementString("InvoiceNumber", item.InvoiceNumber);
                                writer.WriteElementString("LoadNumber", item.LoadNumber);
                                writer.WriteElementString("LotNumber", item.LotNumber);
                                writer.WriteElementString("PoNumber", item.PoNumber);
                                writer.WriteElementString("TicketNumber", item.TicketNumber);
                                writer.WriteElementString("VehicleNumber", item.VehicleNumber);
                                writer.WriteElementString("MaterialTypeID", item.MaterialTypeID.ToString());
                                                                
                                sampleResult = AIMManager.GetAllSampleResult(item.SampleID);
                                foreach (var result in sampleResult)
                                {
                                    writer.WriteStartElement("Results");
                                    writer.WriteElementString("SampleResultID", result.SampleResultID.ToString());
                                    writer.WriteElementString("AnalyzerNutrientCode", result.AnalyzerNutrientCode.ToString());
                                    if (result.AIMNutrientName == null)
                                    {
                                        writer.WriteElementString("AIMNutrientName", " ");
                                    }
                                    else
                                    {
                                        writer.WriteElementString("AIMNutrientName", result.AIMNutrientName.ToString());
                                    }
                                    writer.WriteElementString("ResultValue", result.ResultValue.ToString());
                                    writer.WriteEndElement();

                                }                                
                                writer.WriteEndElement();                                                              
                            }                            
                        }
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();

                        /*
                                                 
						XmlDocument doc = new XmlDocument();
						XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
						doc.AppendChild(docNode);

						XmlNode samplesNode = doc.CreateElement("Samples");
						doc.AppendChild(samplesNode);

						XmlNode exportName = doc.CreateElement("ExportName");
						exportName.AppendChild(doc.CreateTextNode("CFCTechExportXML"));	 
						samplesNode.AppendChild(exportName);

						XmlNode exportDateNode = doc.CreateElement("ExportDate");
						exportDateNode.AppendChild(doc.CreateTextNode(DateTime.Now.ToString()));
						samplesNode.AppendChild(exportDateNode);

						foreach (var item in sample)
						{
							if (item.ExportStatus == 1)
							{
								XmlNode sampleNode = doc.CreateElement("Sample");
								XmlAttribute sampleAttribute = doc.CreateAttribute("SampleID");
								sampleAttribute.Value = item.SampleID.ToString();
								sampleNode.Attributes.Append(sampleAttribute);
								samplesNode.AppendChild(sampleNode);
                                
								XmlNode sampleDateNode = doc.CreateElement("SampleDate");
								sampleDateNode.AppendChild(doc.CreateTextNode(item.SampleDate.ToString()));
								sampleNode.AppendChild(sampleDateNode);																							

								XmlNode sampleMaterialNode = doc.CreateElement("AIMMaterialCode");
								sampleMaterialNode.AppendChild(doc.CreateTextNode(item.AIMMaterialCode.ToString()));
								sampleNode.AppendChild(sampleMaterialNode);

								XmlNode sampleOwnerNode = doc.CreateElement("AIMOwnerCode");
								sampleOwnerNode.AppendChild(doc.CreateTextNode(item.AIMOwnerCode.ToString()));
								sampleNode.AppendChild(sampleOwnerNode);

								XmlNode sampleOwnerLocNode = doc.CreateElement("AIMOwnerLocationCode");
								sampleOwnerLocNode.AppendChild(doc.CreateTextNode(item.AIMOwnerLocationCode.ToString()));
								sampleNode.AppendChild(sampleOwnerLocNode);

								XmlNode sampleSupplierNode = doc.CreateElement("AIMSupplierCode");
								sampleSupplierNode.AppendChild(doc.CreateTextNode(item.AIMSupplierCode.ToString()));
								sampleNode.AppendChild(sampleSupplierNode);

								XmlNode sampleSupplierLocationNode = doc.CreateElement("AIMSupplierLocationCode");
								sampleSupplierLocationNode.AppendChild(doc.CreateTextNode(item.AIMSupplierLocationCode.ToString()));
								sampleNode.AppendChild(sampleSupplierLocationNode);

								XmlNode sampleCodeNode = doc.CreateElement("SampleCode");
								sampleCodeNode.AppendChild(doc.CreateTextNode(item.SampleCode));
								sampleNode.AppendChild(sampleCodeNode);
																
								XmlNode sampleAlternateCode = doc.CreateElement("AlternateCode");
								sampleAlternateCode.AppendChild(doc.CreateTextNode(item.AlternateCode.ToString()));
								sampleNode.AppendChild(sampleAlternateCode);

								XmlNode sampleBillOfLoadNode = doc.CreateElement("BillOfLoad");
								sampleBillOfLoadNode.AppendChild(doc.CreateTextNode(item.BillOfLoad.ToString()));
								sampleNode.AppendChild(sampleBillOfLoadNode);

								XmlNode sampleContractNumberNode = doc.CreateElement("ContractNumber");
								sampleContractNumberNode.AppendChild(doc.CreateTextNode(item.ContractNumber.ToString()));
								sampleNode.AppendChild(sampleContractNumberNode);

								XmlNode sampleInvoiceNumberNode = doc.CreateElement("InvoiceNumber");
								sampleInvoiceNumberNode.AppendChild(doc.CreateTextNode(item.InvoiceNumber.ToString()));
								sampleNode.AppendChild(sampleInvoiceNumberNode);

								XmlNode sampleLoadNumberNode = doc.CreateElement("LoadNumber");
								sampleLoadNumberNode.AppendChild(doc.CreateTextNode(item.LoadNumber.ToString()));
								sampleNode.AppendChild(sampleLoadNumberNode);

								XmlNode sampleLotNumberNode = doc.CreateElement("LotNumber");
								sampleLotNumberNode.AppendChild(doc.CreateTextNode(item.LotNumber.ToString()));
								sampleNode.AppendChild(sampleLotNumberNode);

								XmlNode samplePoNumberNode = doc.CreateElement("PoNumber");
								samplePoNumberNode.AppendChild(doc.CreateTextNode(item.PoNumber.ToString()));
								sampleNode.AppendChild(samplePoNumberNode);

								XmlNode sampleTicketNumberNode = doc.CreateElement("TicketNumber");
								sampleTicketNumberNode.AppendChild(doc.CreateTextNode(item.TicketNumber.ToString()));
								sampleNode.AppendChild(sampleTicketNumberNode);

								XmlNode sampleVehicleNumberNode = doc.CreateElement("VehicleNumber");
								sampleVehicleNumberNode.AppendChild(doc.CreateTextNode(item.VehicleNumber.ToString()));
								sampleNode.AppendChild(sampleVehicleNumberNode);

								XmlNode sampleMaterialTypeNode = doc.CreateElement("MaterialTypeID");
								sampleMaterialTypeNode.AppendChild(doc.CreateTextNode(item.MaterialTypeID.ToString()));
								sampleNode.AppendChild(sampleMaterialTypeNode);

								sampleResult = AIMManager.GetAllSampleResult(item.SampleID);
								foreach (var result in sampleResult)
								{
									XmlNode resultNode = doc.CreateElement("Results");
									XmlAttribute resultAttribute = doc.CreateAttribute("SampleID");
									resultAttribute.Value = result.SampleID.ToString();
									resultNode.Attributes.Append(resultAttribute);
									sampleNode.AppendChild(resultNode);

									XmlNode resultSampleResultIDNode = doc.CreateElement("SampleResultID");
									resultSampleResultIDNode.AppendChild(doc.CreateTextNode(result.SampleResultID.ToString()));
									resultNode.AppendChild(resultSampleResultIDNode);

									XmlNode resultAnalyzerNutrientCodeNode = doc.CreateElement("AnalyzerNutrientCode");
									resultAnalyzerNutrientCodeNode.AppendChild(doc.CreateTextNode(result.AnalyzerNutrientCode.ToString()));
									resultNode.AppendChild(resultAnalyzerNutrientCodeNode);


									XmlNode resultAIMNutrientNameNode = doc.CreateElement("AIMNutrientName");
									if (result.AIMNutrientName == null)
									{
										resultAIMNutrientNameNode.AppendChild(doc.CreateTextNode(" "));
									}
									else
									{
										resultAIMNutrientNameNode.AppendChild(doc.CreateTextNode(result.AIMNutrientName.ToString()));
									}
									resultNode.AppendChild(resultAIMNutrientNameNode);

									XmlNode resultResultValueNode = doc.CreateElement("ResultValue");
									resultResultValueNode.AppendChild(doc.CreateTextNode(result.ResultValue.ToString()));
									resultNode.AppendChild(resultResultValueNode);

									XmlNode resultUnitTypeNode = doc.CreateElement("UnitType");
									resultUnitTypeNode.AppendChild(doc.CreateTextNode(result.UnitType.ToString()));
									resultNode.AppendChild(resultUnitTypeNode);
								}
							}                                                       

                        }
                        
                        doc.Save(txtExportPath.Text + "\\" + txtExportFileName.Text + ".xml");                        
                        */
                    }
                    #endregion

                    AIMGlobal.SplashScreenHide();
					MessageBox.Show("Export Complete", "Information...");

				}
			}
			catch (Exception ex)
			{
				AIMGlobal.SplashScreenHide();
				MessageBox.Show(ex.Message);
			}
}

		private void btnBrowseNew_Click(Object sender, EventArgs e)
		{
			fbdSelectedDirectory.ShowDialog();
			txtExportPathNew.Text = fbdSelectedDirectory.SelectedPath;
		}

		private void btnSave_Click(Object sender, EventArgs e)
		{
			try
			{				
				btnSave.BackColor = SystemColors.Control;

				if (txtTemplateNameNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter Template Name.");
					txtTemplateNameNew.Focus();
					return;
				}				
				if (txtExportFileNameNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter Export File Name.");
					txtExportFileNameNew.Focus();
					return;
				}
				if (txtExportPathNew.Text.Length == 0)
				{
					MessageBox.Show("Please select Export Path.");
					btnBrowseNew.Focus();
					return;
				}
				if ((Int32)lueFileTypeNew.EditValue == 0)
				{
					MessageBox.Show("Please select File Type.");
                    lueFileTypeNew.Focus();
					return;
				}


				SampleExportTemplateInfo export = new SampleExportTemplateInfo();
				export.TemplateName = txtTemplateNameNew.Text;

				//commented - this was for Marilyn's sample export
				//export.FileTypeID = (Int32)FileTypeEnum.CSV;
				//export.DelimiterTypeID = 1;
				//export.DelimiterTypeID = (Int32)lueDelimiterType.EditValue;

				export.FileTypeID = (Int32)lueFileTypeNew.EditValue;
				

				export.ExportPath = txtExportPathNew.Text;
				export.ExportFileName = txtExportFileNameNew.Text;				
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo outPut = AIMManager.SaveSampleExportTemplate(ref export);

				if (_saveAs == true)
				{
					AIMManager.CopyQueryTemplateDetail(outPut.RecordID, (Int32)lueExportTemplate.EditValue, userGlobal.CFCLoginUser.CFCUserID);
				}
				_saveAs = false;

				DataBindLue();
                _firstTime = true;
                lueExportTemplate.EditValue = outPut.RecordID;				

				txtTemplateNameNew.Text = String.Empty;
				txtExportFileNameNew.Text = String.Empty;
				txtExportPathNew.Text = String.Empty;
                lueFileTypeNew.EditValue = 0;
				lueDelimiterType.EditValue = 0;

				//hide NEW panel
				pnlNewQueryTemplate.Visible = false;
				pnlNewQueryTemplate.Location = new Point(421,70);
				pnlNewQueryTemplate.Size = new Size(701,8);

				tabControl1.Visible = true;
				tabControlQuery.SelectedIndex = 0;   //Criteria tab


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void tabControlQuery_DrawItem(Object sender, DrawItemEventArgs e)
		{
			// This event is called once for each tab button in your tab control

			ErrorInfo countField = new ErrorInfo();


			// First paint the background with a color based on the current tab		

			#region Tab BACKGROUND           
			switch (e.Index)    // e.Index is the index of the tab in the TabPages collection.
			{
				case 0:         //SampleDate
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SampleDate);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;

				case 1:         //Material

					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Material);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}

					break;
				case 2:         //Nutrient
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.NutrientCode);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 3:         //Owner
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Owner);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 4:         //OwnerLocation
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 5:         //Supplier			
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Supplier);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 6:         //SupplierLocation
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 7:         //Material-Nutrient
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 8:         //Ingredient
					countField = AIMManager.CountQueryTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.IngredientCode);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				default:
					e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					break;
			}
			#endregion

			#region Tab TEXT
			// Then draw the current tab button text 
			Rectangle paddedBounds = e.Bounds;
			paddedBounds.Inflate(1, -1);
			//e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
			//e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, e.Bounds);
			e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
			#endregion
						
			CheckYellowBox();

		}

		private void CheckYellowBox()
		{
			//If template detail exists, show yellow box help text	   
			lblYellowBox.Visible = false;
			txtYellowBox.Visible = false;
			ErrorInfo countField = new ErrorInfo();
			countField = AIMManager.CountQueryTemplateTotal((Int32)lueExportTemplate.EditValue);
			if (countField.RecordID > 0)
			{
				lblYellowBox.Visible = true;
				txtYellowBox.Visible = true;
			}
		}

		private void lueOperator_EditValueChanged(Object sender, EventArgs e)
		{
			QueryOperationEnum queryOperation;
			Int32 operation = 0;

			//cm
			//lblValue.Visible = true;

			Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
			queryOperation = (QueryOperationEnum)operation;

			switch (queryOperation)
			{
				case QueryOperationEnum.PastNDaysToDate:
					lblDays.Visible = true;
					txtValue.Visible = true;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;
					break;

				case QueryOperationEnum.MonthToDate:
				case QueryOperationEnum.YearToDate:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;
					break;

				case QueryOperationEnum.Equal:
				case QueryOperationEnum.NotEqual:
				case QueryOperationEnum.GreaterThan:
				case QueryOperationEnum.LessThan:
				case QueryOperationEnum.GreaterThanEqual:
				case QueryOperationEnum.LessThanEqual:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = true;
					dtpSecondValue.Visible = false;
					break;
				case QueryOperationEnum.Between:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = true;
					dtpSecondValue.Visible = true;
					break;
				default:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;

					break;
			}
		}

		private void btnAddSampleDate_Click(Object sender, EventArgs e)
		{
			QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
			Int32 days = 0;
			QueryOperationEnum queryOperation;
			Int32 operation = 0;
						
			if ((Int32)lueOperator.EditValue == 0)
			{
				MessageBox.Show("Please select a Date Option.", "Message...", MessageBoxButtons.OK);
				lueOperator.Focus();
				lueOperator.ShowPopup();
				return;
			}
			if (dtpValue.Visible == true && dtpValue.Text.Length == 0)
			{
				MessageBox.Show("Please select a Date.", "Message...", MessageBoxButtons.OK);
				dtpValue.Focus();
				return;
			}
			if (txtValue.Visible == true)
			{
				Int32.TryParse(txtValue.Text, out days);
				if (days == 0)
				{
					MessageBox.Show("Please enter Number of Days (number must be greater than zero).", "Warning...");
					txtValue.Focus();
					txtValue.SelectAll();
					return;
				}
				else if (days < 0)
				{
					MessageBox.Show("Number of Days must be greater than zero.", "Warning...");
					txtValue.Focus();
					txtValue.SelectAll();
					return;
				}
			}

			//If SampleDate option exists for this query, delete it before adding a new sampleDate
			AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SampleDate);

			if (dtpValue.Visible == true)
			{
				queryTemplateDetail.DateValue = dtpValue.Value;

				if (dtpSecondValue.Visible == true)
				{
					queryTemplateDetail.SecondDate = dtpSecondValue.Value;
				}
			}
			else if (txtValue.Visible == true)
			{
				queryTemplateDetail.Value = days;
			}

			queryTemplateDetail.QueryTemplateDetailID = 0;
			queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
			queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SampleDate;
			Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
			queryOperation = (QueryOperationEnum)operation;
			queryTemplateDetail.QueryOperationID = (Int32)queryOperation;
			queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

			DataBindSampleDate();
			SetDateTextField(queryOperation, days);

			lueOperator.EditValue = 0;
			txtValue.Text = "";
		}
		 
		private void SetDateTextField(QueryOperationEnum queryOperation, Int32 days)
		{
			switch (queryOperation)
			{
				case QueryOperationEnum.PastNDaysToDate:
					txtDateOption.Text = days + " Days Past Current Date";
					break;
				case QueryOperationEnum.MonthToDate:
					txtDateOption.Text = "Month-To-Date";
					break;
				case QueryOperationEnum.YearToDate:
					txtDateOption.Text = "Year-To-Date";
					break;
				case QueryOperationEnum.Equal:
					txtDateOption.Text = "Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.GreaterThan:
					txtDateOption.Text = "Greater Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.LessThan:
					txtDateOption.Text = "Less Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.GreaterThanEqual:
					txtDateOption.Text = "Greater Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.LessThanEqual:
					txtDateOption.Text = "Less Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.NotEqual:
					txtDateOption.Text = "Not Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.Between:
					txtDateOption.Text = queryOperation.ToString() + " " + dtpValue.Value.Date.ToString("MM/dd/yyyy") + " and " + dtpSecondValue.Value.Date.ToString("MM/dd/yyyy");
					break;

				default:
					txtDateOption.Text = queryOperation.ToString();

					break;
			}
		}

		private void DataBindSampleDate()
		{
			List<QueryTemplateDetailInfo> sampleDate = new List<QueryTemplateDetailInfo>();
			sampleDate = AIMManager.GetAIMSampleDateForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SampleDate);
			btnRemoveSampleDate.Visible = false;
			if (sampleDate.Count > 0)
			{	
				if(sampleDate[0].StringDate != "")
				{
					dtpValue.Text = sampleDate[0].DateValue.ToString();
				}
								
				if (sampleDate[0].StringSecondDate != "")				
				{
					dtpSecondValue.Text = sampleDate[0].SecondDate.ToString();
				}

				btnRemoveSampleDate.Visible = true;
				List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
				queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);

				String dateText = "";
				dateText = queryOperation[sampleDate[0].QueryOperationID - 1].Operation;
				SetDateTextField((QueryOperationEnum)sampleDate[0].QueryOperationID, sampleDate[0].Value);
			}
		}

		private void lueExportTemplate_EditValueChanged(Object sender, EventArgs e)
		{
			{
				try
				{
					btnExport.Visible = false;
					lueFileType.EditValue = 0;
					lueFileType.BackColor = SystemColors.Control;
					timer1.Stop();

					tabControlQuery.Visible = false;					
					dtpValue.Text = "";
					dtpSecondValue.Text = "";
					txtDateOption.Text = "";

					txtExportFileName.Text = "";
					txtExportPath.Text = "";
					grdSample.Text = "";

					_saveAs = false;
					if (lueExportTemplate.EditValue != null)
					{

						if (tabControl1.SelectedIndex == 0)
						{
							tabControl1.SelectedIndex = 1;   //Preview and Export
						}

						//tabControl1.SelectedIndex = 0;            //Criteria tab
						if ((Int32)lueExportTemplate.EditValue != 0)
						{							
							tabControlQuery.Visible = true;
							//tabControl1.SelectedIndex = 1;		//Preview and Export

							SampleExportTemplateInfo exportTemplate = (SampleExportTemplateInfo)lueExportTemplate.GetSelectedDataRow();
							if (exportTemplate != null)
							{
								txtExportFileName.Text = exportTemplate.ExportFileName;
								txtExportPath.Text = exportTemplate.ExportPath;
								grdSample.Text = "CSV";							
							}
						}

						if ((Int32)lueExportTemplate.EditValue == 0)
						{
							btnSaveExportTemplateAs.Enabled = false;
						}
						else
						{
							btnSaveExportTemplateAs.Enabled = true;
						}

						btnDeleteQueryTemplate.Visible = true;
						
					}

                    if (_firstTime == true)
                    {
                        tabControl1.SelectedIndex = 0;
                    }
					else
					{
						AIMGlobal.SplashScreenShow();
						btnPreview.PerformClick();
						AIMGlobal.SplashScreenHide();  						
					}
					//////_firstTime = false;

					DataBindTemplateLue();
					DataBindTemplateGrids();
					DataBindSampleDate();
					
					CheckYellowBox();	  

					tabControlQuery.SelectedIndex = 0;			
					
					
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void DataBindTemplateGrids()
		{
			grdMaterial.DataSource = AIMManager.GetAIMMaterialForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Material);
			grdNutrient.DataSource = AIMManager.GetAIMNutrientForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.NutrientCode);
			grdOwner.DataSource = AIMManager.GetAIMOwnerForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Owner);
			grdSupplier.DataSource = AIMManager.GetAIMSupplierForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Supplier);
			grdOwnerLocation.DataSource = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
			grdSupplierLocation.DataSource = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
			
		}

		private void DataBindTemplateLue()
		{

			List<QueryFieldInfo> queryField = new List<QueryFieldInfo>();
			queryField = AIMManager.GetQueryField(true);
			queryField.Insert
			(
				0,
				new QueryFieldInfo
				{
					QueryFieldID = 0,
					Field = "--Select--"
				}
			);

			List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
			queryOperation = AIMManager.GetQueryOperation(true);
			queryOperation.Insert
			(
				0,
				new QueryOperationInfo
				{
					QueryOperationID = 0,
					Operation = "--Select--"
				}
			);					

		}

		private void btnNewExportTemplate_Click(Object sender, EventArgs e)
		{
			try
			{
				DataBindLue();
				pnlNewQueryTemplate.Visible = true;
				
				dtpValue.Text = "";
				dtpSecondValue.Text = "";
				txtDateOption.Text = "";

				//pnlNewQueryTemplate.Location = new Point(15,69);
                //pnlNewQueryTemplate.Size = new Size(701,175);


                pnlNewQueryTemplate.Location = new Point(15, 60);
                pnlNewQueryTemplate.Size = new Size(562, 120);


                


                pnlNewQueryTemplate.BringToFront();
							
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DataBindLue()
		{
			List<SampleExportTemplateInfo> exportTemplate = new List<SampleExportTemplateInfo>();
			_exportTemplate = AIMManager.GetSampleExportTemplate();
			_exportTemplate.Insert
				(
					0,
					new SampleExportTemplateInfo
					{
						SampleExportTemplateID = 0,
						TemplateName = "--Select--"
					}
				);
			lueExportTemplate.Properties.DataSource = _exportTemplate;
			XtraEditors.LoadLookUpEdit(ref lueExportTemplate, "SampleExportTemplateID", "TemplateName", "ExportPath");
			lueExportTemplate.Properties.Columns["ExportPath"].Visible = false;


			List<DataSourceTypeRefFileTypeInfo> fileType = new List<DataSourceTypeRefFileTypeInfo>();
			fileType = AIMManager.GetDataSourceTypeRefFileType((Int32)DataSourceTypeEnum.CFCTech);
			fileType.Insert
			(
				0,
				new DataSourceTypeRefFileTypeInfo
				{
					FileTypeID = 0,
					FileTypeName = "--Select --"
				}
			);

			lueFileType.Properties.DataSource = fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "FileTypeName");
			if (fileType.Count > 0)
			{
				lueFileType.EditValue = fileType[0].FileTypeID;
			}

            lueFileTypeNew.Properties.DataSource = fileType;
            XtraEditors.LoadLookUpEdit(ref lueFileTypeNew, "FileTypeID", "FileTypeName");
            if (fileType.Count > 0)
            {
                lueFileTypeNew.EditValue = fileType[0].FileTypeID;
            }


            btnDeleteQueryTemplate.Visible = false;
			AIMGlobal.SplashScreenHide();

		}

		private void btnCancelQueryTemplateSave_Click(Object sender, EventArgs e)
		{			

			pnlQueryButtons.Visible = true;
			pnlNewQueryTemplate.Visible = false;

			txtTemplateNameNew.Text = String.Empty;
			txtExportFileNameNew.Text = String.Empty;
			txtExportPathNew.Text = String.Empty;
			lueFileType.EditValue = 0;
			lueDelimiterType.EditValue = 0;
			_saveAs = false;

			tabControlQuery.Visible = true;
			CheckYellowBox();

		}

		private void btnDeleteQueryTemplate_Click(Object sender, EventArgs e)
		{

			if ((Int32)lueExportTemplate.EditValue == 0)
			{
				MessageBox.Show("Please select Export Template to delete.");
				lueExportTemplate.Focus();
				return;
			}				   

			DialogResult dialogResult = MessageBox.Show("Delete Export Template '" + lueExportTemplate.Text.ToString().TrimEnd() + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{
				Int32 sampleTemplateID = 0;
				sampleTemplateID = (Int32)lueExportTemplate.EditValue;
				AIMManager.DeleteSampleExportTemplate(sampleTemplateID);
				DataBindLue();
				MessageBox.Show("Template Deleted.");
			}
		}

		private Int32 ChecksBeforeAdding(String type, DevExpress.XtraEditors.LookUpEdit lue)
		{
			Int32 rtn = 0;
			
			if ((Int32)lue.EditValue == 0)
			{
				MessageBox.Show("Please select AIM " + type + ".", "Warning...");
				if (type == "Supplier Location" || type == "Owner Location") { }
				else
				{
					lue.ShowPopup();
				}
				rtn = -1;
			}

			//Check to see if item has already been selected
			switch (type)
			{
				case "Material":
					List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
					material = AIMManager.GetAIMMaterialForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Material, (Int32)lueMaterialCode.EditValue);
					if (material.Count != 0)
					{
						MessageBox.Show("This material has been selected.", "Information...");
						rtn = -1;
					}
					break;

				case "Nutrient":
					List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
					nutrient = AIMManager.GetAIMNutrientForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.NutrientCode, (Int32)lueNutrientCode.EditValue);
					if (nutrient.Count != 0)
					{
						MessageBox.Show("This nutrient has been selected.", "Information...");
						rtn = -1;
					}
					break;
				case "Owner":
					List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
					owner = AIMManager.GetAIMOwnerForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Owner, (Int32)lueOwnerCode.EditValue);
					if (owner.Count != 0)
					{
						MessageBox.Show("This owner has been selected.", "Information...");
						rtn = -1;
					}
					break;
				case "Owner Location":
					List<AIMOwnerLocationInfo> ownerLocation = new List<AIMOwnerLocationInfo>();
					ownerLocation = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.OwnerLocation, (Int32)lueOwnerLocationCode.EditValue);
					if (ownerLocation.Count != 0)
					{
						MessageBox.Show("This owner location has been selected.", "Information...");
						rtn = -1;
					}
					break;
				case "Supplier":
					List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
					supplier = AIMManager.GetAIMSupplierForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.Supplier, (Int32)lueSupplierCode.EditValue);
					if (supplier.Count != 0)
					{
						MessageBox.Show("This supplier has been selected.", "Information...");
						rtn = -1;
					}
					break;
				case "Supplier Location":
					List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
					supplierLocation = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SupplierLocation, (Int32)lueSupplierLocationCode.EditValue);
					if (supplierLocation.Count != 0)
					{
						MessageBox.Show("This supplier location has been selected.", "Information...");
						rtn = -1;
					}
					break;
					
				default:
					break;			
			}
			return rtn;
		}

		private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
		{
			if (from == null || to == null)
			{
				return;
			}
			if (to.EditValue == from.EditValue)
			{
				return;
			}
			to.EditValue = from.EditValue;
		}

		private void DeleteQueryTemplateDetail(Int32 queryTemplateDetailID)
		{
			try
			{			   
				AIMManager.DeleteQueryTemplateDetail(queryTemplateDetailID);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueMaterialCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialCode, ref lueMaterialName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueMaterialName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialName, ref lueMaterialCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddMaterial_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Material", lueMaterialCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Material;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND    

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueMaterialCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueMaterialCode.EditValue = 0;
				lueMaterialName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueMaterialCode_Click(Object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}

		private void lueMaterialCode_Enter(Object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}

		private void lueMaterialName_Click(Object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}

		private void lueMaterialName_Enter(Object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}

		private void repbtnDeleteMaterial_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
				aimMaterial = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();
				if (aimMaterial != null)
				{
					DeleteQueryTemplateDetail(aimMaterial.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueNutrientCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientCode, ref lueNutrientName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueNutrientName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientName, ref lueNutrientCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueNutrientCode_Click(Object sender, EventArgs e)
		{
			lueNutrientCode.SelectAll();
		}

		private void lueNutrientCode_Enter(Object sender, EventArgs e)
		{
			lueNutrientCode.SelectAll();
		}

		private void lueNutrientName_Click(Object sender, EventArgs e)
		{
			lueNutrientName.SelectAll();
		}

		private void lueNutrientName_Enter(Object sender, EventArgs e)
		{
			lueNutrientName.SelectAll();
		}

		private void btnAddNutrient_Click(Object sender, EventArgs e)
		{
			try
			{
				if (ChecksBeforeAdding("Nutrient", lueNutrientCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.NutrientCode;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
                //queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND 

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueNutrientCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueNutrientCode.EditValue = 0;
				lueNutrientName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteNutrient_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{	   
				AIMNutrientInfo aimNutrient = new AIMNutrientInfo();
				aimNutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
				if (aimNutrient != null)
				{
					DeleteQueryTemplateDetail(aimNutrient.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerCode, ref lueOwnerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerName, ref lueOwnerCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerCode_Click(Object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}

		private void lueOwnerCode_Enter(Object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}

		private void lueOwnerName_Click(Object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}

		private void lueOwnerName_Enter(Object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}

		private void btnAddOwner_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Owner", lueOwnerCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Owner;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
                //queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND 

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueOwnerCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueOwnerCode.EditValue = 0;
				lueOwnerName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteOwner_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{	   
				AIMOwnerInfo aimOwner = new AIMOwnerInfo();
				aimOwner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
				if (aimOwner != null)
				{
					DeleteQueryTemplateDetail(aimOwner.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerCode_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerCode_Loc, ref lueOwnerName_Loc);
				DataBindOwnerLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerName_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerName_Loc, ref lueOwnerCode_Loc);
				DataBindOwnerLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerLocationCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerLocationCode, ref lueOwnerLocationName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerLocationName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerLocationName, ref lueOwnerLocationCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueOwnerCode_Loc_Click(Object sender, EventArgs e)
		{
			lueOwnerCode_Loc.SelectAll();
		}

		private void lueOwnerCode_Loc_Enter(Object sender, EventArgs e)
		{
			lueOwnerCode_Loc.SelectAll();
		}

		private void lueOwnerName_Loc_Click(Object sender, EventArgs e)
		{
			lueOwnerName_Loc.SelectAll();
		}

		private void lueOwnerName_Loc_Enter(Object sender, EventArgs e)
		{
			lueOwnerName_Loc.SelectAll();
		}

		private void lueOwnerLocationCode_Click(Object sender, EventArgs e)
		{
			lueOwnerLocationCode.SelectAll();
		}

		private void lueOwnerLocationCode_Enter(Object sender, EventArgs e)
		{
			lueOwnerLocationCode.SelectAll();
		}

		private void lueOwnerLocationName_Click(Object sender, EventArgs e)
		{
			lueOwnerLocationName.SelectAll();
		}

		private void lueOwnerLocationName_Enter(Object sender, EventArgs e)
		{
			lueOwnerLocationName.SelectAll();
		}

		private void btnAddOwnerLocation_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Owner Location", lueOwnerLocationCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.OwnerLocation;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
                //queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND 

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueOwnerLocationCode.EditValue;
				queryTemplateDetail.SecondValue = (Int32)lueOwnerCode_Loc.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueOwnerCode_Loc.EditValue = 0;
				lueOwnerName_Loc.EditValue = 0;
				lueOwnerLocationCode.EditValue = 0;
				lueOwnerLocationName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteOwnerLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{	 
				AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
				aimOwnerLocation = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				if (aimOwnerLocation != null)
				{
					DeleteQueryTemplateDetail(aimOwnerLocation.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierCode, ref lueSupplierName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierName, ref lueSupplierCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierCode_Click(Object sender, EventArgs e)
		{
			lueSupplierCode.SelectAll();
		}

		private void lueSupplierCode_Enter(Object sender, EventArgs e)
		{
			lueSupplierCode.SelectAll();
		}

		private void lueSupplierName_Click(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}

		private void lueSupplierName_Enter(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}

		private void btnAddSupplier_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Supplier", lueSupplierName) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Supplier;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
                //queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND 

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueSupplierName.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueSupplierName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteSupplier_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMSupplierInfo aimSupplier = new AIMSupplierInfo();
				aimSupplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();
				if (aimSupplier != null)
				{
					DeleteQueryTemplateDetail(aimSupplier.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierCode_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierCode_Loc, ref lueSupplierName_Loc);
				DataBindSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierName_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierName_Loc, ref lueSupplierCode_Loc);
				DataBindSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierLocationCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierLocationCode, ref lueSupplierLocationName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierLocationName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierLocationName, ref lueSupplierLocationCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierCode_Loc_Click(Object sender, EventArgs e)
		{
			lueSupplierCode_Loc.SelectAll();
		}

		private void lueSupplierCode_Loc_Enter(Object sender, EventArgs e)
		{
			lueSupplierCode_Loc.SelectAll();
		}

		private void lueSupplierName_Loc_Click(Object sender, EventArgs e)
		{
			lueSupplierName_Loc.SelectAll();
		}

		private void lueSupplierName_Loc_Enter(Object sender, EventArgs e)
		{
			lueSupplierName_Loc.SelectAll();
		}

		private void lueSupplierLocationName_Click(Object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}

		private void lueSupplierLocationName_Enter(Object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}

		private void lueSupplierLocationCode_Click(Object sender, EventArgs e)
		{
			lueSupplierLocationCode.SelectAll();
		}

		private void lueSupplierLocationCode_Enter(Object sender, EventArgs e)
		{
			lueSupplierLocationCode.SelectAll();
		}

		private void btnAddSupplierLocation_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Supplier Location", lueSupplierLocationName) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueExportTemplate.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SupplierLocation;

                //9/5/19 - this should have been changed when report query form was changed on 3/6/18
                //queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND 

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				queryTemplateDetail.Value = (Int32)lueSupplierLocationCode.EditValue;
				queryTemplateDetail.SecondValue = (Int32)lueSupplierCode_Loc.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueSupplierCode_Loc.EditValue = 0;
				lueSupplierName_Loc.EditValue = 0;
				lueSupplierLocationCode.EditValue = 0;
				lueSupplierLocationName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteSupplierLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMSupplierLocationInfo aimSupplierLocation = new AIMSupplierLocationInfo();
				aimSupplierLocation = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
				if (aimSupplierLocation != null)
				{
					DeleteQueryTemplateDetail(aimSupplierLocation.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnRemoveSampleDate_Click(Object sender, EventArgs e)
		{
			AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueExportTemplate.EditValue, (Int32)QueryFeildEnum.SampleDate);
			DataBindSampleDate();
			txtDateOption.Text = "";
		}

		private void btnSaveExportTemplateAs_Click(Object sender, EventArgs e)
		{
			try
			{
				_saveAs = true;
				SampleExportTemplateInfo temp = new SampleExportTemplateInfo();
				temp = (SampleExportTemplateInfo)lueExportTemplate.GetSelectedDataRow();

				if (temp == null)
				{
					return;
				}

				if (temp.SampleExportTemplateID == 0)
				{
					MessageBox.Show("Please select the template first.", "Warning...", MessageBoxButtons.OK);
					lueExportTemplate.ShowPopup();
					return;
				}

				lblYellowBox.Visible = false;
				txtYellowBox.Visible = false;

				tabControlQuery.Visible = false;
				
				/////pnlQueryButtons.Visible = false;
				pnlNewQueryTemplate.Visible = true;

				pnlNewQueryTemplate.Location = new Point(15, 69);

				//pnlNewQueryTemplate.Size = new Size(701, 175);
                pnlNewQueryTemplate.Size = new Size(562, 120);

                pnlNewQueryTemplate.BringToFront();
				
			   
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAcceptAll_Click(Object sender, EventArgs e)
		{
			SetAcceptRejectAll((Int32)SampleImportStatus.Accepted);			
			grdSampleView.RefreshData();
		}

		private void btnCancelAll_Click(Object sender, EventArgs e)
		{
			SetAcceptRejectAll((Int32)SampleImportStatus.Cancelled);
			grdSampleView.RefreshData();
		}

		private void SetAcceptRejectAll(Int32 status)
		{
			var samples = from g in _sample select g;
			foreach (var item in samples)
			{
				item.ExportStatus = status;
			}
		}
				
		private void SetAcceptReject(Int32 sampleID, Int32 status)
		{
			var samples = from g in _sample
						  where g.SampleID == sampleID
						  select g;
			foreach (var item in samples)
			{
				if (status == (Int32)SampleImportStatus.Accepted)
				{
					item.ExportStatus = 1;
				}
				else 
				{
					item.ExportStatus = 2;
				}			
			}
		}

		private void repbtnAccept_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{				
			
			if (lueExportTemplate.EditValue == null)
			{
				return;
			}									 										
			SetAcceptReject((Int32)(grdSampleView.GetRowCellValue(grdSampleView.FocusedRowHandle, colSampleID)), (Int32)SampleImportStatus.Accepted);
			grdSample.DataSource = _sample;
			grdSampleView.RefreshData();
			
		}

		private void repbtnReject_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (lueExportTemplate.EditValue == null)
			{
				return;
			}
			SetAcceptReject((Int32)(grdSampleView.GetRowCellValue(grdSampleView.FocusedRowHandle, colSampleID)), (Int32)SampleImportStatus.Cancelled);
			grdSample.DataSource = _sample;
			grdSampleView.RefreshData();
		}

		private void SaveSampleEntity()
		{
			List<SampleExportHeaderInfo> sample = new List<SampleExportHeaderInfo>();
		}

		private void DataBindGrid()
		{
            AIMGlobal.SplashScreenShow();
            _sample = AIMManager.GetAllSampleFilterCSVExport((Int32)lueExportTemplate.EditValue);
			_detail = AIMManager.GetAllSampleFilterCSVExportDetail((Int32)lueExportTemplate.EditValue);

			for (int row = 0; row < _sample.Count; row++)	
			{	 
				var results = from g in _detail
							  where g.SampleID == _sample[row].SampleID
							  select g;

				foreach (SampleExportDetailInfo result in results)
				{	  
					if (_sample[row].DetailExport == null)
					{
						_sample[row].DetailExport = new List<SampleExportDetailInfo>();
					}

					_sample[row].DetailExport.Add(new SampleExportDetailInfo
					{							
						AnalyzerNutrientCode = result.AnalyzerNutrientCode,
						ResultValue = result.ResultValue,
						Unit = result.Unit 					
					}
					);					
					
				}				
			}
			
			
			
			Int32 currentRowNum = 0;
			currentRowNum = grdSampleView.FocusedRowHandle;

			grdSample.DataSource = _sample;
			grdSampleView.FocusedRowHandle = currentRowNum;
						
			lblFileType.Visible = false;
			lueFileType.Visible = false;
			if (_sample.Count > 0)
			{	
				lblFileType.Visible = true;
				lueFileType.Visible = true;
				timer1.Start();
			}

            AIMGlobal.SplashScreenHide();
        }

		private void grdSampleView_CustomDrawCell(Object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			String statusText = "";
			statusText = grdSampleView.GetRowCellValue(e.RowHandle, colStatusDisplay).ToString();
			if (e.Column == colStatusDisplay)
			{
				if (statusText == "Rejected")
				{
					e.Appearance.ForeColor = Color.Red;
				}
			}
		}

		private void grdSample_ViewRegistered(Object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
		{
			GridView view;
			view = e.View as GridView;

			view.Columns["AnalyzerNutrientCode"].OptionsColumn.AllowEdit = false;
			view.Columns["ResultValue"].OptionsColumn.AllowEdit = false;
			view.Columns["Unit"].OptionsColumn.AllowEdit = false;

			view.Columns["ResultValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			view.Columns["ResultValue"].DisplayFormat.FormatString = "n2";
			view.Columns["ResultValue"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			view.Columns["ResultValue"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

			view.Columns["AnalyzerNutrientCode"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["ResultValue"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["Unit"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);

			view.Columns["AnalyzerNutrientCode"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["ResultValue"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["Unit"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);

			//Hide un-used columns					
			view.Columns["SampleID"].Visible = false;
			view.Columns["SampleExportID"].Visible = false;
			view.Columns["SampleExportDetailID"].Visible = false;
			view.Columns["MaterialType"].Visible = false;
			view.Columns["MaterialTypeID"].Visible = false;
			view.Columns["MaterialTypeRawOrFinished"].Visible = false;
			view.Columns["MaterialCode"].Visible = false;
			view.Columns["MaterialName"].Visible = false;
			view.Columns["SampleDate"].Visible = false;
			view.Columns["SampleCode"].Visible = false;
			view.Columns["UpdateStamp"].Visible = false;
			view.Columns["CFCUserID"].Visible = false;
			
		}

		private void lueFileType_EditValueChanged(Object sender, EventArgs e)
		{
			
			btnExport.Visible = false;
			if ((Int32)lueFileType.EditValue != 0)
			{
				btnExport.Visible = true;
				lueFileType.BackColor = SystemColors.Control;
				timer1.Stop();
			}
		}

		private void timer1_Tick(Object sender, EventArgs e)
		{
			if (lueFileType.BackColor == SystemColors.Control)
			{
				lueFileType.BackColor = SystemColors.Info;
			}
			else
			{
				lueFileType.BackColor = SystemColors.Control;
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{

		}
	}
}
