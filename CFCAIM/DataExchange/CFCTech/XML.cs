using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;


namespace CFCAIM.DataExchange
{
	public class CFCTechXML
	{
		private static CFCTechXmlHeaderInfo _xmlHeader = new CFCTechXmlHeaderInfo();
		private static CFCTechXmlSampleInfo _xmlSample = new CFCTechXmlSampleInfo();
		private static CFCUser.Global userGlobal = new CFCUser.Global();

		public static String _message = "";
		public static Int32 _specificStatus = 0;
		public static Int32 _statusMaterialxref = 0;
		public static Int32 _statusMissingOwnerxref = 0;
		public static Int32 _statusMissingOwnerLocationxref = 0;	  
		public static Int32 _statusMissingSupplierxref = 0;
		public static Int32 _statusMissingSupplierLocationxref = 0;
		public static Int32 _status = 0;

		public static Int32 _analyzerMaterialID = 0;
		public static Int32 _analyzerOwnerID = 0;
		public static Int32 _analyzerOwnerLocationID = 0;
		public static Int32 _analyzerSupplierID = 0;
		public static Int32 _analyzerSupplierLocationID = 0;

		public static Int32 _aimMaterialID = 0;
		public static Int32 _aimOwnerID = 0;
		public static Int32 _aimOwnerLocationID = 0;
		public static Int32 _aimSupplierID = 0;
		public static Int32 _aimSupplierLocationID = 0;
		public static Int32 _materialTypeID = 0;

		public static Int32 _validateID = 0;
		public static List<SystemImportCFCTechXMLSampleInfo> _work = new List<SystemImportCFCTechXMLSampleInfo>();
		public static List<SystemImportCFCTechXMLSampleInfo> _initialLoad = new List<SystemImportCFCTechXMLSampleInfo>();

		public static Int32 _missingMaterial = 0;
		public static Int32 _missingSupplier = 0;
		public static Int32 _missingSupplierLocation = 0;
		public static Int32 _missingOwner = 0;
		public static Int32 _missingOwnerLocation = 0;
		public static Int32 _errorCount = 0;
		public static String _comingFrom = "";

		public static Boolean ImportXML(String filename, ref FilesCountInfo filesCount, String comingFrom, String filePathAndName, BackgroundWorker backgroundWorker, Int32 batchID)

