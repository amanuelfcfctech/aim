using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class FormulationInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping()]
		public String UpdateName { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateDate { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserName { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCIngredientCode { get; set; }

		[DataReaderColumnMapping()]
		public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCNutrientCode { get; set; }

		[DataReaderColumnMapping()]
		public String CFCNutrientName { get; set; }

		[DataReaderColumnMapping()]
		public Double CurrentNutrientVal { get; set; }

		[DataReaderColumnMapping()]
		public Int32 Status { get; set; }

		public String StatusDisplay
		{
			get
			{
				if (Status == 1)
				{
					return "Reject";
				}
				else if (Status == 2)
				{
					return "Accept";
				}
				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping()]
		public Double ProposedNutrientValue { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Difference
		{
			get
			{
				return Math.Round(Used - CurrentNutrientVal, 4);
			}
		}
		
		[DataReaderColumnMapping()]
		public Double Override { get; set; }

		public Double Used
		{
			get
			{
				if (Override == 0)
				{
					return Math.Round(ProposedNutrientValue, 4);
				}
				else
				{
					return Math.Round(Override, 4);
				}
			}
		}
		
		#endregion
	}
}
