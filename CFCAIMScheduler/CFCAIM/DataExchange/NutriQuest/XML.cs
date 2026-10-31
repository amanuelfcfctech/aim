using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Linq;


namespace CFCAIM.DataExchange
{
    public class NutriQuestXML
	{
		private static NutriQuestXmlHeaderInfo _xmlHeader = new NutriQuestXmlHeaderInfo();
		private static NutriQuestXmlSampleInfo _xmlSample = new NutriQuestXmlSampleInfo();
		private static CFCUser.Global userGlobal = new CFCUser.Global();

		public static String _message = "";
		public static Int32 _specificStatus = 0;
		public static Int32 _statusMaterialxref = 0;
		public static Int32 _statusMissingSupplierxref = 0;
		public static Int32 _statusMissingSupplierLocationxref = 0;
		public static Int32 _status = 0;

		public static Int32 _analyzerMaterialID = 0;
		public static Int32 _analyzerSupplierID = 0;
		public static Int32 _analyzerSupplierLocationID = 0;

		public static Int32 _aimMaterialID = 0;
		public static Int32 _aimOwnerID = 0;
		public static Int32 _aimOwnerLocationID = 0;
		public static Int32 _aimSupplierID = 0;
		public static Int32 _aimSupplierLocationID = 0;

		public static Int32 _validateID = 0;		
		public static List<InternalImportValidateSampleInfo> _work = new List<InternalImportValidateSampleInfo>();
		public static List<InternalImportValidateSampleInfo> _initialLoad = new List<InternalImportValidateSampleInfo>();

		public static Int32 _missingMaterialxref = 0;
		public static Int32 _missingSupplierxref = 0;
		public static Int32 _missingSupplierLocationxref = 0;
		public static Int32 _errorCount = 0;
		public static String _comingFrom = "";

        public static Boolean ImportXML(String filename, ref FilesCountInfo filesCount, Int32 aimOwnerID, Int32 aimOwnerLocationID, String comingFrom, String filePathAndName, BackgroundWorker backgroundWorker, Int32 batchID)

