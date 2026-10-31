using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
	//[Serializable()]
	public class BrukerSampleResultInfo : DataObjectBase
	{
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BrukerSampleResultID", SqlDbType.Int)]
		public Int32 BrukerSampleResultID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BrukerSampleID", SqlDbType.Int)]
		public Int32 BrukerSampleID { get; set; }

		[DataReaderColumnMapping(columnName: "Spectrum_Path"),
		SqlParameterMapping("@SpectrumPath", SqlDbType.NVarChar)]
		public String SpectrumPath { get; set; }

		[DataReaderColumnMapping(columnName: "Spectrum_Name"),
		SqlParameterMapping("@SpectrumName", SqlDbType.NVarChar)]
		public String SpectrumName { get; set; }

		[DataReaderColumnMapping(columnName: "Data_Block"),
		SqlParameterMapping("@DataBlock", SqlDbType.NVarChar)]
		public String DataBlock { get; set; }

		[DataReaderColumnMapping(columnName: "Method_ID"),
		SqlParameterMapping("@MethodID", SqlDbType.Int)]
		public Int32 MethodID { get; set; }

		[DataReaderColumnMapping(columnName: "Date_Time_Analysis"),
		SqlParameterMapping("@DateTimeAnalysis", SqlDbType.DateTime)]
		public DateTime DateTimeAnalysis { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GMT", SqlDbType.Int)]
		public Int32 GMT { get; set; }

		[DataReaderColumnMapping(columnName: "X_Startpoint"),
		SqlParameterMapping("@XStartpoint", SqlDbType.Real)]
		public Decimal XStartpoint { get; set; }

		[DataReaderColumnMapping(columnName: "X_Endpoint"),
		SqlParameterMapping("@XEndpoint", SqlDbType.Real)]
		public Decimal XEndpoint { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Name"),
		SqlParameterMapping("@ComponentName", SqlDbType.NVarChar)]
		public String ComponentName { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Unit"),
		SqlParameterMapping("@ComponentUnit", SqlDbType.NVarChar)]
		public String ComponentUnit { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Formatting", SqlDbType.NVarChar)]
		public String Formatting { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Prediction", SqlDbType.Real)]
		public Decimal Prediction { get; set; }

		[DataReaderColumnMapping(columnName: "Prediction_Outside"),
		SqlParameterMapping("@PredictionOutside", SqlDbType.Int)]
		public Int32 PredictionOutside { get; set; }

		[DataReaderColumnMapping(columnName: "Target_Concentration"),
		SqlParameterMapping("@TargetConcentration", SqlDbType.Real)]
		public Decimal TargetConcentration { get; set; }

		[DataReaderColumnMapping(columnName: "Warning_Upper_Limit"),
		SqlParameterMapping("@WarningUpperLimit", SqlDbType.Real)]
		public Decimal WarningUpperLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Warning_Lower_Limit"),
		SqlParameterMapping("@WarningLowerLimit", SqlDbType.Real)]
		public Decimal WarningLowerLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Alarm_Upper_Limit"),
		SqlParameterMapping("@AlarmUpperLimit", SqlDbType.Real)]
		public Decimal AlarmUpperLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Alarm_Lower_Limit"),
		SqlParameterMapping("@AlarmLowerLimit", SqlDbType.Real)]
		public Decimal AlarmLowerLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Mah_Dist"),
		SqlParameterMapping("@MahDist", SqlDbType.Real)]
		public Decimal MahDist { get; set; }

		[DataReaderColumnMapping(columnName: "Mah_Dist_Limit"),
		SqlParameterMapping("@MahDistLimit", SqlDbType.Real)]
		public Decimal MahDistLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Mah_Dist_Outlier"),
		SqlParameterMapping("@MahDistOutlier", SqlDbType.Int)]
		public Int32 MahDistOutlier { get; set; }

		[DataReaderColumnMapping(columnName: "Spec_Residual"),
		SqlParameterMapping("@SpecResidual", SqlDbType.Real)]
		public Decimal SpecResidual { get; set; }

		[DataReaderColumnMapping(columnName: "F_Value"),
		SqlParameterMapping("@FValue", SqlDbType.Real)]
		public Decimal FValue { get; set; }

		[DataReaderColumnMapping(columnName: "F_Prob"),
		SqlParameterMapping("@FProb", SqlDbType.Real)]
		public Decimal FProb { get; set; }

		[DataReaderColumnMapping(columnName: "F_Prob_Limit"),
		SqlParameterMapping("@FProbLimit", SqlDbType.Real)]
		public Decimal FProbLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Spec_Residual_Outlier"),
		SqlParameterMapping("@SpecResidualOutlier", SqlDbType.Int)]
		public Int32 SpecResidualOutlier { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Value_Density"),
		SqlParameterMapping("@ComponentValueDensity", SqlDbType.Real)]
		public Decimal ComponentValueDensity { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Value_Density_Limit"),
		SqlParameterMapping("@ComponentValueDensityLimit", SqlDbType.Real)]
		public Decimal ComponentValueDensityLimit { get; set; }

		[DataReaderColumnMapping(columnName: "Process_Channel_Name"),
		SqlParameterMapping("@ProcessChannelName", SqlDbType.NVarChar)]
		public String ProcessChannelName { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Name_User"),
		SqlParameterMapping("@ComponentNameUser", SqlDbType.NVarChar)]
		public String ComponentNameUser { get; set; }

		[DataReaderColumnMapping(columnName: "Component_Unit_User"),
		SqlParameterMapping("@ComponentUnitUser", SqlDbType.NVarChar)]
		public String ComponentUnitUser { get; set; }

		[DataReaderColumnMapping(columnName: "Prediction_Calculated"),
		SqlParameterMapping("@PredictionCalculated", SqlDbType.Real)]
		public Decimal PredictionCalculated { get; set; }

		[DataReaderColumnMapping(columnName: "Prediction_Calculated_Outside"),
		SqlParameterMapping("@PredictionCalculatedOutside", SqlDbType.Int)]
		public Int32 PredictionCalculatedOutside { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Formula", SqlDbType.NVarChar)]
		public String Formula { get; set; }

		[DataReaderColumnMapping(columnName: "External_Bias"),
		SqlParameterMapping("@ExternalBias", SqlDbType.Real)]
		public Decimal ExternalBias { get; set; }

		[DataReaderColumnMapping(columnName: "Mah_Dist_Limit_By_Factor"),
		SqlParameterMapping("@MahDistLimitByFactor", SqlDbType.Real)]
		public Decimal MahDistLimitByFactor { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MDI", SqlDbType.Real)]
		public Decimal MDI { get; set; }

		[DataReaderColumnMapping(columnName: "Method_Path"),
		SqlParameterMapping("@MethodPath", SqlDbType.NVarChar)]
		public String MethodPath { get; set; }

		[DataReaderColumnMapping(columnName: "Method_Name"),
		SqlParameterMapping("@MethodName", SqlDbType.NVarChar)]
		public String MethodName { get; set; }

		[DataReaderColumnMapping(columnName: "Method_Date_Time"),
		SqlParameterMapping("@MethodDateTime", SqlDbType.DateTime)]
		public DateTime MethodDateTime { get; set; }

		[DataReaderColumnMapping(columnName: "Method_GMT"),
		SqlParameterMapping("@MethodGMT", SqlDbType.Int)]
		public Int32 MethodGMT { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DBCodePage", SqlDbType.Int)]
		public Int32 DBCodePage { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }
		#endregion
	}
}
