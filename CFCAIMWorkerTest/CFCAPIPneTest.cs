using CFCAIMCore.Entity;
using Core.Library;
using CFCAPI;
using System;
using System.Collections.Generic;
using System.Text;
using CFCAPICore.Entity;
using CFCAIMCore.DataExchange;
using CFCAIMWorker;


namespace CFCAIMWorkerTest
{
    /// <summary>
    /// Test module for CFCAPIPne class. Tests each public method and reports positive (PASS) or negative (FAIL) results.
    /// </summary>
    public static class CFCAPIPneTest
    {
        #region Test Result Model

        public class TestResult
        {
            public string MethodName { get; set; }
            public bool IsPositive { get; set; }
            public string Message { get; set; }
            public string ExceptionDetails { get; set; }

            public override string ToString()
            {
                string status = IsPositive ? "PASS" : "FAIL";
                var sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", MethodName, status);
                if (!string.IsNullOrEmpty(Message))
                    sb.AppendFormat(" - {0}", Message);
                if (!string.IsNullOrEmpty(ExceptionDetails))
                    sb.AppendFormat(" (Exception: {0})", ExceptionDetails);
                return sb.ToString();
            }
        }

        #endregion

        #region Configuration

        /// <summary>
        /// Database XML path for AIM database connection. Must be set before running tests that require database.
        /// </summary>
        public static string DatabaseXML { get; set; }

        /// <summary>
        /// Setting XML path for loading configuration. Must be set before running tests that require API/database.
        /// </summary>
        public static string SettingXML { get; set; }

        #endregion

        #region Run All Tests

        /// <summary>
        /// Runs all CFCAPIPne method tests and returns a list of results.
        /// Each result indicates whether the method produced a positive (PASS) or negative (FAIL) outcome.
        /// </summary>
        /// <returns>List of test results for each method</returns>
        public static List<TestResult> RunAllTests()
        {
            var results = new List<TestResult>();

            // Apply configuration from settings if available
            ApplyConfiguration();

            // Test each method
            results.Add(Test_SendPendingSampleSpectra());
            results.Add(Test_GetAnalysisResultList());
            results.Add(Test_GetPNECredit());
            results.Add(Test_GettingRequest());
            results.Add(Test_ValidateAnalysisCode());
            results.Add(Test_LoadAnalyse());
            results.Add(Test_AnalyseCode());
            results.Add(Test_AnalyseCodeWithRef());
            results.Add(Test_MaterialCode());

            return results;
        }

        /// <summary>
        /// Runs all tests and returns a formatted report string.
        /// </summary>
        public static string RunAllTestsAndGetReport()
        {
            var results = RunAllTests();
            return FormatReport(results);
        }

        /// <summary>
        /// Formats test results into a readable report.
        /// </summary>
        public static string FormatReport(List<TestResult> results)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== CFCAPIPne Test Report ===");
            sb.AppendLine();

            int passCount = 0;
            int failCount = 0;

            foreach (var result in results)
            {
                sb.AppendLine(result.ToString());
                if (result.IsPositive)
                    passCount++;
                else
                    failCount++;
            }

            sb.AppendLine();
            sb.AppendFormat("Summary: {0} PASS, {1} FAIL", passCount, failCount);
            return sb.ToString();
        }

        #endregion

        #region Individual Test Methods

        private static void ApplyConfiguration()
        {
            if (string.IsNullOrEmpty(SettingXML)) return;

            try
            {
                var setting = new SettingInfo();
                Library.LoadSetting(ref setting, SettingXML);

                CFCAPIPne.AimInstrumentKey = setting.AIMInstrumentKey ?? string.Empty;

                // PNEAPIClient uses CFC API; ensure token is set if we have instrument PNE key
                // APIManager is used by some code paths - sync with CFCAIMWorker pattern
                CFCAIMCore.Manager.APIManager.PNEAPIUrl = setting.IsPNETestUrl == true
                    ? setting.PNEAPITestUrl
                    : setting.PNEAPIUrl;
            }
            catch
            {
                // Configuration may fail if files missing; tests will report accordingly
            }
        }

        private static TestResult Test_SendPendingSampleSpectra()
        {
            const string methodName = "SendPendingSampleSpectra";
            try
            {
                CFCAPIPne.SendPendingSampleSpectra();
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = true,
                    Message = "Completed without exception"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_GetAnalysisResultList()
        {
            const string methodName = "GetAnalysisResultListAll";
            try
            {
                var result = CFCAPIPne.GetAnalysisResultListAll();
                bool isPositive = result != null;
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = isPositive,
                    Message = result != null
                        ? string.Format("Returned list with {0} items", result.Count)
                        : "Returned null"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_GetPNECredit()
        {
            const string methodName = "GetPNECredit";
            try
            {
                var result = CFCAPIPne.GetPNECredit();
                bool isPositive = result != null;
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = isPositive,
                    Message = result != null ? "Returned credit info" : "Returned null"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_GettingRequest()
        {
            const string methodName = "GettingRequest";
            try
            {
                CFCAPIPne.GettingRequest();
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = true,
                    Message = "Completed without exception"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_ValidateAnalysisCode()
        {
            const string methodName = "ValidateAnalysisCode";
            try
            {
                CFCAPIPne.ValidateAnalysisCode();
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = true,
                    Message = "Completed without exception"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_LoadAnalyse()
        {
            const string methodName = "LoadAnalyse";
            try
            {
                var analysesMaterialList = new List<AnalysesMaterialInfo>();
                CFCAPIPne.LoadAnalyse(ref analysesMaterialList);
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = true,
                    Message = "Completed without exception (method has empty implementation)"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_AnalyseCode()
        {
            const string methodName = "AnalyseCode";
            try
            {
                bool isError = CFCAPIPne.AnalyseCode();
                // For AnalyseCode: false = no error (positive), true = error (negative)
                bool isPositive = !isError;
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = isPositive,
                    Message = isError ? "Returned error (isError=true)" : "Returned success (isError=false)"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_AnalyseCodeWithRef()
        {
            const string methodName = "AnalyseCode(ref List<AnalysesMaterialInfo>)";
            try
            {
                var analysesMaterialList = new List<AnalysesMaterialInfo>();
                bool isError = CFCAPIPne.AnalyseCode(ref analysesMaterialList);
                bool isPositive = !isError;
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = isPositive,
                    Message = isError
                        ? string.Format("Returned error (isError=true), list has {0} items", analysesMaterialList?.Count ?? 0)
                        : string.Format("Returned success (isError=false), list has {0} items", analysesMaterialList?.Count ?? 0)
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        private static TestResult Test_MaterialCode()
        {
            const string methodName = "MaterialCode";
            try
            {
                bool isError = CFCAPIPne.MaterialCode();
                // For MaterialCode: false = no error (positive), true = error (negative)
                bool isPositive = !isError;
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = isPositive,
                    Message = isError ? "Returned error (isError=true)" : "Returned success (isError=false)"
                };
            }
            catch (Exception ex)
            {
                return new TestResult
                {
                    MethodName = methodName,
                    IsPositive = false,
                    Message = "Exception thrown",
                    ExceptionDetails = ex.Message
                };
            }
        }

        #endregion
    }
}
