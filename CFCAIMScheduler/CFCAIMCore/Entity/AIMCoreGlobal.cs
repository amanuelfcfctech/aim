using System;
using System.Collections.Generic;
using CFCAIMCore.Manager;
using CFCAIMCore.Entity;

namespace CFCAIMCore
{
    public class AIMCoreGlobal
    {
		private static Int32 _cfcUserID;
		private static Int32 _aimDatabaseID;
		private static Boolean _aimDatabaseIsActive;
		private static MathTypeEnum _defaultForPropose;
		private static String _custom1Equation;
		private static String _custom2Equation;
		private static String _custom3Equation;
		private static String _custom4Equation;
		private static String _custom5Equation;
		private static String _custom6Equation;
		private static String _custom7Equation;
		private static String _custom8Equation;
		private static String _custom9Equation;
		private static String _custom10Equation;
		private static Boolean _aimSpecificProductsTab;
		private static Boolean _aimSpecificIngredientsTab;
		private static Boolean _aimOwnerLocationGroupTab;

		#region Constructors
		public static Int32 CFCUserID
		{
			get
			{
				return _cfcUserID;
			}
			set
			{
				_cfcUserID = value;
			}
		}

		public static Int32 AIMDatabaseID
		{
			get
			{
				return _aimDatabaseID;
			}
			set
			{
				_aimDatabaseID = value;
			}
		}

		public static Boolean AIMDatabaseIsActive
		{
			get
			{
				return _aimDatabaseIsActive;
			}
			set
			{
				_aimDatabaseIsActive = value;
			}
		}

		public static MathTypeEnum DefaultForPropose
		{
			get
			{
				return _defaultForPropose;
			}
			set
			{
				_defaultForPropose = value;
			}
		}
		public static String Custom1Equation
		{
			get
			{                
				return _custom1Equation;
			}
			set
			{
				_custom1Equation = value;
			}
		}
		public static String Custom2Equation
		{
			get
			{
				return _custom2Equation;
			}
			set
			{
				_custom2Equation = value;
			}
		}
		public static String Custom3Equation
		{
			get
			{
				return _custom3Equation;
			}
			set
			{
				_custom3Equation = value;
			}
		}
		public static String Custom4Equation
		{
			get
			{
				return _custom4Equation;
			}
			set
			{
				_custom4Equation = value;
			}
		}
		public static String Custom5Equation
		{
			get
			{
				return _custom5Equation;
			}
			set
			{
				_custom5Equation = value;
			}
		}
		public static String Custom6Equation
		{
			get
			{
				return _custom6Equation;
			}
			set
			{
				_custom6Equation = value;
			}
		}
		public static String Custom7Equation
		{
			get
			{
				return _custom7Equation;
			}
			set
			{
				_custom7Equation = value;
			}
		}
		public static String Custom8Equation
		{
			get
			{
				return _custom8Equation;
			}
			set
			{
				_custom8Equation = value;
			}
		}
		public static String Custom9Equation
		{
			get
			{
				return _custom9Equation;
			}
			set
			{
				_custom9Equation = value;
			}
		}
		public static String Custom10Equation
		{
			get
			{
				return _custom10Equation;
			}
			set
			{
				_custom10Equation = value;
			}
		}

		// DB 230803
		public static Boolean AimSpecificProductsTab
		{
			get
			{
				return _aimSpecificProductsTab;
			}
			set
			{
				_aimSpecificProductsTab = value;
			}
		}
		// DB 230803
		public static Boolean AimSpecificIngredientsTab
		{
			get
			{
				return _aimSpecificIngredientsTab;
			}
			set
			{
				_aimSpecificIngredientsTab = value;
			}
		}
		// DB 230803
		public static Boolean AimOwnerLocationGroupTab
		{
			get
			{
				return _aimOwnerLocationGroupTab;
			}
			set
			{
				_aimOwnerLocationGroupTab = value;
			}
		}
		#endregion

		#region Function
		public static void LoadCustomEquation()
		{
			List<MathTypeInfo> mathType = new List<MathTypeInfo>();

			mathType = AIMManager.GetMathType(true);

			foreach (MathTypeInfo item in mathType)
			{
				switch ((MathTypeEnum)item.MathTypeID)
				{
					case MathTypeEnum.Custom1:
						if (item.Equation == null || item.Equation.Length == 0  )
						{
							AIMCoreGlobal.Custom1Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom1Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom2:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom2Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom2Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom3:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom3Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom3Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom4:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom4Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom4Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom5:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom5Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom5Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom6:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom6Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom6Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom7:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom7Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom7Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom8:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom8Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom8Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom9:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom9Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom9Equation = item.Equation;
						}
						break;
					case MathTypeEnum.Custom10:
						if (item.Equation == null || item.Equation.Length == 0)
						{
							AIMCoreGlobal.Custom10Equation = "0";
						}
						else
						{
							AIMCoreGlobal.Custom10Equation = item.Equation;
						}
						break;
					default:
						break;
				}

			}
		}
		#endregion

	}

}
