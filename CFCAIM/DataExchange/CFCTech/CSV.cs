using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	public class CFCTechCSV
	{
        // * * *  Files exported, under "Export > Other", can be imported using this function.
                
        public static Int32 _aimMaterialID = 0;
        public static Int32 _aimOwnerID = 0;
        public static Int32 _aimOwnerLocationID = 0;
        public static Int32 _aimSupplierID = 0;
        public static Int32 _aimSupplierLocationID = 0;
        public static Int32 _materialTypeID = 0;
        public static Int32 _errorCount = 0;

        private static CFCUser.Global userGlobal = new CFCUser.Global();
        MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

		public ErrorInfo CSVRowProcess(
						FilesInfo file,
						FilesCountInfo filesCount,
						ref SampleInfo sample)
		{
			SampleResultInfo sampleResult = new SampleResultInfo();
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			ErrorInfo error = new ErrorInfo();

			// 1 - SampleDate
			// 2 - SampleID
			// 3 - AimMaterialCode
			// 4 - AimOwnerCode
			// 5 - AimOwnerLocationCode
			// 6 - AimSupplierCode
			// 7 - AimSupplierLocationCode
			// 8 - SampleCode
			// 9 - AlternateCode		 
			// 10 - BillOfLoad			 
			// 11 - ContractNumber		 
			// 12 - InvoiceNumber		 
			// 13 - LoadNumber			 
			// 14 - LotNumber			 
			// 15 - PoNumber			 
			// 16 - TicketNumber		 
			// 17 - VehicleNumber	
			// 18 - MaterialTypeID	 
			// 19+++ - NutrientCode, NutrientValue, NutrientUnits  


			Int32 csvRow = 1;
			Int32 columnNumber = 0;
			Int32 columnCount = 0;	 						
			Int32 sampleNumber = 0;
            
            
			String analyzerNutrientCode = "";
			String analyzerUnits = "";
			Decimal resultValue = 0;
			Boolean resultsAdded = false;		

			using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(file.ProcessPathAndName))
			{
				ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();
				reader.ReadRow(row); //skip heading row

				while (reader.ReadRow(row))
				{
					columnCount = row.Count;
					foreach (string column in row)
					{
						if (columnNumber == 0)
						{
							DateTime.TryParse(row[0], out analysisDate);

                            Int32 type = 0;
                            Int32.TryParse(row[17].ToString(), out type);

                            CheckRequiedFields("Material", Convert.ToString(row[2]), type);  
                            CheckRequiedFields("Owner", Convert.ToString(row[3]), 0);
                            CheckRequiedFields("OwnerLocation", Convert.ToString(row[4]), 0);
                            CheckRequiedFields("Supplier", Convert.ToString(row[5]), 0);
                            CheckRequiedFields("SupplierLocation", Convert.ToString(row[6]), 0);

                            sample = new SampleInfo();
							sample.SampleID = 0;																		
							sample.AIMMaterialID = _aimMaterialID;
							sample.AIMOwnerID = _aimOwnerID;
							sample.AIMOwnerLocationID = _aimOwnerLocationID;
							sample.AIMSupplierID = _aimSupplierID;
							sample.AIMSupplierLocationID = _aimSupplierLocationID;
							sample.SampleCode = row[7];								
							sample.AlternateCode = row[8];
							sample.BillOfLoad = row[9];
							sample.ContractNumber = row[10];
							sample.InvoiceNumber = row[11];
							sample.LoadNumber = row[12];
							sample.LotNumber = row[13];
							sample.PoNumber = row[14];
							sample.TicketNumber = row[15];
							sample.VehicleNumber = row[16];								

							sample.SampleDate = analysisDate;
							sample.AnalysisDate = analysisDate;
							sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.CFCTech;
							sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                            //8/17/23
                            sample.SampleTestTypeID = 0;

							sampleNumber = AIMManager.SaveSample(ref sample).RecordID;
							sample.SampleID = sampleNumber;
                            filesCount.SuccessCount += 1;

                            columnNumber++;

						}
						else if (columnNumber <= 17)
						{
							columnNumber++;
						}
						else if (columnNumber > 17 && resultsAdded == true)	//--> this is here to advance the csv pointer
						{							
						}

						else if (columnNumber > 1 && resultsAdded == false)
						{
							while (columnNumber < columnCount)
							{
								analyzerNutrientCode = row[columnNumber];
								columnNumber++;
								Decimal.TryParse(row[columnNumber], out resultValue);
								columnNumber++;
								analyzerUnits = row[columnNumber];
								columnNumber++;

								sampleResult.SampleID = sampleNumber;
								sampleResult.AnalyzerNutrientCode = analyzerNutrientCode;
								sampleResult.ResultValue = resultValue;
								sampleResult.Unit = analyzerUnits;
								sampleResult.AnalysisDate = analysisDate;
								sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.CFCTech;
								sampleResult.AimDatabaseID = (Int32)_mainMenu.lueDataMap.EditValue;
								sampleResult.IsActive = true;
								sampleResult.SqlResultID = String.Empty;

                                //8/17/22
								//AIMManager.InsertSampleResult(sampleResult, analyzerNutrientCode);
                                AIMManager.InsertSampleResult(sampleResult, analyzerNutrientCode, 0);

                                resultsAdded = true;
							}
														
						}
					}
					resultsAdded = false;
					columnNumber = 0;
					csvRow += 1;				
					
				}
				
				error.ErrorNumber = 0;
				error.RecordID = sampleNumber;
				return error;
			}
		}

        public static void CheckRequiedFields(String requiredField, String code, Int32 materialTypeID)
        {
            try
            {
                switch (requiredField)
                {
                    case "Material":
                        _aimMaterialID = 0;
                        List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
                        aimMaterial = AIMManager.GetAIMMaterialAIMMaterialCode(code);
                        if (aimMaterial != null)
                        {
                            _aimMaterialID = aimMaterial[0].AIMMaterialID;
                        }
                        else
                        {
                            AIMMaterialInfo material = new AIMMaterialInfo();
                            material.AIMMaterialID = 0;
                            material.AIMMaterialCode = code;
                            material.AIMMaterialName = code;
                            material.MaterialTypeID = materialTypeID;
                            material.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            material.AIMOwnerLocationID = 0;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveAIMMaterial(ref material, false);
                            if (error.ErrorNumber > 0)
                            {
                                _errorCount++;
                            }
                            else
                            {
                                _aimMaterialID = error.RecordID;
                            }
                        }
                        return;
                    case "Owner":
                        _aimOwnerID = 0;
                        AIMOwnerInfo aimOwner = new AIMOwnerInfo();
                        aimOwner = AIMManager.GetAIMOwnerCode(code);
                        if (aimOwner != null)
                        {
                            _aimOwnerID = aimOwner.AIMOwnerID;
                        }
                        else
                        {
                            AIMOwnerInfo owner = new AIMOwnerInfo();
                            owner.AIMOwnerID = 0;
                            owner.OwnerCode = code;
                            owner.OwnerName = code;
                            owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            owner.IsActive = true;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveAIMOwner(ref owner, false);
                            if (error.ErrorNumber > 0)
                            {
                                _errorCount++;
                            }
                            else
                            {
                                _aimOwnerID = error.RecordID;
                            }
                        }
                        return;
                    case "OwnerLocation":
                        _aimOwnerLocationID = 0;
                        AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
                        aimOwnerLocation = AIMManager.GetOwnerLocationOwnerIDLocationCode(_aimOwnerID, code);
                        if (aimOwnerLocation != null)
                        {
                            _aimOwnerLocationID = aimOwnerLocation.AIMOwnerLocationID;

                        }
                        else
                        {
                            AIMOwnerLocationInfo ownerLocation = new AIMOwnerLocationInfo();
                            ownerLocation.AIMOwnerLocationID = 0;
                            ownerLocation.AIMOwnerID = _aimOwnerID;
                            ownerLocation.LocationCode = code;
                            ownerLocation.LocationName = code;
                            ownerLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            ownerLocation.IsFormulationPlant = false;
                            ownerLocation.IsActive = true;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveAIMOwnerLocation(ref ownerLocation);
                            if (error.ErrorNumber > 0)
                            {
                                _errorCount++;
                            }
                            else
                            {
                                _aimOwnerLocationID = error.RecordID;
                            }
                        }

                        return;
                    case "Supplier":
                        _aimSupplierID = 0;
                        AIMSupplierInfo aimSupplier = new AIMSupplierInfo();
                        aimSupplier = AIMManager.GetSupplierSupplierCode(code);
                        if (aimSupplier != null)
                        {
                            _aimSupplierID = aimSupplier.AIMSupplierID;
                        }
                        else
                        {
                            AIMSupplierInfo supplier = new AIMSupplierInfo();
                            supplier.AIMSupplierID = 0;
                            supplier.AIMSupplierCode = code;
                            supplier.SupplierName = code;
                            supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            supplier.IsActive = true;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveAIMSupplier(ref supplier, false);
                            if (error.ErrorNumber > 0)
                            {
                                _errorCount++;
                            }
                            else
                            {
                                _aimSupplierID = error.RecordID;
                            }
                        }
                        return;
                    case "SupplierLocation":
                        _aimSupplierLocationID = 0;
                        AIMSupplierLocationInfo aimSupplierLocation = new AIMSupplierLocationInfo();
                        aimSupplierLocation = AIMManager.GetSupplierLocationSupplierIDLocationCode(_aimSupplierID, code);
                        if (aimSupplierLocation != null)
                        {
                            _aimSupplierLocationID = aimSupplierLocation.AIMSupplierLocationID;
                        }
                        else
                        {
                            AIMSupplierLocationInfo supplierLocation = new AIMSupplierLocationInfo();
                            supplierLocation.AIMSupplierLocationID = 0;
                            supplierLocation.AIMSupplierID = _aimSupplierID;
                            supplierLocation.AIMSupplierLocationCode = code;
                            supplierLocation.SupplierLocationName = code;
                            supplierLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            supplierLocation.IsActive = true;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveSupplierLocation(ref supplierLocation);
                            if (error.ErrorNumber > 0)
                            {
                                _errorCount++;
                            }
                            else
                            {
                                _aimSupplierLocationID = error.RecordID;
                            }
                        }
                        return;
                }              
                
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
