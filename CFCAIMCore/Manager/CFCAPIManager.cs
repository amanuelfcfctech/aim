using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Entity.Foss;
using CFCAIMCore.Entity.OutsideLab;
using CFCAIMCore.Entity.PNE;
using CFCAPICore.Entity;
using CFCWebAPI.Entity.Foss;
using Core.Library.API;
using Core.Library.Notification;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace CFCAIMCore.Manager
{
    public class CFCAPIManager
    {

        #region Variables
        private static String _token = String.Empty;
        //private static String _apiUrl = String.Empty;
        private static RestClient _client = new RestClient();

        private static SettingInfo _setting = new SettingInfo();
        private static Boolean _isMaintenance = false;
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

        public static Boolean IsMaintenance
        {
            get
            {
                return _isMaintenance;
            }
            set
            {
                _isMaintenance = value;
            }
        }
        #endregion

        #region API Calls
        private static void Initializer()
        {
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            if (_isMaintenance == true)                                                             //DB240930 Start
            {
                if (_setting.IsMaintenanceTestUrl == true)
                {
                    //_client = new RestClient(Setting.APIMaintenanceUrlTest);
                    _client = new RestClient(new RestClientOptions(Setting.APIMaintenanceUrlTest));
                }
                else
                {
                    //_client = new RestClient(Setting.APIMaintenanceUrl);
                    _client = new RestClient(new RestClientOptions(Setting.APIMaintenanceUrl));
                }                                                                                   //DB240930 End                
            }
            else
            {
                if (_setting.IsTestUrl == true)
                {
                    _client = new RestClient(Setting.APIUrlTest);
                }
                else
                {
                    _client = new RestClient(Setting.APIUrl);
                }
            }

            //if (Environment.MachineName == "CFC-1")
            //{
            //    //_client = new RestClient("https://localhost:44390");
            //    _client = new RestClient(_apiUrl);
            //    // _client.AddDefaultHeader("token", "test");
            //}
            //else if (Environment.MachineName == "SERVER2016")
            //{
            //    //_client = new RestClient("https://localhost:44390");
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


            public static T FromResponse<T>(RestResponse response)
            {
                if (response == null || string.IsNullOrWhiteSpace(response.Content))
                {
                    return default(T);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }
            }

        #endregion

        #region Get Main
        public static List<APITokenInfo> GetToken(String aimAPIKey)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/token", Method.Get);
                //request.RequestFormat = DataFormat.Json;
                request.AddParameter("AIMAPIKey", aimAPIKey);
                RestResponse<List<APITokenInfo>> response = _client.Execute<List<APITokenInfo>>(request);
                return ApiHelper.FromResponse<List<APITokenInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static APITokenInfo GetTokenV2(String aimAPIKey)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/TokenV2", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("AIMAPIKey", aimAPIKey);
                RestResponse<APITokenInfo> response = _client.Execute<APITokenInfo>(request);
                return ApiHelper.FromResponse<APITokenInfo>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static APITokenInfo GetTokenAPIUser(String apiUserKey)
        {
            try
            {
                RestRequest request = new RestRequest("/api/v1/TokenAPIUser", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("apiUserKey", apiUserKey);
                RestResponse<APITokenInfo> response = _client.Execute<APITokenInfo>(request);
                return ApiHelper.FromResponse<APITokenInfo>(response);
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
                RestRequest request = new RestRequest("/api/v1/TokenV1", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("AIMAPIKey", aimAPIKey);
                request.AddParameter("AIMInstrumentKey", aimInstrumentKey);
                RestResponse<List<APITokenV1Info>> response = _client.Execute<List<APITokenV1Info>>(request);
                return ApiHelper.FromResponse<List<APITokenV1Info>>(response);
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
                RestRequest request = new RestRequest("/api/v1/Customer", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<CustomerInfo>> response = _client.Execute<List<CustomerInfo>>(request);
                return ApiHelper.FromResponse<List<CustomerInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/Instrument", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("AIMInstrumentKey", aimInstrumentKey);
                RestResponse<List<InstrumentInfo>> response = _client.Execute<List<InstrumentInfo>>(request);
                return ApiHelper.FromResponse<List<InstrumentInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/InstrumentCustomerID", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<InstrumentInfo>> response = _client.Execute<List<InstrumentInfo>>(request);
                return ApiHelper.FromResponse<List<InstrumentInfo>>(response);               
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static DataSourceTypeInfo GetDataSourceType(String token, Int32 dataSourceTypeID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/DataSourceType", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("token", token);
                request.AddParameter("dataSourceTypeID", dataSourceTypeID);
                RestResponse<DataSourceTypeInfo> response = _client.Execute<DataSourceTypeInfo>(request);
                return ApiHelper.FromResponse<DataSourceTypeInfo>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleNotSentResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddQueryParameter("Token", token);
                request.AddQueryParameter("CustomerID", customerID);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleNotSentSpectrum", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleByBrukerSampleID", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("BrukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleAll", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleByValueProductInfo1", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("ValueProductInfo1", valueProductInfo1);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleByPathFileName", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("Path", path);
                request.AddParameter("FileName", fileName);
                RestResponse<List<BrukerSampleInfo>> response = _client.Execute<List<BrukerSampleInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("brukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleResultInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleResultNotSent", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleResultInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSampleResultNotSent", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("BrukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSampleResultInfo>> response = _client.Execute<List<BrukerSampleResultInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleResultInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSprectrum", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("brukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSpectrumInfo>> response = _client.Execute<List<BrukerSpectrumInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSpectrumInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static List<BrukerSpectrumInfo> GetBrukerSpectrumFileV2(String token, Int32 brukerSampleID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v2/BrukerSprectrum", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("brukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSpectrumInfo>> response = _client.Execute<List<BrukerSpectrumInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSpectrumInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSprectrumList", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("customerID", customerID);
                RestResponse<List<BrukerSpectrumListInfo>> response = _client.Execute<List<BrukerSpectrumListInfo>>(request);
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
                RestRequest request = new RestRequest("/api/v1/BrukerResultSent", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("brukerSampleID", brukerSampleID);
                RestResponse<List<BrukerSampleResultSentInfo>> response = _client.Execute<List<BrukerSampleResultSentInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSampleResultSentInfo>>(response);
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
                RestRequest request = new RestRequest("/api/v1/BrukerSprectrumSent", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("brukerSpectrumID", brukerSpectrumID);
                RestResponse<List<BrukerSpectrumSentInfo>> response = _client.Execute<List<BrukerSpectrumSentInfo>>(request);
                return ApiHelper.FromResponse<List<BrukerSpectrumSentInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static APIStatusInfo GetIsApiDown(String aimApikey, String aimVersion, String programName)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/IsApiDown", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("ApiKey", aimApikey);
                request.AddParameter("Version", aimVersion);
                request.AddParameter("ProgramName", programName);
                Console.WriteLine(_client.Options.BaseUrl);
                RestResponse<APIStatusInfo> response = _client.Execute<APIStatusInfo>(request);
                return ApiHelper.FromResponse<APIStatusInfo>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }

        // Just here for testing purposes, will be removed in the future 
        //public static String GetVersion() 
        //{
        //    Initializer();
        //    RestRequest request = new RestRequest("/api/v1/APIVersion", Method.Get);
        //    RestResponse<String> response = _client.Execute<String>(request);
        //    return ApiHelper.FromResponse<String>(response);
        //}
        #endregion

        #region Delete Bruker
        public static ErrorInfo   DeleteBrukerSampleResultSent(String token, Int32 customerID, Int32 brukerSampleID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerSampleResultSentDelete", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("BrukerSampleID", brukerSampleID);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   DeleteBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID, Boolean isSkip)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerSentLogDelete", Method.Get);
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

                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
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
                RestRequest request = new RestRequest("/api/v1/FossSampleByCustomerIDBatchIDSampleNumber", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("CustomerID", customerID);
                request.AddParameter("BatchID", batchID);
                request.AddParameter("SampleNumber", sampleNumber);
                request.AddParameter("Token", token);
                RestResponse<List<FossSampleInfo>> response = _client.Execute<List<FossSampleInfo>>(request);
                return ApiHelper.FromResponse<List<FossSampleInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static List<FossSampleInfo> GetFossSampleNotSentCSVResult(String token, Int32 customerID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossSampleNotSentCSVResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<FossSampleInfo>> response = _client.Execute<List<FossSampleInfo>>(request);
                return ApiHelper.FromResponse<List<FossSampleInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }

        public static List<FossCSVResultHeaderInfo> GetFossCSVResultNotSent(String token, Int32 customerID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossCSVResultNotSent", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("CustomerID", customerID);
                RestResponse<List<FossCSVResultHeaderInfo>> response = _client.Execute<List<FossCSVResultHeaderInfo>>(request);
                return ApiHelper.FromResponse<List<FossCSVResultHeaderInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }

        public static FossCSVResultByteInfo GetFossCSVResultByte(String token, Int32 fossCSVSampleID, Int32 fossCSVResultID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/GetFossCSVResultByte", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                request.AddParameter("FossCSVSampleID", fossCSVSampleID);
                request.AddParameter("FossCSVResultID", fossCSVResultID);
                RestResponse<FossCSVResultByteInfo> response = _client.Execute<FossCSVResultByteInfo>(request);
                return ApiHelper.FromResponse<FossCSVResultByteInfo>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Post 
        public static ErrorInfo   PutTokenEnd(TokenInfo token)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/TokenEnd", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(token);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBatch(BatchInfo batch)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/Batch", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(batch);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBatchEnd(BatchInfo batch)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BatchEnd", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(batch);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Post Bruker
        public static ErrorInfo   PutBrukerSample(BrukerSampleInfo brukerSample)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/Bruker", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSample);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static CFCAIMCore.Entity.ErrorInfo  PutBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID)
        {
            return PutBrukerSentLog(token, customerID, brukerSampleID, false);
        }
        public static ErrorInfo   PutBrukerSentLog(String token, Int32 customerID, Int32 brukerSampleID, Boolean isSkip)
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
                RestRequest request = new RestRequest("/api/v1/BrukerSentLog", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSentLog);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSampleResult(BrukerSampleResultInfo brukerSampleResult)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerResult", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSampleResult);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSampleResultSent(BrukerSampleResultSentInfo brukerSampleResultSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerResultSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSampleResultSent);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSampleResultReceive(String token, Int32 brukerSampleResultID)
        {
            BrukerSampleResultReceiveInfo brukerSampleResultReceive = new BrukerSampleResultReceiveInfo();

            try
            {
                Initializer();
                brukerSampleResultReceive.Token = token;
                brukerSampleResultReceive.BrukerSampleResultReceiveID = 0;
                brukerSampleResultReceive.BrukerSampleResultID = brukerSampleResultID;
                RestRequest request = new RestRequest("/api/v1/BrukerSampleResultReceive", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSampleResultReceive);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSpectrumFile(BrukerSpectrumInfo brukerSpectrumFile)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerSpectrum", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSpectrumFile);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSpectrumFileV2(BrukerSpectrumInfo brukerSpectrumFile)			//DB241008 
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v2/BrukerSpectrum", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSpectrumFile);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSpectrumSent(BrukerSpectrumSentInfo brukerSpectrumSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/BrukerSpectrumSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSpectrumSent);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutBrukerSpectrumReceive(String token, Int32 brukerSpectrumID)
        {
            BrukerSpectrumReceiveInfo brukerSpectrumReceive = new BrukerSpectrumReceiveInfo();

            try
            {
                Initializer();
                brukerSpectrumReceive.Token = token;
                brukerSpectrumReceive.BrukerSpectrumReceiveID = 0;
                brukerSpectrumReceive.BrukerSpectrumID = brukerSpectrumID;
                RestRequest request = new RestRequest("/api/v1/BrukerSpectrumReceive", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSpectrumReceive);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Post Foss
        public static ErrorInfo   PutFossSample(FossSampleInfo fossSample)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossSample", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSample);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }

        public static ErrorInfo   PutFossCSVResult(FossCSVResultInfo fossCsvResult)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossCSVResult", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossCsvResult);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossCSVResultReceive(FossCSVResultInfo fossCsvResult)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossCSVResultReceive", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossCsvResult);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossCSVResultSent(FossCSVResultSentInfo fossCsvResultSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossCSVResultSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossCsvResultSent);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossSentLog(String token, Int32 customerID, Int32 fossSampleID, Boolean isSkip)
        {
            FossSentLogInfo fossSentLog = new FossSentLogInfo();

            try
            {
                Initializer();
                fossSentLog.Token = token;
                fossSentLog.FossSentLogID = 0;
                fossSentLog.CustomerID = customerID;
                fossSentLog.FossSampleID = fossSampleID;
                fossSentLog.IsSkip = isSkip;
                RestRequest request = new RestRequest("/api/v1/FossSentLog", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSentLog);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossSampleResultSent(FossSampleResultSentInfo fossSampleResultSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossSampleResultSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSampleResultSent);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossSpectrumFile(FossSpectrumInfo fossSpectrumFile)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossSpectrum", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSpectrumFile);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }

        public static ErrorInfo   PutFossSpectrumFileV2(FossSpectrumInfo fossSpectrumFile)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v2/FossSpectrum", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSpectrumFile);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossSpectrumSent(FossSpectrumSentInfo fossSpectrumSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/FossSpectrumSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(fossSpectrumSent);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutFossSpectrumReceive(Int32 FossSpectrumID, String token)
        {
            FossSpectrumReceiveInfo FossSpectrumReceive = new FossSpectrumReceiveInfo();

            try
            {
                Initializer();
                FossSpectrumReceive.Token = token;
                FossSpectrumReceive.FossSpectrumReceiveID = 0;
                FossSpectrumReceive.FossSpectrumID = FossSpectrumID;
                RestRequest request = new RestRequest("/api/v1/FossSpectrumReceive", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(FossSpectrumReceive);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Post PNE
        public static ErrorInfo PutPNEResultsRecieved(PNEResultsReceivedInfo pneResultsRecieved)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/PNEResultsRecieved", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(pneResultsRecieved);
                RestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
                return ApiHelper.FromResponse<ErrorInfo>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Get QA 
        public static List<QAUnitTestInfo> GetQAResult(String token)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/GetQAResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("Token", token);
                RestResponse<List<QAUnitTestInfo>> response = _client.Execute<List<QAUnitTestInfo>>(request);
                return ApiHelper.FromResponse<List<QAUnitTestInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Get Adisseo

        public static List<CFCAPICore.Entity.AnalysesInfo > GetAnalyse(String aimInstrumentKey)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/Analyse", Method.Get);
                request.AddParameter("token", Token);
                request.AddParameter("AIMInstrumentKey", aimInstrumentKey);
                request.RequestFormat = DataFormat.Json;
                RestResponse<List<CFCAPICore.Entity.AnalysesInfo >> response = _client.Execute<List<CFCAPICore.Entity.AnalysesInfo >>(request);
                return ApiHelper.FromResponse<List<CFCAPICore.Entity.AnalysesInfo >>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Get OutsideLab
        public static List<OutsideLabSampleInfo> GetOutsideLabSampleNotSent(String token)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/OutsideLabSampleNotSentResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("token", token);
                RestResponse<List<OutsideLabSampleInfo>> response = _client.Execute<List<OutsideLabSampleInfo>>(request);
                return ApiHelper.FromResponse<List<OutsideLabSampleInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static List<OutsideLabSampleResultInfo> GetOutsideLabSampleResult(String token, Int32 outsideLabSampleID)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/OutsideLabResult", Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("token", token);
                request.AddParameter("outsideLabSampleID", outsideLabSampleID);
                RestResponse<List<OutsideLabSampleResultInfo>> response = _client.Execute<List<OutsideLabSampleResultInfo>>(request);
                return ApiHelper.FromResponse<List<OutsideLabSampleResultInfo>>(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        #endregion

        #region Put OutsideLab
        public static ErrorInfo    PutOutsideLabSentLog(String token, Int32 customerID, Int32 outsideLabSampleID, Boolean isSkip)
        {
            OutsideLabSentLogInfo outsideLabSentLog = new OutsideLabSentLogInfo();

            try
            {
                Initializer();
                outsideLabSentLog.OutsideLabSentLogID = 0;
                outsideLabSentLog.CustomerID = customerID;
                outsideLabSentLog.OutsideLabSampleID = outsideLabSampleID;
                outsideLabSentLog.IsSkip = isSkip;
                RestRequest request = new RestRequest("/api/v1/OutsideLabSentLog", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddQueryParameter("token", token);
                request.AddBody(outsideLabSentLog);
                RestResponse<ErrorInfo   > response = _client.Execute<ErrorInfo   >(request);
                return ApiHelper.FromResponse<ErrorInfo   >(response);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }
        public static ErrorInfo   PutOutsideLabSampleResultSent(String token, OutsideLabSampleResultSentInfo outsideLabSampleResultSent)
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/api/v1/OutsideLabSampleResultSent", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(outsideLabSampleResultSent);
                request.AddQueryParameter("token", token);
                RestResponse<ErrorInfo  > response = _client.Execute<ErrorInfo  >(request);
                return ApiHelper.FromResponse<ErrorInfo  >(response);
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
