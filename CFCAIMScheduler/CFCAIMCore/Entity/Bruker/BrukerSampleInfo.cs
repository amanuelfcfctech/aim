using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
	//[Serializable()]
	public class BrukerSampleInfo : DataObjectBase
	{
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BrukerSampleID", SqlDbType.Int)]
		public Int32 BrukerSampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BatchID", SqlDbType.Int)]
		public Int32 BatchID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Path", SqlDbType.NVarChar)]
		public String Path { get; set; }

		[DataReaderColumnMapping(columnName: "File_Name"),
		SqlParameterMapping("@FileName", SqlDbType.NVarChar)]
		public String FileName { get; set; }

		[DataReaderColumnMapping(columnName: "Scenario_Name"),
		SqlParameterMapping("@ScenarioName", SqlDbType.NVarChar)]
		public String ScenarioName { get; set; }

		[DataReaderColumnMapping(columnName: "Measuring_Point_Name"),
		SqlParameterMapping("@MeasuringPointName", SqlDbType.NVarChar)]
		public String MeasuringPointName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Recipe", SqlDbType.NVarChar)]
		public String Recipe { get; set; }

		[DataReaderColumnMapping(columnName: "Batch_Number"),
		SqlParameterMapping("@BatchNumber", SqlDbType.NVarChar)]
		public String BatchNumber { get; set; }

		[DataReaderColumnMapping(columnName: "Product_Group"),
		SqlParameterMapping("@ProductGroup", SqlDbType.NVarChar)]
		public String ProductGroup { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Product", SqlDbType.NVarChar)]
		public String Product { get; set; }

		[DataReaderColumnMapping(columnName: "Label_Product_Info_1"),
		SqlParameterMapping("@LabelProductInfo1", SqlDbType.NVarChar)]
		public String LabelProductInfo1 { get; set; }

		[DataReaderColumnMapping(columnName: "Value_Product_Info_1"),
		SqlParameterMapping("@ValueProductInfo1", SqlDbType.NVarChar)]
		public String ValueProductInfo1 { get; set; }

		[DataReaderColumnMapping(columnName: "Label_Product_Info_2"),
		SqlParameterMapping("@LabelProductInfo2", SqlDbType.NVarChar)]
		public String LabelProductInfo2 { get; set; }

		[DataReaderColumnMapping(columnName: "Value_Product_Info_2"),
		SqlParameterMapping("@ValueProductInfo2", SqlDbType.NVarChar)]
		public String ValueProductInfo2 { get; set; }

		[DataReaderColumnMapping(columnName: "Label_Product_Info_3"),
		SqlParameterMapping("@LabelProductInfo3", SqlDbType.NVarChar)]
		public String LabelProductInfo3 { get; set; }

		[DataReaderColumnMapping(columnName: "Value_Product_Info_3"),
		SqlParameterMapping("@ValueProductInfo3", SqlDbType.NVarChar)]
		public String ValueProductInfo3 { get; set; }

		[DataReaderColumnMapping(columnName: "Label_Product_Info_4"),
		SqlParameterMapping("@LabelProductInfo4", SqlDbType.NVarChar)]
		public String LabelProductInfo4 { get; set; }

		[DataReaderColumnMapping(columnName: "Value_Product_Info_4"),
		SqlParameterMapping("@ValueProductInfo4", SqlDbType.NVarChar)]
		public String ValueProductInfo4 { get; set; }

		[DataReaderColumnMapping(columnName: "Label_Product_Info_5"),
		SqlParameterMapping("@LabelProductInfo5", SqlDbType.NVarChar)]
		public String LabelProductInfo5 { get; set; }

		[DataReaderColumnMapping(columnName: "Value_Product_Info_5"),
		SqlParameterMapping("@ValueProductInfo5", SqlDbType.NVarChar)]
		public String ValueProductInfo5 { get; set; }

		[DataReaderColumnMapping(columnName: "Device_Type"),
		SqlParameterMapping("@DeviceType", SqlDbType.NVarChar)]
		public String DeviceType { get; set; }

		[DataReaderColumnMapping(columnName: "Device_Description"),
		SqlParameterMapping("@DeviceDescription", SqlDbType.NVarChar)]
		public String DeviceDescription { get; set; }

		[DataReaderColumnMapping(columnName: "Position_Microplate"),
		SqlParameterMapping("@PositionMicroplate", SqlDbType.NVarChar)]
		public String PositionMicroplate { get; set; }

		[DataReaderColumnMapping(columnName: "Sample_Wheel_Position"),
		SqlParameterMapping("@SampleWheelPosition", SqlDbType.NVarChar)]
		public String SampleWheelPosition { get; set; }

		[DataReaderColumnMapping(columnName: "Spectral_Series"),
		SqlParameterMapping("@SpectralSeries", SqlDbType.NVarChar)]
		public String SpectralSeries { get; set; }

		[DataReaderColumnMapping(columnName: "Info_Message_Ident"),
		SqlParameterMapping("@InfoMessageIdent", SqlDbType.NVarChar)]
		public String InfoMessageIdent { get; set; }

		[DataReaderColumnMapping(columnName: "Info_Message_Quant"),
		SqlParameterMapping("@InfoMessageQuant", SqlDbType.NVarChar)]
		public String InfoMessageQuant { get; set; }

		[DataReaderColumnMapping(columnName: "Comment_Ident"),
		SqlParameterMapping("@CommentIdent", SqlDbType.NVarChar)]
		public String CommentIdent { get; set; }

		[DataReaderColumnMapping(columnName: "Comment_Quant"),
		SqlParameterMapping("@CommentQuant", SqlDbType.NVarChar)]
		public String CommentQuant { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Temperature", SqlDbType.NVarChar)]
		public String Temperature { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Pressure", SqlDbType.NVarChar)]
		public String Pressure { get; set; }

		[DataReaderColumnMapping(columnName: "Flow_Rate"),
		SqlParameterMapping("@FlowRate", SqlDbType.NVarChar)]
		public String FlowRate { get; set; }

		[DataReaderColumnMapping(columnName: "Flow_Speed"),
		SqlParameterMapping("@FlowSpeed", SqlDbType.NVarChar)]
		public String FlowSpeed { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Humidity", SqlDbType.NVarChar)]
		public String Humidity { get; set; }

		[DataReaderColumnMapping(columnName: "pH_Value"),
		SqlParameterMapping("@pHValue", SqlDbType.NVarChar)]
		public String pHValue { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Conductivity", SqlDbType.NVarChar)]
		public String Conductivity { get; set; }

		[DataReaderColumnMapping(columnName: "Container_Number"),
		SqlParameterMapping("@ContainerNumber", SqlDbType.NVarChar)]
		public String ContainerNumber { get; set; }

		[DataReaderColumnMapping(columnName: "Sample_ID"),
		SqlParameterMapping("@SampleID", SqlDbType.NVarChar)]
		public String SampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DBCodePage", SqlDbType.NVarChar)]
		public String DBCodePage { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime CreateStamp { get; set; }
		#endregion
	}
}
