using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Foss;
using CFCAIMCore.Entity.Bruker;
using CFCAIMCore.Entity.CFCAPI;
using Core.Library.Notification;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CFCAIMCore.Manager
{
	public class CFCAPIManager
	{

		#region Variables
		private static String _token = String.Empty;
		//private static String _apiUrl = String.Empty;
		private static RestClient _client = new RestClient();
		private static SettingInfo _setting = new SettingInfo();
		#endregion

		#region  Properties
		public static String Token
		{
			get
			{
				return _token;
			}

			set
			{
				_token = value;
			}
		}

		//public static String APIUrl
		//{
		//    get
		//    {
		//        return _apiUrl;
		//    }

		//    set
		//    {
		//        _apiUrl = value;
		//    }
		//}

		public static SettingInfo Setting
		{
			get
			{
				return _setting;
			}

			set
			{
				_setting = value;
			}
		}
		#endregion


		#region API Calls
		private static void Initializer()
		{
			//ServicePointManager.Expect100Continue = true;
			//ServicePointManager.DefaultConnectionLimit = 9999;
			//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

			if (_setting.IsTestUrl == true)
			{
				_client = new RestClient(Setting.APIUrlTest);
			}
			else
			{
				_client = new RestClient(Setting.APIUrl);
			}


			//if (Environment.MachineName == "CFC-1")
			//{
			//    //_client = new RestClient("https://localhost:44390/api/v1");
			//    _client = new RestClient(_apiUrl);
			//    // _client.AddDefaultHeader("token", "test");
			//}
			//else if (Environment.MachineName == "SERVER2016")
			//{
			//    //_client = new RestClient("https://localhost:44390/api/v1");
			//    _client = new RestClient(_apiUrl);
			//    // _client.AddDefaultHeader("token", "test");
			//}

			//else
			//{
			//    _client = new RestClient(_apiUrl);
			//}


			_client.AddDefaultHeader("accept", "application/json");
			//_client.AddDefaultHeader("x-token", "1caddf65fe41b7802a2ed093a4e6952e");
			//_client.AddDefaultHeader("x-token", _token);
		}
		#endregion


		#region Get Main
		public static List<APITokenInfo> GetToken(String aimAPIKey)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/token", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("AIMAPIKey", aimAPIKey);
				IRestResponse<List<APITokenInfo>> response = _client.Execute<List<APITokenInfo>>(request);
				return (List<APITokenInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<APITokenV1Info> GetToken(String aimAPIKey, String aimInstrumentKey)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/TokenV1", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("AIMAPIKey", aimAPIKey);
				request.AddParameter("AIMInstrumentKey", aimInstrumentKey);
				IRestResponse<List<APITokenV1Info>> response = _client.Execute<List<APITokenV1Info>>(request);
				return (List<APITokenV1Info>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<CustomerInfo> GetCustomer(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/Customer", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<CustomerInfo>> response = _client.Execute<List<CustomerInfo>>(request);
				return (List<CustomerInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<InstrumentInfo> GetInstrument(String token, String aimInstrumentKey)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/Instrument", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("AIMInstrumentKey", aimInstrumentKey);
				IRestResponse<List<InstrumentInfo>> response = _client.Execute<List<InstrumentInfo>>(request);
				return (List<InstrumentInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<InstrumentInfo> GetInstrument(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/InstrumentCustomerID", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<InstrumentInfo>> response = _client.Execute<List<InstrumentInfo>>(request);
				return (List<InstrumentInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion

		#region Get Bruker
		public static List<BrukerSampleInfo> GetBrukerSampleNotSentResult(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleNotSentResult", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleInfo> GetBrukerSampleNotSentSpectrum(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleNotSentSpectrum", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleInfo> GetBrukerSample(String token, Int32 customerID, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleByBrukerSampleID", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("BrukerSampleID", brukerSampleID);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleInfo> GetBrukerSample(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleAll", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleInfo> GetBrukerSample(String token, Int32 customerID, String valueProductInfo1)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleByValueProductInfo1", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("ValueProductInfo1", valueProductInfo1);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleInfo> GetBrukerSample(String token, Int32 customerID, String path, String fileName)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleByPathFileName", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("Path", path);
				request.AddParameter("FileName", fileName);
				IRestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
				return (List<BrukerSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<BrukerSampleResultInfo> GetBrukerSampleResult(String token, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerResult", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("brukerSampleID", brukerSampleID);
				IRestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
				return (List<BrukerSampleResultInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleResultInfo> GetBrukerSampleResultNotSent(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleResultNotSent", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
				return (List<BrukerSampleResultInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleResultInfo> GetBrukerSampleResultNotSent(String token, Int32 customerID, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleResultNotSent", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("BrukerSampleID", brukerSampleID);
				IRestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
				return (List<BrukerSampleResultInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSpectrumInfo> GetBrukerSpectrumFile(String token, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSprectrum", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("brukerSampleID", brukerSampleID);
				IRestResponse<List<BrukerSpectrumInfo>> response = _client.Execute<List<BrukerSpectrumInfo>>(request);
				//response.Data.
				List<BrukerSpectrumInfo> keyResponse = JsonConvert.DeserializeObject<List<BrukerSpectrumInfo>>(response.Content);

				return keyResponse;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSpectrumListInfo> GetBrukerSpectrumFileList(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSprectrumList", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("customerID", customerID);
				IRestResponse<List<BrukerSpectrumListInfo>> response = _client.Execute<List<BrukerSpectrumListInfo>>(request);
				List<BrukerSpectrumListInfo> keyResponse = JsonConvert.DeserializeObject<List<BrukerSpectrumListInfo>>(response.Content);

				return keyResponse;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSampleResultSentInfo> GetBrukerSampleResultSent(String token, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerResultSent", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("brukerSampleID", brukerSampleID);
				IRestResponse<List<BrukerSampleResultSentInfo>> response = _client.Execute<List<BrukerSampleResultSentInfo>>(request);
				return (List<BrukerSampleResultSentInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<BrukerSpectrumSentInfo> GetBrukerSpectrumFileSent(Int32 brukerSpectrumID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSprectrumSent", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("brukerSpectrumID", brukerSpectrumID);
				IRestResponse<List<BrukerSpectrumSentInfo>> response = _client.Execute<List<BrukerSpectrumSentInfo>>(request);
				return (List<BrukerSpectrumSentInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion

		#region Delete Bruker
		public static ErrorInfo DeleteBrukerSampleResultSent(String token, Int32 customerID, Int32 brukerSampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSampleResultSentDelete", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("BrukerSampleID", brukerSampleID);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo DeleteBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID, Boolean isSkip)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSentLogDelete", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("BrukerSampleID", brukerSampleID);
				
				if (isSkip == true)
				{
					request.AddParameter("Skip", 1);
				}
				else
				{
					request.AddParameter("Skip", 0);
				}

				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion

		#region Get Foss
		public static List<FossSampleInfo> GetFossSample(Int32 customerID, Int32 batchID, String sampleNumber, String token)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSampleByCustomerIDBatchIDSampleNumber", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("BatchID", batchID);
				request.AddParameter("SampleNumber", sampleNumber);
				request.AddParameter("Token", token);
				IRestResponse<List<FossSampleInfo>> response = _client.Execute<List<FossSampleInfo>>(request);
				return (List<FossSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<FossSampleInfo> GetFossSampleNotSentResult(String token, Int32 customerID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSampleNotSentResult", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				IRestResponse<List<FossSampleInfo>> response = _client.Execute<List<FossSampleInfo>>(request);
				return (List<FossSampleInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static List<FossSampleResultInfo> GetFossSampleResultNotSent(String token, Int32 customerID, Int32 sampleID)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSampleResultNotSent", Method.GET);
				request.RequestFormat = DataFormat.Json;
				request.AddParameter("Token", token);
				request.AddParameter("CustomerID", customerID);
				request.AddParameter("SampleID", sampleID);
				IRestResponse<List<FossSampleResultInfo>> response = _client.Execute<List<FossSampleResultInfo>>(request);
				return (List<FossSampleResultInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		#endregion

		#region Post 
		public static ErrorInfo PutTokenEnd(TokenInfo token)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/TokenEnd", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(token);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBatch(BatchInfo batch)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/Batch", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(batch);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBatchEnd(BatchInfo batch)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BatchEnd", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(batch);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion

		#region Post Bruker
		public static ErrorInfo PutBrukerSample(BrukerSampleInfo brukerSample)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/Bruker", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSample);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID)
		{
			return PutBrukerSentLog(token, customerID, brukerSampleID, false);
		}
		public static ErrorInfo PutBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID, Boolean isSkip)
		{
			BrukerSentLogInfo brukerSentLog = new BrukerSentLogInfo();

			try
			{
				Initializer();
				brukerSentLog.Token = token;
				brukerSentLog.BrukerSentLogID = 0;
				brukerSentLog.CustomerID = customerID;
				brukerSentLog.BrukerSampleID = brukerSampleID;
				brukerSentLog.IsSkip = isSkip;
				RestRequest request = new RestRequest("/BrukerSentLog", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSentLog);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSampleResult(BrukerSampleResultInfo brukerSampleResult)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerResult", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSampleResult);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSampleResultSent(BrukerSampleResultSentInfo brukerSampleResultSent)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerResultSent", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSampleResultSent);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSampleResultReceive(String token, Int32 brukerSampleResultID)
		{
			BrukerSampleResultReceiveInfo brukerSampleResultReceive = new BrukerSampleResultReceiveInfo();

			try
			{
				Initializer();
				brukerSampleResultReceive.Token = token;
				brukerSampleResultReceive.BrukerSampleResultReceiveID = 0;
				brukerSampleResultReceive.BrukerSampleResultID = brukerSampleResultID;
				RestRequest request = new RestRequest("/BrukerSampleResultReceive", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSampleResultReceive);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSpectrumFile(BrukerSpectrumInfo brukerSpectrumFile)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSpectrum", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSpectrumFile);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSpectrumSent(BrukerSpectrumSentInfo brukerSpectrumSent)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/BrukerSpectrumSent", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSpectrumSent);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutBrukerSpectrumReceive(String token, Int32 brukerSpectrumID)
		{
			BrukerSpectrumReceiveInfo brukerSpectrumReceive = new BrukerSpectrumReceiveInfo();

			try
			{
				Initializer();
				brukerSpectrumReceive.Token = token;
				brukerSpectrumReceive.BrukerSpectrumReceiveID = 0;
				brukerSpectrumReceive.BrukerSpectrumID = brukerSpectrumID;
				RestRequest request = new RestRequest("/BrukerSpectrumReceive", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSpectrumReceive);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion

		#region Post Foss
		public static ErrorInfo PutFossSample(FossSampleInfo brukerSample)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSample", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(brukerSample);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutFossSentLog(String token, Int32 customerID, Int32 fossSampleID, Boolean isSkip)
		{
			FossSentLogInfo fossSentLog = new FossSentLogInfo();

			try
			{
				Initializer();
				fossSentLog.Token = token;
				fossSentLog.BrukerSentLogID = 0;
				fossSentLog.CustomerID = customerID;
				fossSentLog.BrukerSampleID = fossSampleID;
				fossSentLog.IsSkip = isSkip;
				RestRequest request = new RestRequest("/FossSentLog", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(fossSentLog);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutFossSampleResultSent(FossSampleResultSentInfo fossSampleResultSent)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossResultSent", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(fossSampleResultSent);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutFossSpectrumFile(FossSpectrumInfo fossSpectrumFile)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSpectrum", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(fossSpectrumFile);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutFossSpectrumSent(FossSpectrumSentInfo fossSpectrumSent)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/FossSpectrumSent", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(fossSpectrumSent);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static ErrorInfo PutFossSpectrumReceive(Int32 FossSpectrumID, String token)
		{
			FossSpectrumReceiveInfo FossSpectrumReceive = new FossSpectrumReceiveInfo();

			try
			{
				Initializer();
				FossSpectrumReceive.Token = token;
				FossSpectrumReceive.FossSpectrumReceiveID = 0;
				FossSpectrumReceive.FossSpectrumID = FossSpectrumID;
				RestRequest request = new RestRequest("/FossSpectrumReceive", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(FossSpectrumReceive);
				IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
				return (ErrorInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		#endregion
	}
}
