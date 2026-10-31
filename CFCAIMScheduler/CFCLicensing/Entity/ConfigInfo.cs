using CFCLicensing.Object;
using Core.Library.Data;
using System;
using System.Data;

namespace CFCLicensing.Entity
{
	[Serializable()]
	public class ConfigInfo: DataObjectBase
	{
		#region Properties
        [DataReaderColumnMapping()]
        public string Address { get; set; }

        [DataReaderColumnMapping()]
        public string City { get; set; }

        [DataReaderColumnMapping()]
        public string State { get; set; }

        [DataReaderColumnMapping()]
        public string Zip { get; set; }

        [DataReaderColumnMapping()]
        public string Phone { get; set; }

		[DataReaderColumnMapping()]
		public decimal AuthorizationCode { get; set; }

		[DataReaderColumnMapping()]
		public decimal BatchSize { get; set; }

		[DataReaderColumnMapping()]
		public BatchIngredientOrder BatchIngredientOrder { get; set; }

		[DataReaderColumnMapping()]
		public Int64 BatchNumberDecimals { get; set; }

		[DataReaderColumnMapping()]
		public decimal BatchRoundTo { get; set; }

		[DataReaderColumnMapping()]
		public string CompanyName { get; set; }

		[DataReaderColumnMapping()]
		public Int64 ConfigID { get; set; }

		[DataReaderColumnMapping()]
		public string DatabaseDirectory { get; set; }

		[DataReaderColumnMapping()]
		public IngredientUnitCost IngredientUnitCost { get; set; }

		[DataReaderColumnMapping()]
		public DateTime InstallDate { get; set; }

		[DataReaderColumnMapping()]
		public string IRUnits { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsAdjustmentRound { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsBatchRound { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsBulkDensityActive { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsLiquidBatchesInteger { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsLiquidBatchRound { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsLiquidBatchVolume { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsBatchesInteger { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsOwning { get; set; }

		[DataReaderColumnMapping()]
		public decimal LeaseCode { get; set; }

		[DataReaderColumnMapping()]
		public Int64 LeaseLenght { get; set; }

		[DataReaderColumnMapping()]
		public LiquidBatchIngredientOrder LiquidBatchIngredientOrder { get; set; }

		[DataReaderColumnMapping()]
		public Int64 LiquidBatchNumberDecimals { get; set; }

		[DataReaderColumnMapping()]
		public Decimal LiquidBatchRoundTo { get; set; }

		[DataReaderColumnMapping()]
		public Decimal LiquidBatchSize { get; set; }

		[DataReaderColumnMapping()]
		public Int64 NNODM { get; set; }

		[DataReaderColumnMapping()]
		public Int64 NNOMoist { get; set; }

		[DataReaderColumnMapping()]
		public Int64 NumberNutDecDry { get; set; }

		[DataReaderColumnMapping()]
		public Int64 NumberNutDecLiquid { get; set; }

		[DataReaderColumnMapping()]
		public decimal TrialInstallCode { get; set; }

		[DataReaderColumnMapping()]
		public Int64 TrialUseCount { get; set; }

		[DataReaderColumnMapping()]
		public UnitOfMeasure UnitOfMeasure { get; set; }

        //CURRENTVERSION (2/22/17)
        [DataReaderColumnMapping(isOptional: true)]
        public Int64 ErrorNumber { get; set; }

        #endregion

        #region Constructor
        public ConfigInfo()
        {
            CompanyName = "";
        }
        #endregion
	}
}
