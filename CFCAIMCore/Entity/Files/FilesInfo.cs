using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    public class FilesInfo
    {
        #region Properties
        [DataReaderColumnMapping()]
		public String ProcessPathAndName
		{
			get
			{
				return ProcessPath + @"\" + FileName;
			}
		}

		public String BackupPathAndName
		{
			get
			{
				if (NewFileName == null)
				{
					return BackupPath + @"\" + FileName;
				}

				if (NewFileName.Length > 0)
				{
					return BackupPath + @"\" + NewFileName;
				}
				else
				{
					return BackupPath + @"\" + FileName;
				}
			}
		}

        [DataReaderColumnMapping()]
        public String NewFileName { get; set; }

		[DataReaderColumnMapping()]
		public String FileName { get; set; }

        [DataReaderColumnMapping()]
        public String ProcessPath { get; set; }

		[DataReaderColumnMapping()]
		public String BackupPath { get; set; }

        [DataReaderColumnMapping()]
        public String ProcessStatus { get; set; }

		[DataReaderColumnMapping()]	
		public Boolean FileStatus { get; set; }

        #endregion
    }
}
