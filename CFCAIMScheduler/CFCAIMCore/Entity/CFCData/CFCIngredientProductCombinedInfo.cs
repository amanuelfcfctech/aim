using Core.Library.Data;
using System;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCIngredientProductCombinedInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 ID { get; set; }
		
		[DataReaderColumnMapping()]
		public String Code { get; set; }

		[DataReaderColumnMapping()]
		public String Name { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Type { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String TypeDisplay
		{
			get
			{
				String rtn = "";
				if (Type == (Int32)MaterialTypeEnum.Ingredient)
				{
					rtn = "Ingredient";
				}

				if (Type == (Int32)MaterialTypeEnum.Product)
				{
					rtn = "Product";
				}				
				return rtn;
			}
			
		}
				
		[DataReaderColumnMapping(isOptional: true)]
		public Boolean CreateMaterial { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean CreateXref { get; set; }

		[DataReaderColumnMapping()]
		public Int32 PlantID { get; set; }

		#endregion
	}
}