		{
			try
			{
				//9/13/16 - TODO should this be commented?				
				AIMGlobal.importError = 0;
								
				_comingFrom = comingFrom;
				_work.Clear();
				_initialLoad.Clear();

				if (_comingFrom == "InitialImport")
				{
					Int32 rtn = ValidateXMLFields(filename);
					if (rtn == -1)
					{
						return false;
					}
					else
					{
						_xmlHeader = DeserializeXMLFileToObject<CFCTechXmlHeaderInfo>(filename);					

						LoadData(ref filesCount, filePathAndName, backgroundWorker, batchID);
						return true;
					}
				}
				else
				{
					LoadData(ref filesCount, "", backgroundWorker, batchID);
					return true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static void LoadData(ref FilesCountInfo filesCount, String filePathAndName, BackgroundWorker backgroundWorker, Int32 batchID)
		{
			try
			{
				Int32 arrayCounter = 0;

				
				LoadFromXML(filePathAndName);

				//This is for the progress bar
				AIMGlobal.importTotalRecordCount = _initialLoad.Count;


				//Check for errors on entire file.  					
				_errorCount = 0;
				foreach (var item in _initialLoad)
				{
					CheckRequiedFields(_initialLoad, arrayCounter);
					arrayCounter++;
				}

				//If there are NO errors, add all records to Sample table and exit;
				if (_errorCount == 0)
				{
					arrayCounter = 0;
					foreach (var item in _initialLoad)
					{
						AddSamples(arrayCounter, _initialLoad, backgroundWorker, ref filesCount, batchID);
						arrayCounter++;
					}
					return;
				}

				//If there are errors, move all records in the file to entity, where they will need to be fixed and then accepted.
				if (_errorCount > 0)
				{
					LoopThruRecordsForEntity(backgroundWorker, ref filesCount, _initialLoad, batchID);
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public static void LoopThruRecordsForEntity(BackgroundWorker backgroundWorker, ref FilesCountInfo filesCount, List<SystemImportCFCTechXMLSampleInfo> entity, Int32 batchID)
		{
			Int32 arrayCounter = 0;

			if (_comingFrom != "InitialImport")
			{
				CFCAIM.AIMGlobal.systemImportCFCTechXMLSampleHeader.Clear();
			}

			foreach (var item in entity)
			{
				CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);

				_errorCount = 0;
				CheckRequiedFields(entity, arrayCounter);

				if (_errorCount != 0)
				{
					AIMGlobal.importError = -1;					
					filesCount.SampleImportRejectCount++;
				}
				else
				{
					_message = "Create New Sample";
					_specificStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
					_statusMaterialxref = 0;
					_statusMissingOwnerxref = 0;
					_statusMissingOwnerLocationxref = 0;
					_statusMissingSupplierxref = 0;
					_statusMissingSupplierLocationxref = 0;
					_status = (Int32)AcceptReject.Accept;
					filesCount.SampleImportAcceptCount++;
				}
				AddToEntity(entity, arrayCounter);

				if (_comingFrom == "Import" && _errorCount == 0)
				{
					AddSamples(arrayCounter, entity, backgroundWorker, ref filesCount, batchID);
				}

				arrayCounter++;
			}
		}

		public static void LoadFromXML(String filePathAndName)
		{


			_initialLoad.Clear();
			List<CFCTechXmlSampleInfo> xml = new List<CFCTechXmlSampleInfo>();
			foreach (CFCTechXmlSampleInfo item in _xmlHeader.Sample)
			{
				_validateID++;
                _initialLoad.Add(new SystemImportCFCTechXMLSampleInfo
                {
                    SystemImportCFCTechXMLSampleID = _validateID,
                    Message = "",
                    MessageStatus = 0,
                    XrefMaterialStatus = 0,
                    XrefSupplierStatus = 0,
                    XrefSupplierLocationStatus = 0,
                    Status = 0,
                    SampleCode = item.SampleCode,
                    SampleDate = item.SampleDate.ToString(),
                    AnalysisDate = item.SampleDate.ToString(),

                    AlternateCode = item.AlternateCode,
                    AnalyzerMaterialID = 0,
                    AnalyzerOwnerID = 0,
                    AnalyzerOwnerLocationID = 0,
                    AnalyzerSupplierID = 0,
                    AnalyzerSupplierLocationID = 0,

                    AnalyzerMaterialCode = item.AIMMaterialCode,
                    AnalyzerMaterialName = item.AIMMaterialCode,
                    AnalyzerOwnerCode = item.AIMOwnerCode,
                    AnalyzerOwnerName = item.AIMOwnerCode,
                    AnalyzerOwnerLocationCode = item.AIMOwnerCode,
                    AnalyzerOwnerLocationName = item.AIMOwnerLocationCode,
                    AnalyzerSupplierCode = item.AIMSupplierCode,
                    AnalyzerSupplierName = item.AIMSupplierCode,
                    AnalyzerSupplierLocationCode = item.AIMSupplierLocationCode,
                    AnalyzerSupplierLocationName = item.AIMSupplierLocationCode,

                    AIMMaterialID = 0,
                    AIMOwnerID = 0,
                    AIMOwnerLocationID = 0,
                    AIMSupplierID = 0,
                    AIMSupplierLocationID = 0,

                    FilePathAndName = filePathAndName,

                    //1/22/18
                    ContractNumber = item.ContractNumber,
                    PoNumber = item.PoNumber,
                    InvoiceNumber = item.InvoiceNumber,
                    TicketNumber = item.TicketNumber,
                    LoadNumber = item.LoadNumber,
                    BillOfLoad = item.BillOfLoad,
                    VehicleNumber = item.VehicleNumber,
                    LotNumber = item.LotNumber,                    

					Results = item.Results

				}
				);


			}
		}
		public static void LoadFromEntity()
		{
			foreach (SystemImportCFCTechXMLSampleInfo item in CFCAIM.AIMGlobal.systemImportCFCTechXMLSampleHeader)
			{
				_validateID++;
				_work.Add(new SystemImportCFCTechXMLSampleInfo
				{
					SystemImportCFCTechXMLSampleID = _validateID,
					Message = "",
					MessageStatus = 0,
					XrefMaterialStatus = 0,
					XrefSupplierStatus = 0,
					XrefSupplierLocationStatus = 0,
					Status = 0,
					SampleCode = item.SampleCode,
					SampleDate = item.SampleDate.ToString(),
					AnalysisDate = item.AnalysisDate.ToString(),

					AlternateCode = item.AlternateCode,
					AnalyzerMaterialID = item.AnalyzerMaterialID,
					AnalyzerOwnerID = item.AnalyzerOwnerID,
					AnalyzerOwnerLocationID = item.AnalyzerOwnerLocationID,
					AnalyzerSupplierID = item.AnalyzerSupplierID,
					AnalyzerSupplierLocationID = item.AnalyzerSupplierLocationID,

					AnalyzerMaterialCode = item.AnalyzerMaterialCode,
					AnalyzerMaterialName = item.AnalyzerMaterialCode,
					AnalyzerOwnerCode = item.AnalyzerOwnerCode,
					AnalyzerOwnerName = item.AnalyzerOwnerCode,
					AnalyzerOwnerLocationCode = item.AnalyzerOwnerLocationCode,
					AnalyzerOwnerLocationName = item.AnalyzerOwnerLocationName,
					AnalyzerSupplierCode = item.AnalyzerSupplierCode,
					AnalyzerSupplierName = item.AnalyzerSupplierName,
					AnalyzerSupplierLocationCode = item.AnalyzerSupplierLocationCode,
					AnalyzerSupplierLocationName = item.AnalyzerSupplierLocationName,

					AIMMaterialID = item.AIMMaterialID,
					AIMOwnerID = item.AIMOwnerID,
					AIMOwnerLocationID = item.AIMOwnerLocationID,
					AIMSupplierID = item.AIMSupplierID,
					AIMSupplierLocationID = item.AIMSupplierLocationID,

					FilePathAndName = item.FilePathAndName,

                    //1/22/18
                    ContractNumber = item.ContractNumber,
                    PoNumber = item.PoNumber,
                    InvoiceNumber = item.InvoiceNumber,
                    TicketNumber = item.TicketNumber,
                    LoadNumber = item.LoadNumber,
                    BillOfLoad = item.BillOfLoad,
                    VehicleNumber = item.VehicleNumber,
                    LotNumber = item.LotNumber,

                    Results = item.Results
				}
				);
			}
		}

		public static void AddSamples(Int32 arrayCounter, List<SystemImportCFCTechXMLSampleInfo> entity, BackgroundWorker backgroundWorker, ref FilesCountInfo filesCount, Int32 batchID)
		{
			try
			{

				CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);

				//-----------------------------------------------------------------
				//ADD SAMPLE HEADER
				SampleInfo sample = new SampleInfo();
				Int32 sampleID = 0;
				Int32 logID = 0;
				sample = new SampleInfo();
				sample.SampleID = 0;
				sample.SampleCode = entity[arrayCounter].SampleCode;
				sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.CFCTech;
				sample.AlternateCode = entity[arrayCounter].AlternateCode;
				sample.SampleDate = Convert.ToDateTime(entity[arrayCounter].SampleDate.ToString());
				sample.AnalysisDate = Convert.ToDateTime(entity[arrayCounter].AnalysisDate.ToString());
				sample.AIMMaterialID = _aimMaterialID;
				sample.AIMOwnerID = _aimOwnerID;
				sample.AIMOwnerLocationID = _aimOwnerLocationID;
				sample.AIMSupplierID = _aimSupplierID;
				sample.AIMSupplierLocationID = _aimSupplierLocationID;
				sample.AnalyzerMaterialCode = entity[arrayCounter].AnalyzerMaterialCode.ToString();

                //1/22/18
                sample.ContractNumber = entity[arrayCounter].ContractNumber.ToString();
                sample.PoNumber = entity[arrayCounter].PoNumber.ToString();
                sample.InvoiceNumber = entity[arrayCounter].InvoiceNumber.ToString();
                sample.TicketNumber = entity[arrayCounter].TicketNumber.ToString();
                sample.LoadNumber = entity[arrayCounter].LoadNumber.ToString();
                sample.BillOfLoad = entity[arrayCounter].BillOfLoad.ToString();
                sample.VehicleNumber = entity[arrayCounter].VehicleNumber.ToString();
                sample.LotNumber = entity[arrayCounter].LotNumber.ToString();

                sample.AnalyzerSupplierCode = entity[arrayCounter].AnalyzerSupplierCode.ToString();
				sample.AnalyzerSupplierName = entity[arrayCounter].AnalyzerSupplierName.ToString();
				sample.AnalyzerSupplierLocationCode = entity[arrayCounter].AnalyzerSupplierLocationCode.ToString();
				sample.AnalyzerSupplierLocationName = entity[arrayCounter].AnalyzerSupplierLocationName.ToString();
				sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//8/17/23
				sample.SampleTestTypeID = 0;

				sampleID = AIMManager.SaveSample(ref sample).RecordID;
				 
				//-----------------------------------------------------------------
				//ADD SAMPLE DETAIL
				List<CFCTechXmlResultsInfo> detailList = new List<CFCTechXmlResultsInfo>();
				detailList = entity[arrayCounter].Results;
				if (detailList != null)
				{
					foreach (CFCTechXmlResultsInfo det in detailList)
					{
						SampleResultInfo sampleResult = new SampleResultInfo();
						sampleResult.SampleID = sampleID;
						sampleResult.AnalyzerNutrientCode = det.AnalyzerNutrientCode;
						sampleResult.ResultValue = Convert.ToDecimal(det.ResultValue);
						sampleResult.Unit = det.UnitType;
						sampleResult.UpdateStamp = DateTime.Now;
						sampleResult.AnalysisDate = Convert.ToDateTime(entity[arrayCounter].AnalysisDate.ToString());
						sampleResult.IsActive = true;
						sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.CFCTech;

						//9/9/20 - change to use AIMCoreGlobal
						//sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
						sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                        sampleResult.SqlResultID = String.Empty;

						//8/17/22
						//AIMManager.InsertSampleResult(sampleResult, det.AnalyzerNutrientCode);
						AIMManager.InsertSampleResult(sampleResult, det.AnalyzerNutrientCode, 0);
					}
				}

				//-----------------------------------------------------------------
				//Add record to SampleImportLog
				SampleImportLogInfo log = new SampleImportLogInfo();
				log.SampleImportLogID = 0;
				log.SampleID = sampleID;
				log.BatchID = batchID;
				log.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				log.SampleCode = entity[arrayCounter].SampleCode;
				logID = AIMManager.SaveSampleImportLog(ref log).RecordID;

				//-----------------------------------------------------------------
				//Add records to hdr/dtl entities so samples can be grouped on Log grid by Batch           
				List<SampleImportLogInfo> logHeader = new List<SampleImportLogInfo>();
				logHeader.Add(new SampleImportLogInfo
				{
					SampleImportLogID = logID,
					BatchID = batchID,
					SampleID = sampleID,
					CFCUserID = userGlobal.CFCLoginUser.CFCUserID
				});
				if (logHeader[0].DetailLog == null)
				{
					logHeader[0].DetailLog = new List<SampleImportLogDetailInfo>();
				}
				logHeader[0].DetailLog.Add(new SampleImportLogDetailInfo
				{
					SampleImportLogID = logID,
					SampleID = sampleID,
					SampleCode = entity[arrayCounter].SampleCode,
					SampleDate = Convert.ToDateTime(entity[arrayCounter].SampleDate.ToString())
				}
				);


				//-----------------------------------------------------------------
				filesCount.SampleImportAcceptCount++;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		public static void CheckRequiedFields(List<SystemImportCFCTechXMLSampleInfo> entity, Int32 arrayCounter)
		{
			try
			{
				//get [analyzer] material code from entity
				//using material code, check AIM Material table to get ID
				//if Material code does not exist, create AIM Material ID		

				//-----------------------------------------------------------
				//Material	
				_aimMaterialID = 0;
				List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
				aimMaterial = AIMManager.GetAIMMaterialAIMMaterialCode(entity[arrayCounter].AnalyzerMaterialCode.ToString());
				if (aimMaterial != null)
				{
					_aimMaterialID = aimMaterial[0].AIMMaterialID;
				}
				else
				{
					AIMMaterialInfo material = new AIMMaterialInfo();
					material.AIMMaterialID = 0;
					material.AIMMaterialCode = entity[arrayCounter].AnalyzerMaterialCode.ToString();
					material.AIMMaterialName = entity[arrayCounter].AnalyzerMaterialCode.ToString();
					material.MaterialTypeID = entity[arrayCounter].MaterialTypeID;
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

				//-----------------------------------------------------------
				//Owner
				_aimOwnerID = 0;
				AIMOwnerInfo aimOwner = new AIMOwnerInfo();
				aimOwner = AIMManager.GetAIMOwnerCode(entity[arrayCounter].AnalyzerOwnerCode.ToString());
				if (aimOwner != null)
				{
					_aimOwnerID = aimOwner.AIMOwnerID;
				}
				else
				{
					AIMOwnerInfo owner = new AIMOwnerInfo();
					owner.AIMOwnerID = 0;
					owner.OwnerCode = entity[arrayCounter].AnalyzerOwnerCode.ToString();
					owner.OwnerName = entity[arrayCounter].AnalyzerOwnerCode.ToString();
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

				//-----------------------------------------------------------
				//OwnerLocation
				_aimOwnerLocationID = 0;
				AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
				aimOwnerLocation = AIMManager.GetOwnerLocationOwnerIDLocationCode(_aimOwnerID, entity[arrayCounter].AnalyzerOwnerLocationCode.ToString());
				if (aimOwnerLocation != null)
				{
					_aimOwnerLocationID = aimOwnerLocation.AIMOwnerLocationID;

				}
				else
				{
					AIMOwnerLocationInfo ownerLocation = new AIMOwnerLocationInfo();
					ownerLocation.AIMOwnerLocationID = 0;
					ownerLocation.AIMOwnerID = _aimOwnerID;
					ownerLocation.LocationCode = entity[arrayCounter].AnalyzerOwnerLocationCode.ToString();
					ownerLocation.LocationName = entity[arrayCounter].AnalyzerOwnerLocationCode.ToString();
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


				//-----------------------------------------------------------
				//Supplier
				_aimSupplierID = 0;
				AIMSupplierInfo aimSupplier = new AIMSupplierInfo();
				aimSupplier = AIMManager.GetSupplierSupplierCode(entity[arrayCounter].AnalyzerSupplierCode.ToString());
				if (aimSupplier != null)
				{
					_aimSupplierID = aimSupplier.AIMSupplierID;
				}
				else
				{
					AIMSupplierInfo supplier = new AIMSupplierInfo();
					supplier.AIMSupplierID = 0;
					supplier.AIMSupplierCode = entity[arrayCounter].AnalyzerSupplierCode.ToString();
					supplier.SupplierName = entity[arrayCounter].AnalyzerSupplierCode.ToString();
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

				//-----------------------------------------------------------
				//SupplierLocation
				_aimSupplierLocationID = 0;
				AIMSupplierLocationInfo aimSupplierLocation = new AIMSupplierLocationInfo();
				aimSupplierLocation = AIMManager.GetSupplierLocationSupplierIDLocationCode(_aimSupplierID, entity[arrayCounter].AnalyzerSupplierLocationCode.ToString());
				if (aimSupplierLocation != null)
				{
					_aimSupplierLocationID = aimSupplierLocation.AIMSupplierLocationID;
				}
				else
				{
					AIMSupplierLocationInfo supplierLocation = new AIMSupplierLocationInfo();
					supplierLocation.AIMSupplierLocationID = 0;
					supplierLocation.AIMSupplierID = _aimSupplierID;
					supplierLocation.AIMSupplierLocationCode = entity[arrayCounter].AnalyzerSupplierLocationCode.ToString();
					supplierLocation.SupplierLocationName = entity[arrayCounter].AnalyzerSupplierLocationCode.ToString();
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
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static void AddToEntity(List<SystemImportCFCTechXMLSampleInfo> work, Int32 arrayCounter)
		{
			CFCAIM.AIMGlobal.systemImportCFCTechXMLSampleHeader.Add(new SystemImportCFCTechXMLSampleInfo
			{
				SystemImportCFCTechXMLSampleID = work[arrayCounter].SystemImportCFCTechXMLSampleID,
				Message = _message,
				MessageStatus = _specificStatus,
				
				Status = _status,
				SampleCode = work[arrayCounter].SampleCode,
				SampleDate = work[arrayCounter].SampleDate.ToString(),
				AnalysisDate = work[arrayCounter].AnalysisDate.ToString(),

				AlternateCode = work[arrayCounter].AlternateCode,
				AnalyzerMaterialID = _analyzerMaterialID,
				AnalyzerOwnerID = _analyzerOwnerID,
				AnalyzerOwnerLocationID = _analyzerOwnerLocationID,
				AnalyzerSupplierID = _analyzerSupplierID,
				AnalyzerSupplierLocationID = _analyzerSupplierLocationID,
                				
				AIMMaterialID = _aimMaterialID,
				AIMOwnerID = _aimOwnerID,
				AIMOwnerLocationID = _aimOwnerLocationID,
				AIMSupplierID = _aimSupplierID,
				AIMSupplierLocationID = _aimSupplierLocationID,
				MaterialTypeID = _materialTypeID,

				FilePathAndName = work[arrayCounter].FilePathAndName,

                //1/22/18
                ContractNumber = work[arrayCounter].ContractNumber,
                PoNumber = work[arrayCounter].PoNumber,
                InvoiceNumber = work[arrayCounter].InvoiceNumber,
                TicketNumber = work[arrayCounter].TicketNumber,
                LoadNumber = work[arrayCounter].LoadNumber,
                BillOfLoad = work[arrayCounter].BillOfLoad,
                VehicleNumber = work[arrayCounter].VehicleNumber,
                LotNumber = work[arrayCounter].LotNumber,

                Results = work[arrayCounter].Results

			}
			);
		}

		public static Int32 ValidateXMLFields(String filename)
		{
			Int32 rtn = 0;
			Int32 counter = 0;
			var doc = XDocument.Load(filename);

			if (doc.Root.Element("ExportName").ToString() != "<ExportName>CFCTechExportXML</ExportName>")
			{
				rtn = -1;				
			}

			else
			{
				foreach (XElement x in doc.Descendants("Sample"))
				{
					if (counter == 1)
					{
						break;
					}
					//Check first XML record. Make sure all fields are in XML file. 
					if (x.Element("SampleDate") == null
						|| x.Element("AIMMaterialCode") == null
						|| x.Element("AIMOwnerCode") == null
						|| x.Element("AIMOwnerLocationCode") == null
						|| x.Element("AIMSupplierCode") == null
						|| x.Element("AIMSupplierLocationCode") == null
						|| x.Element("SampleCode") == null						
						)
					{
						rtn = -1;
					}
					counter++;
				}
			}
			return rtn;
		}
		public static T DeserializeXMLFileToObject<T>(String filename)
		{
			T returnObject = default(T);
			if (string.IsNullOrEmpty(filename)) return default(T);

			try
			{
				StreamReader xmlStream = new StreamReader(filename);
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				returnObject = (T)serializer.Deserialize(xmlStream);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return returnObject;
		}
	}
}
