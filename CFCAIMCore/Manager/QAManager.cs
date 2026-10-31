using CFCAIMCore.Data;
using CFCAIMCore.Entity;

namespace CFCAIMCore.Manager
{
    public class QAManager
    {
        private static QADataAccess _qaData;

        protected static QADataAccess QaData
        {
            get
            { 
            _qaData = new QADataAccess();
            
            return _qaData;
            }
        }
    
    }
}
