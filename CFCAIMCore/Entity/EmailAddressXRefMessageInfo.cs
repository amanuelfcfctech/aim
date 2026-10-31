using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class EmailAddressXRefMessageInfo : DataObjectBase
    {
        #region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailAddressXRefMessageID", SqlDbType.Int)]
		public Int32 EmailAddressXRefMessageID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailAddressID", SqlDbType.Int)]
		public Int32 EmailAddressID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MessageID", SqlDbType.Int)]
		public Int32 MessageID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@EmailAddress", SqlDbType.VarChar)]
		public String EmailAddress { get; set; }	

        [DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]
		public Int32 UpdateCFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public String UpdateUserName { get; set; }

        #endregion
    }
}
