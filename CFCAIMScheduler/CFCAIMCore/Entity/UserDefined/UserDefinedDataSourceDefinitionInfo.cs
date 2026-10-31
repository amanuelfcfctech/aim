using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class UserDefinedDataSourceDefinitionInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UserDefinedDataSourceDefinitionID", SqlDbType.Int)]
		public Int32 UserDefinedDataSourceDefinitionID { get; set; }			

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ImportFileName", SqlDbType.NVarChar)]
		public String ImportFileName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FilePath", SqlDbType.NVarChar)]
		public String FilePath { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FileHasHeader", SqlDbType.Bit)]
		public Boolean FileHasHeader { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsSpectraFile", SqlDbType.Bit)]
		public Boolean IsSpectraFile { get; set; }

		//[DataReaderColumnMapping(),
		//SqlParameterMapping("@IsFurtherAnalyses", SqlDbType.Bit)]
		//public Boolean IsFurtherAnalyses { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSourceID", SqlDbType.Int)]
		public Int32 AIMSourceID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSourceLocationID", SqlDbType.Int)]
		public Int32 AIMSourceLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }


        //---------------------------------------------------------------------
        //For the following 5 fields, the values can be:  
        //  1 = UseMapping
        //  2 = SelectOnImport
        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialMap", SqlDbType.Int)]
		public Int32 AIMMaterialMap { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSourceMap", SqlDbType.Int)]
		public Int32 AIMSourceMap { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSourceLocationMap", SqlDbType.Int)]
		public Int32 AIMSourceLocationMap { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerMap", SqlDbType.Int)]
		public Int32 AIMOwnerMap { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerLocationMap", SqlDbType.Int)]
		public Int32 AIMOwnerLocationMap { get; set; }
        //---------------------------------------------------------------------

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ProcessDirectory", SqlDbType.NVarChar)]
		public String ProcessDirectory { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BackupDirectory", SqlDbType.NVarChar)]
		public String BackupDirectory { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SpectraDirectory", SqlDbType.NVarChar)]
		public String SpectraDirectory { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@HeaderBeginRow", SqlDbType.Int)]
		public Int32 HeaderBeginRow { get; set; }
		
		// 0 = Portrait; 1 = Landscape
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Orientation", SqlDbType.Bit)]
		public Boolean Orientation { get; set; }

		public String OrientationDisplay
		{
			get
			{
				String result = "Portrait";
				if (@Orientation == true)
				{
					result ="Landscape";
				}			
				return result;
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ImportFileTypeID", SqlDbType.Int)]
		public Int32 ImportFileTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DelimiterTypeID", SqlDbType.Int)]
		public Int32 DelimiterTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SpectraFileExtension", SqlDbType.NVarChar)]
		public String SpectraFileExtension { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExcelSheetNumber", SqlDbType.Int)]
		public Int32 ExcelSheetNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientDetailBeginColumn", SqlDbType.Int)]
		public Int32 NutrientDetailBeginColumn { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CreateSampleOrMergeResults", SqlDbType.Bit)]
		public Boolean CreateSampleOrMergeResults { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceIsMapped", SqlDbType.Bit)]
		public Boolean DataSourceIsMapped { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DateFormatID", SqlDbType.Int)]
		public Int32 DateFormatID { get; set; }

        //1/29/18 import type can = Merge, Create or MergeCreate
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ImportType", SqlDbType.Int)]
        public Int32 ImportType { get; set; }
        ////[DataReaderColumnMapping(isOptional: true)]
        ////public Int32 ImportType { get; set; }

        //3/15/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsQualityControl", SqlDbType.Bit)]
        public Boolean IsQualityControl { get; set; }

        //4/1/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DecimalSeparatorID", SqlDbType.Int)]
        public Int32 DecimalSeparatorID { get; set; }

        //8/23/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DetailBeginRow", SqlDbType.Int)]
        public Int32 DetailBeginRow { get; set; }

        //12/2/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AllowDuplicateSampleCodes", SqlDbType.Bit)]
        public Boolean AllowDuplicateSampleCodes { get; set; }

        //12/2/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SystemCreatedSampleCode", SqlDbType.Bit)]
        public Boolean SystemCreatedSampleCode { get; set; }

        //1/6/20    
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@GridXMLStream", SqlDbType.NVarChar)]
        public String GridXMLStream { get; set; }

		//8/12/22 - added for Romer Labs
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsCompositeBatch", SqlDbType.Bit)]
		public Boolean IsCompositeBatch { get; set; }

		#endregion
	}
}
