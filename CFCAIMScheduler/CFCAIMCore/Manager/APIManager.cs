using CFCAIMCore.Entity.Adisseo;
using Core.Library.Notification;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace CFCAIMCore.Manager
{
	public class APIManager
	{
		#region Variables
		private static String _token = String.Empty;
		private static String _pneAPIUrl = String.Empty;
		private static String _errorMessage = String.Empty;
		private static RestClient _client = new RestClient("https://pne.adisseo.com/api");	// Add this url to the setting table/form  //DB230710
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

		public static String PNEAPIUrl
		{
			get
			{
				return _pneAPIUrl;
			}

			set
			{
				_pneAPIUrl = value;
			}
		}

		public static String ErrorMessage
		{
			get
			{
				return _errorMessage;
			}

			set
			{
				_errorMessage = value;
			}
		}
		#endregion

		#region API Calls
		private static void Initializer()
		{
			//ServicePointManager.Expect100Continue = true;
			//ServicePointManager.DefaultConnectionLimit = 9999;
			//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			//ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true; // workaround for their busted ssl cert

			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			_client = new RestClient(PNEAPIUrl);
			//_client = new RestClient("https://pne.adisseo.com/api");
			//_client = new RestClient("https://pne-preprod.adisseo.com/api");
			_client.AddDefaultHeader("accept", "application/json");
//			_client.AddDefaultHeader("accept", "*/*");
			//_client.AddDefaultHeader("User-Agent", "PostmanRuntime/7.29.2");
			//_client.AddDefaultHeader("Accept", "*/*");
			//_client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, br");
			//_client.AddDefaultHeader("Connection", "keep-alive");
			_client.AddDefaultHeader("x-token",_token);
		}

		public static Parameter GetParameter(string parameterCode)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/parameters/{parameterCode}", Method.GET);
				request.AddUrlSegment("parameterCode", parameterCode);
				IRestResponse<Parameter> response = _client.Execute<Parameter>(request);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (Parameter)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<MaterialInfo> GetMaterial()
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/materials", Method.GET);
				IRestResponse<List<MaterialInfo>> response = _client.Execute<List<MaterialInfo>>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (List<MaterialInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static MaterialInfo GetMaterial(string materialCode)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/materials/{materialCode}", Method.GET);
				request.AddUrlSegment("materialCode", materialCode);
				IRestResponse<MaterialInfo> response = _client.Execute<MaterialInfo>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (MaterialInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<AnalysesInfo> GetAnalyse()
		{
			try
			{
				Initializer();
				_client.Proxy = System.Net.WebRequest.GetSystemWebProxy();
				RestRequest request = new RestRequest("/analyses", Method.GET) { RequestFormat = DataFormat.Json };
				IRestResponse<List<AnalysesInfo>> response = _client.Execute<List<AnalysesInfo>>(request);

				if (response.ResponseStatus.ToString() == "Error")
				{
					ApplicationLog.WriteLog("API getAnalyse " + response.ErrorMessage, EventLogEntryType.Warning);
				}

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (List<AnalysesInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static AnalysesInfo GetAnalyse(string analysisCode)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/analyses/{analysisCode}", Method.GET);
				request.AddUrlSegment("analysisCode", analysisCode);
				IRestResponse<AnalysesInfo> response = _client.Execute<AnalysesInfo>(request);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (AnalysesInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static AnalysisRequestInfo GetAnalysisRequests(string analysisRequestId)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/analysisRequests/{analysisRequestId}", Method.GET);
				request.AddUrlSegment("analysisRequestId", analysisRequestId);
				IRestResponse<AnalysisRequestInfo> response = _client.Execute<AnalysisRequestInfo>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (AnalysisRequestInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<AnalysisResultInfo> GetAnalysisResultList(string analysisRequestId)
		{
			try
			{
				Initializer();
				List<AnalysisResultInfo> result = new List<AnalysisResultInfo>();
				RestRequest request = new RestRequest("/analysisrequests/{analysisRequestId}/results", Method.GET);
				request.AddUrlSegment("analysisRequestId", analysisRequestId);
				IRestResponse<List<AnalysisResultInfo>> response = _client.Execute<List<AnalysisResultInfo>>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					//result.Add(new AnalysisResultInfo() { Status = response.StatusCode.ToString() });
				}
				else
				{
					result = (List<AnalysisResultInfo>)response.Data;
				}

				return result;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static List<AnalysisResultsInfo> GetAnalysisResultList()
		{
			try
			{
				Initializer();
				AnanlysisResultParameterInfo ananlysisResultParameter  = new AnanlysisResultParameterInfo();
				//ananlysisResultParameter.submit_date_begin = "2021-12-28";
				//ananlysisResultParameter.submit_date_end = "2021-01-01";
				RestRequest request = new RestRequest("/analysisrequests", Method.GET);
				//request.RequestFormat = DataFormat.Json;
				//request.AddBody(ananlysisResultParameter);

				IRestResponse<List<AnalysisResultsInfo>> response = _client.Execute<List<AnalysisResultsInfo>>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (List<AnalysisResultsInfo>)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}
		public static APICreditInfo GetCredit()
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/credits", Method.GET);
				IRestResponse<APICreditInfo> response = _client.Execute<APICreditInfo>(request);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					ApplicationLog.WriteLog(response.Content + "  Status " + response.StatusCode + "  Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Warning);
					response.Data = null;
				}
				return (APICreditInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}

		public static AnalysisRequestInfo CreateAnalysisRequest(AnalysisRequestParameterInfo newAnalysisRequest)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/analysisrequests", Method.POST);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(newAnalysisRequest);
				IRestResponse<AnalysisRequestInfo> response = _client.Execute<AnalysisRequestInfo>(request);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					ErrorMessage = response.ErrorMessage;
				}

				return (AnalysisRequestInfo)response.Data;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
				throw;
			}
		}


		////public static AnalysisRequestInfo CreateAnalysisRequest(NewAnalysisRequestInfo newAnalysisRequest)
		////{
		////	try
		////	{
		////		Initializer();
		////		RestRequest request = new RestRequest("/analysisRequest/new", Method.POST);
		////		request.RequestFormat = DataFormat.Json;
		////		request.AddBody(newAnalysisRequest);
		////		IRestResponse<AnalysisRequestInfo> response = _client.Execute<AnalysisRequestInfo>(request);
		////		// get analysis request unique Id
		////		//				if (response.StatusCode == System.Net.HttpStatusCode.OK)
		////		return (AnalysisRequestInfo)response.Data;
		////	}
		////	catch (Exception ex)
		////	{
		////		ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
		////		throw;
		////	}
		////}

		#endregion
	}
}
