using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;

namespace CFCAIMCore.DataExchange
{
    public class BrukerSQL
    {
        // DB240710 CFCUser.Global userGlobal = new CFCUser.Global();

        public static Boolean ProcessBrukerResult(BrukerSQLInfo bruker, SampleInfo sample, Int32 dataSourceTypeID, Int32 aimDatabaseID)
        {
            List<SampleResultInfo> checkResult = new List<SampleResultInfo>();
            Boolean isError = false;

            checkResult = AIMManager.GetSampleResultForSQLResultID(sample.SampleID, bruker.UniqueID.ToString());

            if (checkResult.Count == 0)
            {
                try
                {
                    SampleResultInfo sampleResult = new SampleResultInfo();
                    sampleResult.SampleID = sample.SampleID;
                    sampleResult.AnalyzerNutrientCode = bruker.Nutrient;
                    sampleResult.ResultValue = bruker.ResultValue;
                    sampleResult.Unit = bruker.UnitType;
                    sampleResult.DataSourceTypeID = (Int32)dataSourceTypeID;
                    sampleResult.AimDatabaseID = aimDatabaseID;
                    sampleResult.SqlResultID = bruker.UniqueID.ToString();

                    //8/17/22
                    //AIMManager.InsertSampleResult(sampleResult, null);
                    AIMManager.InsertSampleResult(sampleResult, null, 0);
                }
                catch
                {
                    isError = true;
                }
            }
            return isError;
        }
    }
}
