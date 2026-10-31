using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class CFCUserInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 LastActiveDataMapID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FirstName", SqlDbType.VarChar)]
		public String FirstName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@LastName", SqlDbType.VarChar)]
		public String LastName { get; set; }
		
		public String CFCUserName
		{
			get
			{
				return FirstName + ' ' + LastName;
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@EmailAddress", SqlDbType.VarChar)]
		public String EmailAddress { get; set; }

        //6/15/18
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		//2/13/23
		[DataReaderColumnMapping(isOptional: true)]
		public DateTime LastLoginDate { get; set; }

		#endregion
	}
}


