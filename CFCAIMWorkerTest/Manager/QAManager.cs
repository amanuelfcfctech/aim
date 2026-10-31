using CFCAIMWorkerTest.Data;
using CFCAIMWorkerTest.Entity;

namespace CFCAIMWorkerTest.Manager
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
        public QAUnitTestInfo GetQAErrorResult()
        {
            return QaData.GetQAError();
        }
    }
}
