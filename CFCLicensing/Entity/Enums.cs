using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFCLicensing.Entity
{
	/// <summary>Enumeration for Unit Of Measure</summary>
	public enum UnitOfMeasure
	{
		/// <summary>US Units (Pounds, Tons, Acres)</summary>
		US = 0,

		/// <summary>Canadian Units(Pounds, Metric Tons, Acres</summary>
		Canadian = 1,

		/// <summary>Metric Units(Kilograms, Metric Tons, Hectares)</summary>
		Metric = 2
	}

	/// <summary>Enumeration for Unit Of Measure</summary>
	public enum IngredientUnitCost
	{
		/// <summary>Ingredient Cost Per Pound</summary>
		PerPound = 0,

		/// <summary>Ingredient Cost Per 100 LB</summary>
		Per100LB = 1,

		/// <summary>Ingredient Cost Per Ton</summary>
		PerTon = 2
	}

	/// <summary>Enumeration for Unit Of Measure</summary>
	public enum BatchIngredientOrder
	{
		/// <summary>Ingredient Cost Per Pound</summary>
		Amount = 0,

		/// <summary>Ingredient Cost Per 100 LB</summary>
		IngredientCode = 1,

		/// <summary>Ingredient Cost Per Ton</summary>
		IngredientName = 2
	}

	/// <summary>Enumeration for Unit Of Measure</summary>
	public enum LiquidBatchIngredientOrder
	{
		/// <summary>Ingredient Cost Per Pound</summary>
		Amount = 0,

		/// <summary>Ingredient Cost Per 100 LB</summary>
		IngredientCode = 1,

		/// <summary>Ingredient Cost Per Ton</summary>
		IngredientName = 2
	}

    /// <summary>List of all reports for fertilizer</summary>
    //public enum ReportName
    //{
    //    /// <summary>All Nutrient Information</summary>
    //    NutrientList = 0,

    //    /// <summary>All User Information</summary>
    //    UserList = 1

    //}
}
