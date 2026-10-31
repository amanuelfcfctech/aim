using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AlternateNameInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 AlternateNameID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FormID", SqlDbType.Int)]
		public Int32 FormID { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public String ControlName { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public String AlternateName { get; set; }			

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public String TableFieldName { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public String UserControlName { get; set; }

		//cursor change 08/25/2026 - display name for the Groups screen dropdowns; the users work with the alternate
		//                          name from Customize Field Names, so fall back to the original control name only
		//                          when the control has no alternate name
		public String FieldNameDisplay
		{
			get
			{
				String name = UserControlName;

				if (AlternateName != null && AlternateName.Length > 0)
				{
					name = AlternateName;
				}

				return name;
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Visible", SqlDbType.Bit)]
		public Boolean Visible { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@RequiredField", SqlDbType.Bit)]
		public Boolean RequiredField { get; set; }

		//7/26/16
		// This is the link between the caption and the txt field on the sample screen.  ex:  lblBOL and txtBOL go together
		// Added this because user wants to show/hide fields, and the controls for the label and text are separate ID's.
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 LinkToControlID { get; set; }


		//6/26/20 - commented, label fields moved to Labels table
		////LabelFieldFirst is the top line that defaults to Load#. User sets this on Sample > Alternate Name screen.
		//[DataReaderColumnMapping(isOptional: true),
		//SqlParameterMapping("@LabelFieldFirst", SqlDbType.Bit)]
		//public Boolean LabelFieldFirst { get; set; }

        //6/26/20 - commented, label fields moved to Labels table
        ////LabelFieldLast is the bottom line that defaults to Ticket#. User sets this on Sample > Alternate Name screen.
        //[DataReaderColumnMapping(isOptional: true),
		//SqlParameterMapping("@LabelFieldLast", SqlDbType.Bit)]
		//public Boolean LabelFieldLast { get; set; }

        //6/19/20
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ShowCodeNameOrBothID", SqlDbType.Int)]
        public Int32 ShowCodeNameOrBothID { get; set; }



        #endregion
    }
}