        {
			try
			{	
					
				AIMGlobal.importError = 0;

				_aimOwnerID = aimOwnerID;
				_aimOwnerLocationID = aimOwnerLocationID;
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
						_xmlHeader = DeserializeXMLFileToObject<NutriQuestXmlHeaderInfo>(filename);
						LoadData(aimOwnerID, aimOwnerLocationID, ref filesCount, filePathAndName, backgroundWorker, batchID);
						return true;
					}
				}
				else
				{
					LoadData(aimOwnerID, aimOwnerLocationID, ref filesCount, "", backgroundWorker, batchID);
					return true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public static void LoadData(Int32 aimOwnerID, Int32 aimOwnerLocationID, ref FilesCountInfo filesCount, String filePathAndName, BackgroundWorker backgroundWorker, Int32 batchID)
		{
			try
			{
				Int32 arrayCounter = 0;

				if (_comingFrom == "InitialImport")
				{
					LoadFromXML(filePathAndName);                    
                    AIMGlobal.importTotalRecordCount = _initialLoad.Count;


					//Check for errors on entire file.  					
					_errorCount = 0;										
					foreach (var item in _initialLoad)
					{
						CheckCrossReferences(false, _initialLoad, arrayCounter);
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
						LoopThruRecordsForEntity(backgroundWorker, ref filesCount, _initialLoad,batchID);
					}
				}
				else
				{
					LoadFromEntity();
					LoopThruRecordsForEntity(backgroundWorker, ref filesCount, _work, batchID);
				}				  
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);				
			}
		}
		public static void LoopThruRecordsForEntity(BackgroundWorker backgroundWorker, ref FilesCountInfo filesCount, List<InternalImportValidateSampleInfo> entity, Int32 batchID)
		{
			Int32 arrayCounter = 0;
						
			if (_comingFrom != "InitialImport")
			{
				CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Clear();
			}

			foreach (var item in entity)
			{
				CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);

				_errorCount = 0;
				CheckCrossReferences(true, entity, arrayCounter);

				if (_errorCount != 0)					
				{
					AIMGlobal.importError = -1;

					_message = "Missing Cross-Reference";
					_specificStatus = (Int32)SampleImportSpecificMessageDetail.MissingInternalCrossReference;
					_statusMaterialxref = _missingMaterialxref;
					_statusMissingSupplierxref = _missingSupplierxref;
					_statusMissingSupplierLocationxref = _missingSupplierLocationxref;
					_status = (Int32)AcceptReject.Reject;
					filesCount.SampleImportRejectCount++;
				}
				else
				{
					_message = "Create New Sample";
					_specificStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
					_statusMaterialxref = 0;
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
			List<NutriQuestXmlSampleInfo> xml = new List<NutriQuestXmlSampleInfo>();
			foreach (NutriQuestXmlSampleInfo item in _xmlHeader.Sample)
			{

                //1/31/19 - added because import file had null manufacturer-code and manufacturer-name, which cause cross-ref error and error adding to supplier table.  Added check of all 3 required fields
                if (item.SampleTypeCode == null)
                {
                    item.SampleTypeCode = "0";
                }
                if (item.ManufacturerCode == null)
                {
                    item.ManufacturerCode = "0";
                }
                if (item.ManufacturerName == null)
                {
                    item.ManufacturerName = "";
                }
                if (item.ArticleCode == null)
                {
                    item.ArticleCode = "0";
                }
                if (item.ArticleName == null)
                {
                    item.ArticleName = "";
                }


				_validateID++;
				_initialLoad.Add(new InternalImportValidateSampleInfo
				{
					InternalImportValidateSampleID = _validateID,
					Message = "",
					MessageStatus = 0,
					XrefMaterialStatus = 0,
					XrefSupplierStatus = 0,
					XrefSupplierLocationStatus = 0,
					Status = 0,
					SampleCode = item.SampleIDLab,
					SampleDate = item.SampleArrivalDate.ToString(),
					AnalysisDate = item.SamplingDate.ToString(),

					AlternateCode = item.SampleID,
					AnalyzerMaterialID = 0,
					AnalyzerOwnerID = 0,
					AnalyzerOwnerLocationID = 0,
					AnalyzerSupplierID = 0,
					AnalyzerSupplierLocationID = 0,

					AnalyzerMaterialCode = item.SampleTypeCode,
					AnalyzerMaterialName = item.SampleTypeCode,
					AnalyzerSupplierCode = item.ManufacturerCode,
					AnalyzerSupplierName = item.ManufacturerName,
					AnalyzerSupplierLocationCode = item.ArticleCode,
					AnalyzerSupplierLocationName = item.ArticleName,

					AIMMaterialID = 0,
					AIMOwnerID = 0,
					AIMOwnerLocationID = 0,
					AIMSupplierID = 0,
					AIMSupplierLocationID = 0, 

					FilePathAndName = filePathAndName,
                                        
                    Property = item.Property

                }
                );


			}
		}
		public static void LoadFromEntity()
		{
			foreach (InternalImportValidateSampleInfo item in CFCAIM.AIMGlobal.internalImportValidateSampleHeader)
			{
				_validateID++;
				_work.Add(new InternalImportValidateSampleInfo
				{
					InternalImportValidateSampleID = _validateID,
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
					AnalyzerOwnerID = 0,
					AnalyzerOwnerLocationID = 0,
					AnalyzerSupplierID = item.AnalyzerSupplierID,
					AnalyzerSupplierLocationID = item.AnalyzerSupplierLocationID,

					AnalyzerMaterialCode = item.AnalyzerMaterialCode,
					AnalyzerMaterialName = item.AnalyzerMaterialCode,
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
					Property = item.Property
				}
				);		
			}
		}			
		
		public static void AddSamples(Int32 arrayCounter, List<InternalImportValidateSampleInfo> entity, BackgroundWorker backgroundWorker, ref FilesCountInfo filesCount, Int32 batchID)	  								
		{
            try
            {
                //1/31/19 - added this check
                if (entity[arrayCounter].AnalyzerMaterialCode.ToString() == "0" ||
                    entity[arrayCounter].AnalyzerSupplierCode.ToString() == "0" ||
                    entity[arrayCounter].AnalyzerSupplierLocationCode.ToString() == "0")  {}
                else
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
                    sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.NutriQuest;
                    sample.AlternateCode = entity[arrayCounter].AlternateCode;
                    sample.SampleDate = Convert.ToDateTime(entity[arrayCounter].SampleDate.ToString());
                    sample.AnalysisDate = Convert.ToDateTime(entity[arrayCounter].AnalysisDate.ToString());
                    sample.AIMMaterialID = _aimMaterialID;
                    sample.AIMOwnerID = _aimOwnerID;
                    sample.AIMOwnerLocationID = _aimOwnerLocationID;
                    sample.AIMSupplierID = _aimSupplierID;
                    sample.AIMSupplierLocationID = _aimSupplierLocationID;
                    sample.AnalyzerMaterialCode = entity[arrayCounter].AnalyzerMaterialCode.ToString();
                    sample.AnalyzerSupplierCode = entity[arrayCounter].AnalyzerSupplierCode.ToString();
                    sample.AnalyzerSupplierName = entity[arrayCounter].AnalyzerSupplierName.ToString();
                    sample.AnalyzerSupplierLocationCode = entity[arrayCounter].AnalyzerSupplierLocationCode.ToString();
                    sample.AnalyzerSupplierLocationName = entity[arrayCounter].AnalyzerSupplierLocationName.ToString();

					//8/17/23
					sample.SampleTestTypeID = 0;


					sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    sampleID = AIMManager.SaveSample(ref sample).RecordID;

                    //-----------------------------------------------------------------
                    //ADD SAMPLE DETAIL
                    List<NutriQuestXmlPropertyInfo> detailList = new List<NutriQuestXmlPropertyInfo>();

                    detailList = entity[arrayCounter].Property;

                    if (detailList != null)
                    {
                        foreach (NutriQuestXmlPropertyInfo det in detailList)
                        {
                            SampleResultInfo sampleResult = new SampleResultInfo();
                            sampleResult.SampleID = sampleID;
                            sampleResult.AnalyzerNutrientCode = det.PropertyID;
                            sampleResult.ResultValue = Convert.ToDecimal(det.ResultN);
                            sampleResult.Unit = det.Unit;
                            sampleResult.UpdateStamp = DateTime.Now;
                            sampleResult.AnalysisDate = Convert.ToDateTime(entity[arrayCounter].AnalysisDate.ToString());
                            sampleResult.IsActive = true;
                            sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.NutriQuest;

							//9/9/20 - change to use AIMCoreGlobal
							//sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
							sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                            sampleResult.SqlResultID = String.Empty;

							//8/17/22
                            //AIMManager.InsertSampleResult(sampleResult, det.PropertyName);
							AIMManager.InsertSampleResult(sampleResult, det.PropertyName, 0);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }		
		public static void CheckCrossReferences(Boolean addRec, List<InternalImportValidateSampleInfo> entity, Int32 arrayCounter)
		{
			try
			{ 	  			
				_missingMaterialxref = 0;
				_missingSupplierxref = 0;
				_missingSupplierLocationxref = 0;

				//FOR AIM REQUIRED FIELDS:
				//	1) check user-defined table for XML code
				//	2) if exists, use code to get AIM xref
				//	3) if doesn't exist, add to analyzer table but display "missing xref" warning 

				//-----------------------------------------------------------
				//Material	
				_aimMaterialID = 0;
				_analyzerMaterialID = 0;
				String materialName = "";
				AnalyzerMaterialInfo analyzerMaterial = new AnalyzerMaterialInfo();				
				analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)DataSourceTypeEnum.NutriQuest, entity[arrayCounter].AnalyzerMaterialCode.ToString());
				if (analyzerMaterial != null)
				{
					_analyzerMaterialID = analyzerMaterial.AnalyzerMaterialID;
					materialName = analyzerMaterial.AnalyzerMaterialName;
	
					AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
					xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialID(analyzerMaterial.AnalyzerMaterialID);
					if (xref != null)
					{
						_aimMaterialID = xref.AIMMaterialID;
					}
				}
				else
				{
					if (addRec == true)
					{
						AnalyzerMaterialInfo analyzerMaterialAdd = new AnalyzerMaterialInfo();
						analyzerMaterialAdd.AnalyzerMaterialID = 0;
						analyzerMaterialAdd.AnalyzerMaterialCode = entity[arrayCounter].AnalyzerMaterialCode.ToString();
						analyzerMaterialAdd.AnalyzerMaterialName = entity[arrayCounter].AnalyzerMaterialCode.ToString();
						analyzerMaterialAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						analyzerMaterialAdd.DataSourceTypeID = (Int32)DataSourceTypeEnum.NutriQuest;
						analyzerMaterialAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveAnalyzerMaterial(ref analyzerMaterialAdd, true);
						if (error.ErrorNumber > 0)
						{
							return;
						}
					}	 						
				}
				if (_aimMaterialID == 0)
				{
					_missingMaterialxref = 1;
				}
	
				//-----------------------------------------------------------
				//Supplier
				_aimSupplierID = 0;				
				_analyzerSupplierID = 0;
				String supplierName = "";
				AnalyzerSupplierInfo analyzerSupplier = new AnalyzerSupplierInfo();
				analyzerSupplier = AIMManager.GetAnalyzerSupplier(DataSourceTypeEnum.NutriQuest, entity[arrayCounter].AnalyzerSupplierCode.ToString());
				if (analyzerSupplier != null)
				{
					_analyzerSupplierID = analyzerSupplier.AnalyzerSupplierID;
					supplierName = analyzerSupplier.AnalyzerSupplierName;
	
					AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
					xref = AIMManager.GetAIMSupplierRefAnalyzerSupplierAnalyzerID(analyzerSupplier.AnalyzerSupplierID);
					if (xref != null)
					{
						_aimSupplierID = xref.AIMSupplierID;
					}
				}
				else
				{
					if (addRec == true)
					{
						AnalyzerSupplierInfo analyzerSupplierAdd = new AnalyzerSupplierInfo();
						analyzerSupplierAdd.AnalyzerSupplierID = 0;
						analyzerSupplierAdd.AnalyzerSupplierCode = entity[arrayCounter].AnalyzerSupplierCode.ToString();
						analyzerSupplierAdd.AnalyzerSupplierName = entity[arrayCounter].AnalyzerSupplierName.ToString();
						analyzerSupplierAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						analyzerSupplierAdd.DataSourceTypeID = (Int32)DataSourceTypeEnum.NutriQuest;
						analyzerSupplierAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveAnalyzerSupplier(ref analyzerSupplierAdd);
						if (error.ErrorNumber > 0)
						{
							return;
						}
						else
						{
							_analyzerSupplierID = error.RecordID;
						}
					}	 						
				}
				if (_aimSupplierID == 0)
				{
					_missingSupplierxref = 1;
				}
	
	
				//-----------------------------------------------------------
				//SupplierLocation
				_aimSupplierLocationID = 0;
				_analyzerSupplierLocationID = 0;
				String supplierLocationName = "";
				AnalyzerSupplierLocationInfo analyzerSupplierLocation = new AnalyzerSupplierLocationInfo();
				analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)DataSourceTypeEnum.NutriQuest, _analyzerSupplierID, entity[arrayCounter].AnalyzerSupplierLocationCode.ToString());
				if (analyzerSupplierLocation != null)
				{
					_analyzerSupplierLocationID = analyzerSupplierLocation.AnalyzerSupplierLocationID;
					supplierLocationName = analyzerSupplierLocation.AnalyzerSupplierLocationName;
	
					AIMSupplierLocationRefAnalyzerSupplierLocationInfo xref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
					xref = AIMManager.GetAIMSupplierLocationRefAnalyzerSupplierLocationAnalyzerID(analyzerSupplierLocation.AnalyzerSupplierLocationID);
					if (xref != null)
					{
						_aimSupplierLocationID = xref.AIMSupplierLocationID;
					}
				}
				else
				{
					if (addRec == true)
					{
						AnalyzerSupplierLocationInfo analyzerSupplierLocationAdd = new AnalyzerSupplierLocationInfo();
						analyzerSupplierLocationAdd.AnalyzerSupplierLocationID = 0;
						analyzerSupplierLocationAdd.AnalyzerSupplierID = _analyzerSupplierID;
						analyzerSupplierLocationAdd.AnalyzerSupplierLocationCode = entity[arrayCounter].AnalyzerSupplierLocationCode.ToString();
						analyzerSupplierLocationAdd.AnalyzerSupplierLocationName = entity[arrayCounter].AnalyzerSupplierLocationName.ToString();
						analyzerSupplierLocationAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						analyzerSupplierLocationAdd.DataSourceTypeID = (Int32)DataSourceTypeEnum.NutriQuest;
						analyzerSupplierLocationAdd.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveAnalyzerSupplierLocation(ref analyzerSupplierLocationAdd);
						if (error.ErrorNumber > 0)
						{
							return;
						}
						else
						{
							_analyzerSupplierLocationID = error.RecordID;
						}
					}
				}
				if (_aimSupplierLocationID == 0)
				{
					_missingSupplierLocationxref = 1;
				}	
				if (_aimMaterialID == 0 || _aimOwnerID == 0 || _aimOwnerLocationID == 0 || _aimSupplierID == 0 || _aimSupplierLocationID == 0)
				{
					_errorCount++;
				}			
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static void AddToEntity(List<InternalImportValidateSampleInfo> work, Int32 arrayCounter)
		{
			CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Add(new InternalImportValidateSampleInfo
			{	  
				InternalImportValidateSampleID = work[arrayCounter].InternalImportValidateSampleID,
				Message = _message,
				MessageStatus = _specificStatus,
				XrefMaterialStatus = _statusMaterialxref,
				XrefSupplierStatus = _statusMissingSupplierxref,
				XrefSupplierLocationStatus = _statusMissingSupplierLocationxref,
				Status = _status,
				SampleCode = work[arrayCounter].SampleCode,
				SampleDate = work[arrayCounter].SampleDate.ToString(),
				AnalysisDate = work[arrayCounter].AnalysisDate.ToString(),

				AlternateCode = work[arrayCounter].AlternateCode,
				AnalyzerMaterialID = _analyzerMaterialID,
				AnalyzerOwnerID = 0,
				AnalyzerOwnerLocationID = 0,
				AnalyzerSupplierID = _analyzerSupplierID,
				AnalyzerSupplierLocationID = _analyzerSupplierLocationID,

				AnalyzerMaterialCode = work[arrayCounter].AnalyzerMaterialCode,
				AnalyzerMaterialName = work[arrayCounter].AnalyzerMaterialCode,
				AnalyzerSupplierCode = work[arrayCounter].AnalyzerSupplierCode,
				AnalyzerSupplierName = work[arrayCounter].AnalyzerSupplierName,
				AnalyzerSupplierLocationCode = work[arrayCounter].AnalyzerSupplierLocationCode,
				AnalyzerSupplierLocationName = work[arrayCounter].AnalyzerSupplierLocationName,

				AIMMaterialID = _aimMaterialID,
				AIMOwnerID = _aimOwnerID,
				AIMOwnerLocationID = _aimOwnerLocationID,
				AIMSupplierID = _aimSupplierID,
				AIMSupplierLocationID = _aimSupplierLocationID,

				FilePathAndName = work[arrayCounter].FilePathAndName,
														
				Property = work[arrayCounter].Property

			}
			);
		}

		public static Int32 ValidateXMLFields(String filename)
		{
			Int32 rtn = 0;
			Int32 counter = 0;            
            var doc = XDocument.Load(filename);
			foreach (XElement x in doc.Descendants("Sample"))
			{
				if (counter == 1)
				{
					break;
				}
				//Check first XML record. Make sure all fields are in XML file. (ArticleCode, ArticleName, MfgCode, MfgName, SampleIDLab, SampleID, SampleTypeCode 
				if (x.Element("ArticleCode") == null
					|| x.Element("ArticleName") == null
					|| x.Element("ManufacturerCode") == null
					|| x.Element("ManufacturerName") == null
					|| x.Element("SampleIDLab") == null
					|| x.Element("SampleID") == null
					|| x.Element("SampleTypeCode") == null
					)
				{
					rtn = -1;
				}
				counter++;
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

                //1/31/19 -- added this because import files with errors were not going to the "Error" directory -- because the file was "locked".
                xmlStream.Close();

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return returnObject;
		}
	}
}
