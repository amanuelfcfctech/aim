using CFCAPICore.Entity;
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
        private static Boolean _isFirst = true;
        private static String _token = String.Empty;
        private static String _pneAPIUrl = String.Empty;
        private static String _errorMessage = String.Empty;
        private static RestClient _client = new RestClient("https://pne.adisseo.com/api");  // Add this url to the setting table/form  //DB230710
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



            if (_isFirst == true)
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.UseNagleAlgorithm = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                _client = new RestClient(PNEAPIUrl);
                //_client = new RestClient("https://pne.adisseo.com/api");
                //_client = new RestClient("https://pne-preprod.adisseo.com/api");
                _client.AddDefaultHeader("accept", "application/json");
                //_client.AddDefaultHeader("Connection", "close");
                //_client.Timeout = 30000;
                //			_client.AddDefaultHeader("accept", "*/*");
                //_client.AddDefaultHeader("User-Agent", "PostmanRuntime/7.29.2");
                //_client.AddDefaultHeader("Accept", "*/*");
                //_client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, br");
                //_client.AddDefaultHeader("Connection", "keep-alive");
                _client.AddDefaultHeader("x-token", _token);
                _isFirst = false;
            }
        }

		public static Parameter GetParameter(string parameterCode)
		{
			try
			{
				Initializer();
				RestRequest request = new RestRequest("/parameters/{parameterCode}", Method.Get);
				request.AddUrlSegment("parameterCode", parameterCode);
                RestResponse<Parameter> response = _client.Execute<Parameter>(request);
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
				RestRequest request = new RestRequest("/materials", Method.Get);
				RestResponse<List<MaterialInfo>> response = _client.Execute<List<MaterialInfo>>(request);

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
				RestRequest request = new RestRequest("/materials/{materialCode}", Method.Get);
				request.AddUrlSegment("materialCode", materialCode);
				RestResponse<MaterialInfo> response = _client.Execute<MaterialInfo>(request);

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
				//_client.Proxy = System.Net.WebRequest.GetSystemWebProxy();
				RestRequest request = new RestRequest("/analyses", Method.Get) { RequestFormat = DataFormat.Json };
				RestResponse<List<AnalysesInfo>> response = _client.Execute<List<AnalysesInfo>>(request);

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
				RestRequest request = new RestRequest("/analyses/{analysisCode}", Method.Get);
				request.AddUrlSegment("analysisCode", analysisCode);
				RestResponse<AnalysesInfo> response = _client.Execute<AnalysesInfo>(request);
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
				RestRequest request = new RestRequest("/analysisRequests/{analysisRequestId}", Method.Get);
				request.AddUrlSegment("analysisRequestId", analysisRequestId);
				RestResponse<AnalysisRequestInfo> response = _client.Execute<AnalysisRequestInfo>(request);

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
				RestRequest request = new RestRequest("/analysisrequests/{analysisRequestId}/results", Method.Get);
				request.AddUrlSegment("analysisRequestId", analysisRequestId);
				RestResponse<List<AnalysisResultInfo>> response = _client.Execute<List<AnalysisResultInfo>>(request);
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
				RestRequest request = new RestRequest("/analysisrequests", Method.Get);
				//request.RequestFormat = DataFormat.Json;
				//request.AddBody(ananlysisResultParameter);

				RestResponse<List<AnalysisResultsInfo>> response = _client.Execute<List<AnalysisResultsInfo>>(request);

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
				RestRequest request = new RestRequest("/credits", Method.Get);
				RestResponse<APICreditInfo> response = _client.Execute<APICreditInfo>(request);
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
				RestRequest request = new RestRequest("/analysisrequests", Method.Post);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(newAnalysisRequest);
				RestResponse<AnalysisRequestInfo> response = _client.Execute<AnalysisRequestInfo>(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ErrorMessage = response.ErrorMessage;
                }

                //TODO REMOVE THIS CODE ****
                //ErrorMessage =
                //        "StatusCode: " + response.StatusCode +
                //        " | ResponseStatus: " + response.ResponseStatus +
                //        " | ErrorMessage: " + response.ErrorMessage +
                //        " | Content: " + response.Content;
                //MessageBox.Show(response.ErrorMessage, "pne message7");
                //******************************************************

                return (AnalysisRequestInfo)response.Data;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);   //TODO REMOVE
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
